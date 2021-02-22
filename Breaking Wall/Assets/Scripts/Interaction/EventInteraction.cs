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

    public Transform iconPosition;
    public GameObject iconPrefab;
    GameObject icon;

    [Range(0.1f, 5)]
    public float rotationSpeed = 0.1f;

    public UnityEvent events;

    Renderer myRenderer;

    Collider myCollider;

    public bool onlyOnce;

    public bool hideWhenDone;

    public bool readyForInteraction = true;

    bool done;

    bool inRange;


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

        if (iconPrefab != null && iconPosition != null)
        {

            icon = Instantiate(iconPrefab);
            icon.transform.position = iconPosition.position;

            icon.transform.parent = transform;

        }

        myRenderer = GetComponent<Renderer>();
        myCollider = GetComponent<Collider>();

    }

    private void FixedUpdate()
    {
        if (playerTransform != null && readyForInteraction && !(done && onlyOnce))
        {
            if (Vector3.Distance(playerTransform.position, transform.position) <= range)
            {
                inRange = true;

                rotateIcon();

            }
            else
            {
                inRange = false;
                hideIcon();

            }
        }
        else {
            hideIcon();
        }


    }

    void rotateIcon() {

        if (icon != null)
        {

            if (readyForInteraction && !(onlyOnce && done))
            {
                icon.SetActive(true);
                var v = icon.transform.eulerAngles;

                v.y += rotationSpeed;

                icon.transform.eulerAngles = v;
            }

        }
    }

    void hideIcon() {
        if (icon != null)
        {
            icon.SetActive(false);

        }
    }

    public override void setPlayerControls(PlayerControls pc)
    {

        pc.DefaultActionMap.Interaction.performed += ctx => triggerEvent();

    }

    private void triggerEvent()
    {

        if (inRange && readyForInteraction)
        {
            //Debug.Log("Interaction");
            if (done && onlyOnce)
                return;

            events.Invoke();
            done = true;

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





    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
