using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    private AudioSource audioSource;
    private bool costil =true;

    void Start()
    {
        costil = true;
        audioSource = GetComponent<AudioSource>();
    }
    public void Reload()
    {
        audioSource.Play();
        var activeScene = SceneManager.GetActiveScene();
        SceneSwith.SwithToScene(activeScene.buildIndex);
    }

    public void LoadLevl(int LevlIndex)
    {
        if (costil)
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
            
            SceneSwith.SwithToScene(LevlIndex);
            costil = false;
        }
       
    }

    public void Exit()
    {
        Application.Quit();
    }
}
