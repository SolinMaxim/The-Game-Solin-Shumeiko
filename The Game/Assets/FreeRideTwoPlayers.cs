using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreeRideTwoPlayers : MonoBehaviour
{
    public Text place;
    public CollisionChecker competitor;
    public CollisionChecker me;
    private bool isFinished;
    public LayerMask layerFinish;
    private float finishRadius = 0.1f;
    private bool finished = false;
    public GameObject finishMenu;
    public Text finishText;
    private bool isCollisionWithFence;
    public LayerMask layerFence;
    private float collisionRadius = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        place.text = "1st";
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinished)
        {
            finished = true;
            finishMenu.SetActive(true);
            if (place.text == "1st")
            {
                finishText.text = "You win";
                finishText.color = Color.green;
            }
            else
            {
                finishText.text = "You lose";
                finishText.color = Color.red;
            }
            place.gameObject.SetActive(false);
        }
        if (!finished)
        {
            if (gameObject.transform.position.y <= competitor.transform.position.y ||
                (!me.lose && competitor.lose))
            {
                place.text = "1st";
                place.color = Color.green;
            }
            else
            {
                place.text = "2nd";
                place.color = Color.red;
            }
        }
        if (me.lose)
        {
            finishMenu.SetActive(true);
            finishText.text = "You lose";
            finishText.color = Color.red;
            place.gameObject.SetActive(false);
        }
        if (me.lose && competitor.lose)
            finishText.color = Color.yellow;
        if (!finished && isCollisionWithFence)
        {
            finishMenu.SetActive(true);
            finishText.text = "You lose";
        }
    }

    private void FixedUpdate()
    {
        isFinished = Physics2D.OverlapCircle(gameObject.transform.position, finishRadius, layerFinish);
        isCollisionWithFence = Physics2D.OverlapCircle(gameObject.transform.position, collisionRadius, layerFence);
    }
}
