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


        player.InputHandler.UseRunJumpInput();
        player.RunJump();

        //player.SetVelocityY(playerData.runJumpForce);

        isAbilityRunJumpDone = true;
            amountOfRunJumpsLeft--;
            player.RunJumpInAirState.SetIsRunJumping();
            //}else if(player.InputHandler.inputX == 0f)
            //{
            //    Debug.Log("change to jump state from runjump state");
            //    stateMachine.ChangeState(player.JumpState);
            //}
        
       
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.Drag(playerData.dragAmount);
        player.Run(1);
    }


    public bool canRunJump()
    {
        if(amountOfRunJumpsLeft > 0)
        {
            Debug.Log("runjumpleft"+amountOfRunJumpsLeft);
            return true;
        }
        else
        {
            Debug.Log("runjumpleft" + amountOfRunJumpsLeft);
            return false;
        }
    }
    public void ResetAmountOfRunJumpsLeft() => amountOfRunJumpsLeft = playerData.amountOfRunJumps;
    public void DecreaseAmountOfRunJumpsLeft() => amountOfRunJumpsLeft--;
}
