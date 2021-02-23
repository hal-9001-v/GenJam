using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCocinero : MonoBehaviour
{
    //GOs
    public Rigidbody myRb; //My Rigidbody
    public GameObject myCucho; //Meele hit

    //MovementVals
    private Vector2 moveInput; //Input Vector corresponding to WASD or JoyStick input

    //ControlVars
    public bool isGrounded;
    public int currentState;
    public int currentCombatState;
    public bool hitting;
    private bool takeDmg;
    private bool isIdling;
    //Player Stats
    private float movementSpeed; //Actual Movement Speed
    private float airMovementSpeed; //Movement  Speed When airborne
    private float groundMovementSpeed; //Movement speed when on ground, also launching speed
    private float jumpForce; //pretty self explanatory, really
    public int hp; //Life points
    private float inmunity;


    //AI Components
    private float distanceToPlayer;
    private PlayerController myPlayer;
    private Vector3 currentPos;
    private Vector3 currentPlayerPos;
    private bool jattacking;

    public bool cinematicIdle;
    //State
    public enum State
    {
        GROUNDED = 0,
        JUMPING = 1,
        IDLE = 2
    }

    public enum CombatState
    {
        HITTING = 0,
        HIT = 1,
        
    }

    private void Awake()
    {

        //Gos
        if (myRb == null) myRb = GetComponent<Rigidbody>();
        myCucho.GetComponent<Collider>().gameObject.SetActive(true);

        if (myPlayer == null) myPlayer = FindObjectOfType<PlayerController>();

        if (!cinematicIdle)
        {
            //Variable Initialization
            movementSpeed = 10f;
            groundMovementSpeed = 5;
            airMovementSpeed = 5;
            jumpForce = 5f;
            hp = 2;
            inmunity = 0.2f;
            isIdling = false;
        }
        else {
            enabled = false;
            StartCoroutine(Idle(transform.eulerAngles));
        }

        

    }

  


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded(isGrounded);
        UpdateMovement(moveInput);
        ManageAI();

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

    private void UpdateMovement(Vector2 M)
    {

        if (!(currentCombatState == (int)CombatState.HIT)&& !jattacking )
        {
            myRb.velocity = new Vector3(M.x * movementSpeed, myRb.velocity.y, M.y * movementSpeed);
            Quaternion prevRotation = transform.rotation;
            Quaternion currentRot = Quaternion.LookRotation((myPlayer.transform.position - transform.position).normalized);
            transform.rotation = new Quaternion(transform.rotation.x, Quaternion.Slerp(prevRotation, currentRot, 0.4f).y, transform.rotation.z, Quaternion.Slerp(prevRotation, currentRot, 0.4f).w);
        }
    }

    private void CheckGrounded(bool grounded)
    {
        //If grounded -> grounded state
        //If not grounded and was in grounded state, jump.
        if (grounded)
        {
             currentState = (int)State.GROUNDED;
        }
        else if (currentState == (int)State.GROUNDED)
        {
            movementSpeed = airMovementSpeed;
            currentState = (int)State.JUMPING;
        }


    }

    private void Jump()
    {

        //If grounded -> Jump and enter jump state.

        if (currentState == (int)State.GROUNDED)
        {
            movementSpeed = airMovementSpeed;
            myRb.velocity = new Vector3(myRb.velocity.x, jumpForce, myRb.velocity.z);
            currentState = (int)State.JUMPING;
            //Debug.Log("¡Entering Jump State!");
            isGrounded = false;
        }

    }

    

    private void Hit()
    { 
        StartCoroutine(HitCoroutine());
    }


    private IEnumerator HitCoroutine()
    {

        if (!hitting)
        {
            hitting = true;
            movementSpeed = airMovementSpeed * 0.5f;
            yield return new WaitForSeconds(0.3f);
            myCucho.gameObject.tag = "Cucho"; 

            yield return new WaitForSeconds(0.8f);

            myCucho.gameObject.tag = "Untagged";
            movementSpeed = groundMovementSpeed;
            hitting = false;
            
        }
    }

   
    public void ManageAI()
    {

         currentPos = gameObject.transform.position;
         currentPlayerPos = myPlayer.transform.position;
         distanceToPlayer = Vector3.Distance(currentPlayerPos, currentPos);


        Vector3 direction = currentPlayerPos - currentPos;

        if (distanceToPlayer > 10.0 && distanceToPlayer < 50.0)
        {
            if (!(currentCombatState == (int)CombatState.HIT)) moveInput = new Vector2(direction.normalized.x, direction.normalized.z);
        }
        else if (distanceToPlayer > 4.0 && distanceToPlayer < 10.0)
        {

            if (!(currentCombatState == (int)CombatState.HIT) && !isIdling && currentState == (int)State.GROUNDED) StartCoroutine(Idle(direction));

        }
        else if (distanceToPlayer < 4.0) {

            if (!jattacking && currentState == (int)State.GROUNDED && !(currentCombatState == (int)CombatState.HIT)) StartCoroutine(JumpAttack(direction));



        }

    }

    private IEnumerator Idle(Vector3 direction) {
        isIdling = true;
        int i = Random.Range(1, 3);
        float x = Random.Range(0, 0.5f);
        yield return new WaitForSeconds(x);
        if (i == 1)
        {
           
            moveInput = new Vector2(-direction.normalized.x, -direction.normalized.z);
        }
        else
        {
        
            moveInput = new Vector2(direction.normalized.x, direction.normalized.z);
        }
        yield return new WaitForSeconds(x+0.5f);
        moveInput = Vector2.zero;
        yield return new WaitForSeconds(0.1f);
        isIdling = false;
    }

    private IEnumerator JumpAttack(Vector3 direction)
    {
        SoundManager.PlaySound(SoundManager.Sound.COOKATTACKS, 0.3f);
        jattacking = true;
        yield return new WaitForSeconds(Random.Range(0.1f,0.4f));
        Jump();
        moveInput = new Vector2(direction.normalized.x, direction.normalized.z) ;
        yield return new WaitForSeconds(0.5f);
        moveInput = Vector2.zero;
        Hit();
        moveInput = new Vector2(-direction.normalized.x, -direction.normalized.z);
        yield return new WaitForSeconds(2f);
        jattacking = false;
        
    }



    private void TakeDamage() {
        currentCombatState = (int)CombatState.HIT;
        hp--;
        Instantiate(GameAssets.i.particles[10], gameObject.transform.position, gameObject.transform.rotation);
        SoundManager.PlaySound(SoundManager.Sound.PUNCHHITS, 0.8f);
        Vector3 direction = (myPlayer.transform.position - transform.position).normalized;
        myRb.velocity = new Vector3 (-direction.x*5,3, -direction.z*5);
        if (hp <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        SoundManager.PlaySound(SoundManager.Sound.COOKDIES, 1f);
        yield return new WaitForSeconds(inmunity + 0.2f);

        Destroy(gameObject); //Die

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bolso")
        {
            if (!takeDmg)
            {
                TakeDamage();
                StartCoroutine(Inmunity());
            }
        }
    }


    private IEnumerator Inmunity()
    {

        takeDmg = true;
        yield return new WaitForSeconds(inmunity);
        takeDmg = false;
        yield return new WaitForSeconds(1f);
        currentCombatState = (int)CombatState.HITTING;
    }


}
