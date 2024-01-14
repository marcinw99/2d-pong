using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Ball : MonoBehaviour
{
    public event EventHandler<GameManager.Wall> GameOverWallTouched;
    public event EventHandler<GameManager.Player> BallDeflected;

    private float START_X_VELOCITY = 6;

    [SerializeField] private Rigidbody2D rigidBody2D;

    private void Start()
    {
        GameManager.Instance.GameStateChanged += GameManagerOnGameStateChanged;

        Random random = new Random();

        double yVelocity = (random.NextDouble() * 0.6) - 0.3; // range from -0.3 to 0.3
        rigidBody2D.velocity = new Vector2(START_X_VELOCITY, (float)yVelocity);
    }

    private void GameManagerOnGameStateChanged(object sender, GameManager.State e)
    {
        if (e == GameManager.State.GameOver)
        {
            rigidBody2D.position = Vector3.zero;
            rigidBody2D.velocity = Vector2.zero;
        }
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