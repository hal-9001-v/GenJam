using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyalAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    private BassGyalScript myWall;
    private void Awake()
    {
        if (myWall == null) myWall = FindObjectOfType<BassGyalScript>();

    }


    // Update is called once per frame
    void Update()
    {
        if (myWall.moveInput != Vector2.zero)
        {

            anim.SetBool("Running", true);

        }
        else anim.SetBool("Running", false);

        if (myWall.isGrounded)
        {

            anim.SetBool("Grounded", true);

        }
        else anim.SetBool("Grounded", false);

        if (myWall.hitting)
        {

            anim.SetBool("Hitting", true);

        }
        else anim.SetBool("Hitting", false);


        if (myWall.shooting)
        {

            anim.SetBool("Shoot", true);

        }
        else anim.SetBool("Shoot", false);



        if (myWall.isHit)
        {

            anim.SetBool("Hit", true);

        }
        else anim.SetBool("Hit", false);
    }
}
