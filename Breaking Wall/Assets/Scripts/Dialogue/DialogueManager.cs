using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class DialogueManager : MonoBehaviour
{
    [Header("Objects")]
    public TextMeshProUGUI dialogueText;
    public Image image;

    static DialogueManager instance;
    AudioSource source;

    [Header("Typing Sound Settings")]
    public AudioClip typingSound;

    [Range(0, 5)]
    public float pitch = 1f;
    [Range(0, 1)]
    public float volume = 0.5f;

    bool busy;


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
            }

            //Wait for input to end sentence

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

    void playSound() {

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
}
