using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private Animator _anim;
    
    private int _hitPoints;
    private bool isDead = false;
    private Camera _mainCamera;

    private NavMeshAgent _navMeshAgent;
    private Vector3 _spawnPoint;
    private Vector3 _playerPosition;
    
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private int _maxHitPoints = 100;
    [SerializeField] private double agroRadius = 10f;
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private float retreatSpeed = 20f;

    public void Start()
    {
        _mainCamera = Camera.main;
        _hitPoints = _maxHitPoints;
        _healthBar.SetActive(false);
        _anim = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void ApplyDamage(int damage)
    {
        _hitPoints -= damage;

        if (_hitPoints <= 0)
        {
            _hitPoints = 0;
            _anim.SetTrigger("Dead");
            isDead = true;
            _healthBar.SetActive(true);
            _healthBar.GetComponentInChildren<Image>().fillAmount = 0;
        }
        else if (!isDead)
        {
            if (!_healthBar.activeSelf) 
                _healthBar.SetActive(true);

            _healthBar.GetComponentInChildren<Image>().fillAmount =  (float)_hitPoints / (float)_maxHitPoints;

            _anim.SetTrigger("Damage");
        }
    }

    public void Update()
    {
        _healthBar.transform.LookAt(_mainCamera.transform.position);

        if (isDead)
        {
            return;
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
