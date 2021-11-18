using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    public ScriptablePlayer sp;

    private void Start()
    {
        if(slider.name == "volumeControlSlider")
        {
            slider.value = sp.masterVol;
        }
        else
        {
            slider.value = sp.SFXVol;
        }
    }

    public void SetLevel(float sliderVolume)
    {
        sp.masterVol = sliderVolume;
        mixer.SetFloat("MasterVol", Mathf.Log10(sliderVolume) * 20);
    }

    public void SetSFX(float sliderVolume)
    {
        sp.SFXVol = sliderVolume;
        mixer.SetFloat("SFXVol", Mathf.Log10(sliderVolume) * 20);
    }
}
