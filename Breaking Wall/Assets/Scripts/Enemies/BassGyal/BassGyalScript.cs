using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassGyalScript : MonoBehaviour
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
    public int shieldHP;

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
    private MicroScript myMicro;
    private Vector3 currentPos;
    private Vector3 currentPlayerPos;
    private bool busy;
    private bool jattacking;
    private bool canShoot;
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
        myCucho.GetComponent<Collider>().gameObject.SetActive(false);
        if (myHudRenderer == null) myHudRenderer = FindObjectOfType<HUDRenderer>();
        if (myPlayer == null) myPlayer = FindObjectOfType<PlayerController>();
        if (myMicro == null) myMicro = FindObjectOfType<MicroScript>();
        myMicro.gameObject.SetActive(false);
        //Variable Initialization
        movementSpeed = 10f;
        groundMovementSpeed = 10f;
        airMovementSpeed = groundMovementSpeed / 4;
        jumpForce = 10f;
        hp = 4;
        inmunity = 0.2f;
        canShoot = true;
        shieldHP = 3;
    }




    // Start is called before the first frame update
    void Start()
    {
        myHudRenderer.InitBossHudHealth(hp);

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
            transform.rotation = new Quaternion(transform.rotation.x, Quaternion.Slerp(prevRotation, currentRot, 0.4f).y, transform.rotation.z, transform.rotation.w);
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
        if (shieldHP <= 0) shield = false;
        currentPos = gameObject.transform.position;

        if (canShoot) { 
            currentPlayerPos = myPlayer.transform.position;
            movementSpeed = 10;
        }
        else
        {
            currentPlayerPos = myMicro.transform.position;
            movementSpeed = 15;
        }
        distanceToPlayer = Vector3.Distance(myPlayer.transform.position, currentPos);


        Vector3 direction = currentPlayerPos - currentPos;

        if (distanceToPlayer > 5.0 && !busy && distanceToPlayer < 50.0)
        {
            if (!(currentCombatState == (int)CombatState.HIT)) moveInput = new Vector2(direction.normalized.x, direction.normalized.z);
            
            int i = Random.Range(1, 500);
            if (i == 3) {

                if (canShoot && currentState == (int)State.GROUNDED && !(currentCombatState == (int)CombatState.HIT)) StartCoroutine(Shoot());

            }
            if (i == 432)
            {
                {
                    if (!jattacking && currentState == (int) State.GROUNDED && !(currentCombatState == (int)CombatState.HIT)) StartCoroutine(JumpAttack(direction));

                }
            }


        }

        else if (distanceToPlayer < 5.0)
        {
            if (!jattacking && currentState == (int)State.GROUNDED && !(currentCombatState == (int)CombatState.HIT)) StartCoroutine(JumpAttack(direction));
        }

    }

    private IEnumerator Shoot()
    {

        canShoot = false;
        Vector3 playerDirection = currentPlayerPos = myPlayer.transform.position - gameObject.transform.position;
        busy = true;
        if (currentState == (int)State.GROUNDED)
        {
            moveInput = Vector2.zero;
            yield return new WaitForSeconds(2f);
            SoundManager.PlaySound(SoundManager.Sound.SYNTHTHROWS, 0.4f);
            SoundManager.PlaySound(SoundManager.Sound.SWINGSPUNCH, 0.2f);
            myMicro.gameObject.SetActive(true);
            myMicro.transform.parent = null;
            myMicro.GetComponent<Rigidbody>().velocity = playerDirection.normalized * distanceToPlayer * 1.5f;
        }
       
        yield return new WaitForSeconds(2f);
        busy = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Micro") {
            
            
            myMicro.transform.parent = gameObject.transform;
            myMicro.transform.localPosition= new Vector3 (0,0,1);
            myMicro.transform.localRotation = Quaternion.identity;
            canShoot = true;
            myMicro.gameObject.SetActive(false);

        }
       
    }

    private IEnumerator JumpAttack(Vector3 direction)
    {
        jattacking = true;
        yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));
        if (currentState == (int)State.GROUNDED)
        {
            Jump();
            SoundManager.PlaySound(SoundManager.Sound.SYNTHJUMP, 0.4f);
            moveInput = new Vector2(direction.normalized.x, direction.normalized.z) * 2;
        }
        yield return new WaitForSeconds(2f);
        if (currentState == (int)State.JUMPING)
        {
            SoundManager.PlaySound(SoundManager.Sound.SYNTHGRUNT, 0.4f);
            Hit();
            busy = true;
            moveInput = new Vector2(-direction.normalized.x, -direction.normalized.z);
        }
        yield return new WaitForSeconds(2f);
        busy = false;
        jattacking = false;

    }



    private void TakeDamage()
    {
        if (!shield)
        {
            busy = true;
            currentCombatState = (int)CombatState.HIT;
            hp--;
            SoundManager.PlaySound(SoundManager.Sound.PUNCHHITS, 0.4f);
            if (hp <= 0)
            {

                StartCoroutine(Die());

            }
            myHudRenderer.SetBossHudHealth(hp);
            Vector3 direction = (myPlayer.transform.position - transform.position).normalized;
            myRb.velocity = new Vector3(-direction.x * 10, 3, -direction.z * 10);
        }
        else {
            SoundManager.PlaySound(SoundManager.Sound.SYNTHSHIELDHIT, 3f);
        }

    }
    private IEnumerator Die()
    {
        SoundManager.PlaySound(SoundManager.Sound.SYNTHDIES, 0.4f);
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

        if (col.tag == "Ground" && jattacking)
        {
            SoundManager.PlaySound(SoundManager.Sound.ELECTRICSOUND, 0.9f);
            Instantiate(GameAssets.i.particles[0], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 3f, gameObject.transform.position.z), gameObject.transform.rotation);

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
