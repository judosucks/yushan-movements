using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected float xInput;

    private bool JumpInput;

    private bool isGrounded;

    private bool RunJumpInput;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpsLeft();
        player.RunJumpState.ResetAmountOfRunJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.inputX;
        JumpInput = player.InputHandler.JumpInput;
        RunJumpInput = player.InputHandler.RunJumpInput;
        if (JumpInput && player.JumpState.canJump())
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
            Debug.Log("ongroundstate usejumpinput from playerinputhandler" +JumpInput);
        }else if (!isGrounded)
        {
            Debug.Log("going to in air state");
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }else if (isGrounded)
        {
            player.ApplyGroundLinearDrag();
        }
        if(RunJumpInput && player.RunJumpState.canRunJump())
        {
            Debug.Log("going to run jump state");
            player.InputHandler.UseRunJumpInput();
            stateMachine.ChangeState(player.RunJumpState);
        }else if (!isGrounded)
        {
            Debug.Log("going to run jump in air state");
            player.RunJumpInAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.RunJumpInAirState);
        }else if (isGrounded)
        {
            player.ApplyGroundLinearDrag();
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        
        //Debug.Log("drag from groundstate");
       
    }
}
