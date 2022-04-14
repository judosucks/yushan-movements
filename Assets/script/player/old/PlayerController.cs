using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float movementInputDirection;

    private bool isFacingRight = true;

    private Rigidbody2D rb;

    private Animator animator;

    [Header("movement variables")]
    [SerializeField]private float movementSpeed;
    [SerializeField]private float movementAcceleration;
    // Start is called before the first frame update
    void Start()
    {
     rb = GetComponent<Rigidbody2D>();
     animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }
    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");
    }
    private void ApplyMovement()
    {
        rb.AddForce(new Vector2(movementInputDirection,0f) * movementAcceleration);
        Debug.Log("velocity"+rb.velocity.magnitude);
         if (Mathf.Abs(rb.velocity.x) > movementSpeed)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(rb.velocity.x) * movementSpeed, rb.velocity.y);

            
            }
    }
}
