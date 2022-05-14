using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System;
using TMPro;

public class CollisionChecker : MonoBehaviour
{
    public float collisionRadius;
    public LayerMask layerTree;
    public LayerMask layerFence;
    public LayerMask layerStop;
    public bool isCollisionWithTree 
    { 
        get;
        private set;
    }
    private bool isCollisionWithFence;
    public bool stop;
    public int countCollision = 0;
    public bool recentlyWasCollisionWithTree = false;
    public bool recentlyWasCollisionWithFence = false;
    public Player player;
    public bool mustLoseHp;
    public Timer timer;
    public List<GameObject> hp;
    public GameObject finishMenu;
    public GameObject map;
    public Text finishText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollisionWithFence)
        {
            if (Math.Abs(player.angle) > 45)
            {
                recentlyWasCollisionWithFence = true;
                hp[hp.Count - 1].SetActive(false);
                hp.RemoveAt(hp.Count - 1);
            }
            player.speed = 0;
            player.speedY = 0;
            player.speedX = 0;
            player.angle = 0;
            Thread.Sleep(3000);
            timer.time += 3;
            player.transform.position += new Vector3(Mathf.Sign(26 - player.transform.position.x)*5, 0, 0);
        }
        if (isCollisionWithTree)
        {
            if (!recentlyWasCollisionWithTree)
            {
                player.angle = 0;
                player.speed = 0;
            }
            player.speedX = 10 * Math.Sign(player.angle);
            player.speedY = 0;
            recentlyWasCollisionWithTree = true;
        }
        if (!isCollisionWithFence && recentlyWasCollisionWithFence)
        {
            countCollision++;
            recentlyWasCollisionWithFence = false;
            player.speed = 0;
        }
        if (!isCollisionWithTree && recentlyWasCollisionWithTree)
        {
            countCollision++;
            recentlyWasCollisionWithTree = false;
            mustLoseHp = false;
            hp[hp.Count - 1].SetActive(false);
            hp.RemoveAt(hp.Count - 1);
            player.speed = 0;
        }
        if (countCollision == 3)
        {
            map.SetActive(false);
            finishMenu.SetActive(true);
            finishText.text = "Вы врезались слишком много раз";
        }
        if (stop)
        {
            player.speed = 0;
            player.speedY = 0;
            player.speedX = 0;
        }
    }

    private void FixedUpdate()
    {
        isCollisionWithTree = Physics2D.OverlapCircle(gameObject.transform.position, collisionRadius, layerTree);
        isCollisionWithFence = Physics2D.OverlapCircle(gameObject.transform.position, collisionRadius, layerFence);
        stop = Physics2D.OverlapCircle(gameObject.transform.position, collisionRadius, layerStop);
    }
}
