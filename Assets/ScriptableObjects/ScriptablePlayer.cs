using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName="NewPlayer", menuName="OurPlayer")]
public class ScriptablePlayer : ScriptableObject
{
    public float health = 100.0f;
    public float masterVol = 0.2f;
    public float SFXVol = 0.2f;
    public string playerName = "Luke";
}
