using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatekeeper : MonoBehaviour
{
    public GameObject gate;
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
        try
        {

            gate.SetActive(true);
        }
        catch (Exception) { }
    }
}
