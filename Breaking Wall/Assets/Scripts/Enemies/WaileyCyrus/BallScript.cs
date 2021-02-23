using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public CyrusWall myCyrus;
    public GameObject myPeluca;
    private void Awake()
    {
        if(myCyrus == null) myCyrus = GetComponentInChildren<CyrusWall>();
        if(myCyrus !=null) myCyrus.gameObject.SetActive(false);
        

    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {

            SoundManager.PlaySound(SoundManager.Sound.ELECTRICSOUND, 1f);
            StartCoroutine(ChangePhase());

        }
    }

    private IEnumerator ChangePhase() {

        //Cinemática

        SoundManager.PlaySound(SoundManager.Sound.ELECTRICSOUND, 0.2f);
        Quaternion lol = Quaternion.Euler(-90, 0, 0);
        Instantiate(GameAssets.i.particles[0], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), lol);
        yield return new WaitForSeconds(1f);
        SoundManager.PlaySound(SoundManager.Sound.ELECTRICSOUND, 0.2f);
        Instantiate(GameAssets.i.particles[0], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), lol);
        yield return new WaitForSeconds(1f);
        SoundManager.PlaySound(SoundManager.Sound.ELECTRICSOUND, 0.2f);
        Instantiate(GameAssets.i.particles[0], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), lol);
        yield return new WaitForSeconds(1f);
        SoundManager.PlaySound(SoundManager.Sound.ELECTRICSOUND, 0.2f);
        Instantiate(GameAssets.i.particles[0], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), lol);
        yield return new WaitForSeconds(1f);
        SoundManager.PlaySound(SoundManager.Sound.ELECTRICSOUND, 0.2f);
        Instantiate(GameAssets.i.particles[0], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), lol);
        yield return new WaitForSeconds(1f);
        SoundManager.PlaySound(SoundManager.Sound.ELECTRICSOUND, 0.2f);
        Instantiate(GameAssets.i.particles[0], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), lol);
        SoundManager.PlaySound(SoundManager.Sound.ELECTRICSOUND, 0.2f);
        Instantiate(GameAssets.i.particles[0], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), lol);
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
