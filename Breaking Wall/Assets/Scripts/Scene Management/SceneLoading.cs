using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    [Header("Image")]
    [Range(0.1f, 2)]
    public float imageFrameTime = 0.1f;

    public Texture[] frames;
    public RawImage image;
    int currentFrame = 0;

    [Space(5)]
    [Header("Loading Text")]
    public TextMeshProUGUI text;
    
    [Range(0.1f, 2)]
    public float textFrameTime = 0.1f;

    [Space(2)]
    public Color color;
    public Color heartColor;


    void Start()
    {

        if (frames != null && image != null)
            StartCoroutine(imageAnimation());

        if (text != null)
            StartCoroutine(textAnimation());
    }

    IEnumerator imageAnimation()
    {

        while (true)
        {

            if (currentFrame >= frames.Length)
            {
                currentFrame = 0;
            }

            if(frames[currentFrame] != null)
                image.texture = frames[currentFrame];
            currentFrame++;


            yield return new WaitForSeconds(imageFrameTime);
        }

    }

    IEnumerator textAnimation()
    {
        //Counter will be 0 on first frame
        int counter = -1;
        text.color = color;

        while (true)
        {
            counter++;
            switch (counter) {
                case 0:
                    text.color = color;
                    text.text = "Loading";
                    break;
                case 1:
                    text.color = color;
                    text.text = "Loading.";
                    break;

                case 2:
                    text.color = color;
                    text.text = "Loading..";
                    break;

                case 3:
                    text.color = color;
                    text.text = "Loading...";
                    break;

                case 4:
                    text.color = heartColor;
                    text.text = "Loading ";
                    break;
                case 5:
                    text.color = heartColor;
                    text.text = "Loading <3";

                    yield return new WaitForSeconds(textFrameTime);
                    break;

                default:
                    counter = 0;
                    break;
            }

            

            yield return new WaitForSeconds(textFrameTime);
        }

    }


}
