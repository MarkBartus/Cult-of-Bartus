using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            chaseTimer = 5;
            enemy.nav.speed = 5f;            
            enemy.aggressive = true;
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

            chaseTimer -= Time.deltaTime;
            if (chaseTimer <= 0)
            {
                enemy.CheckForMovement();
                enemy.CheckForPlayer();
            }


            enemy.nav.destination = enemy.playerPos.position;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

        }
    }
}