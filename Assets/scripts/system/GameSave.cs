using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSave : MonoBehaviour
{
    public List<string> allSkills;
    public List<string> unlockedSkills;
    // Start is called before the first frame update
    void Start()
    {
        unlockedSkills =new List<string>();
        foreach (string s in allSkills)
        {
            if (PlayerPrefs.GetInt(s) == 1)
            {
                unlockedSkills.Add(s);
            }
        }
    }
    public void save()
    {
        foreach (string s in unlockedSkills)
        {
            PlayerPrefs.SetInt(s, 1);
        }

        PlayerPrefs.Save();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
