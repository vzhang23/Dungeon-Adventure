using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepObjectOnLoad : MonoBehaviour
{
    public List<GameObject> objectToKeep;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        foreach (GameObject i in objectToKeep)
        {
            DontDestroyOnLoad(i);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        foreach (GameObject i in objectToKeep)
        {
            Destroy(i);
        }
    }
    public void activeObject()
    {

        foreach (GameObject i in objectToKeep)
        {
            i.SetActive(true);
        }
    }

    public void unactiveObject()
    {

        foreach (GameObject i in objectToKeep)
        {
            i.SetActive(false);
        }
    }
    public GameObject getObjectByTag(string tag)
    {

        foreach (GameObject i in objectToKeep)
        {
            if (i.tag == tag)
            {
                return i;
            }
        }
        return null;
    }
}
