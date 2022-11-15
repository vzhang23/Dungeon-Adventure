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
    public List<GameObject> currentSkillsValue;
    public List<KeyCode> currentSkillsKey;
    private GameObject newArmorPart;
    public float inActionUntil;
    private float cooldownFinish;
    // Start is called before the first frame update
    void Start()
    {
        cooldownFinish = 0;
        inActionUntil = 0;
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
        if (Time.time < inActionUntil)
        {
            return;
        }
        ProcessInput();

    }

    private void FixedUpdate()
    {
        if (Time.time < inActionUntil)
        {
            return;
        }
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
        for(int i=0; i< currentSkillsKey.Count;i++)
        {
            if (Input.GetKeyDown(currentSkillsKey[i]))
            {
                
                currentSkillsValue[i].GetComponent<PlayerSkill>().useSkill(gameObject);
            }
        }
    }
    public void applyVelocity(float x, float y)
    {

        rb.velocity = new Vector2(x, y);
    }
    public Vector2 getVelocity()
    {

        return rb.velocity;
    }
    private void Move()
    {
        if (isGrounded)
        {
            currentJump = 0;
        }
        applyVelocity(moveDirection * playerAttribute.moveSpeed, rb.velocity.y);

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
            string partOfArmor = newArmorPart.GetComponent<PlayerArmor>().partOfArmor;
            print(GameManager.Instance());
            if (currentArmorParts.ContainsKey(partOfArmor)) {
                GameManager.Instance().displayArmorUI(other.gameObject, currentArmorParts[partOfArmor]);

            }
            else
            {
                GameManager.Instance().displayArmorUI(other.gameObject);
            }
            
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
        if (cooldownFinish <= Time.time)
        {
            if (isGrounded)
            {
                applyVelocity(0, 0);
            }
            cooldownFinish = Time.time + playerAttribute.weapon[i].GetComponent<PlayerWeapon>().coolDown;
            inActionUntil = Time.time + playerAttribute.weapon[i].GetComponent<PlayerWeapon>().recoveryTime;
            Instantiate(playerAttribute.weapon[i], position, playerAttribute.weapon[i].transform.rotation);
        }

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
