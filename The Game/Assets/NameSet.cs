using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameSet : MonoBehaviour
{
    public string userName;
    public TMP_InputField inputName;

    void Start()
    {
        userName = PlayerPrefs.GetString("UserName");
        inputName.text = userName;
    }

    void Update()
    {
        if (inputName.text.Length < 10)
        {
            userName = inputName.text;
        }
        else
            inputName.text = userName;
        PlayerPrefs.SetString("UserName", userName);
        PlayerPrefs.Save();
    }
}
