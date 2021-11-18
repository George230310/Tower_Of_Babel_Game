using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthComponent : MonoBehaviour
{
    public ScriptablePlayer sp;
    public LevelLoader ll;
    public AudioSource blood;
    public Text hp;
    public GameObject screenflash;
    public float screenFlashTime = 1.0f;
    public GameObject collector;
    // Start is called before the first frame update
    private void Start()
    {
        screenflash.SetActive(false);
        hp.text = "Your HP: " + sp.health + "/" + 100.ToString();
    }

    public void TakeDamage(float damage)
    {
        sp.health -= damage;
        StartCoroutine(ShowAndHide(screenflash, screenFlashTime));
        hp.text = "Your HP: " + sp.health + "/" + 100.ToString();
        blood.Play();
        if (sp.health <= 0.0f)
        {
            MetricManager.AddToMetric1(1);
            MetricManager.AddToDeathDis(SceneManager.GetActiveScene().buildIndex);
            sp.health = 100.0f;
            ll.ReLoadScene();
        }
    }

    IEnumerator ShowAndHide(GameObject flash, float delay)
    {
        flash.SetActive(true);
        yield return new WaitForSeconds(delay);
        flash.SetActive(false);
    }

}
