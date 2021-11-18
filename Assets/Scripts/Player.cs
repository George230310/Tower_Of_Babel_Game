using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int life = 10;
    //private bool isDead = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bullet")
        {
            life -= 1;
        }
    }
}
