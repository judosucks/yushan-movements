using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }

    public float inputX { get; private set; }

    public float inputY { get; private set; }

   public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        inputX = RawMovementInput.x;
        inputY = RawMovementInput.y;
        
    }
   public void OnJumpInput(InputAction.CallbackContext context)
    {
        
        
    }
}
