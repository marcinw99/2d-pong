using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler<State> GameStateChanged;
    public event EventHandler<ScoreChangedEventArgs> ScoreChanged;

    public class ScoreChangedEventArgs : EventArgs
    {
        public Player player;
        public int newScore;
    }

    [SerializeField] private Ball ball;

    public enum State
    {
        Playing,
        GameOver
    }

    public enum Wall
    {
        Top,
        Left,
        Bottom,
        Right
    }

    public enum Player
    {
        MainPlayer,
        Opponent
    }

    private State state;
    private bool mainPlayerWon;
    private int mainPlayerScore = 0;
    private int opponentScore = 0;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ball.GameOverWallTouched += BallOnGameOverWallTouched;
        ball.BallDeflected += BallOnBallDeflected;

        state = State.Playing;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void BallOnBallDeflected(object sender, Player player)
    {
        if (player == Player.MainPlayer)
        {
            mainPlayerScore++;
            ScoreChanged?.Invoke(this, new ScoreChangedEventArgs
            {
                player = Player.MainPlayer,
                newScore = mainPlayerScore
            });
        }
        else if (player == Player.Opponent)
        {
            opponentScore++;
            ScoreChanged?.Invoke(this, new ScoreChangedEventArgs
            {
                player = Player.Opponent,
                newScore = opponentScore
            });
        }
    }

    private void BallOnGameOverWallTouched(object sender, Wall wall)
    {
        if (wall == Wall.Left)
        {
            mainPlayerWon = false;
        }
        else
        {
            mainPlayerWon = true;
        }
        state = State.GameOver;
        Cursor.lockState = CursorLockMode.None;
        GameStateChanged?.Invoke(this, state);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public Player GetWinningPlayer()
    {
        return mainPlayerWon == true ? Player.MainPlayer : Player.Opponent;
    }

    public int GetMainPlayerScore()
    {
        return mainPlayerScore;
    }

    public void RestartGame()
    {
        mainPlayerScore = 0;
        opponentScore = 0;
        mainPlayerWon = false;
        state = State.Playing;
        Cursor.lockState = CursorLockMode.Locked;
        GameStateChanged?.Invoke(this, state);
    }
}