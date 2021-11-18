using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Animator anim;

    public float attackDamage;
    public float attackRange;
    public float attackCoolDown;

    private float attackTimer = 0.0f;
    private AudioSource mySource;
    public AudioSource enemyHitSound;
    public AudioSource metalHitSound;

    public Camera cam;
    public GameObject hand;

    private void Start()
    {
        anim = GetComponent<Animator>();
        mySource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
        if(Input.GetMouseButtonDown(0) && attackTimer >= attackCoolDown)
        {
            anim.SetBool("attacking", true);
            mySource.Play();
            DoAttack();
            attackTimer = 0.0f;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            anim.SetBool("attacking", false);
        }
    }

    private void DoAttack()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, attackRange))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyHeath eHealth = hit.collider.gameObject.GetComponent<EnemyHeath>();
                eHealth.TakeDamage(attackDamage);
                enemyHitSound.Play();
            }
            else if(hit.collider.CompareTag("groupEnemy"))
            {
                EnemyGroupHealth gHealth = hit.collider.gameObject.GetComponent<EnemyGroupHealth>();
                gHealth.TakeDamage(attackDamage);
                enemyHitSound.Play();
            }
            else
            {
                metalHitSound.Play();
            }
        }
    }
}
