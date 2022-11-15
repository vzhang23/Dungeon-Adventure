using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Vector3 localScale;
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateLength()
    {
        EnemyAttribute ea = parent.GetComponent<EnemyAttribute>();
        localScale.x = localScale.x * ea.getHP() / ea.totalHealth;
        transform.localScale = localScale;
    }
}
