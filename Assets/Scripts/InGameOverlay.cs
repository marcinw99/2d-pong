using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameOverlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI opponentScoreText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.ScoreChanged += GameManagerOnScoreChanged;
    }

    private void GameManagerOnScoreChanged(object sender, GameManager.ScoreChangedEventArgs e)
    {
        if (e.player == GameManager.Player.MainPlayer)
        {
            playerScoreText.text = e.newScore.ToString();
        }
        else if (e.player == GameManager.Player.Opponent)
        {
            opponentScoreText.text = e.newScore.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}