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

    public bool JumpInput { get; private set; }

    public bool JumpInputStop { get; private set; }

    public bool RunJumpInputStop { get; private set; }

    public bool RunJumpInput { get; private set; }

    public Player player { get; private set; }
   
    [SerializeField]
    private float inputHoldTime = 0.2f;
   
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
        CheckJumpInputHoldTime();
        CheckRunJumpInputHoldTime();
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        inputX = RawMovementInput.x;
        inputY = RawMovementInput.y;
        if (context.started)
        {
            Debug.Log("pressed move");

        }
        if (context.canceled)
        {
            if (player.CheckGrounded())
            {
                Debug.Log("not pressed move apple ground linear drag");
                player.ApplyGroundLinearDrag();
            }else if (!player.CheckGrounded())
            {
                player.ApplyAirLinearDrag();
            }
            
        }
        
    }
   public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("pressed jump");
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }
        if (context.canceled)
        {
            JumpInputStop = true;
        }
        
        
    }
    public void OnRunJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            RunJumpInput = true;
            RunJumpInputStop = false;
            runJumpInputStartTime = Time.time;
        }
        if (context.canceled)
        {
            RunJumpInputStop = true;
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
    }
    private void CheckRunJumpInputHoldTime()
    {
        if(Time.time >= runJumpInputStartTime + inputHoldTime)
        {
            Debug.Log("checkrunjumpinputholdtime");
            RunJumpInput = false;
        }
    }
    private void CheckJumpInputHoldTime()
    {
        if(Time.time >= jumpInputStartTime + inputHoldTime)
        {
            Debug.Log("checkjumpinputHoldTime");
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
