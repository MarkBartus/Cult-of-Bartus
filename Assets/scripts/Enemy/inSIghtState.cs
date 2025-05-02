using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Enemy
{
    public class InSIghtState : State
    {

        //private float timePassed = 2;
        //private float attackCD = 1f;
        public float Range = 2.5f;
        
        // constructor
        public InSIghtState(AiAgent enemy, StateMachine sm) : base(enemy, sm)
        {

        }

        public override void Enter()
        {
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
            
            enemy.FaceTarget();

            

            if (Vector3.Distance(enemy.player.transform.position, enemy.transform.position) <= Range)
            {
                    
                enemy.anim.Play("slash");
                enemy.FaceTarget();
                enemy.healthSystem.TakeDamage(1);             

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
