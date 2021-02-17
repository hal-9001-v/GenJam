using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public PlayerControls myPlayerControls;
    private Vector2 moveInput;
    private bool jump;
    private bool dive;

    private void Awake() {
        
        myPlayerControls = new PlayerControls();
       
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

        pc.DefaultActionMap.Jump.performed += ctx => jump = true;

        pc.DefaultActionMap.Dive.performed += ctx => dive = true;
    
    }

    // Start is called before the first frame update
    void Start()
    {
        setPlayerControls(myPlayerControls);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
