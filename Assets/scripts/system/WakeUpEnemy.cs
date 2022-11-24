using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpEnemy : MonoBehaviour
{
    public GameObject[] enemyList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        foreach(GameObject enemy in enemyList)
        {
            enemy.GetComponent<EnemyController>().sleepIndicator.SetActive(false);
        }
    }
}
