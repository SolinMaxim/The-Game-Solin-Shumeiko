using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using System.Windows.Input;

public class Player : MonoBehaviour
{
    // ???????? ???????? spriteRenderer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Front");
    const float g = 6.92f;
    const double pi = Math.PI;
    const float maxSpeed = 10f;
    private NewControls input;
    public float speedY = 0;
    public float speedX = 0;
    public CollisionChecker collisionChecker;
    private Rigidbody2D myRigidbody;
    private SpriteRenderer playerRenderer;
    public float angle = 0;
    public float changingSpeed = 0;
    public float angleChanging = 100000f;
    public double speed;
    public Text startText;
    public TrailRenderer trailRenderer;
    private string control = "AD";
    public string playerType = "Main";
    public GameObject ski;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        Time.timeScale = 0f;
        if (playerType == "First")
            input = new NewControls("AD");
        else if (playerType == "Second")
            input = new NewControls("Arrows");
        else
        {
            control = PlayerPrefs.GetString("Control");
            if (control == "Mouse")
            {
                input = new NewControls("AD");
                changingSpeed = 0;
            }
            else
                input = new NewControls(control);
        }
        playerRenderer = gameObject.GetComponent<SpriteRenderer>();
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
        input.Player.Turn.performed += context => Turn(context.ReadValue<float>());
        input.Player.Turn.canceled += context => Turn(0);
        trailRenderer.startColor = Color.Lerp(Color.white, Color.gray, 0.2f);
        trailRenderer.endColor = Color.Lerp(Color.white, Color.gray, 0.2f);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        control = PlayerPrefs.GetString("Control");
        if (Time.timeScale < 1e-9 && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            startText.text = "";
        }
        var speed1 = speed + g * Time.deltaTime * (float)Math.Cos(pi * angle / 180);
        speed = speed1 < maxSpeed ? speed1 : maxSpeed;
        speedY = (float)(speed * Math.Cos(pi * angle / 180));
        speedX = (float)(speed * Math.Sin(pi * angle / 180));
        myRigidbody.velocity = new Vector2(speedX, -speedY);
        angle += changingSpeed * Time.deltaTime;
        var pos = Input.mousePosition;
        //var x = pos.x - collisionChecker.transform.position.x - 900;
        //var y = pos.y - collisionChecker.transform.position.y - 700;
        //if  ( y <= 0)
        //    angle = -(float) (180 * Math.Asin(Math.Sign(y) * x/Math.Sqrt(x*x + y*y))/ pi);
        if (control == "Mouse")
            angle = -90 + 180 * pos.x / 1920;
        while (angle > 90 || angle < -90)
            angle -= 1 * Math.Sign(angle);
        ski.transform.rotation = Quaternion.Euler(0, 0, (float)(angle));
        if (Math.Abs(angle) < 10)
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("1");
            spriteRenderer.flipX = angle > 0;
        }
        if (Math.Abs(angle) >= 10 && Math.Abs(angle) < 35)
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("2");
            spriteRenderer.flipX = angle > 0;
        }
        if (Math.Abs(angle) >= 35 && Math.Abs(angle) < 50)
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("3");
            spriteRenderer.flipX = angle > 0;
        }
        if (Math.Abs(angle) >= 50 && Math.Abs(angle) < 70)
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("4");
            spriteRenderer.flipX = angle > 0;
        }
        if (Math.Abs(angle) >= 70)
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("5");
            spriteRenderer.flipX = angle > 0;
        }
    }


    private void Turn(float axis)
    {
        changingSpeed = axis * angleChanging;
    }

    private void OnEnable() => input.Enable();

    private void OnDisable() => input.Disable();
}