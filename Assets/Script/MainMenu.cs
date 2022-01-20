using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void LoadGame()
    {
        PlayerInfo.IsContinue = false;
        SceneManager.LoadScene("MainScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Continue()
    {
        PlayerInfo.IsContinue = true;
        SceneManager.LoadScene("MainScene");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
