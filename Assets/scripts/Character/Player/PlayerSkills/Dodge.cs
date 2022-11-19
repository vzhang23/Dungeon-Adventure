using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour, PlayerSkill
{
    public float duration;
    public float cooldown;
    private float lastUsed=0;
    public float forceMultiplier;

    public void useSkill(GameObject player)
    {
        if (lastUsed + cooldown <= Time.time) {
            lastUsed = Time.time;
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            PlayerAttribute playerAttribute = player.GetComponent<PlayerAttribute>();
            playerMovement.changeVelocity(forceMultiplier * playerAttribute.moveSpeed * playerMovement.faceingDirection * duration, playerMovement.getVelocity().y);
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
