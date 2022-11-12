using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapGenerator : MonoBehaviour
{
    public List<GameObject> mapList;
    public int sizeOfMap;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < sizeOfMap-1; i++) {
            int e = Random.Range(0, mapList.Count);
            print(e);
            Instantiate(mapList[e], new Vector3(i*20, 0, 0), mapList[e].transform.rotation);


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
