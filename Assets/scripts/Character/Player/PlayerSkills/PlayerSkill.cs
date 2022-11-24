using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface PlayerSkill
{
    public string nameOfSkill
    {
        get;
        set;
    }
    public string description
    {
        get;
        set;
    }

    public float duration
    {
        get;
        set;
    }
    public float cooldown
    {
        get;
        set;
    }

    public float mp
    {
        get;
        set;
    }
    public void useSkill(GameObject player);
}
