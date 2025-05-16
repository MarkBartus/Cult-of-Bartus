using Enemy;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float climbSpeed;
    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Refrences")]
    public Climbing climbingScript;

    [Header("Combat")]
    public GameObject enemy;
    public float range = 1;
    public float dmg;
    public float damageAmount = 1f;
    public bool acd = false;
    public HealthSystem ehealthSystem;
    public HealthSystem healthSystem;
    public GameObject checkSphere;
    public LayerMask enemyMask;

    public Transform orientation;
    public Animator anim;
    float horizontalInput;
    float verticalInput;
    public GameObject sS;

    Vector3 moveDirection;

    Rigidbody rb;

    List<GameObject> hasDealtDamage;

    public MovementState state;
    public enum MovementState
    {
        freeze,
        unlimited,
        walking,
        sprinting,
        climbing,
        crouching,
        air,
        attack,
        block
    }

    public bool freeze;
    public bool unlimited;
    public bool climbing;

    public bool restricted;

    public bool attacking = false;
    public bool blocking = false;
    private float time = 1;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        ehealthSystem = enemy.GetComponent<HealthSystem>();
        anim.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        startYScale = transform.localScale.y;

        hasDealtDamage = new List<GameObject>();
    }

    private void Update()
    {
        
        //ground check
        float play = playerHeight * 0.5f + 0.2f;
        grounded = Physics.Raycast(transform.position, Vector3.down, play, whatIsGround);
        Debug.DrawRay(transform.position, Vector3.down, Color.red, play);

        MyInput();
        SpeedControl();
        StateHandler();
        Blocking();
        if (attacking == false && blocking == false)
        {
            Moving();
        }
        Attack();

        //drag
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0f;

        Debug.Log(moveSpeed);

        if (state != MovementState.walking)
        {
            Debug.Log(state);
        }

        if (attacking == true || blocking == true)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                attacking = false;
                blocking = false;
                acd = false;
                time = 1;
            }
        }
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
 
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        //start crouch 
        if(Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        //stop crouch
        if(Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }


    }
    
    private void Attack()
    {

        RaycastHit hit;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            attacking = true;
            anim.Play("punching");

            int layerMsk = 1 << 9;
            if (Physics.SphereCast(sS.transform.position,range,transform.forward , out hit, layerMsk))
            {
                Debug.Log("hitt");

                if (hit.transform.TryGetComponent(out AiAgent enemy) && acd == false && Physics.CheckSphere(checkSphere.transform.position, 1.5f, enemyMask))
                {
                    enemy.TakeDamage(damageAmount);               
                    hasDealtDamage.Add(hit.transform.gameObject);
                    acd = true;
                }

            }
        }
    }
    public void StartDealDamage()
    {
        hasDealtDamage.Clear();
    }
    private void Blocking()
    {

        if (Input.GetKey(KeyCode.Mouse1))
        {
            anim.Play("block");
            blocking = true;
        }

    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,range);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(sS.transform.position,range);

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(checkSphere.transform.position, 1.5f);
    }

    public void Moving()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                anim.Play("running");
            }
        }
        else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.Play("walking");
        }
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.Play("walking");
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.Play("walking");
        }
        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.Play("walking");
        }
        else
        {
            anim.Play("Idle");
        }
    }

    private void StateHandler()
    {
        if(freeze)
        {
            state = MovementState.freeze;
            rb.linearVelocity = Vector3.zero;

        }

        else if (unlimited)
        {
            state = MovementState.unlimited;
            moveSpeed = sprintSpeed;
            return;
        }

        //climbing
        if(climbing)
        {
            state = MovementState.climbing;
            moveSpeed = climbSpeed;
        }

        //Crouching
        if(Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }      

        //Sprint
        if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        //Waking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        //Air
        else
        {
            state = MovementState.air;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            state = MovementState.attack;
            moveSpeed = 2;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            state = MovementState.block;
            moveSpeed = 2;
        }
    }

    private void MovePlayer()
    {
        if (restricted) return;

        if (climbingScript.exitingWall) return;

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on slope
        
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.angularVelocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
           
        }
        
        else if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        //gravity when on slope
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        //speed on slope
        if(OnSlope() && !exitingSlope)
        {
            if(rb.linearVelocity.magnitude > moveSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
            }
        }

        //speed on ground or air
        else
        {
            Vector3 flatvel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            //velocity
            if (flatvel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatvel.normalized * moveSpeed;
                rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
            }
        }
    }

    private void jump()
    {
        exitingSlope = true;

        //reset y vel
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

}
