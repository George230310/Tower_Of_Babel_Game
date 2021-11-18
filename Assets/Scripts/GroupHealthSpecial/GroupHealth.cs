using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GroupHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    private int currentHealth;

    public event Action<float> OnHealthPctChanged = delegate { };
    private AudioSource[] m_ArrayMusic;
    private AudioSource m_music2;

    public EnemyManager GroupManager;

    private bool isDead = false;

    public AudioSource bloodyHit;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        m_ArrayMusic = gameObject.GetComponents<AudioSource>();
        m_music2 = m_ArrayMusic[1];
    }

    public void ModifyHealth(int amount)
    {
        currentHealth += amount;
        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);
    }

    private void Update()
    {
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            GroupManager.enemyCount--;
            m_music2.Play();
            Destroy(gameObject, 0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            bloodyHit.Play();
            ModifyHealth(-20);
        }
    }
}
