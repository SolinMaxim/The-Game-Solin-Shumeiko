using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TopList : MonoBehaviour
{
    public GameObject panel;
    public GameObject slalomTop;
    public GameObject freeRideTop;
    public Text slalomText;
    public Text freeRideText;
    public StreamReader read;
    private RectTransform slalomRectTransform;
    private RectTransform freeRideRectTransform;
    private bool alreadyPrint;

    // Start is called before the first frame update
    void Start()
    {
        slalomRectTransform = slalomTop.GetComponent<RectTransform>();
        freeRideRectTransform = freeRideTop.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (panel.activeInHierarchy && !alreadyPrint)
        {
            read = new StreamReader("TopListSlalom.txt");
            while (!read.EndOfStream)
            {
                slalomText.text += read.ReadLine() + "\r\n";
            }
            read.Close();
            read = new StreamReader("TopListFreeRide.txt");
            while (!read.EndOfStream)
            {
                freeRideText.text += read.ReadLine() + "\r\n";
            }
            read.Close();
            alreadyPrint = true;
        }
        if (!panel.activeInHierarchy && alreadyPrint)
        {
            alreadyPrint = false;
        }
        slalomRectTransform.sizeDelta = slalomText.rectTransform.sizeDelta; 
        freeRideRectTransform.sizeDelta = freeRideText.rectTransform.sizeDelta;
    }
}
