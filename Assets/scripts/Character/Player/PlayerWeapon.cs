using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerWeapon : MonoBehaviour
{
    public float attackMultiplier;
    public float duration;
    public float coolDown;
    public float recoveryTime;
    public Vector2 attactOffset;
    public Vector2 speed;
    public bool followPlayer;
    private Vector2 startingPosition;
    private float startTime;
    private int direction;
    private GameObject mainPlayer; 
    public Vector2 force;
    public float hitRecovery;
    public LayerMask enemyLayer;
    public int attackPower;
    public float waitBeforeAttack;
    public float moveUp;
    public float rotate;

    public int dealDamageTimes;
    private int currentDealDamageTimes;
    private string groundType;

    // Start is called before the first frame update
    void Start()
    {
        currentDealDamageTimes = 0;
        mainPlayer = GameObject.FindGameObjectsWithTag("player")[0];
        PlayerMovement cs = mainPlayer.GetComponent<PlayerMovement>();
        this.groundType = cs.groundType;
        Destroy(gameObject, duration);
        Vector2 currentOffset = new Vector2(attactOffset.x, attactOffset.y);
        GameObject player = GameObject.FindGameObjectsWithTag("player")[0];

        direction = cs.faceingDirection;
        gameObject.transform.position = new Vector2(player.transform.position.x + currentOffset.x* direction, player.transform.position.y + currentOffset.y);
        startingPosition = gameObject.transform.position;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
        float timePassed = Time.time - startTime;
        if (followPlayer)
        {
            Vector2 currentOffset = new Vector2(attactOffset.x + timePassed * speed.x, attactOffset.y + timePassed * speed.y);
            
            gameObject.transform.position = new Vector2(mainPlayer.transform.position.x + currentOffset.x* direction, mainPlayer.transform.position.y + currentOffset.y);
        }
        else
        {
            Vector2 currentOffset = new Vector2(startingPosition.x + timePassed * speed.x * direction, startingPosition.y + timePassed * speed.y);
            gameObject.transform.position = currentOffset;
        }
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + moveUp *(timePassed/duration));
        gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z - rotate * (timePassed / duration) * direction);
    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentDealDamageTimes >= dealDamageTimes)
        {
            Destroy(gameObject);
        }
        if (enemyLayer == (enemyLayer | (1 << collision.gameObject.layer)) && currentDealDamageTimes <= dealDamageTimes)
        {
            PlayerAttribute player = mainPlayer.gameObject.GetComponent<PlayerAttribute>();
            float fixAttack = 1;
            if (groundType == "fireGround")
            {
                fixAttack = 1.3f;
            }
            collision.gameObject.GetComponent<EnemyController>().receiveDamage(player.attack * attackMultiplier* fixAttack, attackPower, direction, force, hitRecovery);
            if (currentDealDamageTimes >= dealDamageTimes)
            {
                Destroy(gameObject);
            }
        }
    }

}
