using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 25f;
    public float lifespan = 3f;
    private float lifecount = 0f;
    private Rigidbody myRigidBody;
    private GameObject gun;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        lifecount += Time.deltaTime;

        if(lifecount > lifespan)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
