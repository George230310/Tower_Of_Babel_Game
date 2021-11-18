using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f;
    [SerializeField] GameObject target = default;
    private Vector3 start = Vector3.zero;
    //private AudioSource shoot;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(target.transform.position);
        start = transform.position;
        //shoot = gameObject.GetComponent<AudioSource>();
    }
    private void Update()
    {
        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        if(Vector3.Distance(transform.position, start) >= 100)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealthComponent>().TakeDamage(10.0f);
        }

        Destroy(this.gameObject);
    }
}
