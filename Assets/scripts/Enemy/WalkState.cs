using Enemy;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Enemy
{
    public class WalkState : State
    {
        public float direction;

        // constructor
        public WalkState(AiAgent enemy, StateMachine sm) : base(enemy, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("walk");           
            enemy.nav.speed = 3.5f;
            enemy.aggressive = false;
            enemy.ascB.SetActive(false);
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
            enemy.CheckForIdle();
            enemy.CheckForPlayer();
            enemy.CheckForAsc();
            if (enemy.guardActive == true)
            {
                enemy.sm.ChangeState(enemy.guardwalkstate);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            // Move
            /*

            if (!enemy.nav.pathPending && enemy.nav.remainingDistance < 0.5f)
            {
                if (enemy.points.Length == 0)
                {
                    enemy.anim.Play("walk");
                    return;
                }
                Debug.Log("finding path");
                enemy.anim.Play("walk");

                enemy.nav.destination = enemy.points[enemy.desPoint].position;
                enemy.desPoint = (enemy.desPoint + 1) % enemy.points.Length;
                
            }
            */
            if (!enemy.nav.pathPending && enemy.nav.remainingDistance < 0.5f)
            {
                if (enemy.points.Length == 0) return;

                Debug.Log("finding path");
                enemy.anim.Play("walk");

                int randomIndex = Random.Range(0, enemy.points.Length); // Pick a random point
                enemy.nav.SetDestination(enemy.points[randomIndex].position);

                enemy.anim.Play("walk");
            }
        }
    }
}