using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class GameAssets : MonoBehaviour
{

    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null)
            {
                _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            }
                return _i;
        }
    }


    public SoundAudioClip[] soundAudioClipArryay;
    public GameObject[] particles;
    public Sprite[] healthArray;


    [System.Serializable]
    public class SoundAudioClip {

        public SoundManager.Sound sound;
        public AudioClip audioClip;


    } 


}
