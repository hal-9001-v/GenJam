using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViroteScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject myLr;
    public GameObject myCube;
    public bool falling;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tick") {

            Destroy(myLr);
            SoundManager.PlaySound(SoundManager.Sound.BREAKROPE, 0.4f);
            Destroy(myCube.GetComponent<SpringJoint>());
            
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            falling = false;
            SoundManager.PlaySound(SoundManager.Sound.SMASHFLOOR, 0.4f);
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GameObject.Instantiate(gameObject, myCube.transform, true) ;
        }
    }

}
