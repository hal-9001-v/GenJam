using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonScript : MonoBehaviour
{

    public WallDaLuciaScript myWall;

    private void Awake()
    {
        myWall = FindObjectOfType<WallDaLuciaScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bolso") {

            myWall.hp--;
            Debug.Log("Pipo");
            Destroy(gameObject);

        }
    }

}
