using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour, PlayerSkill
{
    public float duration;
    public float cooldown;
    public float lastUsed=0;
    public float healAmount;
    public int mp;

    public void useSkill(GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        PlayerAttribute playerattribute = player.GetComponent<PlayerAttribute>();
        if (playerattribute.mp >= mp && lastUsed + cooldown <= Time.time)
        {
            playerattribute.mp -= mp;
            playerattribute.hp = Mathf.Min(playerattribute.totalHealth, playerattribute.hp+healAmount);
            playerMovement.changeVelocity(0, 0);
            playerMovement.inActionUntil = Time.time + duration;

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
