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

    //private PlayerInput playerInput;

    public Vector2 RawMovementInput { get; private set; }

    public float inputX { get; private set; }

    public float inputY { get; private set; }

    public bool JumpInput { get; private set; }
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
            
        }
        
    }
    public void UseJumpInput()
    {
        JumpInput = false;
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
