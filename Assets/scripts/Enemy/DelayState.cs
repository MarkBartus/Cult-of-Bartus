using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class DelayState : State
    {
        public float timer;

        // constructor
        public DelayState(AiAgent enemy, StateMachine sm) : base(enemy, sm)
        {
        }

        public override void Enter()
        {
            timer = 2;
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
            Debug.Log("timer delay=" + timer);
            base.LogicUpdate();

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //change to desired new state
                sm.ChangeState(sm.GetNextState());
            }

        }

        public override void PhysicsUpdate()
        {
        }
    }
}