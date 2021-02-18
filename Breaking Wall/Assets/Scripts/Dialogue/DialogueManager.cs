using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class DialogueManager : InputComponent
{
    [Header("Objects")]
    public TextMeshProUGUI dialogueText;
    public Image image;
    public Transform boxTransform;

    static DialogueManager instance;
    AudioSource source;

    [Header("Typing Sound Settings")]
    public AudioClip typingSound;

    [Range(0, 5)]
    public float pitch = 1f;
    [Range(0, 1)]
    public float volume = 0.5f;

    bool busy;

    bool interactionPressed;


    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            source = GetComponent<AudioSource>();

            if (dialogueText == null)
            {
                Debug.LogWarning("No dialogue Text in DialogueManager!");
            }

            if (image == null)
            {
                Debug.LogWarning("No image in DialogueManager!");
            }

            if (boxTransform == null)
            {
                Debug.LogWarning("No Box Transform in DialogueManager!");
            }

            hide();
        }
        else
        {
            Debug.LogWarning("Deleting " + name + " because " + instance.name + " is singleton");
            Destroy(this);

        }
    }

    public void startDialogue(Dialogue dialogue)
    {
        if (!busy)
        {
            show();
            busy = true;
            StartCoroutine(TypeText(dialogue));

        }

    }

    IEnumerator TypeText(Dialogue dialogue)
    {

        if (boxTransform != null && dialogue.talkingPivot != null)
        {
            boxTransform.position = Camera.main.WorldToScreenPoint(dialogue.talkingPivot.position);
        }
        //Get every Sentence
        foreach (string s in dialogue.texts)
        {
            char[] characters = s.ToCharArray();
            dialogueText.text = "";

            //Get every char from sentence
            foreach (char c in characters)
            {
                dialogueText.text += c;

                //Sound should be played now
                playSound();

                yield return new WaitForSeconds(dialogue.typeDelay);

                if (interactionPressed)
                {
                    dialogueText.text = characters.ArrayToString();
                    interactionPressed = false;

                    goto endOfLine;
                }
            }

        endOfLine:
            //Wait for input to end sentence
            while (!interactionPressed)
            {
                yield return null;
            }

            interactionPressed = false;



        }

        if (dialogue.nextDialogue != null)
        {
            dialogue.atEndEvent.Invoke();
            StartCoroutine(TypeText(dialogue.nextDialogue));
        }
        else
        {
            hide();
            busy = false;
        }


    }

    void hide()
    {
        image.enabled = false;
        dialogueText.enabled = false;

    }

    void playSound()
    {

        if (!source.isPlaying && typingSound != null)
        {
            source.clip = typingSound;

            source.pitch = pitch;
            source.volume = volume;

            source.Play();
        }
    }

    void show()
    {
        image.enabled = true;
        dialogueText.enabled = true;

    }

    public override void setPlayerControls(PlayerControls inputs)
    {
        inputs.DefaultActionMap.Interaction.performed += ctx => interactionPressed = true;
    }
}
