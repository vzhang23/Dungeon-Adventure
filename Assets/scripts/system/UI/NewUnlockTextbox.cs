using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewUnlockTextbox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI textBox = gameObject.GetComponentInChildren<TextMeshProUGUI>();

  
        textBox.SetText(GameManager.Instance().getNewUnlockText());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
