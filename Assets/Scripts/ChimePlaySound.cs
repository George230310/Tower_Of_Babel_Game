using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimePlaySound : MonoBehaviour
{
    public float fadeInTime = 3.0f;
    public float fadeOutTime = 3.0f;

    private bool fadingIn = false;
    private bool fadingOut = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !fadingIn)
        {
            //GetComponent<AudioSource>().Play();
            StartCoroutine(SoundFadeIn());
        }
    }

    IEnumerator SoundFadeIn()
    {
        fadingIn = true;
        AudioSource ad = GetComponent<AudioSource>();
        ad.Play();
        ad.volume = 0.0f;
        float timer = 0.0f;
        float fadeInStep = fadeInTime / 1000.0f;
        while(timer < fadeInStep)
        {
            ad.volume += (fadeInStep / fadeInTime);
            timer += fadeInStep;
            yield return new WaitForSeconds(fadeInStep);
        }

        ad.volume = 1.0f;

        fadingIn = false;
        yield return null;
    }

    IEnumerator SoundFadeOut()
    {
        fadingOut = true;
        AudioSource ad = GetComponent<AudioSource>();
        float timer = 0.0f;
        float fadeOutStep = fadeOutTime / 1000.0f;
        while (timer < fadeInTime)
        {
            ad.volume -= (fadeOutStep / fadeOutTime);
            timer += fadeOutStep;
            yield return new WaitForSeconds(fadeOutStep);
        }

        ad.volume = 0.0f;
        ad.Stop();

        fadingOut = false;
        yield return null;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !fadingOut)
        {
            //GetComponent<AudioSource>().Stop();
            StartCoroutine(SoundFadeOut());
        }
    }
}
