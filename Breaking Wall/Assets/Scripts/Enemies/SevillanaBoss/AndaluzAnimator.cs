using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndaluzAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    private WallDaLuciaScript myWall;

    private void Awake()
    {
        if (myWall == null) myWall = FindObjectOfType<WallDaLuciaScript>();

    }


    // Update is called once per frame
    void Update()
    {




        if (myWall.isHit)
        {

            anim.SetBool("Hit", true);

        }
        else anim.SetBool("Hit", false);

        if (myWall.sleeping)
        {

            anim.SetBool("Sleep", true);

        }
        else anim.SetBool("Sleep", false);
    }
}
