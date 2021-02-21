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
            Instantiate(GameAssets.i.particles[7], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z), gameObject.transform.rotation);
            SoundManager.PlaySound(SoundManager.Sound.BREAKROPE, 0.4f);
            Destroy(myCube.GetComponent<SpringJoint>());
            
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Quaternion lol = Quaternion.Euler(-90, 0, 0);
            Instantiate(GameAssets.i.particles[8], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z), lol);
            falling = false;
            SoundManager.PlaySound(SoundManager.Sound.PUNCHHITS, 0.2f);
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GameObject.Instantiate(gameObject, myCube.transform, true) ;
        }
    }

}
