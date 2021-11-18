using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PopUpMenuController : MonoBehaviour
{
    public GameObject popUp;
    private bool popUpActive = false;

    public ScriptablePlayer sp;

    public AudioMixer music_mix;
    public AudioMixer sfx_mix;

    public Slider slider1;
    public Slider slider2;

    private void Start()
    {
        music_mix.SetFloat("MasterVol", Mathf.Log10(sp.masterVol) * 20);
        sfx_mix.SetFloat("SFXVol", Mathf.Log10(sp.SFXVol) * 20);
        slider1.value = sp.masterVol;
        slider2.value = sp.SFXVol;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M) && !popUpActive)
        {
            popUp.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            popUpActive = true;
        }
        else if(Input.GetKeyDown(KeyCode.M) && popUpActive)
        {
            popUp.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            popUpActive = false;
        }
    }
}
