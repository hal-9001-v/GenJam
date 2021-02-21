using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") {

            SoundManager.PlaySound(SoundManager.Sound.BANGSOUND, 0.2f);

        }
    }
}
