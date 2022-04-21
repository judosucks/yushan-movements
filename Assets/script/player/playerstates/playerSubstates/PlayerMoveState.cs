using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
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
        if (!isExitingState)
        {
           
           if (normalInputX == 0)
            {
                Debug.Log("idle");
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                Debug.Log("movecharacter");
                player.MoveCharacter();
            }

        }


        //player.MoveCharacter(playerData.movementAcceleration * xInput);
        //player.SetVelocityX(playerData.movementAcceleration * xInput);

        //if(xInput == 0f && !isExitingState)
        //{
        //    stateMachine.ChangeState(player.IdleState);
        //}
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        //player.Run(1);
       
    }
}
