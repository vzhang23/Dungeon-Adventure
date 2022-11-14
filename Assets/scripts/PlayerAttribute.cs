using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttribute : MonoBehaviour
{
    public int level;
    public int exp;
    public float hp;
    public float mp;
    public float attack;
    public float defense;
    public int jumpLimit;
    public float moveSpeed;
    public List<GameObject> weapon;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void wearArmor(PlayerArmor armor)
    {
        hp *= armor.hp;
        mp *= armor.mp;
        attack *= armor.attack;
        defense *= armor.defense;
        jumpLimit += armor.jumpLimit;
        moveSpeed += armor.moveSpeed;
    }

    public void removeArmor(PlayerArmor armor)
    {
        hp /= armor.hp;
        mp /= armor.mp;
        attack /= armor.attack;
        defense /= armor.defense;
        jumpLimit -= armor.jumpLimit;
        moveSpeed -= armor.moveSpeed;
    }
}
