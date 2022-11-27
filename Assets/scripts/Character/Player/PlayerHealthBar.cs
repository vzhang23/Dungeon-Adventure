using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    Vector3 localScale;
    GameObject player;
    private float max_scale;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        localScale = transform.localScale;
        max_scale = localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        updateLength();
    }

    public void updateLength()
    {
        CommonAttribute attribute = player.GetComponent<CommonAttribute>();
        localScale.x = max_scale * attribute.hp / attribute.totalHealth;
        transform.localScale = localScale;
    }
}
