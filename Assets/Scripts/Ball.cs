using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Ball : MonoBehaviour
{
    public event EventHandler<GameManager.Wall> GameOverWallTouched;
    public event EventHandler<GameManager.Player> BallDeflected;

    private float START_X_VELOCITY = 6;
    private bool bouncinessIsLimited = false;

    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private CircleCollider2D circleCollider2D;


    private void Start()
    {
        GameManager.Instance.GameStateChanged += GameManagerOnGameStateChanged;

        LaunchTheBall();
    }

    private void Update()
    {
        if (rigidBody2D.velocity.x > 15 && !bouncinessIsLimited)
        {
            ChangeBounciness(1.005f);
            bouncinessIsLimited = true;
        }

        Debug.Log("Velocity X: " + rigidBody2D.velocity.x);
        Debug.Log("bounciness: " + circleCollider2D.bounciness);
    }

    private void GameManagerOnGameStateChanged(object sender, GameManager.State e)
    {
        if (e == GameManager.State.GameOver)
        {
            HoldTheBall();
        }
        else if (e == GameManager.State.Playing)
        {
            LaunchTheBall();
        }
    }

    private void LaunchTheBall()
    {
        bouncinessIsLimited = false;
        ChangeBounciness(1.1f);

        Random random = new Random();

        double yVelocity = (random.NextDouble() * 1.6) - 0.8; // range from -0.8 to 0.8
        rigidBody2D.velocity = new Vector2(START_X_VELOCITY, (float)yVelocity);
    }

    private void HoldTheBall()
    {
        rigidBody2D.position = Vector3.zero;
        rigidBody2D.velocity = Vector2.zero;
    }

    private void ChangeBounciness(float newBounciness)
    {
        PhysicsMaterial2D newMaterial = new PhysicsMaterial2D();
        newMaterial.bounciness = newBounciness;
        newMaterial.friction = 0;
        circleCollider2D.sharedMaterial = newMaterial;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "RightWall":
                GameOverWallTouched?.Invoke(this, GameManager.Wall.Right);
                break;
            case "LeftWall":
                GameOverWallTouched?.Invoke(this, GameManager.Wall.Left);
                break;
            case "PlayerPaddle":
                BallDeflected?.Invoke(this, GameManager.Player.MainPlayer);
                break;
            case "OpponentPaddle":
                BallDeflected?.Invoke(this, GameManager.Player.Opponent);
                break;
        }
    }
}