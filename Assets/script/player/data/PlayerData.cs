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
    public float movementSpeed;
    public float movementAcceleration = 10f;

    public float runMaxSpeed;
    public float runAccel;
    public float runDeccel;
    [Range(0, 1)] public float accelInAir;
    [Range(0, 1)] public float deccelInAir;
    [Space(5)]
    [Range(.5f, 2f)] public float accelPower;
    [Range(.5f, 2f)] public float stopPower;
    [Range(.5f, 2f)] public float turnPower;

    //jump
    [Header("Jump")]
    public float jumpForce;
    [Range(0, 1)] public float jumpCutMultiplier;
    [Space(10)]
    [Range(0, 0.5f)] public float jumpBufferTime;//time after pressing the jump button where if the requirements are met a jump will be automatically performed
                                                 //OTHER
    [Header("Other Settings")]
    public bool doKeepRunMomentum; //player movement will not decrease speed if above maxSpeed, letting only drag do so. Allows for conservation of momentum
    public bool doTurnOnWallJump; //player will rotate to face wall jumping direction
}
