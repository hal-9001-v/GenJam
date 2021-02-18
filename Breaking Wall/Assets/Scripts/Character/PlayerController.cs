using Cinemachine;
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
    public Vector2 angle;

    public Transform cameraFollower;
    CinemachineImpulseSource source;


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

    private void Awake()
    {

        myPlayerControls = new PlayerControls();
        myRb = GetComponent<Rigidbody>();

        activateInput();

        source = GetComponent<CinemachineImpulseSource>();
    }

    //Enable input on components
    void activateInput()
    {
        foreach (InputComponent ic in FindObjectsOfType<InputComponent>())
        {
            ic.setPlayerControls(myPlayerControls);
        }
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

    private void CheckGrounded(bool grounded)
    {

        if (grounded) { currentState = (int)State.GROUNDED; } else currentState = (int)State.JUMPING;


    }

    private void Jump()
    {

        Debug.Log("Jumped!");
        if (currentState == (int)State.GROUNDED) myRb.velocity = new Vector3(0, 14, 0);
        currentState = (int)State.JUMPING;
        isGrounded = false;

    }


    private void OnEnable()
    {
        myPlayerControls.Enable();
    }

    private void OnDisable()
    {
        myPlayerControls.Disable();
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + moveInput.x, transform.position.y, transform.position.z + moveInput.y);

        cameraFollower.transform.rotation *= Quaternion.AngleAxis(angle.x * 1, Vector3.up);

        var v = cameraFollower.transform.eulerAngles;

        cameraFollower.transform.rotation *= Quaternion.AngleAxis(angle.y * 1, Vector3.right);



    }

    public void setPlayerControls(PlayerControls pc)
    {

        pc.DefaultActionMap.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>() * 0.25f;

        pc.DefaultActionMap.Movement.canceled += ctx => moveInput = Vector2.zero;

        pc.DefaultActionMap.Aim.performed += ctx => angle = ctx.ReadValue<Vector2>();
        pc.DefaultActionMap.Aim.canceled += ctx => angle = Vector2.zero;

        pc.DefaultActionMap.Jump.performed += ctx => { 
            source.GenerateImpulse();
            Debug.Log("OI oI");
        };

    }

}
