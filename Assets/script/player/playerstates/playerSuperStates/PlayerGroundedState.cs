using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected float xInput;

    private bool JumpInput;

    private bool isGrounded;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIsGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpsLeft();
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

        if (JumpInput && player.JumpState.canJump())
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
            Debug.Log("ongroundstate usejumpinput from playerinputhandler" +JumpInput);
        }else if (!isGrounded)
        {
            player.JumpState.DeCreaseAmountOfJumpsLeft();
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        //player.Drag(playerData.frictionAmount);
        //Debug.Log("drag from groundstate");
    }
}
