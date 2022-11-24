using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadPlayerSkillDropdownInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        PlayerMovement player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerMovement>();
        List<string> listOfSkills = new List<string>();
        foreach (GameObject skillObject in player.currentSkillsValueInstance)
        {
            listOfSkills.Add(skillObject.GetComponent<PlayerSkill>().nameOfSkill);
        }

        TMP_Dropdown dropDown = transform.GetComponent<TMP_Dropdown>();

        dropDown.ClearOptions();
        dropDown.AddOptions(listOfSkills);
        dropDown.SetValueWithoutNotify(0);
    }
}
