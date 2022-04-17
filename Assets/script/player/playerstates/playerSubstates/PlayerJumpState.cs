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

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityY(playerData.jumpForce);
        isAbilityDone = true;
        amountOfJumpsLeft--;
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        //player.Drag(playerData.dragAmount);
        //player.Run(1);
        //Debug.Log("drag from jumpstate");
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
