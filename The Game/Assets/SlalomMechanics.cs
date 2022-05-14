using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class SlalomMechanics : MonoBehaviour
{
    public Player player;
    const double pi = Math.PI;
    public GameObject arrow;
    public List<Transform> flags;
    public double directionAngle;
    public Transform currentFlag;
    public bool finished = false;
    public GameObject map;
    public Text finishText;
    public GameObject finishMenu;
    public CollisionChecker collisionChecker;
    public Finish finish;

    void Start()
    {
        collisionChecker = player.collisionChecker;
    }

    // Update is called once per frame
    void Update()
    {
        currentFlag = flags[0];
        finished = finish.finished;
        var direction = currentFlag.position - collisionChecker.transform.position;
        directionAngle = Math.Atan2(direction.y, direction.x) / pi * 180 + 90;
        if (SceneManager.GetActiveScene().name == "Slalom")
            arrow.transform.rotation = Quaternion.Euler(0, 0, -(float)(90 - directionAngle));
        if (!finished && Math.Abs(currentFlag.position.y - collisionChecker.transform.position.y) < 0.1)
        {
            if (!(currentFlag.tag == "RightFlag" && currentFlag.position.x - collisionChecker.transform.position.x < 0 ||
                currentFlag.tag == "LeftFlag" && currentFlag.position.x - collisionChecker.transform.position.x > 0))
            {
                finishMenu.SetActive(true);
                map.SetActive(false);
                finishText.text = "Вы объехали флаг не с той стороны";
            }
            flags.RemoveAt(0);
        }
    }
}
