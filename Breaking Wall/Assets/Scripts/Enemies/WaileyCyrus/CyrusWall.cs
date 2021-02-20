using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyrusWall : MonoBehaviour
{
    //GOs
    private Rigidbody myRb; //My Rigidbody
    public GameObject myCucho; //Meele hit
    //MovementVals
    private Vector2 moveInput; //Input Vector corresponding to WASD or JoyStick input

    //ControlVars
    private bool isGrounded;
    public int currentState;
    public int currentCombatState;
    private bool hitting;
    private bool takeDmg;
    public bool shield = true;
    public bool canAI;

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
    private bool busy;
    private bool jattacking;

    private HUDRenderer myHudRenderer;
    //State
    public enum State
    {
        GROUNDED = 0,
        JUMPING = 1,
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
        if (myHudRenderer== null) myHudRenderer= FindObjectOfType<HUDRenderer>();
        myCucho.GetComponent<Collider>().gameObject.SetActive(false);
        if (myPlayer == null) myPlayer = FindObjectOfType<PlayerController>();
        
        //Variable Initialization
        movementSpeed = 50f;
        groundMovementSpeed = 7.5f;
        airMovementSpeed = groundMovementSpeed / 2;
        jumpForce = 10f;
        hp = 10; myHudRenderer.InitBossHudHealth(hp);

        inmunity = 0.2f;
        canAI = false;
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
       if(canAI) ManageAI();

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

        if (!(currentCombatState == (int)CombatState.HIT) && !busy)
        {
            myRb.velocity = new Vector3(M.x * movementSpeed, myRb.velocity.y, M.y * movementSpeed);
            Quaternion prevRotation = transform.rotation;
            Quaternion currentRot = Quaternion.LookRotation((currentPlayerPos - transform.position).normalized);
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
            myCucho.GetComponent<Collider>().gameObject.SetActive(true);

            yield return new WaitForSeconds(0.6f);

            myCucho.GetComponent<Collider>().gameObject.SetActive(false);
            movementSpeed = groundMovementSpeed;
            hitting = false;

        }
    }


    public void ManageAI()
    {
        if (hp <= 0) Destroy(gameObject);
        
        currentPos = gameObject.transform.position;
        currentPlayerPos = myPlayer.transform.position;
        distanceToPlayer = Vector3.Distance(myPlayer.transform.position, currentPos);
        Vector3 direction = currentPlayerPos - currentPos;

        if (distanceToPlayer > 5.0 && !busy && distanceToPlayer < 50.0)
        {
            if (!(currentCombatState == (int)CombatState.HIT)) moveInput = new Vector2(direction.normalized.x, direction.normalized.z);

            int i = Random.Range(1, 2000);
            if (i == 3)
            {
                if (!jattacking) StartCoroutine(Frenzy(direction));
            }
            if (i == 432 || i == 4)
            {
                {
                    if (!jattacking) StartCoroutine(JumpAttack(direction));

                }
            }
        }
        else if (distanceToPlayer < 5.0)
        {

            if (!jattacking) StartCoroutine(Frenzy(direction));
        }
    }


    
    private IEnumerator JumpAttack(Vector3 direction)
    {
        jattacking = true;
        yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));
        Jump();
        moveInput = new Vector2(direction.normalized.x, direction.normalized.z) * 2;
        yield return new WaitForSeconds(2f);
        Hit();
        busy = true;
        moveInput = new Vector2(-direction.normalized.x, -direction.normalized.z);
        yield return new WaitForSeconds(2f);
        busy = false;
        jattacking = false;

    }
    private IEnumerator Frenzy(Vector3 direction)
    {
        jattacking = true;
        jumpForce = 5;
        float t = 0.6f;
        yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));
        Jump();
        moveInput = new Vector2(direction.normalized.x, direction.normalized.z) * 2;
        yield return new WaitForSeconds(t+0.1f);
        Hit();
        busy = true;
        moveInput = new Vector2(-direction.normalized.x, -direction.normalized.z);
        yield return new WaitForSeconds(t);
        Jump();
        moveInput = new Vector2(direction.normalized.x, direction.normalized.z) * 2;
        yield return new WaitForSeconds(t+0.1f);
        Hit();
        busy = true;
        moveInput = new Vector2(-direction.normalized.x, -direction.normalized.z);
        yield return new WaitForSeconds(t);
        busy = false; Jump();
        moveInput = new Vector2(direction.normalized.x, direction.normalized.z) * 2;
        yield return new WaitForSeconds(t+0.1f);
        Hit();
        busy = true;
        moveInput = new Vector2(-direction.normalized.x, -direction.normalized.z);
        yield return new WaitForSeconds(2);
        busy = false;
        jattacking = false;
        jumpForce = 10;

    }



    private void TakeDamage()
    {
        if (!shield)
        {
            busy = true;
            currentCombatState = (int)CombatState.HIT;
            hp--;
            if (hp <= 0) {
                StartCoroutine(Die());
            }
            myHudRenderer.SetBossHudHealth(hp);
            Vector3 direction = (myPlayer.transform.position - transform.position).normalized;
            myRb.velocity = new Vector3(-direction.x * 10, 3, -direction.z * 10);
        }

    }
    private IEnumerator Die()
    {

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
        yield return new WaitForSeconds(2f);
        currentCombatState = (int)CombatState.HITTING;
        busy = false;
    }
}
