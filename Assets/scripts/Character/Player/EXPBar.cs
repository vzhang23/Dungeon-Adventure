using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPBar : MonoBehaviour
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
        float scale;
        if (attribute.level == 20)
        {
            scale = 0;
        }
        else
        {
            scale = (attribute.exp- attribute.getExpCurve()[attribute.level-1])*1.0f /( attribute.getExpCurve()[attribute.level] - attribute.getExpCurve()[attribute.level - 1]);
        }
        localScale.x = scale*max_scale;
        transform.localScale = localScale;
    }
}
