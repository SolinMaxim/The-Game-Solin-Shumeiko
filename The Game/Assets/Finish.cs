using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;

public class Finish : MonoBehaviour
{
    public Player player;
    public LayerMask layerFinish;
    public GameObject finishMenu;
    public Text finishText;
    public StreamReader read;
    public StreamWriter write;
    public int place = 0;
    public bool finished = false;
    private bool isFinished;
    public float finishRadius;
    public Timer timer;
    public GameObject map;
    private List<Tuple<float, string>> sortedTopList;
    public string userName = "Player";

    private void Start()
    {
        userName = PlayerPrefs.GetString("UserName");
    }

    void Update()
    {
        if (isFinished && player.collisionChecker.countCollision < 3 && !finished)
        {
            finished = true;
            finishMenu.SetActive(true);
            //map.SetActive(false);
            var strFinishTime = timer.timeText.text;
            var floatFinishTime = timer.ToInt(strFinishTime);
            timer.gameObject.SetActive(false);
            finishText.text = "ѕоздравл€ем, вы прошли трассу за " + strFinishTime;
            if (SceneManager.GetActiveScene().name == "Slalom")
                read = new StreamReader("TopListSlalom.txt");
            else
                read = new StreamReader("TopListFreeRide.txt");
            var topList = new List<Tuple<float, string>>();
            while (!read.EndOfStream)
            {
                var s = read.ReadLine();
                var parts = s.Split(" ");
                var time = parts[1].Split(':');
                float floatTime = int.Parse(time[0]) * 60 + int.Parse(time[1]) + float.Parse(time[2]) / 100;
                topList.Add(Tuple.Create(floatTime, parts[2]));
            }
            read.Close();
            topList.Add(Tuple.Create(floatFinishTime, userName));
            sortedTopList = topList.OrderBy(x => x.Item1).ToList();
            if (SceneManager.GetActiveScene().name == "Slalom")
                write = new StreamWriter("TopListSlalom.txt");
            else
                write = new StreamWriter("TopListFreeRide.txt");
            for (var i = 0; i < sortedTopList.Count; i++)
            {
                if (Math.Abs(sortedTopList[i].Item1 - floatFinishTime) < 1e-9 && sortedTopList[i].Item2 == userName)
                    place = i + 1;
                write.WriteLine((i + 1) + " " + timer.ConvertTimeToString(sortedTopList[i].Item1) + " " + sortedTopList[i].Item2);
            }
            //finishText.text = "ѕоздравл€ем, вы прошли трассу за " + strFinishTime + " и зан€ли " + place + " место";
            finishText.text = strFinishTime;
            write.Close();
        }
    }

    private void FixedUpdate()
    {
        isFinished = Physics2D.OverlapCircle(gameObject.transform.position, finishRadius, layerFinish);
    }
}
