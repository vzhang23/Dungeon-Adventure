using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private bool used;
    void Start()
    {
        used = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="player" && !used) {
            GameManager gameManager = GameManager.Instance();
            gameManager.NextStage();
            used = true;
        }
    }
}
