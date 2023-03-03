using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeliosUnity.Utilities
{
    public abstract class State
    {
        public abstract void Enter();
        public abstract void Tick(float time);
        public abstract void Exit();
    }

    public abstract class StateMachineBase
    {
        private State currentState = null;

        private void Update()
        {
            currentState?.Tick(Time.deltaTime);
        }

        public void SwitchState(State newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }

    }
}