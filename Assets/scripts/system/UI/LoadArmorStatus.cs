using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadArmorStatus : MonoBehaviour
{
    public GameObject armorDropdown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI textBox = gameObject.GetComponent<TextMeshProUGUI>();
        TMP_Dropdown dropdown = armorDropdown.GetComponent<TMP_Dropdown>();

        string newText = "No armor equiped";
        if (dropdown.options.Count != 0)
        {
            string partOfArmor = dropdown.options[dropdown.value].text;
            PlayerMovement player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerMovement>();
            PlayerArmor armor = player.currentArmorParts[partOfArmor];
            newText = string.Format("Part: {0} \n HP multiplier: {1}\n MP multiplier: {2}\n ATTACK multiplier: {3}\n DEFENSE multiplier: {4}\n JUMP LIMIT increase: {5}\n SPEED increase: {6}", armor.partOfArmor, armor.hp, armor.mp, armor.attack, armor.defense, armor.jumpLimit, armor.moveSpeed);
            textBox.SetText(newText);
        }
       
       textBox.SetText(newText);
    }

}
