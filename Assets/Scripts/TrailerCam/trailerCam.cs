using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trailerCam : MonoBehaviour
{
    public Camera playerCam;
    public Camera secondCam;
    public GameObject UI;

    private bool isPlayerCam = true;
    private bool UIActive = true;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V) && UIActive)
        {
            UI.SetActive(false);
            UIActive = false;
        }
        else if(Input.GetKeyDown(KeyCode.V) && !UIActive)
        {
            UI.SetActive(true);
            UIActive = true;
        }

        if(Input.GetKeyDown(KeyCode.C) && isPlayerCam)
        {
            playerCam.enabled = false;
            secondCam.enabled = true;
            isPlayerCam = false;
            UI.SetActive(false);
            
        }
        else if(Input.GetKeyDown(KeyCode.C) && !isPlayerCam)
        {
            playerCam.enabled = true;
            secondCam.enabled = false;
            isPlayerCam = true;
            UI.SetActive(true);
        }
    }
}
