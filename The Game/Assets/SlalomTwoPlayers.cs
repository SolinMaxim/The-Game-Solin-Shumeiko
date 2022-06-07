using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlalomTwoPlayers : MonoBehaviour
{
    public Text place;
    public CollisionChecker competitor;
    public SlalomMechanics me;
    private bool isFinished;
    public LayerMask layerFinish;
    private float finishRadius = 0.1f;
    private bool finished = false;
    public GameObject finishMenu;
    public Text finishText;
    private SpriteRenderer spriteRenderer1;

    // Start is called before the first frame update
    void Start()
    {
        place.text = "1st";
        spriteRenderer1 = finishMenu.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinished)
        {
            finished = true;
            finishMenu.SetActive(true);
            finishText.gameObject.SetActive(true);
            if (place.text == "1st")
            {
                spriteRenderer1.sprite = Resources.Load<Sprite>("Winner");
            }
            else
            {
                spriteRenderer1.sprite = Resources.Load<Sprite>("Loser");
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
            spriteRenderer1.sprite = Resources.Load<Sprite>("Loser");
            place.gameObject.SetActive(false);
        }
        if (me.lose && competitor.lose)   
            finishText.color = Color.yellow;
    }

    private void FixedUpdate()
    {
        isFinished = Physics2D.OverlapCircle(gameObject.transform.position, finishRadius, layerFinish);
    }
}
