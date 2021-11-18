using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDetector : MonoBehaviour
{
    private SkeletonAI ai;

    private void Start()
    {
        ai = GetComponentInParent<SkeletonAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ai.currState = SkeletonAI.SkeletonState.run;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ai.currState = SkeletonAI.SkeletonState.idle;
        }
    }
}
