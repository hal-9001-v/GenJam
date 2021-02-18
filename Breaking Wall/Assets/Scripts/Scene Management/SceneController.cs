using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    static SceneController instance;
    public GameObject rootDestroy;

    const float fadeTime = 2;

    const int loadingSceneIndex = 1;

    public Animator screenAnimator;
    const string goToBlackTrigger = "Go Black";
    const string goToWhiteTrigger = "Go White";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);

            if (screenAnimator == null)
            {
                Debug.LogWarning("No Animator in Scene Controller");
            }
            else
            {
                screenAnimator.SetTrigger(goToWhiteTrigger);
            }


        }
        else
        {
            if (rootDestroy != null)
                Destroy(rootDestroy);

                gameObject.SetActive(false);
        }
    }



    public void loadSceneAsynch(int index)
    {
        if (instance == this)
        {
            Debug.Log("Loading scene " + SceneManager.GetSceneByBuildIndex(index).name);

            StartCoroutine(SceneLoader(index));
        }
        else
        {
            instance.loadSceneAsynch(index);
        }

    }

    public void loadNextScene()
    {
        if (instance == this)
        {
            loadSceneAsynch(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            instance.loadNextScene();
        }
    }


    IEnumerator SceneLoader(int index)
    {
        screenAnimator.SetTrigger(goToBlackTrigger);
        
        yield return new WaitForSeconds(fadeTime);

        SceneManager.LoadScene(loadingSceneIndex);

        yield return new WaitForSeconds(1);

        var operation = SceneManager.LoadSceneAsync(index);

        while (!operation.isDone)
        {
            Debug.Log("Progress: " + operation.progress * 100 + " %");
            yield return null;
        }

        screenAnimator.SetTrigger(goToWhiteTrigger);

    }

}
