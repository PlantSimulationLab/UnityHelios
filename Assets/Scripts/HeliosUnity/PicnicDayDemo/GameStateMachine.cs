using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeliosUnity.Utilities;


namespace HeliosUnity.PicnicDayDemo
{
    public enum GameState
    {
        NONE = -1,
        INITIALIZED = 0,
        START = 1,
        INTERACT_BASE = 2,
        INTERACT_SPECIFIC = 3,
        STOP = 4,
        DEINITIALIZED = 5,
        REINITIALIZED = 6,
        EXIT = 7
    }

    public class GameStateMachine : StateMachineBase
    {
        protected GameState currentGameState = GameState.NONE;
    }

    public abstract class GameBaseState: State
    {
        protected GameStateMachine gameStateMachine = null;
        protected GameState gameState = GameState.NONE;

        public GameBaseState(GameStateMachine stateMachine)
        {
            gameStateMachine = stateMachine;
        }
 
    }
}