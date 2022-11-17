using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPBar : MonoBehaviour
{
    Vector3 localScale;
    GameObject parent;
    private float max_scale;
    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        localScale = transform.localScale;
        max_scale = localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        updateLength();
    }

    public void updateLength()
    {
        PlayerAttribute attribute = parent.GetComponent<PlayerAttribute>();
        localScale.x = max_scale * attribute.mp / attribute.totalMP;
        transform.localScale = localScale;
    }
}
