using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public PlayerAttribute playerAttribute;
    public Transform groundCheck;
    public LayerMask groundObjects;
    public LayerMask armorObjects;
    public int faceingDirection;
    public float jumpForce;
    private Rigidbody2D rb;
    private float moveDirection;
    private bool isJumping;
    private int currentJump;
    private bool isGrounded;
    private Dictionary<string, PlayerArmor> currentArmorParts;
    private GameObject newArmorPart;

    // Start is called before the first frame update
    void Start()
    {
        currentArmorParts = new Dictionary<string, PlayerArmor>();
        newArmorPart = null;
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
        if (Input.GetKeyDown(KeyCode.W) && currentJump < playerAttribute.jumpLimit)
        {
            isJumping = true;
            isGrounded = false;
            currentJump++;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (newArmorPart!=null)
            {
                string partOfArmor = newArmorPart.GetComponent<PlayerArmor>().partOfArmor;
                print(partOfArmor);
                print(currentArmorParts);
                if (currentArmorParts.ContainsKey(partOfArmor))
                {
                    removeArmor(currentArmorParts[partOfArmor]);
                    
                }
                wearArmor(newArmorPart.GetComponent<PlayerArmor>());
                Destroy(newArmorPart);
            }
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
        rb.velocity = new Vector2(moveDirection * playerAttribute.moveSpeed, rb.velocity.y);

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
        if (groundObjects == (groundObjects | (1 << other.gameObject.layer)))
        {
            isGrounded = true;
        }
        
     }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (armorObjects == (armorObjects | (1 << other.gameObject.layer)))
        {     
            newArmorPart = other.gameObject;
            print(GameManager.Instance());
            GameManager.Instance().displayArmorUI(other.gameObject);
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (armorObjects == (armorObjects | (1 << other.gameObject.layer)))
        {
            newArmorPart = null;
            GameManager.Instance().hideArmorUI();
        }
    }
    void releaseAttack(Vector2 position, int i)
    {
        Instantiate(playerAttribute.weapon[i], position,  playerAttribute.weapon[i].transform.rotation);

    }

    void removeArmor(PlayerArmor newArmor)
    {
        playerAttribute.removeArmor(currentArmorParts[newArmor.partOfArmor]);
        currentArmorParts.Remove(newArmor.partOfArmor);
    }

    void wearArmor(PlayerArmor newArmor)
    {
        playerAttribute.wearArmor(newArmor);
        currentArmorParts.Add(newArmor.partOfArmor, newArmor);
    }

}
