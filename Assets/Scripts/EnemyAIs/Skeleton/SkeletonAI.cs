using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public EnemyGroupHealth skeletonHealth;
    public PlayerHealthComponent pHealth;

    public float moveSpeed = 10.0f;

    private bool deathStarted = false;

    public enum SkeletonState
    {
        idle,
        run,
        attack,
        death
    }

    public SkeletonState currState = SkeletonState.idle;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        skeletonHealth = GetComponentInChildren<EnemyGroupHealth>();
        pHealth = player.GetComponent<PlayerHealthComponent>();
        currState = SkeletonState.idle;
    }

    IEnumerator Die()
    {
        deathStarted = true;
        anim.SetBool("nowDead", true);
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        playerPos.y = 0.0f;
        Vector3 myPos = gameObject.transform.position;
        myPos.y = 0.0f;

        //check death
        if(skeletonHealth.health <= 0.0f)
        {
            currState = SkeletonState.death;
        }

        if (currState == SkeletonState.idle && skeletonHealth.health > 0.0f)
        {
            anim.SetBool("nowAttack", false);
            anim.SetBool("nowRun", false);
        }
        else if(currState == SkeletonState.run && skeletonHealth.health > 0.0f)
        {
            anim.SetBool("nowAttack", false);
            anim.SetBool("nowRun", true);

            //look at player
            Vector3 dir = playerPos - myPos;
            dir.Normalize();
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 0.04f);

            //move towards player
            gameObject.transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);
        }
        else if(currState == SkeletonState.attack && skeletonHealth.health > 0.0f)
        {
            //attack
            anim.SetBool("nowAttack", true);

            //look at player
            Vector3 dir = playerPos - myPos;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);
        }
        else if(currState == SkeletonState.death && !deathStarted)
        {
            StartCoroutine(Die());
        }
    }
}
