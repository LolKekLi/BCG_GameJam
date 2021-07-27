using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwith : MonoBehaviour
{
    private static SceneSwith instace;
    private static bool shouldPlayOpeningAnimation = false;

    private Animator animator;

    private AsyncOperation loadSceneAsync;

    public static void SwithToScene(int levlIndex)
    {
        instace.animator.SetTrigger("Closer");

        instace.loadSceneAsync = SceneManager.LoadSceneAsync(levlIndex);
        instace.loadSceneAsync.allowSceneActivation = false;
    }
    
    void Start()
    {
        instace = this;

        animator = GetComponent<Animator>();

        if (shouldPlayOpeningAnimation)
        {
            animator.SetTrigger("Opening");
        }
    }


    private void OnAnimationover()
    {
        shouldPlayOpeningAnimation = true;
        instace.loadSceneAsync.allowSceneActivation = true;
    }
}
