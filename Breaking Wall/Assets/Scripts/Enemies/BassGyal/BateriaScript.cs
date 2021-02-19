using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BateriaScript : MonoBehaviour
{
    public BassGyalScript myBadGyal;
    public GameObject bigBattery;
    public GameObject smolBattery;
    private void Awake()
    {
        if (myBadGyal == null) myBadGyal = FindObjectOfType<BassGyalScript>();


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bolso" && !bigBattery.activeSelf )
        {

            myBadGyal.shieldHP--;
           Destroy(gameObject);

        }
    }

}
