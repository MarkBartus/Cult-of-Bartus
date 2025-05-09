using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Enemy
{
    public class Assassination : State
    {


        // constructor
        public Assassination(AiAgent enemy, StateMachine sm) : base(enemy, sm)
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


        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

        }
    }
}
