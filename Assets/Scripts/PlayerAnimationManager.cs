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

        //PlayerStateManager.Instance.OnStateChanged += OnStateChanged;
    }

    public void Attack(bool attack)
    {
        _playerAnimator.SetBool("IsAttacking", attack);
    }

    public void Move(bool move)
    {
        _playerAnimator.SetBool("IsMoving", move);
    }
}
