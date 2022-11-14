using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerWeapon : MonoBehaviour
{
    public float attackMultiplier;
    public float duration;
    public Vector2 attactOffset;
    public Vector2 speed;
    public bool followPlayer;
    private Vector2 startingPosition;
    private float startTime;
    private int direction;
    private GameObject mainPlayer ;
    // Start is called before the first frame update
    void Start()
    {
        mainPlayer = GameObject.FindGameObjectsWithTag("player")[0];
        CharacterMovement cs = mainPlayer.GetComponent<CharacterMovement>();
        Object.Destroy(gameObject, duration);
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
}
