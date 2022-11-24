using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int phase;
    private EnemyAttribute attribute;
    public GameObject[] enemySpawner;
    public float spawnerCooldown;
    private float lastUse;
    public GameObject[] gatekeeper;
    public float flyingSpeed;
    public GameObject bossFightWall;
    private EnemyController controller;
    // Start is called before the first frame update
    void Start()
    {
        phase = 1;
        lastUse = 0;
        attribute = gameObject.GetComponent<EnemyAttribute>();
        controller = gameObject.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        checkChangePhase();
        if (lastUse + spawnerCooldown <= Time.time && controller.isActive)
        {

            if (!bossFightWall.activeSelf)
            {
                bossFightWall.SetActive(true);
            }
            attack();
            lastUse = Time.time;
        }
    }
    private void checkChangePhase()
    {
        if (attribute.hp / attribute.totalHealth <= 0.3 && phase==3)
        {
            phase = 4;
            spawnerCooldown = spawnerCooldown * 0.8f;
            attribute.cooldown *= 0.5f;
            attribute.recovery *= 0.5f;
            attribute.defense += 10;
            spawn(gatekeeper[1]);
        }
        else if (attribute.hp / attribute.totalHealth <= 0.5 && phase == 2)
        {
            phase = 3;
            spawnerCooldown = spawnerCooldown * 0.8f;
            attribute.cooldown *= 0.7f;
            attribute.recovery *= 0.7f;
            attribute.attack += 5;
            attribute.defense += 10;
            spawn(gatekeeper[0]);
        }
        else if (attribute.hp/ attribute.totalHealth <= 0.7 && phase == 1)
        {
            phase = 2;
            spawnerCooldown = spawnerCooldown * 0.8f;
            attribute.cooldown *= 0.9f;
            attribute.recovery *= 0.9f;
        }
        
    }
    private void attack()
    {
        spawn(enemySpawner[0]);
        if (phase >= 2)
        {
            spawn(enemySpawner[1]);
        }
        if (phase == 3)
        {
            spawn(enemySpawner[2]);
        }
    }
    private void spawn(GameObject spawner)
    {
       Instantiate(spawner, transform.position, spawner.transform.rotation);
    }
}
