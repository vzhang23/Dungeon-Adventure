using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : MonoBehaviour, PlayerSkill
{
    public float duration;
    public float cooldown;
    private float lastUsed=0;
    public float forceMultiplier;
    public float mp;
    public void useSkill(GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        PlayerAttribute playerAttribute = player.GetComponent<PlayerAttribute>();
        if (playerAttribute.mp >= mp && lastUsed + cooldown <= Time.time)
        {
            PlayerWeapon weapon = playerAttribute.weapon[0].GetComponent<PlayerWeapon>();
            playerAttribute.mp -= mp;
            lastUsed = Time.time;
            playerMovement.inActionUntil = Time.time + duration;
            Instantiate(playerAttribute.weapon[0], player.transform.position, playerAttribute.weapon[0].transform.rotation);
            playerMovement.changeVelocity(forceMultiplier * playerAttribute.moveSpeed * playerMovement.faceingDirection * duration, playerMovement.getVelocity().y);
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
