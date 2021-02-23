using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocineroAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    private EnemyCocinero myCocinero;

    private void Awake()
    {
        if (myCocinero == null) myCocinero = GetComponentInChildren<EnemyCocinero>();

    }


    // Update is called once per frame
    void Update()
    {
        if (myCocinero.myRb.velocity.x != 0f || myCocinero.myRb.velocity.z != 0f)
        {

            anim.SetBool("Running", true);

        }
        else anim.SetBool("Running", false);

        if (myCocinero.isGrounded)
        {

            anim.SetBool("Grounded", true);

        }
        else anim.SetBool("Grounded", false);


        if (myCocinero.currentCombatState == (int)PlayerController.CombatState.HIT)
        {

            anim.SetBool("Hit", true);

        }
        else anim.SetBool("Hit", false);
    }
}
