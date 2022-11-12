using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string gameScene = "Stage1";
    private string menuScene = "MenuScene";
    public GameObject ending;
    public GameObject gameTextbox;
    public GameObject pauseMenu;
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
    }

    private void Start()
    {
        Init();
    }


    public void StartingGame()
    {

        SceneManager.LoadScene(gameScene);
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

}
