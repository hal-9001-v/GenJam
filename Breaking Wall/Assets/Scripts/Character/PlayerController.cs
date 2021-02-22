using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //GOs
    PlayerControls myPlayerControls; //Player Input Action Asset
    public Rigidbody myRb; //My Rigidbody
    //private PlayerStats ps; //My player stats
    private GameObject myBolso; //Meele hit
    private GameObject myDiveHit; //Meele hit

    public Transform bodyTransform;
    public Transform followTransform;

    public GameObject[] pelucas;

    //MovementVals
    private Vector2 moveInput; //Input Vector corresponding to WASD or JoyStick input

    //ControlVars
    public bool isGrounded;
    public int currentState;
    public int currentCombatState;
    private bool moving; //Is moving?
    public bool hitting;
    private bool takeDmg;
    public bool hasVirote;
    public bool ballestaLoaded;
    private bool canArrowInteract;
    private bool canBallestaInteract;
    //Player Stats
    private float movementSpeed; //Actual Movement Speed
    private float airMovementSpeed; //Movement  Speed When airborne
    private float groundMovementSpeed; //Movement speed when on ground, also launching speed
    private float jumpForce; //pretty self explanatory, really
    public int hp; //Life points
    //private int level; //Actual level (scene)
    private float inmunity;


    bool canMove = true;

    //Hud
    HUDRenderer myHud;

    //State
    public enum State
    {
        GROUNDED = 0,
        JUMPING = 1,
        DIVING = 2,

    }
    public enum CombatState
    {
        HITTING = 0,
        HIT = 1,

    }

    //Animator
    public bool isMoving;
    public bool diving;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //Gos
        if (myPlayerControls == null) myPlayerControls = new PlayerControls();
        if (myRb == null) myRb = GetComponent<Rigidbody>();
        //if (ps == null) ps = FindObjectOfType<PlayerStats>();
        if (myBolso == null) myBolso = GameObject.Find("Bolso");
        if (myDiveHit == null) myDiveHit = GameObject.Find("DiveHit");
        if (myHud== null) myHud= FindObjectOfType<HUDRenderer>();
        myBolso.GetComponent<Collider>().gameObject.SetActive(false);
        myDiveHit.GetComponent<Collider>().gameObject.SetActive(false);

        //Variable Initialization
        movementSpeed = 10f;
        groundMovementSpeed = 10f;
        airMovementSpeed = groundMovementSpeed / 2;
        jumpForce = 6f;
        hp = 10;
        inmunity = 0.5f;
        hasVirote = false;
        ballestaLoaded = false;
        canBallestaInteract = false;
        canArrowInteract = false;
        /*
        //playerStats business
        hp = ps.hp;
        level = ps.level;
        */
        foreach (InputComponent ic in FindObjectsOfType<InputComponent>())
        {
            ic.setPlayerControls(myPlayerControls);
        }

    }

    /*
    //PlayerStatsUpdate
    private void UpdatePlayerStats()
    {
        ps.level = level;
        ps.hp = hp;
    }
    */
    //Load pertinent level
    private void nextLevel()
    {
        //level++;
        //UpdatePlayerStats();
        Debug.Log("Loading Scene correspondiente");

    }



    // Start is called before the first frame update
    void Start()
    {
        setPlayerControls(myPlayerControls);
        myHud.UpdateHUD();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove) {
            CheckGrounded(isGrounded);
            UpdateMovement(moveInput);
        }
    }

    public void enableMove() {
        canMove = true;
    }
    public void disableMove() {
        canMove = false;
    }


    //Check if player grounded
    private void OnTriggerStay(Collider col)
    {

        if (col.tag == "Ground")
        {
            isGrounded = true;
        }


        if (col.tag == "Platform")
        {
            isGrounded = true;
            gameObject.transform.parent = col.transform;
        }

        
        if (col.tag == "Arrows") {

            canArrowInteract = true;

        }

        if (col.tag == "Ballesta")
        {

            canBallestaInteract = true;


        }

        
    }

    //Check when player leaves ground
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            isGrounded = false;
        }

        if (other.tag == "Platform")
        {
            isGrounded = false;
            gameObject.transform.parent = null;
        }
        if (other.tag == "Arrows")
        {
            canArrowInteract = false;
        }
        if (other.tag == "Ballesta")
        {
            canBallestaInteract = false;
        }
    }

    private void UpdateMovement(Vector2 M)
    {

        const float lerpFactor = 5;
        //If not diving, velocity is calculated normally
        if (currentState != (int)State.DIVING)
        {
            if (!(currentCombatState == (int)CombatState.HIT))
            {
                Vector3 v = new Vector3();
                Vector3 v2;

                //v += followTransform.forward * movementSpeed * M.y;
                //v += followTransform.right * movementSpeed * M.x;

                v2 = followTransform.forward;
                v2.y = 0;

                v2.Normalize();
                v2 *= movementSpeed * M.y;

                v += v2;

                v2 = followTransform.right;
                v2.y = 0;

                v2.Normalize();
                v2 *= movementSpeed * M.x;

                v += v2;

                v.y = myRb.velocity.y;

                myRb.velocity = v;

            }

            //If moving, calculate rotation lerping current rotation with previous.
            if (moving)
            {

                Quaternion prevRotation = bodyTransform.transform.rotation;
                Quaternion actualRot = Quaternion.LookRotation(myRb.velocity);

                var rot =  Quaternion.Lerp(prevRotation, actualRot, Time.deltaTime * lerpFactor).eulerAngles;

                rot.z = 0;
                rot.x = 0;
                bodyTransform.transform.eulerAngles = rot;
            }
        }
    }

    private void CheckGrounded(bool grounded)
    {
        //If grounded -> grounded state
        //If not grounded and was in grounded state, jump.

        if (grounded)
        {
            myDiveHit.GetComponent<Collider>().gameObject.SetActive(false);
            currentState = (int)State.GROUNDED;
            if (!hitting) movementSpeed = groundMovementSpeed;
            //Debug.Log("¡Entering Grounded State!");
        }
        else if (currentState == (int)State.GROUNDED)
        {
            if (!hitting) movementSpeed = airMovementSpeed;
            currentState = (int)State.JUMPING;
            // Debug.Log("¡Entering Jump State!");
        }

    }

    private void Jump()
    {
        //If grounded -> Jump and enter jump state.

        if (currentState == (int)State.GROUNDED && !hitting)
        {
            movementSpeed = airMovementSpeed;
            myRb.velocity = new Vector3(myRb.velocity.x, jumpForce, myRb.velocity.z);
            currentState = (int)State.JUMPING;
            //Debug.Log("¡Entering Jump State!");
            isGrounded = false;
            SoundManager.PlaySound(SoundManager.Sound.WWJUMP, 0.3F);
        }

    }

    private void Dive()
    {


        //If jumping -> Dive and enter dive state
        if (currentState == (int)State.JUMPING)
        {
            SoundManager.PlaySound(SoundManager.Sound.WWISHIT2, 0.4f);
            diving = true;
            StartCoroutine(DiveRoutine());
            currentState = (int)State.DIVING;
            //Debug.Log("¡Entering Diving State!");

        }


    }

    private IEnumerator DiveRoutine()
    {
        //Stop in the air, then lunge forward.
        myRb.velocity = new Vector3(myRb.velocity.x * 0.1f, 0, myRb.velocity.z * 0.1f);
        myDiveHit.GetComponent<Collider>().gameObject.SetActive(true);
        takeDmg = true;
        yield return new WaitForSeconds(0.15f);
        myRb.velocity = new Vector3(bodyTransform.forward.x * 2, bodyTransform.forward.y - 0.4f, bodyTransform.forward.z * 2) * groundMovementSpeed;
        yield return new WaitForSeconds(0.3f);
        groundMovementSpeed = 1f;
        yield return new WaitForSeconds(1f);
        groundMovementSpeed = 10f;
        takeDmg = false;
        yield return new WaitForSeconds(0.4f);
        diving = false;

    }

    private void Hit()
    {

        //HitAnim
        if (currentState == (int)State.GROUNDED)
        {
            StartCoroutine(HitCourutine());
        }

    }


    private IEnumerator HitCourutine()
    {

        if (!hitting && !diving)
        {
            hitting = true;
            movementSpeed = airMovementSpeed * 0.25f;
            yield return new WaitForSeconds(0.3f);
            SoundManager.PlaySound(SoundManager.Sound.WWHITS, 0.4f);
            SoundManager.PlaySound(SoundManager.Sound.SWINGSPUNCH, 0.4f);
            yield return new WaitForSeconds(0.1f);
            myBolso.GetComponent<Collider>().gameObject.SetActive(true);
            yield return new WaitForSeconds(0.8f);
            myBolso.GetComponent<Collider>().gameObject.SetActive(false);
            movementSpeed = groundMovementSpeed;
            hitting = false;
        }
    }

    private void OnTriggerEnter(Collider col)
    {


        int force;
        if (col.gameObject.tag == "Cucho")
        {
            force = 10;
            if (!takeDmg)
            {
                TakeDamage(col, force);
                StartCoroutine(Inmunity());
            }
        }
        if (col.gameObject.tag == "FanCucho")
        {
            force = 10;
            if (!takeDmg)
            {

                TakeDamage(col, force);
                StartCoroutine(Inmunity());
            }
        }


        if (col.gameObject.tag == "Micro")
        {
            force = 10;
            if (!takeDmg)
            {

                TakeDamage(col, force);
                StartCoroutine(Inmunity());
            }
        }


    }

    private void TakeDamage(Collider col, int force)
    {
        
        currentCombatState = (int)CombatState.HIT;
        hp--;
        Instantiate(GameAssets.i.particles[10], gameObject.transform.position, gameObject.transform.rotation);

        SoundManager.PlaySound(SoundManager.Sound.WWISHIT, 0.4f);
        if (hp <= 0) {

            StartCoroutine(Die());

        }
        myHud.UpdateHUD();
        Vector3 direction = (transform.position - col.transform.position).normalized;
        myRb.AddRelativeForce(new Vector3((direction.x+0.1f) * force, 3, (direction.z+0.1f) * force), ForceMode.VelocityChange);
    }

    private IEnumerator Die() {
        
        SoundManager.PlaySound(SoundManager.Sound.WWDIES2, 0.2f);
        yield return new WaitForSeconds(1f);
        
        SoundManager.PlaySound(SoundManager.Sound.WWDIES, 0.2f);
        Debug.Log("Is dead");

    }
    private IEnumerator Inmunity()
    {
        currentCombatState = (int)CombatState.HIT;
        takeDmg = true;
        yield return new WaitForSeconds(inmunity);
        takeDmg = false;
        yield return new WaitForSeconds(1.5f);
        currentCombatState = (int)CombatState.HITTING;

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

    public void Interact() {

        if (canArrowInteract && !hasVirote) {
            hasVirote = true;
            myHud.SetVirote(hasVirote);
            SoundManager.PlaySound(SoundManager.Sound.GETVIROTE, 1.5f);
        }

        if (canBallestaInteract && !ballestaLoaded && hasVirote)
        {
          ballestaLoaded = true;
          myHud.SetVirote(false);
        }

    }

    public void setPlayerControls(PlayerControls pc)
    {
        //Callback contexts to bind player actions.
        pc.DefaultActionMap.Movement.performed += ctx => { moveInput = ctx.ReadValue<Vector2>(); moving = true; };

        pc.DefaultActionMap.Movement.canceled += ctx => { moveInput = Vector2.zero; moving = false; };

        pc.DefaultActionMap.Jump.performed += ctx => Jump();

        pc.DefaultActionMap.Dive.performed += ctx => Dive();

        pc.DefaultActionMap.Hit.performed += ctx => Hit();

        pc.DefaultActionMap.Interaction.performed += ctx => Interact();

    }

}
