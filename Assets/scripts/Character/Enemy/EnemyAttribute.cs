using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttribute : CommonAttribute
{

    public int exp;
    public float cooldown;
    public float recovery;
    public GameObject weapon;
    public float waitBeforeAttack;
    public int superArmor;
    public float beenHitCooldown;
    public float dropEquipmentChance;
    public float attackChance;
    void Start()
    {

        hp = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
