using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        //player.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(xInput != 0f)
        {
            stateMachine.ChangeState(player.MoveState);
        }else if(xInput == 0f && player.CheckGrounded())
        {
            Debug.Log("applygroundlineardrag from idlestate");
            player.ApplyGroundLinearDrag();
        }else if(xInput == 0f && !player.CheckGrounded())
        {
            Debug.Log("applyairlineardrag from idlestate");
            player.ApplyAirLinearDrag();
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        
    }
}
