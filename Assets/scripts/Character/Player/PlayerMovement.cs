using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public bool dead;
    private float moveDirection;
    private bool isJumping;
    private int currentJump;
    private bool isGrounded;
    public Dictionary<string, PlayerArmor> currentArmorParts;
    public List<GameObject> currentSkillsValueInstance;
    public List<KeyCode> currentSkillsKey;
    private Animator animator;
    public List<KeyCode> availableSkillsKey;
    private GameObject newArmorPart;
    public float inActionUntil;
    private float cooldownFinish;
    private float lastBeenHit;
    public float beenHitCooldown;
    public string status;
    public string groundType;
    private List<GameObject> learningNewSkill;
    public GameObject textboxUI;
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        print(GameManager.Instance().getSkillChoosed());
        animator = gameObject.GetComponent<Animator>();

        status = "";
        cooldownFinish = 0;
        inActionUntil = 0;
        currentArmorParts = new Dictionary<string, PlayerArmor>();
        newArmorPart = null;
        isJumping = false;
        rb = GetComponent<Rigidbody2D>();
        currentJump = 0;
        faceingDirection = 1; 
        rb = GetComponent<Rigidbody2D>();
        learningNewSkill = new List<GameObject>();
        foreach (GameObject o in playerAttribute.skillsToLearn)
        {
            print(o.GetComponent<PlayerSkill>().nameOfSkill);
            if (o.GetComponent<PlayerSkill>().nameOfSkill == GameManager.Instance().getSkillChoosed())
            {
                print("learned");
                learnSkill(o);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance().pauseOrResumeGame();
        }

        if (Time.time < inActionUntil || GameManager.Instance().getPauseState())
        {
            return;
        }
        ProcessInput();

    }

    private void FixedUpdate()
    {
        if (dead)
        {
            playerAttribute.hp = 0;
            return;
        }
        if (playerAttribute.hp <= 0)
        {
            dead = true;
            StartCoroutine(playDeathAnimation());
            return;
        }
        if (!isGrounded)
        {
            groundType = "";
        }
        if (Time.time < inActionUntil)
        {
            animator.SetInteger("speedInt", 0);
            return;
        }
        if (moveDirection != 0)
        {
            animator.SetInteger("speedInt", 1);
        }
        else
        {
            animator.SetInteger("speedInt", 0);
        }
        if (groundType == "fireGround")
        {
            playerAttribute.hp -= 2f / playerAttribute.defense;
        }
        Move();
    }
    private IEnumerator playDeathAnimation()
    {
        animator.SetBool("Death", true);
        yield return new WaitForSeconds(1);
        GameManager.Instance().gameOver();
    }
    private void ProcessInput()
    {

        status = "";
        moveDirection = Input.GetAxis("Horizontal");


        if (Input.GetKeyDown(KeyCode.W) && currentJump < playerAttribute.jumpLimit)
        {
            isJumping = true;
            isGrounded = false;

            animator.SetTrigger("Jump");
            currentJump++;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (newArmorPart!=null)
            {
                string partOfArmor = newArmorPart.GetComponent<PlayerArmor>().partOfArmor;
                if (currentArmorParts.ContainsKey(partOfArmor))
                {
                    removeArmor(currentArmorParts[partOfArmor]);
                    
                }
                wearArmor(newArmorPart.GetComponent<PlayerArmor>());
                Destroy(newArmorPart.transform.parent.gameObject);
            }
            else
            {
                Vector2 attackPosition = gameObject.transform.position;
                attackPosition.x = attackPosition.x + 0.5f;
                StartCoroutine(releaseAttack(attackPosition, playerAttribute.weapon[0]));
            }
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
           
            Vector2 attackPosition = gameObject.transform.position;
            attackPosition.x = attackPosition.x + 0.5f;
            StartCoroutine(releaseAttack(attackPosition, playerAttribute.weapon[2]));
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
        if (learningNewSkill.Count != 0)
        {
            TextMeshPro textBox = textboxUI.GetComponentInChildren<TextMeshPro>();
            string newText = "Press a key in range [";
            foreach(KeyCode key in availableSkillsKey)
            {
                newText += key.ToString()+" , ";
            }
            newText = newText.Substring(0, newText.Length-2);
            newText += "] to learn the skill [";
            newText += learningNewSkill[0].GetComponent<PlayerSkill>().nameOfSkill+"]";


            textBox.SetText(newText);

            for (int i = 0; i < availableSkillsKey.Count; i++)
            {
                if (Input.GetKeyDown(availableSkillsKey[i]))
                {
                    KeyCode k = availableSkillsKey[i];
                    availableSkillsKey.RemoveAt(i);
                    currentSkillsKey.Add(k);
                    currentSkillsValueInstance.Add(learningNewSkill[0]);
                    learningNewSkill.RemoveAt(0);
                    textBox.SetText("");
                }
            }
        }
    }

    internal void learnSkill(GameObject gameObject)
    {
        GameObject skillInst=Instantiate(gameObject, transform.position, gameObject.transform.rotation);
        skillInst.transform.parent = transform;

        learningNewSkill.Add(skillInst);
        playerAttribute.skillsToLearn.Remove(gameObject);
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
    public void swapScale(int positive)
    {
        transform.localScale = new Vector3(positive*Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        textboxUI.transform.localScale= new Vector3(positive * Mathf.Abs(textboxUI.transform.localScale.x), textboxUI.transform.localScale.y, textboxUI.transform.localScale.z);
        playerAttribute.levelUI.transform.localScale = new Vector3(positive * Mathf.Abs(playerAttribute.levelUI.transform.localScale.x), playerAttribute.levelUI.transform.localScale.y, playerAttribute.levelUI.transform.localScale.z);
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
            swapScale(-1);
        }
        else if(moveDirection > 0 && faceingDirection != 1)
        {
            faceingDirection = 1;
            swapScale(1);
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
            animator.ResetTrigger("Jump");
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
    public void receiveRealPercentageDamage(float value, int direction, Vector2 force, float hitRecovery)
    {
        if (dead)
        {
            return;
        }
        if (status == "block")
        {
            if (playerAttribute.getBlockAttack() != null)
            {

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
        playerAttribute.hp -= value*playerAttribute.totalHealth;
        if (hitRecovery != 0)
        {

            inActionUntil = hitRecovery + Time.time;
            changeVelocity(force.x * direction, force.y);
            StartCoroutine(ResetForce(hitRecovery));
        }

        lastBeenHit = Time.time;
    }
    public void receiveDamage(float value, int direction, Vector2 force, float hitRecovery)
    {
        if (dead)
        {
            return;
        }
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
