using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUDRenderer : MonoBehaviour
{
    private GameObject health;
    private Image healthImage;

    private PlayerController myPlayer;
    private void Awake()
    {
        if (health == null) health = GameObject.Find("HealthImage");
        if (health != null) healthImage = health.GetComponent<Image>();
        if (myPlayer == null) myPlayer = FindObjectOfType<PlayerController>(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void UpdateHUD()
    {

        healthImage.sprite = GameAssets.i.healthArray[myPlayer.hp];


    }
}
