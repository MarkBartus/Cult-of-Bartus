using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using static UnityEngine.GraphicsBuffer;

namespace Enemy
{
    public class InSIghtState : State
    {

        //private float timePassed = 2;
        //private float attackCD = 1f;
        public float Range = 2.5f;
        public bool acd = false;
        private float ac = 1;
        
        // constructor
        public InSIghtState(AiAgent enemy, StateMachine sm) : base(enemy, sm)
        {

        }

        public override void Enter()
        {
            base.Enter();
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
            
            enemy.FaceTarget();
            


            if (Vector3.Distance(enemy.player.transform.position, enemy.transform.position) <= Range && acd == false)
            {
                    
                enemy.anim.Play("slash");
                enemy.FaceTarget();
                if (enemy.playerMovement.blocking == true)
                {
                    enemy.healthSystem.TakeDamage(0);
                }
                else
                {
                    enemy.healthSystem.TakeDamage(1);
                }
                acd = true;
            }

            else if(acd == true)
            {
                ac -= Time.deltaTime;
                if (ac <= 0)
                {
                    acd = false;
                    ac = 1;
                }
                   
            }
            else if (Vector3.Distance(enemy.player.transform.position, enemy.transform.position) > Range)
            {
                sm.ChangeState(enemy.attackState);
            }

            
        }
        

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

        }
    }
}
