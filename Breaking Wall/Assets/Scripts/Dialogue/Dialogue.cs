using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    [TextArea(5, 10)]
    public string[] texts;
    DialogueManager myDM;

    [Header("Dialogue after This")]
    public Dialogue nextDialogue;

    [Header("Typing Settings")]
    [Range(0, 1)]
    public float typeDelay = 0.05f;



    [Space(5)]
    [Header("Events")]
    public UnityEvent atStartEvent;
    public UnityEvent atEndEvent;

    [Space(5)]
    public Transform talkingPivot;
    public bool playOnStart;

    // Start is called before the first frame update
    void Start()
    {
        myDM = FindObjectOfType<DialogueManager>();

        if (myDM == null)
        {
            Debug.LogWarning("No Dialogue Manager in scene");
        }

        if (playOnStart)
            trigger();
    }

    public void trigger()
    {
        if (myDM != null)
        {
            myDM.startDialogue(this);
        }
        else {
            myDM = FindObjectOfType<DialogueManager>();

            if (myDM != null)
                myDM.startDialogue(this);
            else {
                Debug.LogWarning("No dialogue Manager in Scene");
            }
        }
    }



}
