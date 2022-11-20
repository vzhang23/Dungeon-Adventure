using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour, PlayerSkill
{
    public float duration;
    public float cooldown;
    public float lastUsed;
    public float healAmount;
    public int mp;

    public void useSkill(GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        PlayerAttribute playerAttribute = player.GetComponent<PlayerAttribute>();
        if (playerAttribute.mp >= mp && lastUsed + cooldown <= Time.time)
        {
            playerAttribute.mp -= mp;
            playerAttribute.hp = Mathf.Min(playerAttribute.totalHealth, playerAttribute.hp+healAmount);
            playerMovement.changeVelocity(0, 0);
            playerMovement.inActionUntil = Time.time + duration;
            lastUsed = Time.time;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
