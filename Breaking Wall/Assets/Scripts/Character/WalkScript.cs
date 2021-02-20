using UnityEngine;
using System.Collections;

public class WalkScript : MonoBehaviour
{

    // Use this for initialization
    PlayerController pc;
    AudioSource aso;
    void Start()
    {
        if(aso == null) aso = GetComponent<AudioSource>();
        if(pc == null) pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.isGrounded == true && pc.GetComponent<Rigidbody>().velocity.magnitude > 2f && aso.isPlaying == false)
        {
            aso.volume = Random.Range(0.8f, 1f);
            aso.pitch= Random.Range(0.8f, 1.1f);
            aso.Play();
        }
    }
}