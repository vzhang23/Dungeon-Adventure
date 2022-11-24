using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string gameScene = "Stage";
    private string menuScene = "MenuScene";
    public GameObject ending;
    public GameObject gameTextbox;
    public GameObject pauseMenu;
    public int stageNumber=1;
    public static GameManager instance = null;
    public GameObject armorUI;
    void Awake()
    {
        instance = this;


    }
    public static GameManager Instance()
    {
        return instance;
    }

    public void pause()
    {
        pauseMenu.SetActive(true);
    }
    public int getStage()
    {
        return stageNumber;
    }
    public void resume()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Init()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Init();
        armorUI.SetActive(true);
        armorUI.SetActive(false);
    }


    public void StartingGame()
    {

        SceneManager.LoadScene(gameScene+ stageNumber);
        Init();
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    public void ReturnToMenu()
    {
        Destroy(GameObject.FindGameObjectWithTag("player"));
        SceneManager.LoadScene(menuScene);
    }

    public IEnumerator NextStage() {

        yield return new WaitForSeconds(0.2f);

        if (stageNumber == 3)
        {
            ReturnToMenu();
        }
        else {
            stageNumber++;
            GameObject currentPlayer = GameObject.FindGameObjectsWithTag("player")[0];
            currentPlayer.transform.position = new Vector2(-3, 3);
            SceneManager.LoadScene(gameScene + stageNumber);
            Init();
        }


    }

    public void displayArmorUI(GameObject armorObject)
    {
        PlayerArmor armor = armorObject.GetComponent<PlayerArmor>();

        armorUI.SetActive(true);
        TextMeshProUGUI textBox = armorUI.GetComponentInChildren<TextMeshProUGUI>();

        string newText = string.Format("Part: {0} \n HP multiplier: {1}\n MP multiplier: {2}\n ATTACK multiplier: {3}\n DEFENSE multiplier: {4}\n JUMP LIMIT increase: {5}\n SPEED increase: {6} \n Press J to equip", armor.partOfArmor, armor.hp, armor.mp, armor.attack, armor.defense, armor.jumpLimit, armor.moveSpeed);
        
        textBox.SetText(newText);
    }

    public void displayArmorUI(GameObject armorObject, PlayerArmor currentArmor)
    {
        PlayerArmor armor = armorObject.GetComponent<PlayerArmor>();
        armorUI.SetActive(true);
        TextMeshProUGUI textBox = armorUI.GetComponentInChildren<TextMeshProUGUI>();

        string newText = string.Format("Part: {0} \n HP multiplier: {1}\n MP multiplier: {2}\n ATTACK multiplier: {3}\n DEFENSE multiplier: {4}\n JUMP LIMIT increase: {5}\n SPEED increase: {6} \n Press J to equip", currentArmor.partOfArmor, currentArmor.hp + "->" + armor.mp, currentArmor.mp + "->" + armor.mp, currentArmor.attack + "->" + armor.attack, currentArmor.defense + "->" + armor.defense, currentArmor.jumpLimit + "->" + armor.jumpLimit, currentArmor.moveSpeed + "->" + armor.moveSpeed);

        textBox.SetText(newText);
    }

    public void hideArmorUI()
    {
        armorUI.SetActive(false);
    }


}
