using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class SceneController : MonoBehaviour
{
    static SceneController instance;
    public GameObject rootDestroy;

    const float fadeTime = 2;

    const int menuSceneIndex = 0;
    const int loadingSceneIndex = 2;
    const int lobbySceneIndex = 3;
    const int restartSceneIndex = 12;

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

            enabled = false;
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

    public void freeCursor() {
        Cursor.lockState = CursorLockMode.None;
    }

    public void lockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
    }


    IEnumerator SceneLoader(int index)
    {
        screenAnimator.SetTrigger(goToBlackTrigger);

        yield return new WaitForSeconds(fadeTime);

        SceneManager.LoadScene(loadingSceneIndex);

        yield return new WaitForSeconds(3);

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

    public void blackScreen() {
        if (instance == this) {
            screenAnimator.SetTrigger(goToBlackTrigger);
        }
        else {
            instance.blackScreen();
        }
    }

    public void whiteScreen() {
        if (instance == this)
        {
            screenAnimator.SetTrigger(goToWhiteTrigger);
        }
        else
        {
            instance.blackScreen();
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

    public void loadLobby() {
        if (instance == this)
        {
            Cursor.lockState = CursorLockMode.Locked;
            loadSceneAsynch(lobbySceneIndex);
        }
        else {
            instance.loadLobby();
        }
    }

    public void loadMenu() {
        if (instance == this)
        {
            loadSceneAsynch(menuSceneIndex);
        }
        else
        {
            instance.loadLobby();
        }
    }
}
