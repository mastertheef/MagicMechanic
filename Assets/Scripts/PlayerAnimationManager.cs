using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : SingletonBase<PlayerAnimationManager>
{
    private GameObject _player;
    private Animator _playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if (_player == null)
        {
            throw new System.Exception("No player found");
        }

        _playerAnimator = _player.GetComponent<Animator>();

        PlayerStateManager.Instance.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(PlayerState oldState, PlayerState newState)
    {
        bool isAttacking = newState == PlayerState.Attacking;
        _playerAnimator.SetBool("IsAttacking", isAttacking);
    }
}
