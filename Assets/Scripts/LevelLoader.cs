using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public static bool toScene4 = false;
    public int lastSceneIndex = 7;

    private void Update()
    {
        //SCENE 2 to 3
        //scene 3 to 4
        if(SceneManager.GetActiveScene().buildIndex == 4 && toScene4)
        {
            LoadNextLevel();
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            LoadNextLevel();
        }
    }

    public void ReLoadScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex < lastSceneIndex)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
        else
        {
            // load menu
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            StartCoroutine(LoadLevel(0));
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        transition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(transitionTime);
        //Load
        SceneManager.LoadScene(levelIndex);
    }
}
