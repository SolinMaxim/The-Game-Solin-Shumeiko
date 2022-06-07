using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameSet : MonoBehaviour
{
    public string userName;
    public InputField inputName;

    void Start()
    {
        userName = PlayerPrefs.GetString("UserName");
        inputName.text = userName;
    }

    void Update()
    {
        if (inputName.text.Length < 9)
        {
            userName = inputName.text;
        }
        else
            inputName.text = userName;
        PlayerPrefs.SetString("UserName", userName);
        PlayerPrefs.Save();
    }
}
