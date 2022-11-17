using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyAttribute enemyAttribute;
    
    Rigidbody2D rb;
    float timer;
    public int facingDirection = 1;
    bool alive = true;
    Animator animator;
    public bool isActive;
    GameObject player;
    public bool isAttacking;
    public float inActionUntil;
    public float cooldownFinish;
    public GameObject healthBar;
    public float dropEquipmentChance;
    public GameObject equipmentToDrop;
    // Start is called before the first frame update
    void Start()
    {
        cooldownFinish = 0;
        inActionUntil = 0;
        isAttacking = false;
        isActive = false;
        enemyAttribute = gameObject.GetComponent<EnemyAttribute>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    private void Awake()
    {
        
        player = GameObject.FindGameObjectWithTag("player");
    }
    void Update()
    {
        try { 
            if(!alive)
            {
                return;
            }
            float distance = gameObject.transform.position.x - player.gameObject.transform.position.x;
            if (distance<=5 || enemyAttribute.hp!=enemyAttribute.totalHealth )
            {
                rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                isActive = true;
            }
            if (Time.time < inActionUntil)
            {
                return;
            }
            if (isActive)
            {
                if (gameObject.transform.position.x < player.transform.position.x)
                {
                    facingDirection = 1;
                }
                else
                {
                    facingDirection = -1;
                }
                if (isAttacking)
                {
                    StartCoroutine(releaseAttack(gameObject.transform.position));
                }
            }
        }
        catch (Exception e)
        {

        }
    }
    public IEnumerator releaseAttack(Vector2 position)
    {
        if (cooldownFinish <= Time.time)
        {
            rb.velocity = new Vector2(0, 0);
            cooldownFinish = Time.time + enemyAttribute.cooldown;
            inActionUntil = Time.time + enemyAttribute.recovery;
            yield return new WaitForSeconds(enemyAttribute.waitBeforeAttack);
            GameObject proj = Instantiate(enemyAttribute.weapon, position, enemyAttribute.weapon.transform.rotation);
            proj.GetComponent<EnemyWeapon>().parent = gameObject;
        }

    }
    void FixedUpdate()
    {

        if (Time.time < inActionUntil)
        {
            return;
        }
        if (alive)
        {


            /*animator.SetFloat("Move X", facingDirection);
            animator.SetFloat("Move Y", 0);*/

            rb.velocity = new Vector2(enemyAttribute.moveSpeed * facingDirection, rb.velocity.y);
        }

    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
    }
    
    //Public because we want to call it from elsewhere like the projectile script
    public void enemydefeat()
    {
        player.GetComponent<PlayerAttribute>().addExp(enemyAttribute.exp);
        if(UnityEngine.Random.Range(0, 1f) <= dropEquipmentChance)
        {
            Instantiate(equipmentToDrop, gameObject.transform.position, equipmentToDrop.transform.rotation);
        }
        //should add dealth animation later
        Destroy(gameObject);
    }

    private void Move()
    {
        rb.velocity = new Vector2(facingDirection * enemyAttribute.moveSpeed, rb.velocity.y);
    }

    public void receiveDamage(float value, int direction, Vector2 force, float hitRecovery)
    {
        StopAllCoroutines();
        float newHealth = enemyAttribute.hp - value*value/ enemyAttribute.defense;
        if (newHealth <= 0)
        {
            newHealth = 0;
        }
        enemyAttribute.hp=newHealth;
        healthBar.GetComponent<HealthBar>().updateLength();
        if (enemyAttribute.hp<= 0)
        {
            enemydefeat();
        }
        inActionUntil = hitRecovery + Time.time;
        rb.velocity = new Vector2(force.x * direction, force.y);
        StartCoroutine(ResetForce(hitRecovery));

    }
    private IEnumerator ResetForce(float hitRecovery)
    {
        yield return new WaitForSeconds(hitRecovery);
        rb.velocity = Vector2.zero;
    }
}