using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProjectileCollision : MonoBehaviour
{
    private const string EnemyTag = "Enemy";

    [SerializeField] private float _damageRadius = 1;
    [SerializeField] private int _baseMinDamage = 7;
    [SerializeField] private int _baseMaxDamage = 15;

    private void OnCollisionEnter(Collision collision)
    {
        var foundEnemies = Physics.OverlapSphere(collision.transform.position, _damageRadius)
            .Where(x => x.CompareTag(EnemyTag))
            .Select(x => x.GetComponent<EnemyBehaviour>())
            .ToList();

        if (collision.collider.CompareTag(EnemyTag))
        {
            foundEnemies.Add(collision.collider.GetComponent<EnemyBehaviour>());
        }

        foundEnemies.ForEach(x => x.ApplyDamage(Random.Range(_baseMinDamage, _baseMaxDamage)));
    }
}
