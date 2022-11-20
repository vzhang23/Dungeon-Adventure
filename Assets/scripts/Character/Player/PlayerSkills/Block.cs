using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, PlayerSkill
{
    public float duration;
    public float cooldown;
    public float lastUsed;
    public GameObject blockAttack;
    public void useSkill(GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        PlayerAttribute playerAttribute = player.GetComponent<PlayerAttribute>();
        if (lastUsed + cooldown <= Time.time)
        {
            playerMovement.changeVelocity(0, 0);
            playerMovement.status = "block";
            playerAttribute.setBlockAttack(blockAttack);
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
