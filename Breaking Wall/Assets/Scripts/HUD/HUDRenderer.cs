using System.Collections;
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
    int bossMaxHp;
    private void Awake()
    {
        if (health == null) health = GameObject.Find("HealthImage");
        if (health != null) healthImage = health.GetComponent<Image>();
        if (myPlayer == null) myPlayer = FindObjectOfType<PlayerController>();

        virote.transform.localScale = Vector3.zero;

    }


    private void Start()
    {
    }
    // Update is called once per frame
    public void UpdateHUD()
    {
        if(myPlayer.hp >= 0) healthImage.sprite = GameAssets.i.healthArray[myPlayer.hp];

    }

 
    public void SetBossHudHealth(int bossHp)
    {
        slider.value = (float)bossHp/bossMaxHp;

    }
    
    public void SetVirote(bool b) {
        if (!b)
        {
            virote.transform.localScale = Vector3.zero;
        } else virote.transform.localScale = Vector3.one;
    }

    public void InitBossHudHealth(int bossStartHp)
    {

        bossMaxHp = bossStartHp;
        slider.maxValue = 1;
        slider.value = bossStartHp/bossMaxHp;
    }
}

