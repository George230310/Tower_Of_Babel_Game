using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantAI : MonoBehaviour
{
    public GameObject player;
    public GameObject crystal;
    public GameObject hookObj;
    public Animator anim;
    public EnemyHeath bossHealth;
    public PlayerHealthComponent pHealth;
    public GameObject footSound;
    public AudioSource footSoundSource;

    public float moveAnimationActualSpeed = 0.0f;
    public float moveMax = 0.7f;
    public float attackDamage = 20.0f;
    public float lerpDuration = 4.0f;

    private bool moveStarted = false;
    private bool stopStarted = false;
    private bool attackStarted = false;
    private bool dieStarted = false;
    private bool attack2Started = false;

    

    //public float maxAttackInterval = 1.23f;

    //private float attackTimer = 0.0f;

    enum GiantState
    {
        Idle,
        Trace,
        Attack1,
        Attack2,
        Death
    }

    private GiantState currState;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        bossHealth = crystal.GetComponent<EnemyHeath>();
        pHealth = player.GetComponent<PlayerHealthComponent>();
        currState = GiantState.Idle;
        footSoundSource = footSound.GetComponent<AudioSource>();
    }

    IEnumerator MoveToPlayer()
    {
        moveStarted = true;

        float timeElapsed = 0.0f;
        while(timeElapsed < lerpDuration)
        {
            moveAnimationActualSpeed = Mathf.Lerp(0.0f, moveMax, timeElapsed/lerpDuration);
            anim.SetFloat("locomotion", moveAnimationActualSpeed);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        moveAnimationActualSpeed = moveMax;
        anim.SetFloat("locomotion", moveAnimationActualSpeed);

        moveStarted = false;
    }

    IEnumerator StopMotion(float currSpeed)
    {
        stopStarted = true;

        float timeElapsed = 0.0f;
        while (timeElapsed < lerpDuration)
        {
            moveAnimationActualSpeed = Mathf.Lerp(currSpeed, 0.0f, timeElapsed/lerpDuration);
            anim.SetFloat("locomotion", moveAnimationActualSpeed);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        moveAnimationActualSpeed = 0.0f;
        anim.SetFloat("locomotion", moveAnimationActualSpeed);

        stopStarted = false;
    }

    IEnumerator AttackPlayer()
    {
        attackStarted = true;
        anim.SetTrigger("jump");
        yield return new WaitForSeconds(1.3f);
        footSoundSource.Play();

        //vertical distance check
        Vector3 playerPos = player.transform.position;
        Vector3 myPos = transform.position;

        Vector3 player2D = playerPos;
        player2D.y = 0.0f;
        Vector3 my2D = myPos;
        my2D.y = 0.0f;

        float hDistance = Vector3.Distance(player2D, my2D);
        float vDistance = (playerPos - myPos).y;

        if(vDistance < 15.0f && hDistance < 20.0f)
        {
            pHealth.TakeDamage(attackDamage);
        }

        attackStarted = false;
    }

    IEnumerator AttackPlayer2()
    {
        attack2Started = true;

        anim.SetTrigger("attack1B");
        yield return new WaitForSeconds(1.3f);

        //vertical distance check
        Vector3 playerPos = player.transform.position;
        Vector3 myPos = transform.position;

        Vector3 player2D = playerPos;
        player2D.y = 0.0f;
        Vector3 my2D = myPos;
        my2D.y = 0.0f;

        float vDistance = (playerPos - myPos).y;
        float hDistance = Vector3.Distance(player2D, my2D);
        if (vDistance >= 15.0f && vDistance < 35.0f && hDistance < 20.0f)
        {
            pHealth.TakeDamage(attackDamage);
        }

        attack2Started = false;
    }

    IEnumerator Die()
    {
        dieStarted = true;

        Destroy(hookObj, 2.0f);
        Destroy(crystal, 2.0f);

        anim.SetTrigger("death");
        yield return new WaitForSeconds(6.5f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //calculate player 2d distance
        Vector3 player2D = player.transform.position;
        Vector3 my2D = gameObject.transform.position;
        player2D.y = 0.0f;
        my2D.y = 0.0f;
        float distanceToPlayer = Vector3.Distance(my2D, player2D);

        //vertical distance calculation
        Vector3 playerPos = player.transform.position;
        Vector3 myPos = transform.position;
        float vDistance = (playerPos - myPos).y;

        //switch state
        if (distanceToPlayer > 120.0f && bossHealth.health > 0.0f)
        {
            currState = GiantState.Idle;
        }
        else if(distanceToPlayer > 20.0f && bossHealth.health > 0.0f)
        {
            currState = GiantState.Trace;
        }
        else if(bossHealth.health > 0.0f && vDistance < 15.0f)
        {
            currState = GiantState.Attack1;
        }
        else if(bossHealth.health > 0.0f && vDistance >= 15.0f && vDistance < 35.0f)
        {
            currState = GiantState.Attack2;
        }
        else if(bossHealth.health <= 0.0f)
        {
            currState = GiantState.Death;
        }

        //look at player
        if(currState == GiantState.Trace || currState == GiantState.Attack1 || currState == GiantState.Attack2)
        {
            Vector3 dir = player2D - my2D;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);
        }


        if (currState == GiantState.Idle && !stopStarted)
        {
            StartCoroutine(StopMotion(moveAnimationActualSpeed));
        }
        else if(currState == GiantState.Trace && !moveStarted && moveAnimationActualSpeed <= 0.0f)
        {

            StartCoroutine(MoveToPlayer());
        }
        else if(currState == GiantState.Attack1 && !attackStarted)
        {
            StartCoroutine(AttackPlayer());
        }
        else if(currState == GiantState.Attack2 && !attack2Started)
        {
            StartCoroutine(AttackPlayer2());
        }
        else if(currState == GiantState.Death && !dieStarted)
        {
            StartCoroutine(Die());
        }
    }
}
