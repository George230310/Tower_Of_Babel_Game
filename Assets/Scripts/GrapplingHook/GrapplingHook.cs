using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrapplingHook : MonoBehaviour
{
    public Vector3 hookScale = new Vector3(0.5681573f, 0.5029866f, 0.1334556f);
    public GameObject hook;
    public GameObject hookHolder;

    public float hookTravelSpeed;
    public float playerTravelSpeed;

    public AudioSource hookExtendSound;
    public AudioSource retractSound;
    private bool extendSoundPlaying = false;
    private bool retractSoundPlaying = false;

    public bool fired;
    public bool hooked;
    public GameObject hookedObject;

    public float maxDistance;
    private float currentDistance;

    private bool grounded;
    private AudioSource hookSound;

    private LineRenderer rope;

    private void Start()
    {
        hookSound = GetComponent<AudioSource>();
        rope = hook.GetComponent<LineRenderer>();
        CheckIfGrounded();
    }

    private void Update()
    {
        CheckIfGrounded();

        if(Input.GetMouseButtonDown(1) && !fired)
        {
            hookSound.Play();
            MetricManager.AddToMetric2(1);
            MetricManager.AddToHookDis(SceneManager.GetActiveScene().buildIndex);
            fired = true;
        }

        if(fired)
        {
            rope.positionCount = 2;
            rope.SetPosition(0, hookHolder.transform.position);
            rope.SetPosition(1, hook.transform.position);
        }

        if(fired && !hooked)
        {
            if (!extendSoundPlaying)
            {
                StartCoroutine(PlayExtendSound());
            }

            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);

            if(currentDistance >= maxDistance)
            {
                ReturnHook();
            }
        }

        if(hooked && fired)
        {
            if(!retractSoundPlaying)
            {
                StartCoroutine(PlayRetractSound());
            }

            hook.transform.parent = hookedObject.transform;
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, playerTravelSpeed * Time.deltaTime);
            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);

            this.GetComponent<Rigidbody>().useGravity = false;

            if(distanceToHook < 1.5f)
            {
                if(!grounded)
                {
                    this.transform.Translate(Vector3.forward * Time.deltaTime * 61.0f);
                    this.transform.Translate(Vector3.up * Time.deltaTime * 71.0f);
                }

                StartCoroutine(Climb());
            }
        }
        else
        {
            hook.transform.parent = hookHolder.transform;
            this.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    IEnumerator Climb()
    {
        yield return new WaitForSeconds(0.1f);
        ReturnHook();
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    IEnumerator PlayExtendSound()
    {
        extendSoundPlaying = true;
        hookExtendSound.Play();
        yield return new WaitForSeconds(1.2f);
        extendSoundPlaying = false;
    }

    IEnumerator PlayRetractSound()
    {
        retractSoundPlaying = true;
        retractSound.Play();
        yield return new WaitForSeconds(1.2f);
        retractSoundPlaying = false;
    }

    public void ReturnHook()
    {
        // fix hook scale
        hook.transform.parent = hookHolder.transform;
        hook.transform.localScale = hookScale;

        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;

        rope.positionCount = 0;
    }

    void CheckIfGrounded()
    {
        RaycastHit hit;
        float distance = 0.3f;

        Vector3 dir = new Vector3(0.0f, -1.0f);
        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
}
