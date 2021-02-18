using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //GOs
    PlayerControls myPlayerControls; //Player Input Action Asset
    private Rigidbody myRb; //My Rigidbody
    private PlayerStats ps; //My player stats
    private GameObject myBolso; //Meele hit
    public Light controlLight;
    //MovementVals
    private Vector2 moveInput; //Input Vector corresponding to WASD or JoyStick input

    //ControlVars
    private bool isGrounded;
    private int currentState; 
    private bool moving; //Is moving?
    private bool hitting; 
    //Player Stats
    private float movementSpeed; //Actual Movement Speed
    private float airMovementSpeed; //Movement  Speed When airborne
    private float groundMovementSpeed; //Movement speed when on ground, also launching speed
    private float jumpForce; //pretty self explanatory, really
    private int hp; //Life points
    private int level; //Actual level (scene)
    //State
    public enum State
    {
        GROUNDED = 0,
        JUMPING = 1,
        DIVING = 2,
    }
    
    private void Awake() {
        //Gos
        myPlayerControls = new PlayerControls();
        if(myRb == null) myRb = GetComponent<Rigidbody>();
        if (ps == null) ps = FindObjectOfType<PlayerStats>();
        if (myBolso == null) myBolso = GameObject.Find("Bolso");
        myBolso.GetComponent<Collider>().gameObject.SetActive(false);
        //Variable Initialization
        movementSpeed = 10f;
        groundMovementSpeed = 10f;
        airMovementSpeed = groundMovementSpeed / 2;
        jumpForce = 5f;
        hp = 10;


        //playerStats business
        hp = ps.hp;
        level = ps.level;
    }

    //PlayerStatsUpdate
    private void UpdatePlayerStats()
    {
        ps.level = level;
        ps.hp = hp;
    }

    //Load pertinent level
    private void nextLevel()
    {
        level++;
        UpdatePlayerStats();
        Debug.Log("Loading Scene correspondiente");

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
        UpdateMovement(moveInput);
    }


    //Check if player grounded
    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Ground")
        {
            isGrounded = true;
        }
        
    }

    //Check when player leaves ground
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            isGrounded = false;
        }
     
    }

    private void UpdateMovement(Vector2 M) {


        //If not diving, velocity is calculated normally
        if (currentState != (int)State.DIVING)
        {
            myRb.velocity = new Vector3(M.x * movementSpeed, myRb.velocity.y, M.y * movementSpeed);

            //If moving, calculate rotation lerping current rotation with previous.
            if (moving)
            {
               Quaternion prevRotation = transform.rotation;
               Quaternion actualRot = Quaternion.LookRotation(myRb.velocity);
               transform.rotation =Quaternion.Euler(transform.rotation.x,Vector3.Lerp(prevRotation.eulerAngles, actualRot.eulerAngles, 0.5f).y, transform.rotation.z);
                
            }
        }
    }

    private void CheckGrounded(bool grounded) {
        //If grounded -> grounded state
        //If not grounded and was in grounded state, jump.
        if (grounded) { 
            currentState = (int)State.GROUNDED;
            if(!hitting) controlLight.color = Color.white;
            //Debug.Log("¡Entering Grounded State!");
        }
        else if (currentState == (int)State.GROUNDED) {
            movementSpeed = groundMovementSpeed;
            currentState = (int)State.JUMPING;
           // Debug.Log("¡Entering Jump State!");
        }


    }

    private void Jump(){

        //If grounded -> Jump and enter jump state.

        if (currentState == (int)State.GROUNDED)
        {
            movementSpeed = airMovementSpeed;
            myRb.velocity = new Vector3(myRb.velocity.x, jumpForce, myRb.velocity.z);
            currentState = (int)State.JUMPING;
            controlLight.color = Color.green;
            //Debug.Log("¡Entering Jump State!");
            isGrounded = false;
        }

    }

    private void Dive(){

        //If jumping -> Dive and enter dive state
        if (currentState == (int)State.JUMPING) {
            StartCoroutine(DiveRoutine());
            currentState = (int)State.DIVING;
            controlLight.color = Color.red;
            //Debug.Log("¡Entering Diving State!");
        }

    }

    private IEnumerator DiveRoutine() {
        //Stop in the air, then lunge forward.
        myRb.velocity = new Vector3(myRb.velocity.x*0.1f, 0, myRb.velocity.z*0.1f);
        yield return new WaitForSeconds(0.15f);
        myRb.velocity = new Vector3(transform.forward.x*2, transform.forward.y - 0.4f, transform.forward.z*2) * groundMovementSpeed;

    }

    private void Hit() {

        //HitAnim
        if (currentState == (int)State.GROUNDED) {
            Debug.Log("Hit");
            StartCoroutine(HitCourutine());
        }

    }


    private IEnumerator HitCourutine() {

        if (!hitting)
        {
            hitting = true;
            controlLight.color = Color.blue;
            movementSpeed = airMovementSpeed * 0.5f;
            myBolso.GetComponent<Collider>().gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            myBolso.GetComponent<Collider>().gameObject.SetActive(false);
            movementSpeed = groundMovementSpeed;
            controlLight.color = Color.white;
            hitting = false;
        }
    }

    //Enable player controls
    private void OnEnable()
    {
        myPlayerControls.Enable();
    }
    
    private void OnDisable()
    {
        myPlayerControls.Disable();
    }

    
   public void setPlayerControls(PlayerControls pc){
        //Callback contexts to bind player actions.
        pc.DefaultActionMap.Movement.performed += ctx => { moveInput = ctx.ReadValue<Vector2>(); moving = true; };

        pc.DefaultActionMap.Movement.canceled += ctx => { moveInput = Vector2.zero; moving = false; };
        
        pc.DefaultActionMap.Jump.performed += ctx => Jump();

        pc.DefaultActionMap.Dive.performed += ctx => Dive();

        pc.DefaultActionMap.Hit.performed += ctx => Hit();


    }

}
