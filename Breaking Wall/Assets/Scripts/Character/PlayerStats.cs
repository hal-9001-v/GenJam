using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //Global player stats between stats (kind of a Save file)
    PlayerController pc;
    public int hp = 10;
    public int level = 0;


    //Reset values in case of game reset/death...
    public void reset()
    {
        hp = 10;
        level = 0;

    }

    //On awake, DDOL + Look for PS
    void Awake()
    {
        if(pc ==null) pc = FindObjectOfType<PlayerController>();
        DontDestroyOnLoad(gameObject);
    }

}
