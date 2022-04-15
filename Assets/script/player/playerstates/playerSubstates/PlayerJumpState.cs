using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Jump();
        isAbilityDone = true;
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.Drag(playerData.dragAmount);
        player.Run(1);
    }
}
