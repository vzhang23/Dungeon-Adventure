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
    private static int stageNumber=1;
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
    }


    public void StartingGame()
    {

        SceneManager.LoadScene(gameScene+ stageNumber);
        Init();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void NextStage() {
        stageNumber++;
        GameObject currentPlayer = GameObject.FindGameObjectsWithTag("player")[0];
        currentPlayer.transform.position = new Vector2(-3, 3);
        SceneManager.LoadScene(gameScene+ stageNumber);
        Init();

    }

    public void displayArmorUI(GameObject armorObject)
    {
        PlayerArmor armor = armorObject.GetComponent<PlayerArmor>();

        armorUI.SetActive(true);
        TextMeshProUGUI textBox = armorUI.GetComponentInChildren<TextMeshProUGUI>();

        string newText = string.Format("Part: {0} \n HP multiplier: {1}\n MP multiplier: {2}\n ATTACK multiplier: {3}\n DEFENSE multiplier: {4}\n JUMP LIMIT increase: {5}\n SPEED increase: {6} \n Press J to equip", armor.partOfArmor, armor.hp, armor.mp, armor.attack, armor.defense, armor.jumpLimit, armor.moveSpeed);
        
        textBox.SetText(newText);
    }

    public void hideArmorUI()
    {
        armorUI.SetActive(false);
    }
}
