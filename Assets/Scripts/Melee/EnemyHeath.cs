using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHeath : MonoBehaviour
{
    public float health;
    public float givenHealth;
    public Text eHealth;
    public GameObject transport;
    public Text objective;

    private bool isDead = false;

    private void Start()
    {
        transport.SetActive(false);
        givenHealth = health;
    }

    private void Update()
    {
        if(health <= 0 && !isDead)
        {
            transport.SetActive(true);
            objective.text = "Objective: Touch the Golden Memorial";
            Debug.Log("Enemy died");
            isDead = true;
        }
    }

    public void TakeDamage(float damage)
    {
        if(health > 0)
        {
            Debug.Log("take damage");
            health -= damage;
            eHealth.text = "Enemy Health: " + health.ToString() + "/" + givenHealth.ToString();
        }
    }
}
