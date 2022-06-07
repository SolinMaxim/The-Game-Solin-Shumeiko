using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class MenuControls : MonoBehaviour
{
    public void ExitPressed()
    {
        Application.Quit();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Back()
    {
        gameObject.SetActive(false);
    }

    public void PlayFreeRide()
    {
        SceneManager.LoadScene("FreeRide");
    }

    public void PlaySlalom()
    {
        SceneManager.LoadScene("Slalom");
    }

    public void PlayTwoPlayers()
    {
        SceneManager.LoadScene("SlalomTwoPlayers");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OpenOnFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
