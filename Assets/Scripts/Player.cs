using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public void StartAttacking()
    {
        PlayerStateManager.Instance.SetState(PlayerState.Attacking);
    }

    public void StartIdle()
    {
        PlayerStateManager.Instance.SetState(PlayerState.Idle);
    }

    public void StartMoving()
    {
        PlayerStateManager.Instance.SetState(PlayerState.Moving);
    }
}
