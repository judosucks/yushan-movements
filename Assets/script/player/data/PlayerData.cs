using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="newPlayerData",menuName ="Data/Player Data/Base Data")]
public class PlayerData :ScriptableObject
{
    //PHYSICS
    [Header("Gravity")]
    public float gravityScale;//overrides rb.gravityScale

    [Header("Drag")]
    public float dragAmount;//drag is air
    public float frictionAmount;//drag on ground
    [Header("Other physics")]
    [Range(0, 0.5f)] public float coyoteTime;//grace time to Jump after player has fallen off a playformer



    //GROUND
    [Header("Move State")]

    
    public float runMaxSpeed;
    public float runAccel;
    public float runDeccel;
    [Range(0, 1)] public float accelInAir;
    [Range(0, 1)] public float deccelInAir;
    [Space(5)]
    [Range(.5f, 2f)] public float accelPower;
    [Range(.5f, 2f)] public float stopPower;
    [Range(.5f, 2f)] public float turnPower;
    //new move variables
    public float movementAcceleration;
    public float moveMaxSpeed;
    public float setVelocityXSpeed;

    [Header("in air state")]
    public float variableJumpHeightMultiplier = 0.5f;
    public float variableRunJumpHeightMultiplier = 0.5f;
    [Range(0, 100f)] public float inAirMovementForce = 0.5f;

    //jump
    [Header("Jump")]
    public float runJumpForce;
    public int amountOfJumps = 1;
    [Range(0,100f)]public float straightJumpHeight;
    
    
    
    

    [Header("check variables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
    public float groundRayCastLength;
    public float wallCheckDistance = 0.4f;
    public LayerMask whatIsWall;

    [Header("run jump")]
    public int amountOfRunJumps = 1;

    [Header("wall slide state")]
    public float wallSlideVelocity = 3f;
}
