using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    CyrusWall myCyrus;

    private void Awake()
    {
        myCyrus = GetComponentInChildren<CyrusWall>();
        myCyrus.gameObject.SetActive(false);

    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {

            StartCoroutine(ChangePhase());

        }
    }

    private IEnumerator ChangePhase() {

        //Cinemática
        yield return new WaitForSeconds(5f);
        myCyrus.gameObject.SetActive(true);
        myCyrus.canAI = true;
        myCyrus.shield = false;
        myCyrus.gameObject.transform.parent = null;
        myCyrus.transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);

    }
}
