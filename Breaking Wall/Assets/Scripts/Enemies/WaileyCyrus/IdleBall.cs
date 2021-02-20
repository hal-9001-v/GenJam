using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBall : MonoBehaviour
{

    Rigidbody myRb;
    
    private void Awake()
    {
       if(myRb == null)  myRb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
   
}
