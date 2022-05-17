using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

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

    private void Awake()
    {
        Time.timeScale = 0f;
        input = new NewControls();
        playerRenderer = gameObject.GetComponent<SpriteRenderer>();
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
        input.Player.Turn.performed += context => Turn(context.ReadValue<float>());
        input.Player.Turn.canceled += context => Turn(0);
        trailRenderer.startColor = Color.Lerp(Color.white, Color.gray, 0.2f);
        trailRenderer.endColor = Color.Lerp(Color.white, Color.gray, 0.2f);
    }

    private void Update()
    {
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
        while (angle > 90 || angle < -90)
            angle -= 1 * Math.Sign(angle);
    }


    private void Turn(float axis)
    {
        changingSpeed = axis * angleChanging;
    }

    private void OnEnable() => input.Enable();

    private void OnDisable() => input.Disable();
}