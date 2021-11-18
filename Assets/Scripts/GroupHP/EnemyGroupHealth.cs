using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupHealth : MonoBehaviour
{
    public float health;
    public EnemyManager manager;

    private bool isDead = false;

    private void Update()
    {
        if (health <= 0 && !isDead)
        {
            Debug.Log("Enemy died");
            manager.enemyCount--;
            isDead = true;
        }
    }

    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
        }
    }
}
