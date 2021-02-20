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
            Destroy(myCube.GetComponent<SpringJoint>());
        
            
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            falling = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GameObject.Instantiate(gameObject, myCube.transform, true) ;
        }
    }

}
