using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveManager : SingletonBase<PlayerMoveManager>
{
    private Transform _playerTransform;
    private Transform _spellCastPoint;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStateManager.Instance.CurrentState == PlayerState.Attacking)
        {
            var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out var hit))
            {
                var lookPoint = new Vector3(hit.point.x, hit.normal.y, hit.point.z);
                _playerTransform.LookAt(lookPoint);
                _spellCastPoint.LookAt(lookPoint);
            }
        }
    }
}
