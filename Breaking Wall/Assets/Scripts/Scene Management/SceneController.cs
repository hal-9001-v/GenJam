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
    const int restartSceneIndex = 4;

    public Animator screenAnimator;
    const string goToBlackTrigger = "Go Black";
    const string goToWhiteTrigger = "Go White";

    [Header("Debugging")]
    public int levelCounter = 0;

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

    private void Start()
    {
        enableInteraction();
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
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextSceneIndex == loadingSceneIndex) {
                nextSceneIndex++;
            }
            loadSceneAsynch(nextSceneIndex);
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

        Debug.Log("Loading Done!");
        screenAnimator.SetTrigger(goToWhiteTrigger);

        enableInteraction();
    }

    void enableInteraction()
    {
        InteractionEnabler interactionEnabler = FindObjectOfType<InteractionEnabler>();

        if (interactionEnabler != null)
        {
            interactionEnabler.setLevel(levelCounter);
            levelCounter++;
        }

    }

    public void exit()
    {
        Application.Quit();
    }

    public void restartGame() {
        if (instance == this)
        {
            loadSceneAsynch(restartSceneIndex);
        }
        else {
            instance.restartGame();
        }
    }

}
