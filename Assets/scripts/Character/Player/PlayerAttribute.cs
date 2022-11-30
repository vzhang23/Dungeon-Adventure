using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttribute : CommonAttribute
{
    private int[] expCurve = {0, 10, 40, 90, 150,220, 300, 450,600, 800, 1000, 1200, 1500, 1800, 2100, 2500, 3000, 3500, 4000, 5000};
    public List<GameObject> skillsToLearn;
    public int[] learnLevel = { 2, 4, 7, 10};
    private float hpMultiplier;
    private float mpMultiplier;
    private float defenseMultiplier;
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
        hpMultiplier = 1.2f;
        mpMultiplier = 1.2f;
        defenseMultiplier = 1.05f;
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
        print(defenseMultiplier);
        totalHealth *= hpMultiplier;
        totalMP *= mpMultiplier;
        hp = totalHealth;
        mp = totalMP;
        defense *= defenseMultiplier;

        TextMeshPro textBox = levelUI.GetComponentInChildren<TextMeshPro>();
        string newText = string.Format("level-" + level);
        textBox.SetText(newText);
        foreach (int i in learnLevel) {
            if (i == level)
            {
                int e = UnityEngine.Random.Range(0, skillsToLearn.Count);
                gameObject.GetComponent<PlayerMovement>().learnSkill(skillsToLearn[e]);
            }
        
        }
        addExp(0);
    }
    internal void addExp(int exp)
    {
        this.exp += exp;
        if (level >= 20) {
            if (this.exp >= expCurve[19]*Mathf.Pow(1.5f, level-19))
            {
                level++;
                levelUp();
            }
        }else if (this.exp >= expCurve[level])
        {
            level++;
            levelUp();
        }
    }
    public int getExpCurve(int i)
    {
        if (i >= 20)
        {
            return (int)(expCurve[19] * Mathf.Pow(1.5f, i - 19));
        }
        return expCurve[i];
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
