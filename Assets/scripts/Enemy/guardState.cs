using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using static UnityEngine.GraphicsBuffer;

namespace Enemy
{
    public class guardState : State
    {


        // constructor
        public guardState(AiAgent enemy, StateMachine sm) : base(enemy, sm)
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
            enemy.CheckForPlayer();
            enemy.CheckForAsc();
            enemy.anim.Play("idle");
            Debug.Log("guarding");

        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

        }
    }
}