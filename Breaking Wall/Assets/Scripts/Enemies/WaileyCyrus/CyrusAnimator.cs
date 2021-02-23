using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyrusAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    private CyrusWall myWall;

    private void Awake()
    {
        if (myWall == null) myWall = GetComponentInChildren<CyrusWall>();

    }


    // Update is called once per frame
    void Update()
    {
       if (myWall.myRb.velocity.x != 0f || myWall.myRb.velocity.z != 0f)
        {

            anim.SetBool("Running", true);

        }
        else anim.SetBool("Running", false);

        


        if (myWall.currentCombatState == (int)PlayerController.CombatState.HIT)
        {

            anim.SetBool("Hit", true);

        }
        else anim.SetBool("Hit", false);


        if (myWall.frenzy)
        {

            anim.SetBool("Frenzy", true);

        }
        else anim.SetBool("Frenzy", false);


        if (myWall.jumpAttack)
        {

            anim.SetBool("Hitting", true);

        }
        else anim.SetBool("Hitting", false);

    }



}
