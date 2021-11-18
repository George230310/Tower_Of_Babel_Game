using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum EnemyState
    {
        idle,
        chase,
        attack
    };

    public GameObject bulletPrefab;
    public float StopShootDistance = 30.0f;
    public GameObject shooter;
    public float FPS = 1.0f;

    [Range(0, 10)] public int MoveSpeed = 3;

    private Vector3 startPos = Vector3.zero;
    private Vector3 desPos = Vector3.zero;
    private bool movingRight = true;
    private float nextFire = 0.0f;
    private EnemyState state;
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        startPos = transform.position;
        desPos = new Vector3(startPos.x + MoveSpeed * 7, startPos.y, startPos.z);
    }
    private void Update()
    {
        Move();
        switch (state)
        {
            default:
            case EnemyState.idle:
                //maybe path finding?
                if (Vector3.Distance(transform.position, target.position) < StopShootDistance)
                {
                    state = EnemyState.attack;
                }
                break;
            case EnemyState.chase:
                break;
            case EnemyState.attack:
                if (Time.time > nextFire)
                {
                    nextFire = Time.time + FPS;
                    Vector3 relativePos = target.position - transform.position;
                    GameObject clone = Instantiate(bulletPrefab, shooter.transform.position, Quaternion.LookRotation(relativePos));
                    clone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    clone.GetComponent<AudioSource>().Play();
                }

                if (Vector3.Distance(transform.position, target.position) > StopShootDistance)
                {
                    state = EnemyState.idle;
                }
                break;
        }
    }

    private void Move()
    {
        Vector3 pos = transform.position;
        if (movingRight)
        {
            transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
            if (pos.x >= desPos.x)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
            if (pos.x <= startPos.x)
            {
                movingRight = true;
            }
        }
    }

}
