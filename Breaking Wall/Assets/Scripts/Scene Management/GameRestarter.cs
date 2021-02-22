using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestarter : MonoBehaviour
{
    public GameObject container;
    private void Awake()
    {

        Cursor.lockState = CursorLockMode.None;
        foreach (GameObject go in FindObjectsOfType<GameObject>()) {
            if(go != container)
                Destroy (go);
        }
        Debug.Log("Restarting!");

        SceneManager.LoadScene(0);
    }
}
