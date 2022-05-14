using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isOnPause = false;
    public GameObject pauseMenu;
    public GameObject map;
    public Text StartText;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOnPause)
            {
                DoResume();
            }
            else
            {
                DoPause();
            }
        }
    }
    void DoResume()
    {
        StartText.gameObject.SetActive(true);
        if (StartText.text != "")
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
        map.SetActive(true);
        pauseMenu.SetActive(false);
        isOnPause = false;
    }
    void DoPause()
    {
        StartText.gameObject.SetActive(false);
        map.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isOnPause = true;
    }
}
