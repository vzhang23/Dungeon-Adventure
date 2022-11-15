using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyAttribute enemyAttribute;
    
    Rigidbody2D rb;
    float timer;
    public int facingDirection = 1;
    bool broken = true;
    Animator animator;
    public bool isActive;
    GameObject player;
    public bool isAttacking;
    public float inActionUntil;
    public float cooldownFinish;
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
        if(!broken)
        {
            return;
        }
        if (GetComponent<Renderer>().isVisible)
        {
            isActive = true;
        }
        if (Time.time < inActionUntil)
        {
            return;
        }
        if (isActive)
        {
            if(gameObject.transform.position.x < player.transform.position.x)
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
        if (broken)
        {
            Vector2 position = rb.position;


            position.x = position.x + Time.deltaTime * enemyAttribute.moveSpeed * facingDirection;
            animator.SetFloat("Move X", facingDirection);
            animator.SetFloat("Move Y", 0);

            rb.MovePosition(position);
        }

    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
    }
    
    //Public because we want to call it from elsewhere like the projectile script
    public void Fix()
    {
        broken = false;
        rb.simulated = false;
        //optional if you added the fixed animation
        animator.SetTrigger("Fixed");
    }

    private void Move()
    {
        rb.velocity = new Vector2(facingDirection * enemyAttribute.moveSpeed, rb.velocity.y);
    }


}