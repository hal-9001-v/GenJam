using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallestaScript : MonoBehaviour
{
    public ViroteScript myVirote;
    public GameObject myObjective;
    Vector3 direction;
    Rigidbody myViroteRb;
    bool canShoot;
    PlayerController myPlayer;
    private void Awake()
    {
        if (myPlayer == null) myPlayer = FindObjectOfType<PlayerController>();
        if (myViroteRb == null) myViroteRb = myVirote.GetComponent<Rigidbody>();
        myVirote.gameObject.SetActive(false);
        canShoot = false;

    }
    private void Start()
    {
         direction = myObjective.transform.position - transform.position;
        myVirote.transform.rotation = Quaternion.LookRotation(new Vector3(direction.normalized.x * 40, direction.normalized.y * 30, direction.normalized.z * 40), myVirote.transform.up);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Bolso" && canShoot) {
            Quaternion lol = Quaternion.Euler(0, -90, 0);
            Instantiate(GameAssets.i.particles[8], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+1 , gameObject.transform.position.z), lol) ;
            SoundManager.PlaySound(SoundManager.Sound.BALLISTALAUNCH, 0.5f);
            myVirote.falling = true;
            myViroteRb.isKinematic = false;
            myViroteRb.constraints = RigidbodyConstraints.None;
            myViroteRb.velocity = new Vector3(direction.normalized.x*40, direction.normalized.y*30, direction.normalized.z * 40);
            myVirote.transform.rotation = Quaternion.LookRotation(myViroteRb.velocity, myVirote.transform.up);
            canShoot = false;
        }

    }

    private void Update()
    {
        if (myPlayer.ballestaLoaded) {
            LoadBallesta();
        }
        if (myVirote.falling)
        {
            myVirote.transform.forward =
            Vector3.Slerp(myVirote.transform.forward, new Vector3(myViroteRb.velocity.normalized.x, myViroteRb.velocity.normalized.y-0.2f, myViroteRb.velocity.normalized.z), Time.deltaTime);
        }
    }

    private void LoadBallesta()
    {
        Start();
        SoundManager.PlaySound(SoundManager.Sound.LOADBALLISTA, 0.5f);
        myVirote.transform.localPosition = new Vector3(0, -2.1f, 0); 
        myVirote.gameObject.SetActive(true);
        myViroteRb.isKinematic = true;
        myViroteRb.constraints = RigidbodyConstraints.FreezeAll;
        canShoot = true;
        myPlayer.hasVirote= false;
        myPlayer.ballestaLoaded = false;
    }
}
