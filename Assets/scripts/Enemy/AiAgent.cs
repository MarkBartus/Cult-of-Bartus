using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

namespace Enemy
{
    public class AiAgent : MonoBehaviour
    {

        public Animator anim;
        public NavMeshAgent nav;

        public float speed;

        [Header("Combat")]
        [SerializeField] float attackRange = 1f;
        public float WeaponDamage = 1f;
        public float ascRange = 1f;

        float timePassed;

        [Header("health")]
        [SerializeField] float health = 3;
        public float currentHealth;
        public enemyHealhBar enemyhealhBar;

        public GameObject player;
        public Transform target;

        public GameObject sight;
        public GameObject ascR;

        public Transform[] points;
        public int desPoint;

        // variables holding the different states
        public EnemyAi sensor;
        public HealthSystem healthSystem;
        public PlayerMovement playerMovement;
        public HealthSystem ehealthSystem;
        public WalkState walkState;
        public AttackState attackState;
        public DelayState delayState;
        public InSIghtState inSightState;
        public guardState guardstate;
        public Assassination asc;
        public guardWalkState guardwalkstate;
        public StateMachine sm;

        public float direction;
        public bool aggressive;
        private float timer = 2f;
        public Transform playerPos;
        public GameObject ascB;
        public bool guardActive = false;

        public Transform guard;
        // Start is called before the first frame update
        void Start()
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            ehealthSystem = GetComponent<HealthSystem>();
            sm = gameObject.AddComponent<StateMachine>();
            anim = GetComponent<Animator>();
            nav = GetComponent<NavMeshAgent>();
            player = GameObject.FindWithTag("Player");
            healthSystem = player.GetComponent<HealthSystem>();
            sensor = GetComponent<EnemyAi>();

            Debug.Log(GameObject.FindWithTag("Player").name);

            // add new states here
            walkState = new WalkState(this, sm);
            attackState = new AttackState(this, sm);
            delayState = new DelayState(this, sm);
            inSightState = new InSIghtState(this, sm);
            asc = new Assassination(this, sm);
            guardstate = new guardState(this, sm);
            guardwalkstate = new guardWalkState(this, sm);

            // initialise the statemachine with the default state
            sm.Init(walkState);

            ascB.SetActive(false);

            currentHealth = health;
            //enemyhealhBar.SetHealth(currentHealth);
        }

        // Update is called once per frame
        public void Update()
        {
            sm.CurrentState.LogicUpdate();

            
        }
        public void guardStateCheck()
        {
            if(guardActive == true)
            {
                sm.ChangeState(guardstate);
            }
        }
            


        public void TakeDamage(float damageAmount)
        {
            currentHealth -= damageAmount;
            playerMovement.StartDealDamage();
            //enemyhealhBar.SetHealth(currentHealth);

            //anim.SetTrigger("damage");


            if (currentHealth <= 0)
            {

                Destroy(this.gameObject);
            }

        }

        void FixedUpdate()
        {
            sm.CurrentState.PhysicsUpdate();
        }
        public void CheckForAsc()
        {
            if (Vector3.Distance(player.transform.position, ascR.transform.position) <= ascRange && sensor.Objects.Count <= 0)
            {
                Debug.Log("asc");
                ascB.SetActive(true);
                if(Input.GetKey(KeyCode.E))
                {
                    ascB.SetActive(false);
                    Destroy(this.gameObject);
                }
            }
            else
            {
                ascB.SetActive(false);
            }
        }

        public void CheckForInSight()
        {
         if (Vector3.Distance(player.transform.position, sight.transform.position) <= attackRange)
         {

            sm.ChangeState(inSightState);     
         }          
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(sight.transform.position, attackRange);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(ascR.transform.position, ascRange);
        }

        public void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
        }

        public void CheckForMovement()
        {
            if (nav.velocity != Vector3.zero)
            {
                sm.ChangeState(walkState);
                return;
            }
        }

        public void CheckForPlayer()
        {
            Debug.Log("timer" + sensor.Objects.Count);

            if(sensor.Objects.Count > 0)
            {
                //change to the delay state
                sm.ChangeState(delayState, attackState);
            }

           
        }
        public void CheckForIdle()
        {
            if (!nav.pathPending && nav.remainingDistance < 0.5f)
            {
                sm.ChangeState(walkState);
                return;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
               /* PlayerScript player = other.GetComponentInParent<PlayerScript>();
                player.alive = false;
                player.sm.ChangeState(player.idleState); */
                Debug.Log("Kill");
                //SceneManager.LoadScene(0);
            }
        }

    }
}
