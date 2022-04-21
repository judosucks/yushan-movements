using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallTouchingState : PlayerState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected float xInput;
    protected int normalInputX;
    protected int normalInputY;
    protected bool GrabInput;

    public PlayerWallTouchingState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.inputX;
        normalInputX = player.InputHandler.normalInputX;
        normalInputY = player.InputHandler.normalInputY;
        GrabInput = player.InputHandler.GrabInput;
        if (isGrounded && !GrabInput)
        {
            Debug.Log("chanestate to idle state");
            stateMachine.ChangeState(player.IdleState);
        }
        else if(!isTouchingWall || (normalInputX == player.facingDirection&& !GrabInput))
        {
            Debug.Log("change to in air state"+ xInput+""+player.facingDirection);
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
