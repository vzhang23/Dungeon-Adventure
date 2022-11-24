using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerAttribute playerAttribute;
    public Transform groundCheck;
    public LayerMask groundObjects;
    public LayerMask armorObjects;
    public int faceingDirection;
    public float jumpForce;
    public Rigidbody2D rb;
    private float moveDirection;
    private bool isJumping;
    private int currentJump;
    private bool isGrounded;
    private Dictionary<string, PlayerArmor> currentArmorParts;
    public List<GameObject> currentSkillsValue;
    public List<GameObject> currentSkillsValueInstance;
    public List<KeyCode> currentSkillsKey;

    public List<KeyCode> availableSkillsKey;
    private GameObject newArmorPart;
    public float inActionUntil;
    private float cooldownFinish;
    private float lastBeenHit;
    public float beenHitCooldown;
    public string status;
    public string groundType;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < currentSkillsValue.Count; i++)
        {
            GameObject skillInst = Instantiate(currentSkillsValue[i], transform.position, currentSkillsValue[i].transform.rotation);
            skillInst.transform.parent = gameObject.transform;
            currentSkillsValueInstance.Add(skillInst);
        }
        status = "";
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
        if (playerAttribute.hp <= 0)
        {
            Destroy(gameObject);
        }
        if (!isGrounded)
        {
            groundType = "";
        }
        if (Time.time < inActionUntil)
        {
            return;
        }
        if (groundType == "fireGround")
        {
            playerAttribute.hp -= 2f / playerAttribute.defense;
        }
        Move();
    }

    private void ProcessInput()
    {

        status = "";
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
            StartCoroutine(releaseAttack(attackPosition, playerAttribute.weapon[0]));
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Vector2 attackPosition = gameObject.transform.position;
            attackPosition.x = attackPosition.x + 0.5f;
            StartCoroutine(releaseAttack(attackPosition, playerAttribute.weapon[1]));
        }
        for(int i=0; i< currentSkillsValueInstance.Count;i++)
        {
            if (Input.GetKeyDown(currentSkillsKey[i]))
            {

                currentSkillsValueInstance[i].GetComponent<PlayerSkill>().useSkill(gameObject);
            }
        }
    }

    internal void learnSkill(GameObject gameObject)
    {
        KeyCode k = availableSkillsKey[0];
        availableSkillsKey.RemoveAt(0);
        currentSkillsKey.Add(k);
        GameObject skillInst=Instantiate(gameObject, transform.position, gameObject.transform.rotation);
        skillInst.transform.parent = transform;
        currentSkillsValueInstance.Add(skillInst);
    }

    public void changeVelocity(float x, float y)
    {
        if (groundType == "iceGround")
        {
            float newSpeed = rb.velocity.x + x * 0.05f;
            if (newSpeed >= playerAttribute.moveSpeed*1.5f)
            {
                newSpeed = playerAttribute.moveSpeed * 1.5f;
            }
            if (newSpeed <= -playerAttribute.moveSpeed * 1.5f)
            {
                newSpeed = -playerAttribute.moveSpeed * 1.5f;
            }
            rb.velocity = new Vector2(newSpeed, y);
            return;
        }
        rb.velocity = new Vector2(x, y);
    }
    public void addForce(float x, float y)
    {

        rb.AddForce(new Vector2(x, y));
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
        float fixSpeed = 1;
        if (groundType == "sandGround")
        {
            fixSpeed = 0.7f;
        }
        changeVelocity(moveDirection * playerAttribute.moveSpeed* fixSpeed, rb.velocity.y);

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
            changeVelocity(0, 0);
        }
        groundType = other.gameObject.tag;
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
    public IEnumerator releaseAttack(Vector2 position, GameObject weaponObject)
    {
        if (cooldownFinish <= Time.time)
        {
            if (isGrounded)
            {
                changeVelocity(0, 0);
            }
            PlayerWeapon weapon = weaponObject.GetComponent<PlayerWeapon>();

            cooldownFinish = Time.time + weapon.coolDown;
            inActionUntil = Time.time + weapon.recoveryTime;
            yield return new WaitForSeconds(weapon.waitBeforeAttack);
            Instantiate(weapon, position, weapon.transform.rotation);
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
    public void receiveDamage(float value, int direction, Vector2 force, float hitRecovery)
    {
        if (status == "block")
        {
            if (playerAttribute.getBlockAttack() != null) {

                cooldownFinish = 0;
                Vector2 attackPosition = gameObject.transform.position;
                attackPosition.x = attackPosition.x + 0.5f;
                StartCoroutine(releaseAttack(attackPosition, playerAttribute.getBlockAttack()));
            }
            return;
        }



        if (lastBeenHit + beenHitCooldown >= Time.time)
        {
            return;
        }
        StopAllCoroutines();
        playerAttribute.hp -= value * value / playerAttribute.defense;
        if (hitRecovery != 0) {

            inActionUntil = hitRecovery + Time.time;
            changeVelocity(force.x* direction, force.y);
            StartCoroutine(ResetForce(hitRecovery));
        }
        
        lastBeenHit = Time.time;
    }
    private IEnumerator ResetForce(float hitRecovery)
    {
        yield return new WaitForSeconds(hitRecovery);
        rb.velocity = Vector2.zero;
    }

}
