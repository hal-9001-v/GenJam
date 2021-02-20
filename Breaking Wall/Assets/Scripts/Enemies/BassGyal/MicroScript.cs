using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroScript : MonoBehaviour
{

   
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
        if (col.gameObject.tag == "Bateria") {

            col.GetComponent<BateriaScript>().bigBattery.SetActive(false);
            SoundManager.PlaySound(SoundManager.Sound.ELECTRICSOUND, 0.8f);

        }
    }
        private  void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Ground")
            {

                SoundManager.PlaySound(SoundManager.Sound.BANGSOUND, 0.2f);

            }
        }

    }

