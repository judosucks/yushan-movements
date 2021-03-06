using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    //[Header("Input values")]
    //public Action<InputArgs> OnJumpPressed;
    //public Action<InputArgs> OnJumpReleased;
    //public Action<InputArgs> OnDash;

    //public static PlayerInputHandler instance;

    private PlayerInput playerInput;

    private Camera cam;

    public Vector2 RawMovementInput { get; private set; }

    public float inputX { get; private set; }

    public float inputY { get; private set; }

    public int normalInputX { get; private set; }

    public int normalInputY { get; private set; }

    public bool JumpInput { get; private set; }

    public bool JumpInputStop { get; private set; }

    public bool RunJumpInputStop { get; private set; }

    public bool RunJumpInput { get; private set; }

    public bool GrabInput { get; private set; }

    

    public Player player { get; private set; }
   
    [SerializeField]
    private float inputHoldTime = 0.2f;
    [SerializeField]
    private float runJumpInputHoldTime = 0.2f;
   
    private float jumpInputStartTime;
    private float dashInputStartTime;
    private float runJumpInputStartTime;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();


        cam = Camera.main;
    }
    private void Update()
    {
        
        if(normalInputX == 0f)
        {
            Debug.Log("checkjumpinputholdtime");
            CheckJumpInputHoldTime();
        }else if(normalInputX != 0f)
        {
            Debug.Log("checkrunjumpinputholdtime");
            CheckRunJumpInputHoldTime();
        }
        
        
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
    
        RawMovementInput = context.ReadValue<Vector2>();

        inputX = (RawMovementInput*Vector2.right).x;
        inputY = (RawMovementInput*Vector2.up).y;
        normalInputX = Mathf.RoundToInt(inputX);
        normalInputY = Mathf.RoundToInt(inputY);
        if (context.started)
        {
            Debug.Log("pressed move");
           
        

        }
        if (context.canceled)
        {
            Debug.Log("canceled press move");
        }
       
       

    }
   public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started && inputX == 0f)
        {
            Debug.Log("pressed jump");
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }
        if (context.canceled && JumpInput)
        {
            Debug.Log("released jump");
            JumpInputStop = true;
        }
        


    }
    public void OnRunJumpInput(InputAction.CallbackContext context)
    {
    
        if (context.started && inputX != 0f)
        {
            Debug.Log("pressed run jump");
            RunJumpInput = true;
            RunJumpInputStop = false;
            runJumpInputStartTime = Time.time;
            
        }
        if (context.canceled && RunJumpInput)
        {
            Debug.Log("released run jump");
            RunJumpInputStop = true;
        }
        
    }
    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabInput = true;
        }
        if (context.canceled)
        {
            Debug.Log("released");
        }
    }
    public void UseJumpInput()
    {
        JumpInput = false;
        Debug.Log("usejumpinput from inputhandler" + JumpInput);
    } 
    public void UseRunJumpInput()
    {
        RunJumpInput = false;
        Debug.Log("userunjumpinput from inputhandler");
    }
    public void UseGrabInput()
    {
        GrabInput = false;
    }
    
    private void CheckRunJumpInputHoldTime()
    {
        if(Time.time >= runJumpInputStartTime + runJumpInputHoldTime)
        {
            float time = runJumpInputStartTime + runJumpInputHoldTime;
            Debug.Log("checkrunjumpinputholdtime"+time);
            RunJumpInput = false;
        }
    }
    private void CheckJumpInputHoldTime()
    {
        if(Time.time >= jumpInputStartTime + inputHoldTime)
        {
            float time = jumpInputStartTime + inputHoldTime;
            Debug.Log("checkjumpinputHoldTime"+time);
            JumpInput = false;
        }
    }
    //#region Events
    //public class InputArgs
    //{
    //    public InputAction.CallbackContext context;
    //}
    //#endregion
    //private void OnEnable()
    //{
    //    playerInput.Enable();   
    //}
    //private void OnDisable()
    //{
    //    playerInput.Disable();
    //}
}
