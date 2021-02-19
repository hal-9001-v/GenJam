using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CollisionInteraction : MonoBehaviour
{


    public UnityEvent events;

    Renderer myRenderer;

    Collider myCollider;

    public bool onlyOnce;

    public bool hideWhenDone;

    bool done;

    public bool readyForInteraction = true;


    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        myRenderer = GetComponent<Renderer>();
        myCollider = GetComponent<Collider>();
        if (myCollider != null)
        {
            myCollider.isTrigger = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (readyForInteraction)
        {
            if (other.tag == "Player")
            {
                Debug.Log("HEY YOU");
                if (done && onlyOnce)
                {
                    return;
                }

                events.Invoke();
                done = true;

                if (myCollider != null && onlyOnce)
                    myCollider.enabled = false;

                if (hideWhenDone)
                {
                    if (myRenderer != null)
                    {
                        myRenderer.enabled = false;
                    }

                    if (myCollider != null)
                    {
                        myCollider.enabled = false;
                    }

                }

            }
        }
    }


}
