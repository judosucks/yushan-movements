using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    private bool isGrounded;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

       

       
       
        if(xInput == 0f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        //player.Run(1);
        player.MoveCharacter();
        //player.ApplyGroundLinearDrag();
        if (isGrounded)
        {
            Debug.Log("player drag from movestate" + isGrounded);
            //player.Drag(playerData.frictionAmount);
            player.ApplyGroundLinearDrag();
        }
        else
        {
            Debug.Log("player airlineardrage from groundedstate");
            player.ApplyAirLinearDrag();
        }
    }
}
