using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunJump : PlayerAbilityState
{
    private int amountOfRunJumpsLeft;

    

    public PlayerRunJump(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfRunJumpsLeft = playerData.amountOfRunJumps;
        
    }
    public override void DoChecks()
    {
        base.DoChecks();
        
    }

    public override void Enter()
    {
        base.Enter();
        
      
            Debug.Log("excuting from run jump state");
        player.SetVelocityY(playerData.jumpForce);

       isAnilityRunJumpDone = true;
            amountOfRunJumpsLeft--;
            player.RunJumpInAirState.SetIsRunJumping();
            //}else if(player.InputHandler.inputX == 0f)
            //{
            //    Debug.Log("change to jump state from runjump state");
            //    stateMachine.ChangeState(player.JumpState);
            //}
        
       
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
    public bool canRunJump()
    {
        if(amountOfRunJumpsLeft > 0)
        {
            Debug.Log("can run jump");
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ResetAmountOfRunJumpsLeft() => amountOfRunJumpsLeft = playerData.amountOfRunJumps;
    public void DecreaseAmountOfRunJumpsLeft() => amountOfRunJumpsLeft--;
}
