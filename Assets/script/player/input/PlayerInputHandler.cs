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
    //private void Awake()
    //{
    //    #region Singleton
    //    if(instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }
    //    #endregion

    //    playerInput = new PlayerInput();

    //    #region Assign Inputs
    //    playerInput.Gameplay.Move.performed += ctx => RawMovementInput = ctx.ReadValue<Vector2>();
    //    playerInput.Gameplay.Move.canceled += ctx => RawMovementInput = Vector2.zero;

    //    playerInput.Gameplay.Jump.performed += ctx => OnJumpPressed(new InputArgs { context = ctx });
    //    playerInput.Gameplay.Jump.performed += ctx => OnJumpPressed(new InputArgs { context = ctx });

    //    #endregion
    //}
    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;
    private float dashInputStartTime;
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();


        cam = Camera.main;
    }
    private void Update()
    {
        //CheckJumpInputHoldTime();
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        inputX = RawMovementInput.x;
        inputY = RawMovementInput.y;
        
    }
   public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }
        if (context.canceled)
        {
            JumpInputStop = true;
        }
        
    }
    public void UseJumpInput() => JumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if(Time.time >= jumpInputStartTime * inputHoldTime)
        {
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
