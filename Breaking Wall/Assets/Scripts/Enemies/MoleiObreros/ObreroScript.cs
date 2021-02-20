using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObreroScript : MonoBehaviour
{
    //GOs
    private Rigidbody myRb; //My Rigidbody
    public BrickScript myBrick; 
    //MovementVals
    private Vector2 moveInput; //Input Vector corresponding to WASD or JoyStick input

    //ControlVars
    private bool isGrounded;
    public int currentState;
    public int currentCombatState;
    private bool takeDmg;
    private bool canShoot;
    //Player Stats
    private float movementSpeed; //Actual Movement Speed
    private float airMovementSpeed; //Movement  Speed When airborne
    private float groundMovementSpeed; //Movement speed when on ground, also launching speed
    public int hp; //Life points
    private float inmunity;


    //AI Components
    private float distanceToPlayer;
    private PlayerController myPlayer;
    private Vector3 currentPos;
    private Vector3 currentPlayerPos;
    private bool busy;
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
        if (myBrick != null) myBrick.gameObject.SetActive(false);
        //Variable Initialization
        movementSpeed = 10f;
        groundMovementSpeed = 5f;
        airMovementSpeed = groundMovementSpeed / 2;
        hp = 2;
        inmunity = 0.2f;
        canShoot = true;
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



    public void ManageAI()
    {

        currentPos = gameObject.transform.position;
        if (canShoot) {
            currentPlayerPos = myPlayer.transform.position;
            movementSpeed = 10;
        }
        else {
            currentPlayerPos = myBrick.transform.position;
            movementSpeed = 15;                
             }

        Vector3 direction = currentPlayerPos - currentPos;
        distanceToPlayer = Vector3.Distance(currentPlayerPos, currentPos);

        /////////////////////////////////////////////////////////////////////////
        
        if (distanceToPlayer > 10.0 && !busy && distanceToPlayer < 100 &&canShoot)
        {
            if (!(currentCombatState == (int)CombatState.HIT)) moveInput = new Vector2(direction.normalized.x, direction.normalized.z);
            int i = Random.Range(1, 200);
            if (i == 3)
            {
                if (canShoot) StartCoroutine(Shoot());
            }
        }
        else if (distanceToPlayer == 10.0 &&canShoot) {
            moveInput = Vector2.zero; 
        }
        else if (distanceToPlayer < 10.0 && canShoot)
        {
            if (!(currentCombatState == (int)CombatState.HIT) && canShoot) moveInput = new Vector2(-direction.normalized.x, -direction.normalized.z);
        }
        else if (distanceToPlayer < 50 && !canShoot)
        {
            if (!(currentCombatState == (int)CombatState.HIT)) moveInput = new Vector2(direction.normalized.x, direction.normalized.z);
        }
    }


    private IEnumerator Shoot()
    {
        canShoot = false;
        Vector3 playerDirection = myPlayer.transform.position - gameObject.transform.position;
        busy = true;
        moveInput = Vector2.zero;
        myBrick.gameObject.SetActive(true);
        SoundManager.PlaySound(SoundManager.Sound.BULLTHROWS, 0.4f);
        SoundManager.PlaySound(SoundManager.Sound.SWINGSPUNCH, 0.2f);
        myBrick.transform.parent = null;
        myBrick.GetComponent<Rigidbody>().velocity = playerDirection.normalized * distanceToPlayer*2;
        yield return new WaitForSeconds(2f);
        busy = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Micro")
        {
            myBrick.transform.parent = gameObject.transform;
            myBrick.transform.localPosition = new Vector3(0, 0, 1);
            myBrick.transform.localRotation = Quaternion.identity;
            StartCoroutine(ShootTimer());
            myBrick.gameObject.SetActive(false);
        }
    }
    private IEnumerator ShootTimer() {
        yield return new WaitForSeconds(2f);
        canShoot = true;
    }



    private void TakeDamage()
    {
        busy = true;
        currentCombatState = (int)CombatState.HIT;
        hp--;
        SoundManager.PlaySound(SoundManager.Sound.PUNCHHITS, 0.4f);
        if (hp <= 0)
        {

            StartCoroutine(Die());

        }
        Vector3 direction = (myPlayer.transform.position - transform.position).normalized;
        myRb.velocity = new Vector3(-direction.x * 10, 3, -direction.z * 10);
    }

    private IEnumerator Die()
    {

        SoundManager.PlaySound(SoundManager.Sound.BULLDIES, 0.8f);
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
