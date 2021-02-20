using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDaLuciaScript : MonoBehaviour
{
    //GOs
    private Rigidbody myRb; //My Rigidbody
    public GameObject mySphere;
    //MovementVals
    private Vector2 moveInput; //Input Vector corresponding to WASD or JoyStick input

    //ControlVars
    private bool isGrounded;
    public int currentState;
    public int currentCombatState;
    private bool hitting;
    private bool takeDmg;

    //Player Stats
    private float movementSpeed; //Actual Movement Speed
    private float airMovementSpeed; //Movement  Speed When airborne
    private float groundMovementSpeed; //Movement speed when on ground, also launching speed
    private float jumpForce; //pretty self explanatory, really
    public int hp; //Life points
    private float inmunity;
    private bool sleepAudio;

    //AI Components
    private float distanceToPlayer;
    private PlayerController myPlayer;
    private Vector3 currentPos;
    private Vector3 currentPlayerPos;
    private bool busy;
    private bool sleeping;
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
        if (myPlayer == null) myPlayer = FindObjectOfType<PlayerController>();
        if (myHudRenderer == null) myHudRenderer = FindObjectOfType<HUDRenderer>();

        //Variable Initialization
        movementSpeed = 10f;
        groundMovementSpeed = 5f;
        airMovementSpeed = groundMovementSpeed / 2;
        jumpForce = 5f;
        hp = 6;
        inmunity = 0.2f;
        sleeping = true;
    }




    // Start is called before the first frame update
    void Start()
    {
        myHudRenderer.InitBossHudHealth(hp);

    }

    private IEnumerator SleepAudio() {
        int i = Random.Range(4, 7);
        sleepAudio = true;
        SoundManager.PlaySound(SoundManager.Sound.SLEEPAUDIO, 4f);
        yield return new WaitForSeconds(i);
        sleepAudio = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded(isGrounded);
        UpdateMovement(moveInput);
        if(!sleepAudio && sleeping) StartCoroutine(SleepAudio());
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

        if (!(currentCombatState == (int)CombatState.HIT) && !busy && !sleeping)
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

            yield return new WaitForSeconds(0.6f);

            movementSpeed = groundMovementSpeed;
            hitting = false;

        }
    }


    public void ManageAI()
    {

        if (hp <= 2) sleeping = false;

        //Behaviour
        if (!sleeping) {

            currentPos = gameObject.transform.position;
            currentPlayerPos = myPlayer.transform.position;
            distanceToPlayer = Vector3.Distance(currentPlayerPos, currentPos);
            Vector3 direction = currentPlayerPos - currentPos;

            StartCoroutine(WallRoutine());

        }

    }

    private IEnumerator WallRoutine() {

        yield return new WaitForSeconds(1f);
       if(mySphere != null)  Destroy(mySphere);

    }

    public void TakeDamage()
    {
        busy = true;
        currentCombatState = (int)CombatState.HIT;
        hp--;
        Vector3 direction = (myPlayer.transform.position - transform.position).normalized;
        myRb.velocity = new Vector3(-direction.x * 10, 3, -direction.z * 10);
        if (hp <= 1)
        {
            hp = 0;
            StartCoroutine(Die());
        }
        myHudRenderer.SetBossHudHealth(hp);
    }
  private IEnumerator Die()
    {

        yield return new WaitForSeconds(inmunity+0.2f);
        SoundManager.PlaySound(SoundManager.Sound.ILLODIE, 0.8f);

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
        SoundManager.PlaySound(SoundManager.Sound.ILLO, 0.8f);
        takeDmg = false;
        yield return new WaitForSeconds(2f);
        currentCombatState = (int)CombatState.HITTING;
        busy = false;
    }
}
