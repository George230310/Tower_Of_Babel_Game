using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngelAI : MonoBehaviour
{
    public GameObject player;
    public EnemyHeath bossHealth;
    public PlayerHealthComponent pHealth;
    public GameObject bulletPrefab;
    public float shoutDamage = 50.0f;

    public Text warning;

    public AudioSource angelShout;
    public GameObject deathSphere;

    private bool isDead = false;
    private bool attackMotion = false;
    private bool dieStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        bossHealth = GetComponentInChildren<EnemyHeath>();
        pHealth = player.GetComponent<PlayerHealthComponent>();
    }

    IEnumerator AttackRot()
    {
        attackMotion = true;

        yield return new WaitForSeconds(10.0f);

        angelShout.Play();
        deathSphere.SetActive(true);
        warning.text = "Stay Away From Angel Attack Orb!!!";
        yield return new WaitForSeconds(5.1f);

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= 82.0f)
        {
            pHealth.TakeDamage(shoutDamage);
        }

        yield return new WaitForSeconds(0.5f);
        deathSphere.SetActive(false);
        warning.text = "";

        float shootPeriod = 10.0f;
        float timer = 0.0f;

        while(timer < shootPeriod)
        {
            Vector3 relativePos = player.transform.position - transform.position;
            GameObject clone = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(relativePos));
            clone.transform.localScale = new Vector3(0.5f, 0.5f, 40.0f);
            clone.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.5f);

            timer += 0.5f;
        }

        attackMotion = false;
    }

    IEnumerator DieRot()
    {
        dieStarted = true;

        Destroy(gameObject);

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if(bossHealth.health <= 0.0f)
        {
            isDead = true;
        }

        if(!isDead)
        {
            if (!attackMotion)
            {
                StartCoroutine(AttackRot());
            }
        }
        else
        {
            if(!dieStarted)
            {
                StartCoroutine(DieRot());
            }
        }
    }
}
