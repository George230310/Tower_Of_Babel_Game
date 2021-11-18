using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour
{
    public GameObject player;
    public GrapplingHook gp;
    public AudioSource hookedSound;

    private void Start()
    {
        hookedSound = GetComponent<AudioSource>();
        gp = player.GetComponent<GrapplingHook>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hookable"))
        {
            if(gp.fired)
            {
                hookedSound.Play();
            }

            gp.hooked = true;
            gp.hookedObject = other.gameObject;
        }
        else
        {
            gp.ReturnHook();
        }
    }
}
