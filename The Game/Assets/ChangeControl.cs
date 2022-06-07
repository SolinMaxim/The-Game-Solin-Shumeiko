using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeControl : MonoBehaviour
{
    public string control = "AD";
    // Start is called before the first frame update
    void Start()
    {
        control = PlayerPrefs.GetString(control);
    }
    
    void Update()
    {
        control = PlayerPrefs.GetString("Control");
    }

    // Update is called once per frame
    public void ADControl()
    {
        control = "AD";
        PlayerPrefs.SetString("Control",control);
        PlayerPrefs.Save();
    }

    public void ArrowControl()
    {
        control = "Arrow";
        PlayerPrefs.SetString("Control", control);
        PlayerPrefs.Save();
    }

    public void MouseControl()
    {
        control = "Mouse";
        PlayerPrefs.SetString("Control", control);
        PlayerPrefs.Save();
    }
}
