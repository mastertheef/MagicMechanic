using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Moving,
    Attacking,
    Knockdown,
    Dead
}

public class PlayerStateManager : SingletonBase<PlayerStateManager>
{
    public delegate void StateChanged(PlayerState oldState, PlayerState newState);
    public event StateChanged OnStateChanged;

    public PlayerState CurrentState { get; private set; }

    public void SetState(PlayerState state)
    {
        if (state == CurrentState) return;
        CurrentState = state;
    }
}
