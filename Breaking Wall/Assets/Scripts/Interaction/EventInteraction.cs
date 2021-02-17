using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventInteraction : InputComponent
{

    [Range(0, 20)]
    public float range;

    public Color gizmosColor;

    static Transform playerTransform;

    public UnityEvent events;

    Renderer myRenderer;

    Collider myCollider;

    public bool onlyOnce;

    public bool hideWhenDone;

    bool done;


    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("No player in Scene!");
        }

        myRenderer = GetComponent<Renderer>();
        myCollider = GetComponent<Collider>();

    }


    public override void setPlayerControls()
    {
        triggerEvent();
    }

    private void triggerEvent()
    {
        if (Vector3.Distance(playerTransform.position, transform.position) <= range)
        {
            if (done && onlyOnce)
                return;

            events.Invoke();
            done = true;

            if (hideWhenDone){
                if (myRenderer != null) {
                    
                    myRenderer.enabled = false;
                    myCollider.enabled = false;

                }
            }


        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
