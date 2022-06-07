using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TopList : MonoBehaviour
{
    public GameObject panel;
    public Text slalomText;
    public Text freeRideText;
    public StreamReader read;
    private bool alreadyPrint;

    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        if (panel.activeInHierarchy && !alreadyPrint)
        {
            read = new StreamReader("TopListSlalom.txt");
            while (!read.EndOfStream)
            {
                var str = read.ReadLine();
                if (str.Split()[0].Length == 1)
                    str = "0" + str;
                slalomText.text += str + "\r\n";
            }
            read.Close();
            read = new StreamReader("TopListFreeRide.txt");
            while (!read.EndOfStream)
            {
                var str = read.ReadLine();
                if (str.Split()[0].Length == 1)
                    str = "0" + str;
                freeRideText.text += str + "\r\n";
            }
            read.Close();
            alreadyPrint = true;
        }
        if (!panel.activeInHierarchy && alreadyPrint)
        {
            alreadyPrint = false;
        }
    }
}
