using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="newPlayerData",menuName ="Data/Player Data/Base Data")]
public class PlayerData :ScriptableObject
{
    //PHYSICS
    [Header("Gravity")]
    public float gravityScale;//overrides rb.gravityScale
    public float fallGravityMult;
    public float quickFallGravityMult;
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
    public float groundLinearDrag;
    public float airLinearDrag;

    [Header("in air state")]
    public float variableJumpHeightMultiplier = 0.5f;
    public float variableJumpMoveMultiplier = 0.5f;
    public float variableRunJumpHeightMultiplier = 0.5f;

    //jump
    [Header("Jump")]
    public float jumpForce;
    [Range(0, 1)] public float jumpCutMultiplier;
    [Space(10)]
    [Range(0, 0.5f)] public float jumpBufferTime;//time after pressing the jump button where if the requirements are met a jump will be automatically performed
    public int amountOfJumps = 1;
    [Range(0,100f)]public float straightJumpHeight;
    //OTHER
    [Header("Other Settings")]
    public bool doKeepRunMomentum; //player movement will not decrease speed if above maxSpeed, letting only drag do so. Allows for conservation of momentum
    public bool doTurnOnWallJump; //player will rotate to face wall jumping direction

    [Header("check variables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
    public float groundRayCastLength;
    public float wallCheckDistance = 0.4f;

    [Header("run jump")]
    public int amountOfRunJumps = 1;

    [Header("wall slide state")]
    public float wallSlideVelocity = 3f;
}
