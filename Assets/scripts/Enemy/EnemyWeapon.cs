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
    // Start is called before the first frame update
    void Start()
    {
        EnemyController cs = parent.GetComponent<EnemyController>();
        Destroy(gameObject, duration);
        Vector2 currentOffset = new Vector2(attactOffset.x, attactOffset.y);

        direction = cs.facingDirection;
        gameObject.transform.position = new Vector2(parent.transform.position.x + currentOffset.x* direction, parent.transform.position.y + currentOffset.y);
        startingPosition = gameObject.transform.position;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        print(attactOffset.x);

        float timePassed = Time.time - startTime;
        if (follow)
        {
            Vector2 currentOffset = new Vector2(attactOffset.x + timePassed * speed.x, attactOffset.y + timePassed * speed.y);
            
            gameObject.transform.position = new Vector2(parent.transform.position.x + currentOffset.x* direction, parent.transform.position.y + currentOffset.y);
        }
        else
        {
            Vector2 currentOffset = new Vector2(startingPosition.x + timePassed * speed.x * direction, startingPosition.y + timePassed * speed.y);
            gameObject.transform.position = currentOffset;
        }
    }
}
