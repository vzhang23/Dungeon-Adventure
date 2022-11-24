using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    // Start is called before the first frame update
    private bool active;
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            gameObject.SetActive(true);
        }
    }
    public void activeSelf()
    {
        active = true;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="player") {
            GameManager gameManager = GameManager.Instance();
            StartCoroutine(gameManager.NextStage());
        }
    }
}
