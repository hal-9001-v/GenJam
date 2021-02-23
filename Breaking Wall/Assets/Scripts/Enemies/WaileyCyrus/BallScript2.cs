using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript2 : MonoBehaviour
{
  
    public CyrusWall myCyrus;
    public GameObject myPeluca;
    public SceneController mySceneC;
    private void Awake()
    {
        if (myCyrus == null) myCyrus = GetComponentInChildren<CyrusWall>();
        if (mySceneC == null) mySceneC = FindObjectOfType<SceneController>();
        if (myCyrus != null) myCyrus.gameObject.SetActive(false);


    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {

            SoundManager.PlaySound(SoundManager.Sound.ELECTRICSOUND, 1f);
            ChangePhase();

        }
    }

    private void ChangePhase()
    {
        //ChangePhase
        myPeluca.SetActive(false);
        myCyrus.gameObject.SetActive(true);
        myCyrus.canAI = true;
        myCyrus.shield = false;
        myCyrus.gameObject.transform.parent = null;
        myCyrus.transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);


    }
}


