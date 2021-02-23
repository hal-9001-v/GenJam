using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    private PlayerController myPlayerController;

    private void Awake()
    {
        if (myPlayerController == null) myPlayerController = FindObjectOfType<PlayerController>();
        
    }
   

    // Update is called once per frame
    void Update()
    {
        if (myPlayerController.myRb.velocity.x != 0f || myPlayerController.myRb.velocity.z != 0f)
        {

            anim.SetBool("Running", true);

        }
        else anim.SetBool("Running", false);
       
        if (myPlayerController.isGrounded)
        {

            anim.SetBool("Grounded", true);

        }
        else anim.SetBool("Grounded", false);

        if (myPlayerController.hitting)
        {

            anim.SetBool("Hitting", true);

        }
        else anim.SetBool("Hitting", false);


        if (myPlayerController.currentState == (int) PlayerController.State.DIVING)
        {

            anim.SetBool("Diving", true);

        }
        else anim.SetBool("Diving", false);



        if (myPlayerController.currentCombatState == (int)PlayerController.CombatState.HIT)
        {

            anim.SetBool("Hit", true);

        }
        else anim.SetBool("Hit", false);

        if (myPlayerController.hp <= 0) {

            anim.SetBool("Dead", true);

        } 
    }
}
