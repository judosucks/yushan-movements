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

    public Vector2 CurrentVelocity { get; private set; }

    public int facingDirection { get; private set; }

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

        stateMachine.Initialize(IdleState);
    }
    private void Update()
    {
        CurrentVelocity = rb.velocity;
        stateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicUpdate();
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
    private void Flip()
    {
        facingDirection *= -1;
        spriteRenderer.flipX = true;
        //transform.Rotate(0.0f, 180.0f, 0.0f);
        //Debug.Log(facingDirection);
    }

}
