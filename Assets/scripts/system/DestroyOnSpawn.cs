using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnSpawn : MonoBehaviour
{
    public float destroyRate;
    void Start()
    {
        if(Random.Range(0, 1f) <= destroyRate)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
