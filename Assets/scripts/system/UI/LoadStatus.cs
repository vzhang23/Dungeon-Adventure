using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadStatus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI textBox = gameObject.GetComponent<TextMeshProUGUI>();

        string newText = "";
        PlayerAttribute player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerAttribute>();
        newText += "Max HP: " + player.totalHealth + "\n";
        newText += "Current HP: " + player.hp + "\n";
        newText += "Max MP: " + player.totalMP + "\n";
        newText += "Current MP: " + player.mp + "\n";
        newText += "Attack: " + player.attack + "\n";
        newText += "Defense: " + player.defense + "\n";
        newText += "Speed: " + player.moveSpeed + "\n";
        newText += "Jump Times: " + player.jumpLimit + "\n";

        textBox.SetText(newText);
    }
}
