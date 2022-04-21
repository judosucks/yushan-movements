using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected float xInput;

    protected bool JumpInput;

    protected bool isGrounded;

    protected bool RunJumpInput;

    protected bool isTouchingWall;

    protected bool GrabInput;

    protected int normalInputX;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
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
        normalInputX = player.InputHandler.normalInputX;
        xInput = player.InputHandler.inputX;
        JumpInput = player.InputHandler.JumpInput;
        RunJumpInput = player.InputHandler.RunJumpInput;
        GrabInput = player.InputHandler.GrabInput;
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
        }
        if(RunJumpInput && player.RunJumpState.canRunJump())
        {
            Debug.Log("going to run jump state");
            player.InputHandler.UseRunJumpInput();
            stateMachine.ChangeState(player.RunJumpState);
        }else if (!isGrounded && stateMachine.CurrentState == player.RunJumpInAirState || stateMachine.CurrentState == player.RunJumpState)
        {
            Debug.Log("going to run jump in air state");
            player.RunJumpInAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.RunJumpInAirState);
        }else if(isTouchingWall && GrabInput)
        {
            Debug.Log("grab wall");
            stateMachine.ChangeState(player.WallGrabState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        
        //Debug.Log("drag from groundstate");
       
    }
}
