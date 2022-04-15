using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    public Animator Anim { get; private set; }

    public PlayerInputHandler InputHandler { get; private set; }

    public Rigidbody2D rb { get; private set; }

    public SpriteRenderer spriteRenderer { get; private set; }

    public Vector3 CurrentVelocity { get; private set; }

    public int facingDirection { get; private set; }

    #region STATE PARAMETERS
    public bool IsFacingRight { get; private set; }
    public bool IsJumping { get; private set; }

    public float LastOnGroundTime { get; private set; }
    #endregion


    [SerializeField]
    private PlayerData playerData;

    private Vector2 workspace;
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this,stateMachine,playerData,"idle");
        MoveState = new PlayerMoveState(this,stateMachine,playerData,"move");
    }
    private void Start()
    {

        Anim = GetComponentInChildren<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        facingDirection = 1;
        IsFacingRight = true;

        SetGravityScale(playerData.gravityScale);

        stateMachine.Initialize(IdleState);
    }
    private void Update()
    {
        LastOnGroundTime -= Time.deltaTime;
        CurrentVelocity = rb.velocity;
        stateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicUpdate();
    }

    #region MOVEMENT METHODS

    public void SetGravityScale(float scale)
    {
      
        rb.gravityScale = scale;
    }
    private void Drag(float amount)
    {
        Vector2 force = amount * rb.velocity.normalized;
        force.x = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(force.x));//ensure we only slow the player down, if the player is going really slowly we just apply a force stopping them 確定玩家的速度只會慢下來 當玩家速度真的很慢的時候 執行一個動力強迫玩家停下
                                                                          
        force.y = Mathf.Min(Mathf.Abs(rb.velocity.y), Mathf.Abs(force.y));
        force.x *= Mathf.Sign(rb.velocity.x);//finds direction to apply force 在x軌道找出執行動力的方向
        force.y *= Mathf.Sign(rb.velocity.y);

        rb.AddForce(-force, ForceMode2D.Impulse);
    }
    public void Run(float lerpAmount)
    {
        float targetSpeed = InputHandler.inputX * playerData.runMaxSpeed;//calculate the direction we want to move in our desire velocity 計算預計行動的方向速度
        float speedDif = targetSpeed - rb.velocity.x;//calculate differece between current velocity and desire velocity 計算當前速度跟預計的速度

        #region Acceleration Rate
        float accelRate;

        //gets an accleration value based of if we are accelerating (includes turning) or trying to stop (decelerating).as well as applying a mutiplier if we are in air borne 加速跟減速 當玩家在空中時執行乘法落下(加速)
        if(LastOnGroundTime > 0)
        {
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? playerData.runAccel : playerData.runDeccel;
        }
        else
        {
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? playerData.runAccel * playerData.accelInAir : playerData.runDeccel * playerData.deccelInAir;
        }
        //if we want to run but are already going faster than max run speed 假如玩家已經超出最高限速
        if(((rb.velocity.x > targetSpeed && targetSpeed > 0.01f)||(rb.velocity.x < targetSpeed && targetSpeed < -0.01f))&& playerData.doKeepRunMomentum)
        {
            accelRate = 0; // prevent any deceleration from happening, or in other words conserve are current momentum 預防減速發生 動力定律
        }
        #endregion
        #region Velocity Power
        float velPower;
        if(Mathf.Abs(targetSpeed)< 0.01f)
        {
            velPower = playerData.stopPower;
        }
        else
        {
            velPower = playerData.accelPower;
        }
        #endregion
        //applies accleration to speed difference, then is raised to a set power so the acceleration increased with higher speeds,finally multiplies by sign to preserve direction 執行當前速度與預計速度差別 設置力量來增加更快的速度 最後乘以Mathf.Sign維持方向



        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        movement = Mathf.Lerp(rb.velocity.x, movement, lerpAmount);

        rb.AddForce(movement * Vector2.right); //applies force force to rigibody,multiplying by vector2.right so that it only affects X axis 執行動力來強迫rigibody乘以Vector2.right 將他只在x軌道發生作用
        if(InputHandler.inputX != 0)
        {
            CheckDirectionToFace(InputHandler.inputX > 0);
        }
    }

    private void Turn()
    {
        Vector3 scale = transform.localScale;//stores scale and flips x axis,"flipping" the entire gameObject around (could rotate the player instead)
        scale.x *= -1;
        transform.localScale = scale;

        IsFacingRight = !IsFacingRight;
    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        rb.velocity = workspace;
        CurrentVelocity = workspace;

        rb.AddForce(new Vector2(velocity, 0f) * playerData.movementAcceleration);
        Debug.Log("i was excuted addforce"+CurrentVelocity.magnitude+rb.velocity.magnitude);
        if (Mathf.Abs(CurrentVelocity.x) > playerData.movementSpeed)
        {
            Debug.Log("excuted if");
            CurrentVelocity = new Vector2(Mathf.Sign(rb.velocity.x)*playerData.movementSpeed,rb.velocity.y);
        }
    }
#endregion
public void CheckIfShouldFlip(float xInput)
    {
        if(xInput > 0f)
        {
            spriteRenderer.flipX = false;
        }
        else if (xInput < 0f)
        {
            
            spriteRenderer.flipX = true;
        }
    }
    #region CHECK METHODS
    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
            Turn();
    }
    #endregion
}
