using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Animator _anim;
    private Vector3 _spawnPoint;
    private Vector3 _playerPosition;
    private bool isDead = false;

    [SerializeField] private double agroRadius = 10f;
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private float retreatSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerPosition = PlayerMoveManager.Instance.PlayerPosition;

        if (isDead)
        {
            _anim.SetTrigger("Dead");
        }

        if (Vector3.Distance(_playerPosition, transform.position) <= agroRadius)
        {
            _navMeshAgent.destination = PlayerMoveManager.Instance.PlayerPosition;
            _navMeshAgent.speed = speed;
            _anim.SetBool("IsRunning", true);
            _anim.SetBool("IsAttacking", false);
        }
        else if (Vector3.Distance(_playerPosition, transform.position) > agroRadius && 
                 Vector3.Distance(transform.position, _spawnPoint) > _navMeshAgent.stoppingDistance)
        {
            _navMeshAgent.destination = _spawnPoint;
            _navMeshAgent.speed = retreatSpeed;
            _anim.SetBool("IsRunning", true);
            _anim.SetBool("IsAttacking", false);
        }
        else if (Vector3.Distance(transform.position, _playerPosition) <= _navMeshAgent.stoppingDistance)
        {
            _navMeshAgent.isStopped = true;
            _anim.SetBool("IsAttacking", true);
            _anim.SetBool("IsRunning", false);
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
