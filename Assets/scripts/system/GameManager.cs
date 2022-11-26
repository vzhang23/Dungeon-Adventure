using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int customStageNumber;
    private string gameScene = "Stage";
    private string menuScene = "MenuScene";
    private string clearScene = "ClearScene";
    
    public static int stageNumber=1;
    public static GameManager instance = null;
    public GameObject armorUI;
    public GameObject pauseMenu;
    public static bool pause;
    public GameObject saveObject;
    private static string newUnlock;
    GameObject teleporter;
    private static string skillChoosed;
    void Awake()
    {
        if(stageNumber>0 && stageNumber<= customStageNumber)
        {
            stageNumber = customStageNumber;
        }
        pause = false;
        instance = this;
        teleporter = null;
    }
    private void Update()
    {
        if (teleporter != null)
        {
            teleporter.SetActive(true);
            teleporter = null;
        }
    }
    public void activeTeleporter(GameObject teleporter)
    {
        this.teleporter = teleporter;
    }
    public string getNewUnlockText()
    {
        return newUnlock;
    }
    public static GameManager Instance()
    {
        return instance;
    }


    public int getStage()
    {
        return stageNumber;
    }
    public bool getPauseState()
    {
        return pause;
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

        EditorUtility.SetDirty(saveObject);
        AssetDatabase.SaveAssets();
        Time.timeScale = 1;
        GameObject keep=GameObject.FindGameObjectWithTag("keepObjectOnLoad");
        keep.SetActive(true);
        Destroy(keep);
        stageNumber = 1;
        SceneManager.LoadScene(menuScene);
    }

    public void InfinityLevel()
    {

        EditorUtility.SetDirty(saveObject);
        AssetDatabase.SaveAssets();
        KeepObjectOnLoad keep = GameObject.FindGameObjectWithTag("keepObjectOnLoad").GetComponent<KeepObjectOnLoad>();
        keep.activeObject();
        stageNumber = -1;
        NextStage();
    }

    public void NextStage() {

        if (stageNumber == -1)
        {
            GameObject currentPlayer = GameObject.FindGameObjectsWithTag("player")[0];
            currentPlayer.transform.position = new Vector2(-3, 3);
            SceneManager.LoadScene(gameScene + stageNumber);
            Init();
        }
        else if (stageNumber == 3)
        {
            GameClear();
        }
        else {
            stageNumber++;
            GameObject currentPlayer = GameObject.FindGameObjectsWithTag("player")[0];
            currentPlayer.transform.position = new Vector2(-3, 3);
            SceneManager.LoadScene(gameScene + stageNumber);
            Init();
        }


    }
    public static GameObject[] GetDontDestroyOnLoadObjects()
    {
        GameObject temp = null;
        try
        {
            temp = new GameObject();
            DontDestroyOnLoad(temp);
            Scene dontDestroyOnLoad = temp.scene;
            DestroyImmediate(temp);
            temp = null;

            return dontDestroyOnLoad.GetRootGameObjects();
        }
        finally
        {
            if (temp != null)
                DestroyImmediate(temp);
        }
    }
    public void setSkillChoosed(string newSkillChoosed)
    {
        skillChoosed = newSkillChoosed;
    }
    public string getSkillChoosed()
    {
        return skillChoosed;
    }
    private void GameClear()
    {
        GameSave save=saveObject.GetComponent<GameSave>();
        GameObject[] doNotDestroyOnLoad = GetDontDestroyOnLoadObjects();

        GameObject keep = null;
        foreach (GameObject g in doNotDestroyOnLoad)
        {
            if (g.tag == "keepObjectOnLoad")
            {
                keep = g;

            }

        }

        GameObject currentPlayer = keep.GetComponent<KeepObjectOnLoad>().getObjectByTag("player");
        newUnlock = "";
        foreach (GameObject skill in currentPlayer.GetComponent<PlayerMovement>().currentSkillsValueInstance)
        {
            string name = skill.GetComponent<PlayerSkill>().nameOfSkill;
            if (!save.unlockedSkills.Contains(name))
            {
                newUnlock = "you can carry the skill "+name+" to your next game";
                save.unlockedSkills.Add(name);
                break;
            }
        }
        keep.GetComponent<KeepObjectOnLoad>().unactiveObject();

        SceneManager.LoadScene(clearScene);


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

    public void pauseOrResumeGame()
    {
        if (pauseMenu.activeSelf)
        {
            pause = false;
            Time.timeScale = 1;

        pauseMenu.SetActive(false);
        }
        else
        {
            pause = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }

    }


}
