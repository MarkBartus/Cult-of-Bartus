using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Enemy
{
    public class AttackState : State
    {
        public float chaseTimer;

        public float direction;
        
        // constructor
        public AttackState(AiAgent enemy, StateMachine sm) : base(enemy, sm)
        {
        }

        public override void Enter()
        {
            Debug.Log("chase");
            chaseTimer = 3;
            enemy.nav.speed = 8f;
            enemy.ascB.SetActive(false);

            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            

            enemy.CheckForInSight();
            

            chaseTimer -= Time.deltaTime;
            if (chaseTimer <= 0 & enemy.sensor.Objects.Count <= 0)
            {

                if (enemy.guardActive == true)
                {
                    enemy.sm.ChangeState(enemy.guardwalkstate);
                }
                else
                {
                    sm.ChangeState(enemy.walkState);
                }
                
            }
            else if (chaseTimer <= 0 & enemy.sensor.Objects.Count > 0)
            {
                chaseTimer = 3;
            }

            enemy.FaceTarget();
            enemy.nav.SetDestination(enemy.player.transform.position);
            enemy.anim.Play("running");

            enemy.CheckForInSight();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

        }
    }
}