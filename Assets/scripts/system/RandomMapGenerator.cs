using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapGenerator : MonoBehaviour
{
    public List<GameObject> mapMiddleList;
    public List<GameObject> mapEndList;
    public int sizeOfMap;
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < sizeOfMap-1; i++) {
            int e = Random.Range(0, mapMiddleList.Count);
            Instantiate(mapMiddleList[e], new Vector3(i*20, 0, 0), mapMiddleList[e].transform.rotation);


        }
        int ra = Random.Range(0, mapEndList.Count);
        Instantiate(mapEndList[ra], new Vector3(sizeOfMap * 20-20, 0, 0), mapEndList[ra].transform.rotation);
        if (GameObject.FindGameObjectsWithTag("GameManager").Length == 0)
        {
            Instantiate(gameManager, new Vector3(0, 0, 0), gameManager.transform.rotation);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
