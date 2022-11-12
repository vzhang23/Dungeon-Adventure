using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;
    private bool faceingDirection;
    private float moveDirection;
    private bool isJumping;
    public float jumpForce;
    public int jumpLimit;
    private int currentJump;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundObjects;
    public float checkRadius;
    // Start is called before the first frame update
    void Start()
    {
        isJumping = false;
        rb = GetComponent<Rigidbody2D>();
        currentJump = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
        Move();
    }
    private void ProcessInput()
    {
        moveDirection = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.W) && currentJump < jumpLimit)
        {
            isJumping = true;
            currentJump++;
        }
        print(currentJump);
    }


    private void Move()
    {
        if (isGrounded)
        {
            currentJump = 0;
        }
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
        }
        isJumping = false;
    }

}
