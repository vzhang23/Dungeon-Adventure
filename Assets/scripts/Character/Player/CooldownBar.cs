using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownBar : MonoBehaviour
{
    Vector3 localScale;
    GameObject parent;
    private float max_scale;
    private float max_cooldown_time;
    private float thisInActionUntil;
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
        PlayerMovement cs = parent.GetComponent<PlayerMovement>();
        if (cs.inActionUntil > Time.time) {
            if (thisInActionUntil != cs.inActionUntil)
            {
                max_cooldown_time = Time.time;
                thisInActionUntil = cs.inActionUntil;
            }

            localScale.x = max_scale * (cs.inActionUntil - Time.time) / (cs.inActionUntil - max_cooldown_time);
        }
        else
        {

            localScale.x = 0;
        }
        transform.localScale = localScale;
    }
}
