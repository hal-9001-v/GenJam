using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class DialogueManager : InputComponent
{
    [Header("Objects")]
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;
    public Image image;

    public PlayerController player;

    public Transform boxTransform;
    public Transform defaultPosition;
    public Animator animator;

    static DialogueManager instance;
    AudioSource source;

    [Header("Typing Sound Settings")]
    public AudioClip typingSound;

    [Range(0, 5)]
    public float pitch = 1f;
    [Range(0, 1)]
    public float volume = 0.5f;

    [Header("Time Line")]
    public PlayableDirector director;

    public Dialogue[] timelineDialogues;
    int currentDialogue = 0;

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

            image.enabled = false;
            dialogueText.enabled = false;

        }
    }

    public void startDialogue(Dialogue dialogue)
    {

        if (instance == this)
        {
            if (!busy)
            {
                show();
                busy = true;
                StartCoroutine(TypeText(dialogue));

                CameraController.LockCamera();

                if (player != null)
                    player.disableMove();
                else
                {
                    player = FindObjectOfType<PlayerController>();

                    if (player != null)
                        player.disableMove();
                }

            }
        }
        else
        {
            instance.startDialogue(dialogue);
        }


    }

    IEnumerator TypeText(Dialogue dialogue)
    {
        if (boxTransform != null)
        {
            if (dialogue.talkingPivot != null)
                boxTransform.position = Camera.main.WorldToScreenPoint(dialogue.talkingPivot.position);
            else if (defaultPosition != null)
                boxTransform.position = defaultPosition.position;
        }

        if (director != null)
        {
            director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        }

        if (speakerText != null)
        {
            speakerText.text = dialogue.speakerName;

        }

        interactionPressed = false;
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
            CameraController.FreeCamera();
            busy = false;

            if (player != null)
                player.enableMove();
            else
            {
                player = FindObjectOfType<PlayerController>();

                if (player != null)
                    player.enableMove();
            }

            if (director != null)
            {
                director.playableGraph.GetRootPlayable(0).SetSpeed(1);
            }
        }


    }

    void hide()
    {

        if (animator != null)
            animator.SetTrigger("Hide Box");

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


        if (animator != null)
            animator.SetTrigger("Show Box");

        if (image != null)
        {
            image.enabled = true;
        }

        if (dialogueText != null)
        {
            dialogueText.enabled = true;
        }


    }


    public override void setPlayerControls(PlayerControls inputs)
    {
        inputs.DefaultActionMap.Interaction.performed += ctx =>
        {
            interactionPressed = true;
        };

        inputs.DefaultActionMap.Interaction.canceled += ctx => interactionPressed = false;

    }

    public void setDirector(PlayableDirector d)
    {
        director = d;
    }

    public void playNextDialogue()
    {

        if (instance == this)
        {
            Debug.Log("Playing Dialogue in Timeline");
            if (timelineDialogues != null && timelineDialogues.Length > currentDialogue)
            {
                startDialogue(timelineDialogues[currentDialogue]);
            }
        }
        else
        {
            instance.director = director;
            instance.startDialogue(timelineDialogues[currentDialogue]);
        }

        currentDialogue++;

    }
}
