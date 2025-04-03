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

namespace Enemy
{
    public class AiAgent : MonoBehaviour
    {

        public Animator anim;
        public NavMeshAgent nav;

        public float speed;

        public Transform[] points;
        public int desPoint;

        // variables holding the different states
        public EnemyAi sensor;
        public WalkState walkState;
        public AttackState attackState;
        public DelayState delayState;
        public StateMachine sm;

        public float direction;
        public bool aggressive;
        private float timer = 2f;
        public Transform playerPos;

        // Start is called before the first frame update
        void Start()
        {
            sm = gameObject.AddComponent<StateMachine>();
            anim = GetComponent<Animator>();
            nav = GetComponent<NavMeshAgent>();

            sensor = GetComponent<EnemyAi>();

            // add new states here
            walkState = new WalkState(this, sm);
            attackState = new AttackState(this, sm);
            delayState = new DelayState(this, sm);

            // initialise the statemachine with the default state
            sm.Init(walkState);
        }

        // Update is called once per frame
        public void Update()
        {
            sm.CurrentState.LogicUpdate();
        }

        void FixedUpdate()
        {
            sm.CurrentState.PhysicsUpdate();
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
