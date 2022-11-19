using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTemplate : MonoBehaviour
{
    public List<GameObject> enemyList;
    public List<float> chance;
    // Start is called before the first frame update
    void Start()
    {
        float totalChance = 0;
        for(int i = 0; i < chance.Count; i++)
        {
            totalChance += chance[i];
        }
        float previousChange = 0.0f;
        for(int i = 0; i < chance.Count; i++)
        {
            if (UnityEngine.Random.Range(0, totalChance- previousChange ) <=  chance[i])
            {
                Instantiate(enemyList[i], gameObject.transform.position, enemyList[i].transform.rotation);
                break;
            }
            previousChange += chance[i];
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
