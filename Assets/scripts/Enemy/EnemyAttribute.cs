using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttribute : MonoBehaviour
{
    public float totalHealth;
    private float hp;
    public float attack;
    public float defense;
    public float moveSpeed;
    public float cooldown;
    public float recovery;
    public GameObject weapon;
    public float waitBeforeAttack;
    void Start()
    {
        hp = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setHP(float newHP)
    {
        hp = newHP;
    }
    public float getHP()
    {
        return hp;
    }
}
