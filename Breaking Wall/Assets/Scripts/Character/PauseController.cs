using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : InputComponent
{
    private AudioSource[] allAudioSources ;

    static PauseController instance;
    public Canvas pauseCanvas;
    public bool canPauseGame = true;

    bool pauseDisplayed;
    private void Awake()
    {
        allAudioSources = FindObjectsOfType<AudioSource>();
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;

            if (pauseCanvas == null)
            {
                Debug.LogWarning("No canvas assigned on Pause Controller!");
            }
            else
            {
                pauseCanvas.enabled = false;
            }
        }

        else
        {
            Debug.LogWarning("2 Pause Controllers in Scene!");
        }

    }


    public void pauseGame()
    {
        if (canPauseGame)
        {
            Time.timeScale = 0;
            StopAllAudio();
            pauseDisplayed = true;
            show();
        }

    }

    private void StopAllAudio() {
    
        foreach (AudioSource a in allAudioSources)
        {
            a.Pause();
        }
    
    }

    private void ResumeAllAudio() {

        foreach (AudioSource a in allAudioSources)
        {
            a.UnPause();
        }

    }

    
    public void resumeGame()
    {
        pauseDisplayed = false;
        Time.timeScale = 1;
        ResumeAllAudio();
        hide();
    }


    void show()
    {
        if (pauseCanvas != null)
        {
            Cursor.lockState = CursorLockMode.None;
            pauseCanvas.enabled = true;
        }

    }

    void hide()
    {
        if (pauseCanvas != null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            pauseCanvas.enabled = false;
        }

    }

    public void restartGame()
    {
        Time.timeScale = 1;

        if (pauseCanvas != null)
            pauseCanvas.enabled = false;

        Cursor.lockState = CursorLockMode.None;

    }

    public override void setPlayerControls(PlayerControls inputs)
    {
        inputs.DefaultActionMap.Pause.performed += ctx =>
        {
            if (pauseDisplayed)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }

        };

    }
}
