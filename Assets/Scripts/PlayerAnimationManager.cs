using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimatorController
{
    Attack,
    Move
}

public class PlayerAnimationManager : SingletonBase<PlayerAnimationManager>
{
    private GameObject _player;
    private Animator _playerAnimator;

    private Dictionary<AnimatorController, RuntimeAnimatorController> _animatorControllers;

    private void LoadAnimations()
    {
        _animatorControllers = new Dictionary<AnimatorController, RuntimeAnimatorController>();
        var moveController = Resources.Load("AnimationControllers/PlayerMove") as RuntimeAnimatorController;
        _animatorControllers.Add(AnimatorController.Move, moveController);
        _animatorControllers.Add(AnimatorController.Attack, _playerAnimator.runtimeAnimatorController);
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if (_player == null)
        {
            throw new System.Exception("No player found");
        }

        _playerAnimator = _player.GetComponent<Animator>();

        LoadAnimations();

        PlayerStateManager.Instance.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(PlayerState oldState, PlayerState newState)
    {
        bool isAttacking = newState == PlayerState.Attacking;

        switch (newState)
        {
            case PlayerState.Attacking:
                _playerAnimator.runtimeAnimatorController = _animatorControllers[AnimatorController.Attack];
                _playerAnimator.SetTrigger("Attack");
                break;
            case PlayerState.Moving:
                _playerAnimator.runtimeAnimatorController = _animatorControllers[AnimatorController.Move];
                _playerAnimator.SetTrigger("Move");
                break;
            case PlayerState.Idle:
                _playerAnimator.SetTrigger("Idle");
                break;
        }
    }
}
