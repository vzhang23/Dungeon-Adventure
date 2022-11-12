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
    private int stageNumber=1;
    public static GameManager instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);


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

}
