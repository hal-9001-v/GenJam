using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroScript : MonoBehaviour
{

    public GameObject myParticles;

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
            myParticles = Instantiate(GameAssets.i.particles[1],col.transform.position, col.transform.rotation);
            myParticles.transform.parent = col.transform.parent;
            if(col.GetComponent<BateriaScript>().bigBattery!=null) col.GetComponent<BateriaScript>().bigBattery.SetActive(false);
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

