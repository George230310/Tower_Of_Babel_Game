using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    private int currentHealth;

    public Text eHealth;
    public Text objective;

    public event Action<float> OnHealthPctChanged = delegate { };
    private AudioSource[] m_ArrayMusic;
    private AudioSource m_music2;

    public AudioSource bloodyHit;

    public GameObject transport;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        transport.SetActive(false);
        m_ArrayMusic = gameObject.GetComponents<AudioSource>();
        m_music2 = m_ArrayMusic[1];
    }

    public void ModifyHealth(int amount)
    {
        currentHealth += amount;
        eHealth.text = "Enemy Health: " + currentHealth + "/" + "100";
        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);
    }

    private void Update()
    {
        if (currentHealth == 0)
        {
            transport.SetActive(true);
            m_music2.Play();
            Destroy(gameObject, 1.5f);
            objective.text = "Objective: Touch the Golden Memorial";
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
