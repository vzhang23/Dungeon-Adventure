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
    private bool dealDamage;
    public Vector2 force;
    public float hitRecovery;
    public LayerMask enemyLayer;
    // Start is called before the first frame update
    void Start()
    {
        mainPlayer = GameObject.FindGameObjectsWithTag("player")[0];
        CharacterMovement cs = mainPlayer.GetComponent<CharacterMovement>();
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (enemyLayer == (enemyLayer | (1 << collision.gameObject.layer)) && !dealDamage)
        {
            dealDamage = true;
            PlayerAttribute player = mainPlayer.gameObject.GetComponent<PlayerAttribute>();
            collision.gameObject.GetComponent<EnemyController>().receiveDamage(player.attack * attackMultiplier, direction, force, hitRecovery);
        }
    }

}