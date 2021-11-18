using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public int enemyCount;
    public GameObject transportor;

    public Text eHealth;
    public Text objective;

    public bool finishLevel = false;

    public float fogDen = 0.05f;

    private void Start()
    {
        eHealth.text = "Remaining Enemy Number: " + enemyCount;
        transportor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        eHealth.text = "Remaining Enemy Number: " + enemyCount;
        if (enemyCount <= 0 && !finishLevel)
        {
            objective.text = "Touch the Golden Memorial";
            transportor.SetActive(true);
            finishLevel = true;
        }
        else if(enemyCount <= 0)
        {
            RenderSettings.fogDensity = Mathf.Lerp(fogDen, 0.00f, Time.deltaTime);
            fogDen = RenderSettings.fogDensity;
        }
    }
}
