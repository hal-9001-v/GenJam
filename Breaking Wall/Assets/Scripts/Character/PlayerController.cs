using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //GOs
    PlayerControls myPlayerControls;
    private Rigidbody myRb;
     
    //MovementVals
    private Vector2 moveInput;
    
    //ControlVars
    private bool jump;
    public bool isGrounded;
    public int currentState;
    //State
    public enum State
    {
        GROUNDED = 0,
        JUMPING = 1,
        DIVING = 2,
    }
    
    private void Awake() {
        
        myPlayerControls = new PlayerControls();
        myRb = GetComponent<Rigidbody>();
        
    }

    
    
    // Start is called before the first frame update
    void Start()
    {
        setPlayerControls(myPlayerControls);
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded(isGrounded);
    }

    private void OnTriggerStay(Collider other)
    {

        Debug.Log(other.name);
        if (other.tag == "Ground")
        {
            isGrounded = true;
            Debug.Log("Grounded");
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name);
        if (other.tag == "Ground")
        {
            isGrounded = false;
            Debug.Log("Not Grounded");
        }
     
    }

    private void CheckGrounded(bool grounded) {

        if (grounded) { currentState = (int)State.GROUNDED; } else currentState = (int)State.JUMPING;


    }

    private void Jump(){

        Debug.Log("Jumped!");
        if(currentState == (int) State.GROUNDED) myRb.velocity = new Vector3(0, 14, 0);
        currentState = (int)State.JUMPING;
        isGrounded = false;

    }

    private void Dive(){

        Debug.Log("Dove!");

    }

    private void OnEnable()
    {
        myPlayerControls.Enable();
    }
    
    private void OnDisable()
    {
        myPlayerControls.Disable();
    }

    
   public void setPlayerControls(PlayerControls pc){

        pc.DefaultActionMap.Movement.performed += ctx => ctx.ReadValue<Vector2>();

        pc.DefaultActionMap.Movement.canceled += ctx => ctx.ReadValue<Vector2>();
        
        pc.DefaultActionMap.Jump.performed += ctx => Jump();

        pc.DefaultActionMap.Dive.performed += ctx => Dive();
        

    }

}
