using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeAttack : MonoBehaviour, PlayerSkill
{
    public float duration;
    public float cooldown;
    public float lastUsed=0;
    public float forceMultiplier;

    public void useSkill(GameObject player)
    {
        if (lastUsed + cooldown <= Time.time) {
            lastUsed = Time.time;
            CharacterMovement playerMovement = player.GetComponent<CharacterMovement>();
            PlayerAttribute playerattribute = player.GetComponent<PlayerAttribute>();
            playerMovement.applyVelocity(forceMultiplier * playerattribute.moveSpeed * playerMovement.faceingDirection * duration, playerMovement.getVelocity().y);
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
