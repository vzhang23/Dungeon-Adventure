using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmor : MonoBehaviour
{
    private string[] rangeOfPart= {"helmet", "chest plate", "leggings", "boot"};
    public string partOfArmor; 
    public float hp;
    public float mp;
    public float attack;
    public float defense;
    public int jumpLimit;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        partOfArmor = rangeOfPart[Random.Range(0, rangeOfPart.Length)];
        hp = Random.Range(8, 13)/10f;
        mp = Random.Range(8, 20) / 10f;
        attack = Random.Range(8, 13) / 10f;
        defense = Random.Range(8, 13) / 10f;
        jumpLimit = Random.Range(0, 2);
        moveSpeed= Random.Range(-1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
