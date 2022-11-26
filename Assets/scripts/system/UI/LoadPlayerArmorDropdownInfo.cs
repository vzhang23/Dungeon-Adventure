using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadPlayerArmorDropdownInfo : MonoBehaviour
{
    TMP_Dropdown dropDown;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        PlayerMovement player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerMovement>();
        List<string> listOfArmor = new List<string>();
        foreach (string key in player.currentArmorParts.Keys)
        {
            listOfArmor.Add(player.currentArmorParts[key].partOfArmor);
        }

        dropDown = transform.GetComponent<TMP_Dropdown>();

        dropDown.ClearOptions();
        dropDown.AddOptions(listOfArmor);
        dropDown.SetValueWithoutNotify(0);
    }
    // Update is called once per frame
    void Update()
    {
    }
}
