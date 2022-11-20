using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttribute : CommonAttribute
{
    private int[] expCurve = {0, 10, 40, 90, 150,220, 300, 450,600, 800, 1000, 1200, 1000000 };
    public GameObject[] skillsToLearn;
    public int[] learnLevel = { 2, 4, 7, 10};
    public float hpMultiplier = 1.2f;
    public float mpMultiplier = 1.2f;
    public float totalMP;
    public float mp;
    public int level;
    public int exp;
    public int jumpLimit;
    public List<GameObject> weapon;

    public GameObject blockAttack;
    public GameObject levelUI;
    void Start()
    {
        mp = totalMP;
        hp = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void wearArmor(PlayerArmor armor)
    {
        totalHealth *= armor.hp;
        totalMP *= armor.mp;
        hp *= armor.hp;
        mp *= armor.mp;
        attack *= armor.attack;
        defense *= armor.defense;
        jumpLimit += armor.jumpLimit;
        moveSpeed += armor.moveSpeed;
    }

    public void removeArmor(PlayerArmor armor)
    {
        totalHealth /= armor.hp;
        totalMP /= armor.mp;
        hp /= armor.hp;
        mp /= armor.mp;
        attack /= armor.attack;
        defense /= armor.defense;
        jumpLimit -= armor.jumpLimit;
        moveSpeed -= armor.moveSpeed;
    }
    public void levelUp()
    {
        totalHealth *= hpMultiplier;
        totalMP *= mpMultiplier;
        hp = totalHealth;
        mp = totalMP;

        TextMeshPro textBox = levelUI.GetComponentInChildren<TextMeshPro>();
        string newText = string.Format("level-" + level);
        textBox.SetText(newText);
        foreach (int i in learnLevel) {
            if (i == level)
            {
                int e = UnityEngine.Random.Range(0, skillsToLearn.Length);
                gameObject.GetComponent<PlayerMovement>().learnSkill(skillsToLearn[e]);
            }
        
        }
    }
    internal void addExp(int exp)
    {
        this.exp += exp;

        if (this.exp >= expCurve[level])
        {
            level++;
            levelUp();
        }
    }
    public int[] getExpCurve()
    {
        return expCurve;
    }
    public void setBlockAttack(GameObject blockAttack)
    {
        this.blockAttack = blockAttack;
    }
    public GameObject getBlockAttack()
    {
        return blockAttack;
    }
}
