using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapGenerator : MonoBehaviour
{
    public List<GameObject> mapMiddleList;
    public List<GameObject> mapEndList;
    public int sizeOfMap;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < sizeOfMap-1; i++) {
            int e = Random.Range(0, mapMiddleList.Count);
            print(e);
            Instantiate(mapMiddleList[e], new Vector3(i*20, 0, 0), mapMiddleList[e].transform.rotation);


        }
        int ra = Random.Range(0, mapEndList.Count);
        print(ra);
        Instantiate(mapEndList[ra], new Vector3(sizeOfMap * 20-20, 0, 0), mapEndList[ra].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
