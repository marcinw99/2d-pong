using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverOverlay : MonoBehaviour
{
    [SerializeField] private Transform gameOverOverlay;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI yourScoreValueText;
    [SerializeField] private Button retryButton;

    // Start is called before the first frame update
    void Start()
    {
        gameOverOverlay.gameObject.SetActive(false);
        GameManager.Instance.GameStateChanged += GameManagerOnGameStateChanged;
        retryButton.onClick.AddListener(() => GameManager.Instance.RestartGame());
    }

    private void GameManagerOnGameStateChanged(object sender, GameManager.State e)
    {
        if (e == GameManager.State.GameOver)
        {
            bool mainPlayerWon = GameManager.Instance.GetWinningPlayer() == GameManager.Player.MainPlayer;

            if (mainPlayerWon)
            {
                gameOverText.text = "You won!";
            }
            else
            {
                gameOverText.text = "You lost!";
            }

            yourScoreValueText.text = GameManager.Instance.GetMainPlayerScore().ToString();

            gameOverOverlay.gameObject.SetActive(true);
        }
        else if (e == GameManager.State.Playing)
        {
            gameOverOverlay.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}