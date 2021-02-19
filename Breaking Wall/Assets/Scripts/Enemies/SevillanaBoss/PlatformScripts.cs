using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScripts : MonoBehaviour
{
    // User Inputs
    public float degreesPerSecond = 0.0f;
    public float amplitude;
    public float frequency;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Use this for initialization
    void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
        amplitude = Random.Range(1, 4);
        frequency = Random.Range(0.5f, 0.6f);
        degreesPerSecond = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}

