using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using static UnityEngine.GraphicsBuffer;

namespace Enemy
{
    public class guardWalkState : State
    {

        
        // constructor
        public guardWalkState(AiAgent enemy, StateMachine sm) : base(enemy, sm)
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

            enemy.nav.SetDestination(enemy.guard.transform.position);
            Debug.Log("guardingWalk");
            Debug.Log(enemy.anim);
            enemy.anim.Play("walk");
            if (!enemy.nav.pathPending && enemy.nav.remainingDistance < 0.5f)
            {
                enemy.sm.ChangeState(enemy.guardstate);
                
            }
        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

        }
    }
}