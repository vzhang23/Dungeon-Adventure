using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour, PlayerSkill
{
    public float lastUsed;
    public float forceMultiplier;
    [field: SerializeField] public float cooldown { get; set; }
    [field: SerializeField] public float duration { get; set; }
    [field: SerializeField] public float mp { get; set; }
    [field: SerializeField] public string nameOfSkill { get; set; }
    [field: SerializeField] public string description { get; set; }

    public void useSkill(GameObject player)
    {

        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        PlayerAttribute playerAttribute = player.GetComponent<PlayerAttribute>();
        if (playerAttribute.mp >= mp && lastUsed + cooldown <= Time.time) {
            playerAttribute.mp -= mp;
            lastUsed = Time.time;
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
