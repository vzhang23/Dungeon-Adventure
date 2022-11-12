using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public PlayerAttribute playerAttribute;
    public float moveSpeed;
    private Rigidbody2D rb;
    public int faceingDirection;
    private float moveDirection;
    private bool isJumping;
    public float jumpForce;
    public int jumpLimit;
    private int currentJump;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundObjects;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        isJumping = false;
        rb = GetComponent<Rigidbody2D>();
        currentJump = 0;
        faceingDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

    }

    private void FixedUpdate()
    {
        Move();
    }
    private void ProcessInput()
    {
        moveDirection = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.W) && currentJump < jumpLimit)
        {
            isJumping = true;
            isGrounded = false;
            currentJump++;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Vector2 attackPosition = gameObject.transform.position;
            attackPosition.x = attackPosition.x + 0.5f;
            releaseAttack(attackPosition, 0);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Vector2 attackPosition = gameObject.transform.position;
            attackPosition.x = attackPosition.x + 0.5f;
            releaseAttack(attackPosition, 1);
        }
    }


    private void Move()
    {
        if (isGrounded)
        {
            currentJump = 0;
        }
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        if(moveDirection<0 && faceingDirection != -1)
        {
            faceingDirection = -1;
        }
        else if(moveDirection > 0 && faceingDirection != 1)
        {
            faceingDirection = 1;
        }

        if (isJumping)
        {
            // rb.AddForce(new Vector2(0f, jumpForce));
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        isJumping = false;
    }
     void OnCollisionEnter2D(Collision2D other)
     {  
        print(other.gameObject.layer+":"+groundObjects.value);
        if (groundObjects == (groundObjects | (1 << other.gameObject.layer)))
        { 
            isGrounded = true;
        }
     }

    void releaseAttack(Vector2 position, int i)
    {
        Instantiate(playerAttribute.weapon[i], position,  playerAttribute.weapon[i].transform.rotation);

    }

}
