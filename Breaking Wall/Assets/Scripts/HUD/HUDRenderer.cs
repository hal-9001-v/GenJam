﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUDRenderer : MonoBehaviour
{
    private GameObject health;
    private Image healthImage;
    public Image virote;
    private PlayerController myPlayer;
    public Slider slider;
    
    private void Awake()
    {
        if (health == null) health = GameObject.Find("HealthImage");
        if (health != null) healthImage = health.GetComponent<Image>();
        if (myPlayer == null) myPlayer = FindObjectOfType<PlayerController>();

        virote.transform.localScale = Vector3.zero;

    }


    // Update is called once per frame
    public void UpdateHUD()
    {
        healthImage.sprite = GameAssets.i.healthArray[myPlayer.hp-1];

    }

 
    public void SetBossHudHealth(int bossHp)
    {
        slider.value = bossHp;

    }

    public void SetVirote(bool b) {
        if (!b)
        {
            virote.transform.localScale = Vector3.zero;
        } else virote.transform.localScale = Vector3.one;
    }

    public void InitBossHudHealth(int bossStartHp)
    {

        
        slider.maxValue = bossStartHp;
        slider.value = bossStartHp;
    }
}

