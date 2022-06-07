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
    public GameObject finish;
    public bool lose = false;
    private bool isCollisionWithFence;
    public LayerMask layerFence;
    private float collisionRadius = 0.1f;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer spriteRenderer1;

    void Start()
    {
        collisionChecker = player.collisionChecker;
        spriteRenderer = arrow.GetComponent<SpriteRenderer>();
        spriteRenderer1 = finishMenu.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        finished = finish.transform.position.y - collisionChecker.transform.position.y >= 0;
        if (lose)
            player.speed = 0;
        if (!finished && isCollisionWithFence)
        {
            finishMenu.SetActive(true);
            spriteRenderer1.sprite = Resources.Load<Sprite>("Loser");
            finishText.gameObject.SetActive(true);
        }
        if (!finished && flags.Count > 0)
        {
            currentFlag = flags[0];
            var direction = currentFlag.position - collisionChecker.transform.position;
            directionAngle = Math.Atan2(direction.y, direction.x) / pi * 180;
            if (currentFlag.tag == "RightFlag")
                spriteRenderer.sprite = Resources.Load<Sprite>("arrow right");
            else
                spriteRenderer.sprite = Resources.Load<Sprite>("arrow left");
            arrow.transform.rotation = Quaternion.Euler(0, 0, (float)(directionAngle));
            if (!finished && Math.Abs(currentFlag.position.y - collisionChecker.transform.position.y) < 0.1)
            {
                if (!(currentFlag.tag == "RightFlag" && currentFlag.position.x - collisionChecker.transform.position.x < 0 ||
                    currentFlag.tag == "LeftFlag" && currentFlag.position.x - collisionChecker.transform.position.x > 0))
                {
                    finishMenu.SetActive(true);
                    spriteRenderer1.sprite = Resources.Load<Sprite>("WrongSide");
                    finishText.gameObject.SetActive(true);
                    lose = true;
                }
                flags.RemoveAt(0);
            }
        }
    }

    private void FixedUpdate()
    {
        isCollisionWithFence = Physics2D.OverlapCircle(gameObject.transform.position, collisionRadius, layerFence);
    }
}
