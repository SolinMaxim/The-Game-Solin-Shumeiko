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
    public LayerMask layerStop;
    public bool isCollisionWithTree 
    { 
        get;
        private set;
    }
    public bool stop;
    public int countCollision = 0;
    public bool recentlyWasCollisionWithTree = false;
    public Player player;
    public List<GameObject> hp;
    public GameObject finishMenu;
    public Text finishText;
    public bool lose = false;
    private SpriteRenderer spriteRenderer1;

    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer1 = finishMenu.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (!isCollisionWithTree && recentlyWasCollisionWithTree)
        {
            countCollision++;
            recentlyWasCollisionWithTree = false;
            hp[hp.Count - 1].SetActive(false);
            hp.RemoveAt(hp.Count - 1);
            player.speed = 0;
        }
        if (countCollision == 3)
        {
            finishMenu.SetActive(true);
            spriteRenderer1.sprite = Resources.Load<Sprite>("Collisions");
            finishText.gameObject.SetActive(true);
            lose = true;
        }
        if (stop)
        {
            player.speed = 0;
            player.speedY = 0;
            player.speedX = 0;
            if (finishText.text.Length == 0)
            {
                finishMenu.SetActive(true);
                spriteRenderer1.sprite = Resources.Load<Sprite>("TooFar");
                finishText.gameObject.SetActive(true);
            }
        }

    }

    private void FixedUpdate()
    {
        isCollisionWithTree = Physics2D.OverlapCircle(gameObject.transform.position, collisionRadius, layerTree);
        stop = Physics2D.OverlapCircle(gameObject.transform.position, collisionRadius, layerStop);
    }
}
