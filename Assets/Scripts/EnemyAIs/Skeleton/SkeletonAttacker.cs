using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttacker : MonoBehaviour
{
    private SkeletonAI ai;
    // Start is called before the first frame update

    void Start()
    {
        ai = GetComponentInParent<SkeletonAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ai.currState = SkeletonAI.SkeletonState.attack;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ai.currState = SkeletonAI.SkeletonState.run;
        }
    }
}
