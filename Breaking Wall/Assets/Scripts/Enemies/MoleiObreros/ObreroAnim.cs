using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObreroAnim : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    private ObreroScript myObrero;

    private void Awake()
    {
        if (myObrero == null) myObrero = GetComponentInChildren<ObreroScript>();

    }


    // Update is called once per frame
    void Update()
    {
        if (myObrero.myRb.velocity.x != 0f || myObrero.myRb.velocity.z != 0f)
        {

            anim.SetBool("Running", true);

        }
        else anim.SetBool("Running", false);

        if (myObrero.shotAnim)
        {

            anim.SetBool("Shoot", true);

        }
        else anim.SetBool("Shoot", false);



        if (myObrero.currentCombatState == (int)PlayerController.CombatState.HIT)
        {

            anim.SetBool("Hit", true);

        }
        else anim.SetBool("Hit", false);
    }
}
