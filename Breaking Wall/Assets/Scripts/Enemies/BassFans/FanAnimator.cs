using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanAnimator : MonoBehaviour
{

    // Start is called before the first frame update
    public Animator anim;
    private FanScript myFan;

    private void Awake()
    {
        if (myFan == null) myFan = GetComponentInChildren<FanScript>();

    }


    // Update is called once per frame
    void Update()
    {
        if (myFan.myRb.velocity.x != 0f || myFan.myRb.velocity.z != 0f)
        {

            anim.SetBool("Running", true);

        }
        else anim.SetBool("Running", false);

        if (myFan.attackAnim)
        {

            anim.SetBool("Hitting", true);

        }
        else anim.SetBool("Hitting", false);


        if (myFan.currentCombatState == (int)PlayerController.CombatState.HIT)
        {

            anim.SetBool("Hit", true);

        }
        else anim.SetBool("Hit", false);
    }

}
