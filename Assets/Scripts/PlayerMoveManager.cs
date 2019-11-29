using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMoveManager : SingletonBase<PlayerMoveManager>
{
    private Transform _playerTransform;
    private Transform _spellCastPoint;
    private NavMeshAgent _navMeshAgent;
    private Vector3 _moveDirection;
    private Ray _ray;
    private RaycastHit _hit;

    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            throw new System.Exception("No player found");
        }

        _playerTransform = player.transform;
        _spellCastPoint = player.GetComponent<RFX4_EffectEvent>().AttachPoint;
        _navMeshAgent = player.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStateManager.Instance.CurrentState == PlayerState.Attacking)
        {
            _navMeshAgent.isStopped = true;

            _ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(_ray, out _hit))
            {
                var lookPoint = new Vector3(_hit.point.x, _hit.normal.y, _hit.point.z);
                _playerTransform.LookAt(lookPoint);
                _spellCastPoint.LookAt(lookPoint);
            }
        }

        if (Input.GetMouseButton(1))
        {
            PlayerAnimationManager.Instance.Move(true);

            _ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(_ray, out _hit))
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.destination = _hit.point;
            }
        }

        if (GetDistance(_hit.point) <= _navMeshAgent.stoppingDistance && PlayerStateManager.Instance.CurrentState == PlayerState.Moving)
        {
            PlayerAnimationManager.Instance.Move(false);
        }
    }

    private float GetDistance(Vector3 target)
    {
        var distance = _navMeshAgent.pathPending
            ? Vector3.Distance(target, _navMeshAgent.destination)
            : _navMeshAgent.remainingDistance;
        
        return distance;
    }
}
