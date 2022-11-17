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
    private bool dealDamage;
    public Vector2 force;
    public float hitRecovery;
    // Start is called before the first frame update
    void Start()
    {
        dealDamage = false;
        EnemyController cs = parent.GetComponent<EnemyController>();
        Destroy(gameObject, duration);
        Vector2 currentOffset = new Vector2(attactOffset.x, attactOffset.y);

        direction = cs.facingDirection;
        gameObject.transform.position = new Vector2(parent.transform.position.x + currentOffset.x * direction, parent.transform.position.y + currentOffset.y);
        startingPosition = gameObject.transform.position;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            float timePassed = Time.time - startTime;
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
        }catch(System.Exception e)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (collision.gameObject.tag == "player" && !dealDamage)
            {
                dealDamage = true;
                EnemyAttribute ea = parent.GetComponent<EnemyAttribute>();
                collision.gameObject.GetComponent<PlayerMovement>().receiveDamage(ea.attack * attackMultiplier, direction, force, hitRecovery);
            }
            }
        catch (System.Exception e)
        {

        }
    }

}
