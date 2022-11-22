using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject equipmentToDrop;
    private string status;
    public float distanceToStaySafe;
    private float whenToChangeDirection;
    private float currentMovementSpeed;
    public GameObject attackIndicator;
    public GameObject sleepIndicator;
    private GameObject currentAttack;
    private float lastBeenHit;
    // Start is called before the first frame update
    void Start()
    {
        

        distanceToStaySafe = UnityEngine.Random.Range(2f, 4f);
        whenToChangeDirection = Time.time;
        status = "def";
        cooldownFinish = 0;
        inActionUntil = 0;
        isAttacking = false;
        isActive = false;
        enemyAttribute = gameObject.GetComponent<EnemyAttribute>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentMovementSpeed = enemyAttribute.moveSpeed * UnityEngine.Random.Range(0.2f, 0.5f);


    }

    private void Awake()
    {
        
        player = GameObject.FindGameObjectWithTag("player");
    }
    void LateUpdate()
    {

        try { 
            if(!alive)
            {
                return;
            }
            float distance = gameObject.transform.position.x - player.gameObject.transform.position.x;
            if (!isActive && (sleepIndicator.activeSelf==false && distance <= 5 ) || enemyAttribute.hp!=enemyAttribute.totalHealth )
            {
                rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                isActive = true;
                sleepIndicator.SetActive(false);
            }
            if (Time.time < inActionUntil)
            {
                return;
            }

            if (isActive)
            {

                if (status == "def" && Mathf.Abs(gameObject.transform.position.y - player.transform.position.y) <= 2)
                {
                    if (whenToChangeDirection <= Time.time)
                    {

                        currentMovementSpeed = enemyAttribute.moveSpeed * UnityEngine.Random.Range(0.4f, 0.8f);
                        whenToChangeDirection = Time.time + UnityEngine.Random.Range(0.7f, 1.5f);
                        if (gameObject.transform.position.x < player.transform.position.x)
                        {
                            if (gameObject.transform.position.x > player.transform.position.x - distanceToStaySafe)
                            {
                                facingDirection = -1;
                            }
                            else
                            {
                                facingDirection = 1;
                            }
                        }
                        else
                        {
                            if (gameObject.transform.position.x < player.transform.position.x + distanceToStaySafe)
                            {
                                facingDirection = 1;
                            }
                            else
                            {
                                facingDirection = -1;
                            }
                        }
                        if (UnityEngine.Random.Range(0, 100)/100f <= enemyAttribute.attackChance)
                        {
                            status = "att";
                        }
                    }
                }
                else if(status=="att")
                {
                    currentMovementSpeed = enemyAttribute.moveSpeed;
                    if (isAttacking)
                    {
                        StartCoroutine(releaseAttack(gameObject.transform.position));
                        status = "def";
                    }

                    if (gameObject.transform.position.x < player.transform.position.x)
                    {
                        facingDirection = 1;
                    }
                    else
                    {
                        facingDirection = -1;
                    }
                }
                else
                {
                    if (whenToChangeDirection <= Time.time)
                    {
                        whenToChangeDirection = Time.time + UnityEngine.Random.Range(1f, 2f);
                        facingDirection = facingDirection * -1;
                    }

                }

            }
        }
        catch (Exception)
        {

        }
    }
    public IEnumerator releaseAttack(Vector2 position)
    {
        if (cooldownFinish <= Time.time)
        {
            attackIndicator.SetActive(true);
            rb.velocity = new Vector2(0, 0);
            cooldownFinish = Time.time + enemyAttribute.cooldown;
            inActionUntil = Time.time + enemyAttribute.recovery;
            yield return new WaitForSeconds(enemyAttribute.waitBeforeAttack);
            currentAttack= Instantiate(enemyAttribute.weapon, position, enemyAttribute.weapon.transform.rotation);
            currentAttack.GetComponent<EnemyWeapon>().parent = gameObject;

            if (!enemyAttribute.weapon.GetComponent<EnemyWeapon>().follow)
            {
                currentAttack = null;
            }
            attackIndicator.SetActive(false);
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


            animator.SetFloat("Move X", facingDirection);
            animator.SetFloat("Move Y", 0);

            rb.velocity = new Vector2(currentMovementSpeed * facingDirection, rb.velocity.y);
        }

    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
    }

    //Public because we want to call it from elsewhere like the projectile script
    public void enemydefeat()
    {
        player.GetComponent<PlayerAttribute>().addExp(enemyAttribute.exp);
        if(UnityEngine.Random.Range(0, 1f) <= enemyAttribute.dropEquipmentChance)
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

    public void receiveDamage(float value, float attackPower, int direction, Vector2 force, float hitRecovery)
    {
        if (lastBeenHit + enemyAttribute.beenHitCooldown >= Time.time)
        {
            return;
        }
        if (attackPower> enemyAttribute.superArmor)
        {
            StopAllCoroutines();

            rb.velocity = new Vector2(force.x * direction, force.y);
            attackIndicator.SetActive(false);
            inActionUntil = hitRecovery + Time.time;
            try
            {
                Destroy(currentAttack);
            }
            catch (Exception) { }
        }
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
        lastBeenHit = Time.time;
        StartCoroutine(ResetForce(hitRecovery));

    }
    private IEnumerator ResetForce(float hitRecovery)
    {
        yield return new WaitForSeconds(hitRecovery);
        rb.velocity = Vector2.zero;
    }
}