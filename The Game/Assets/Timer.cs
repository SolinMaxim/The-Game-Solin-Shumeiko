using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeText;
    public float time = 0f;

    public string ConvertTimeToString(float time)
    {
        var minutes = (int)(time / 60);
        var seconds = (int)(time - minutes * 60);
        var millisecondes = (int)((time - minutes * 60 - seconds) * 100);
        var strTime = (minutes < 10 ? "0" : "") + minutes.ToString() + ":" + 
            (seconds < 10 ? "0" : "") + seconds.ToString() + ":" + 
            (millisecondes < 10 ? "0" : "") + millisecondes.ToString();
        return strTime;
    }

    public float ToInt(string timeText)
    {
        var parts = timeText.Split(':');
        return (float)(int.Parse(parts[0]) * 60 + int.Parse(parts[1]) + double.Parse(parts[2])/100);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeText.text = ConvertTimeToString(time);
    }
}
