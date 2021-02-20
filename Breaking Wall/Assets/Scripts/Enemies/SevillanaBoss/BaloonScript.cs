using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonScript : MonoBehaviour
{

    public WallDaLuciaScript myWall;

    private void Awake()
    {
        myWall = FindObjectOfType<WallDaLuciaScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bolso") {

            SoundManager.PlaySound(SoundManager.Sound.ILLOBALLOON, 0.8f);
            myWall.TakeDamage();
         
            Destroy(gameObject);

        }
    }

}
