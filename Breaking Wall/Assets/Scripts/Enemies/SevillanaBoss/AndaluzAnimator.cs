using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndaluzAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    private WallDaLuciaScript myWall;
    bool awoken = false;
    int currentState;
    private void Awake()
    {
        if (myWall == null) myWall = FindObjectOfType<WallDaLuciaScript>();

    }

    enum State { 
    
        HIT = 0,
        SLEEP = 1,
        AWAKE = 2,
        IDLE = 3,
    }


    private void Start()
    {
        currentState = 3;
    }
    // Update is called once per frame
    void Update()
    {


        if (myWall.isHit)
        {


            if (awoken && currentState == (int)State.AWAKE)
            {
                anim.SetBool("Hit", true);
            }

            if (!awoken && currentState == (int)State.IDLE) {
                anim.transform.position = new Vector3(anim.transform.position.x, anim.transform.position.y + 7, anim.transform.position.z);
                awoken = true;
                currentState = (int)State.AWAKE;
            } 


        }
        else anim.SetBool("Hit", false);

        if (myWall.sleeping)
        {
     
            anim.SetBool("Sleep", true);
            
             if (currentState == (int)State.IDLE)
            {

                currentState = (int)State.SLEEP;
                anim.transform.position = new Vector3(anim.transform.position.x, anim.transform.position.y - 7, anim.transform.position.z);


            }

        }
        else if( !myWall.sleeping )
        {
            if (currentState == (int)State.AWAKE)
            {

                currentState = (int)State.IDLE;
                anim.transform.position = new Vector3(anim.transform.position.x, anim.transform.position.y + 7, anim.transform.position.z);


            }
            
            anim.SetBool("Sleep", false);
        }
    }
}
