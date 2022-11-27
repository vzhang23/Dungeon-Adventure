using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChooseSkill : MonoBehaviour
{
    public GameObject saveObject;
    TMP_Dropdown dropDown;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        GameSave save = saveObject.GetComponent<GameSave>();
        List<string> listOfSkills = new List<string>();
        dropDown = transform.GetComponent<TMP_Dropdown>();
        foreach (string skill in save.unlockedSkills)
        {
            listOfSkills.Add(skill);
        }
        dropDown.ClearOptions();
        dropDown.AddOptions(listOfSkills);
        dropDown.SetValueWithoutNotify(0);
    }
    // Update is called once per frame
    void Update()
    {


        GameManager.Instance().setSkillChoosed(dropDown.options[dropDown.value].text);


    }
}
