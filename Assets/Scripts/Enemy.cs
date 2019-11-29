using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private int _maxHitPoints = 100;
    private int _hitPoints;
    private bool isDead = false;
    private Camera _mainCamera;

    public void Start()
    {
        _mainCamera = Camera.main;
        _hitPoints = _maxHitPoints;
        _healthBar.SetActive(false);
        _anim = GetComponent<Animator>();
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
    }
}
