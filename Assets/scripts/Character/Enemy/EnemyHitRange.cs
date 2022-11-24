using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitRange : MonoBehaviour
{
    private Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = new Vector2(offset.x* transform.parent.GetComponent<EnemyController>().facingDirection,  offset.y);
        gameObject.transform.localPosition = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {

            transform.parent.GetComponent<EnemyController>().isAttacking = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag=="player")
        {
            transform.parent.GetComponent<EnemyController>().isAttacking = false;
        }
        
    }

}
