using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected bool isExitingState;
    protected float startTime;
    protected float runJumpStartTime;
    public bool isAnimationFinished;
    private string animBoolName;

    public PlayerState(Player player,PlayerStateMachine stateMachine,PlayerData playerData,string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        runJumpStartTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
        Debug.Log("playerstate aniboolname"+animBoolName);       
    }
    public virtual void Exit()
    {   
        player.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks()
    {

    }
    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
