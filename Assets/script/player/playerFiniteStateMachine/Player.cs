using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    #region State Variables
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerRunJump RunJumpState { get; private set; }
    public PlayerRunJumpInAir RunJumpInAirState { get; private set; }
    public PlayerRunJumpLanding RunJumpLandState { get; private set; }

    [SerializeField]
    private PlayerData playerData;
    #endregion

    public Animator Anim { get; private set; }

    public PlayerInputHandler InputHandler { get; private set; }

    public Rigidbody2D rb { get; private set; }

    public SpriteRenderer spriteRenderer { get; private set; }

    public Vector2 CurrentVelocity { get; private set; }

    public int facingDirection { get; private set; }

    #region STATE PARAMETERS
    public bool IsFacingRight { get; private set; }
    public bool IsJumping { get; private set; }

    public float LastOnGroundTime { get; private set; }

    #endregion
    #region Input Parameters
    public float LastPressedJumpTime { get; private set; }
    #endregion
    #region  check transofrms
    [SerializeField] private Transform groundCheck;
    #endregion

    private bool isGrounded;

    private Vector2 workspace;
    #region OTHER VARIABLES
    private Vector2 PreviousFramePosition = Vector2.zero; //initial position
    public float Speed { get; private set; }
    #endregion
    //change direction
    public bool changeingDirection => (rb.velocity.x > 0f && InputHandler.inputX < 0f) || (rb.velocity.x < 0f && InputHandler.inputX > 0f);
    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion

        stateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this,stateMachine,playerData,"idle");
        MoveState = new PlayerMoveState(this,stateMachine,playerData,"move");
        JumpState = new PlayerJumpState(this, stateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, stateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this,stateMachine,playerData,"land");
        RunJumpState = new PlayerRunJump(this, stateMachine, playerData, "runJumpInAir");
        RunJumpInAirState = new PlayerRunJumpInAir(this, stateMachine, playerData, "runJumpInAir");
        RunJumpLandState = new PlayerRunJumpLanding(this, stateMachine, playerData, "runJumpLand");
        isGrounded = CheckGrounded();
        
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
        #region TIMERS
        LastOnGroundTime -= Time.deltaTime;
        LastPressedJumpTime -= Time.deltaTime;
        #endregion
        CurrentVelocity = rb.velocity;
        Speed = Mathf.Abs(CurrentVelocity.x);
        stateMachine.CurrentState.LogicUpdate();
       
        OnDrawGizmos();
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
    public void Drag(float amount)
    {
        
        Vector2 force = amount * rb.velocity.normalized;
        force.x = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(force.x));//ensure we only slow the player down, if the player is going really slowly we just apply a force stopping them 確定玩家的速度只會慢下來 當玩家速度真的很慢的時候 執行一個動力強迫玩家停下
                                                                          
        force.y = Mathf.Min(Mathf.Abs(rb.velocity.y), Mathf.Abs(force.y));
        force.x *= Mathf.Sign(rb.velocity.x);//finds direction to apply force 在x軌道找出執行動力的方向
        force.y *= Mathf.Sign(rb.velocity.y);
        
        rb.AddForce(-force, ForceMode2D.Impulse);
        Debug.Log("drag" + -force);
    }
    public void Run(float lerpAmount)
    {
        Debug.Log("run");
        CalculateSpeed();
        float targetSpeed = InputHandler.inputX * playerData.runMaxSpeed;//calculate the direction we want to move in our desire velocity 計算預計行動的方向速度
        float speedDif = targetSpeed - rb.velocity.x;//calculate differece between current velocity and desire velocity 計算當前速度跟預計的速度
       
        #region Acceleration Rate
        //float accelRate;
        //accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? playerData.runAccel : playerData.runDeccel;
        //gets an accleration value based of if we are accelerating (includes turning) or trying to stop (decelerating).as well as applying a mutiplier if we are in air borne 加速跟減速 當玩家在空中時執行乘法落下(加速)
        //if(LastOnGroundTime > 0)
        //{
        //    Debug.Log("lastongroundtime");
        //    accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? playerData.runAccel : playerData.runDeccel;
        //}
        //else
        //{

        //    accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? playerData.runAccel * playerData.accelInAir : playerData.runDeccel * playerData.deccelInAir;
        //    Debug.Log("else lastongroundtime>0" + accelRate);
        //}
        //if we want to run but are already going faster than max run speed 假如玩家已經超出最高限速
        //if (((rb.velocity.x > targetSpeed && targetSpeed > 0.01f)||(rb.velocity.x < targetSpeed && targetSpeed < -0.01f))&& playerData.doKeepRunMomentum)
        //{
        //    Debug.Log("prevent any deceleration");
        //    accelRate = 0; // prevent any deceleration from happening, or in other words conserve are current momentum 預防減速發生 動力定律
        //}
        #endregion
        #region Velocity Power
        float velPower;
        if (Mathf.Abs(targetSpeed) < 0.01f)
        {

            velPower = playerData.stopPower;
        }
        else if (Mathf.Abs(rb.velocity.x) > 0 && (Mathf.Sign(targetSpeed) != Mathf.Sign(rb.velocity.x)))
        {
            velPower = playerData.turnPower;
        }
        else
        {
           
            velPower = playerData.accelPower;
        }
        #endregion
        //applies accleration to speed difference, then is raised to a set power so the acceleration increased with higher speeds,finally multiplies by sign to preserve direction 執行當前速度與預計速度差別 設置力量來增加更快的速度 最後乘以Mathf.Sign維持方向



        float movement = Mathf.Pow(Mathf.Abs(speedDif), velPower) * Mathf.Sign(speedDif);
        movement = Mathf.Lerp(rb.velocity.x, movement, lerpAmount);
      
        rb.AddForce(movement * Vector2.right); //applies force force to rigibody,multiplying by vector2.right so that it only affects X axis 執行動力來強迫rigibody乘以Vector2.right 將他只在x軌道發生作用
        
        if(InputHandler.inputX != 0)
        {
            CheckDirectionToFace(InputHandler.inputX > 0);
        }
    }
   
    public void MoveCharacter()
    {
        

        rb.AddForce(new Vector2(InputHandler.inputX, 0f) * playerData.movementAcceleration);
        float targetSpeed = InputHandler.inputX * playerData.movementAcceleration;
        float speedDif = targetSpeed - rb.velocity.x;
        
        float movementPerFrame = Vector2.Distance(PreviousFramePosition, transform.position);
        Speed = movementPerFrame / Time.deltaTime;
        PreviousFramePosition = transform.position;
        Debug.Log(Mathf.Abs(rb.velocity.x) +"+"+ targetSpeed +"+"+ Speed +"+"+ speedDif +"+"+ Mathf.Abs(PreviousFramePosition.x));
        if (Mathf.Abs(rb.velocity.x)> playerData.moveMaxSpeed)
        {
            Debug.Log("math.abs rb.v.x" +Mathf.Abs(rb.velocity.x));
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * playerData.moveMaxSpeed, CurrentVelocity.y);
            
        }
        if (InputHandler.inputX != 0 && isGrounded)
        {
            CheckDirectionToFace(InputHandler.inputX > 0);
        }
    }
    public void Jumping(float velocity)
    {
        workspace.Set(Speed, velocity);
        rb.velocity = CurrentVelocity;
        CurrentVelocity = workspace;
        if (RunJumpInAirState.isRunJumping)
        {
            Debug.Log("isrunjumping from jumping player");
            workspace.Set(Speed, velocity * playerData.jumpForce);
            rb.velocity = workspace;
            CurrentVelocity = workspace;
        }
        else
        {
            rb.AddForce(new Vector2( Speed , velocity * playerData.jumpForce), ForceMode2D.Impulse);
            Debug.Log("run jumping" + Speed);
        }
        
    }
    public void StraightJump()
    {
        float absoluteZero = 0f;
        workspace.Set(absoluteZero, CurrentVelocity.y);
        rb.velocity = CurrentVelocity;
        CurrentVelocity = workspace;
        if(absoluteZero > 0 || absoluteZero < 0)
        {
            Debug.Log("straight jump"+absoluteZero);
            absoluteZero = 0f;
            Debug.Log(absoluteZero);
            rb.AddForce(new Vector2(absoluteZero, CurrentVelocity.y * playerData.straightJumpHeight), ForceMode2D.Impulse);
            Debug.Log(absoluteZero);
        }
        else if(absoluteZero == 0)
        {
            Debug.Log("else straight jump"+absoluteZero);
            absoluteZero = 0f;
            Debug.Log(absoluteZero);
            rb.AddForce(new Vector2(absoluteZero, CurrentVelocity.y * playerData.straightJumpHeight), ForceMode2D.Impulse);
            Debug.Log(absoluteZero);
        }
        else
        {
            Debug.Log("straight jump else shit");
        }
        //rb.AddForce(new Vector2(absoluteZero, CurrentVelocity.y * playerData.straightJumpHeight), ForceMode2D.Impulse);
        Debug.Log("straight jump");
    }
    public void ApplyGroundLinearDrag()
    {
        if(Mathf.Abs(InputHandler.inputX)< 0.4f || changeingDirection)
        {
            Debug.Log("apply ground linear drag");
            rb.drag = playerData.groundLinearDrag;
        }
        else
        {
            
            rb.drag = 0f;
        }
    }
    public void ApplyAirLinearDrag()
    {
        Debug.Log("apply air linear drag");
        rb.drag = playerData.airLinearDrag;
    }
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, rb.velocity.y);
        rb.velocity = workspace;
        CurrentVelocity = workspace;
        Debug.Log("currentvelocityx");

    }
    public void SetVelocityY(float velocity)
    {
        
        workspace.Set(rb.velocity.x, velocity);
        rb.velocity = workspace;
        CurrentVelocity = workspace;
        Debug.Log("setveloctyy"+CurrentVelocity.y+"cur rb"+rb.velocity.y);
    }
    public void Jump()
    {
        //ensures we can't call a jump multiple times from one press
        LastOnGroundTime = 0;
        LastPressedJumpTime = 0;
        #region perform jump
        float force = playerData.jumpForce;

        if (rb.velocity.y < 0)
        {
            Debug.Log("if" + rb.velocity.y);
            force -= rb.velocity.y;
        }
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        #endregion
    }
    private void Turn()
    {
        Vector3 scale = transform.localScale;//stores scale and flips x axis,"flipping" the entire gameObject around (could rotate the player instead)
        scale.x *= -1;
        transform.localScale = scale;

        IsFacingRight = !IsFacingRight;
    }
    #endregion
    #region OTHER

    private void CalculateSpeed()
    {
        float movementPerFrame = Vector2.Distance(PreviousFramePosition, transform.position);
        Speed = movementPerFrame / Time.deltaTime;
        PreviousFramePosition = transform.position;
        Debug.Log(Speed);
        
    }
    public void AnimationTrigger() => stateMachine.CurrentState.AnimationTrigger();

    public void AnimationFinishTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();

    #endregion
    //public void SetVelocityX(float velocity)
    //{
    //    workspace.Set(velocity, CurrentVelocity.y);
    //    rb.velocity = workspace;
    //    CurrentVelocity = workspace;

    //    rb.AddForce(new Vector2(velocity, 0f) * playerData.movementAcceleration);
    //    Debug.Log("i was excuted addforce"+CurrentVelocity.magnitude+rb.velocity.magnitude);
    //    if (Mathf.Abs(CurrentVelocity.x) > playerData.movementSpeed)
    //    {
    //        Debug.Log("excuted if");
    //        CurrentVelocity = new Vector2(Mathf.Sign(rb.velocity.x)*playerData.movementSpeed,rb.velocity.y);
    //    }
    //}


    #region CHECK METHODS
    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
            Turn();
    }
    public bool CheckIsGrounded()
    {
        //ground check
         if(Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround))
        {
            isGrounded = true;
            return true;
        }
        else
        {
            isGrounded = false;
            return false;
        }
        
        
    }
    public bool CheckGrounded()
    {
       isGrounded = Physics2D.Raycast(transform.position * playerData.groundRayCastLength, Vector2.down, playerData.groundRayCastLength, playerData.whatIsGround);
        if (isGrounded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * playerData.groundRayCastLength);
    }
    #endregion
}
