using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{

    private int amountOfJumpsLeft;

    

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
        
    }
    public override void DoChecks()
    {
        base.DoChecks();
        
    }
    public override void Enter()
    {
        base.Enter();
       
        //player.SetVelocityY(playerData.jumpForce);
       
            Debug.Log("inpux is 0 straight jump");

        player.InputHandler.UseJumpInput();
        player.StraightJump();
       
            isAbilityDone = true;
            amountOfJumpsLeft--;
            player.InAirState.SetIsJumping();
       
       
        //else if(player.InputHandler.inputX != 0f)
        //{
        //    Debug.Log("changing to run jump state");
        //    stateMachine.ChangeState(player.RunJumpState);
        //}
        
        
    }

    
    public bool canJump()
    {
        if(amountOfJumpsLeft > 0)
        {
            Debug.Log("canJump");
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.amountOfJumps;

    public void DeCreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
}
