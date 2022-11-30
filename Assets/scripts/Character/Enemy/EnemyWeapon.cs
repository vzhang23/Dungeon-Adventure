using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyWeapon : MonoBehaviour
{

    public float attackMultiplier;
    public float duration;
    public Vector2 attactOffset;
    public Vector2 speed;
    public bool follow;
    private Vector2 startingPosition;
    private float startTime;
    private int direction;
    public GameObject parent;
    public int dealDamageTimes;
    private int currentDealDamageTimes;
    private float lastDealDamage;
    public float dealDamageCooldown;
    public Vector2 force;
    public float hitRecovery;
    public bool targetPlayer;
    private GameObject player;
    private Rigidbody2D rb;

    public float moveUp;
    public float rotate;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastDealDamage = 0;
        currentDealDamageTimes = 0;
        EnemyController cs = parent.GetComponent<EnemyController>();
        Destroy(gameObject, duration);
        Vector2 currentOffset = new Vector2(attactOffset.x, attactOffset.y);

        direction = cs.facingDirection;
        gameObject.transform.position = new Vector2(parent.transform.position.x + currentOffset.x * direction, parent.transform.position.y + currentOffset.y);
        startingPosition = gameObject.transform.position;
        startTime = Time.time;
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        float timePassed = Time.time - startTime;
        if (currentDealDamageTimes == dealDamageTimes)
        {
            Destroy(gameObject);
        }
        if (targetPlayer)
        {
            float newSpeedX = rb.velocity.x + speed.x * 0.01f * (transform.position.x <= player.transform.position.x ? 1 : -1);
            float newSpeedY = rb.velocity.y + speed.y * 0.01f * (transform.position.y <= player.transform.position.y ? 1 : -1);
            if (newSpeedX >= speed.x)
            {
                newSpeedX = speed.x;
            }else if (newSpeedX <= -speed.x)
            {
                newSpeedX = -speed.x;
            }
            if (newSpeedY >= speed.y)
            {
                newSpeedY = speed.y;
            }
            else if (newSpeedY <= -speed.y)
            {
                newSpeedY = -speed.y;
            }
            rb.velocity = new Vector2(newSpeedX, newSpeedY);
            if (rb.velocity.x >= speed.x)
            {

            }
            return;
        }
        try
        {
            if (follow)
            {
                Vector2 currentOffset = new Vector2(attactOffset.x + timePassed * speed.x, attactOffset.y + timePassed * speed.y);

                gameObject.transform.position = new Vector2(parent.transform.position.x + currentOffset.x * direction, parent.transform.position.y + currentOffset.y);
            }
            else
            {
                Vector2 currentOffset = new Vector2(startingPosition.x + timePassed * speed.x * direction, startingPosition.y + timePassed * speed.y);
                gameObject.transform.position = currentOffset;
            }
        }catch(System.Exception)
        {

        }

        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + moveUp * (timePassed / duration));
        gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z - rotate * (timePassed / duration) * direction);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        try
        {
            if (collision.gameObject.tag == "player" && currentDealDamageTimes<=dealDamageTimes && lastDealDamage+dealDamageCooldown<=Time.time)
            {
                lastDealDamage = Time.time;
                currentDealDamageTimes++;
                EnemyAttribute ea = parent.GetComponent<EnemyAttribute>();
                collision.gameObject.GetComponent<PlayerMovement>().receiveDamage(ea.attack * attackMultiplier, direction, force, hitRecovery);
                if (currentDealDamageTimes >= dealDamageTimes)
                {
                    Destroy(gameObject);
                }
            }
            }
        catch (System.Exception)
        {

        }
        if(currentDealDamageTimes > dealDamageTimes)
        {
            Destroy(gameObject);
        }
    }

}
