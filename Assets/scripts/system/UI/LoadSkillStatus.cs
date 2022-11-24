using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadSkillStatus : MonoBehaviour
{
    public GameObject skillDropdown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI textBox = gameObject.GetComponent<TextMeshProUGUI>();
        TMP_Dropdown dropdown = skillDropdown.GetComponent<TMP_Dropdown>();

        string newText = "No skill";
        if (dropdown.options.Count != 0)
        {
            string skillName = dropdown.options[dropdown.value].text;
            PlayerMovement player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerMovement>();
            foreach(GameObject skillObject in player.currentSkillsValueInstance)
            {
                PlayerSkill skill = skillObject.GetComponent<PlayerSkill>();
                if (skillName == skill.nameOfSkill)
                {
                    int i = player.currentSkillsValueInstance.IndexOf(skillObject);
                    KeyCode key= player.currentSkillsKey[i];
                    newText = skill.description + "\n"+
                        "Cooldown: " + skill.cooldown+"\n" +
                        "duration: " + skill.duration + "\n" +
                        "mp usage: " + skill.mp + "\n" +
                        "press " + key.ToString() + " to use";
                    textBox.SetText(newText);
                    return;
                }
            }

        }

        textBox.SetText(newText);

    }
}
