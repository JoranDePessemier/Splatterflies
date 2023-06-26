using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuStringController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _gameOverStoryText;

    [SerializeField]
    private TextMeshProUGUI _gameOverEndlessText;

    [SerializeField]
    private TextMeshProUGUI _highScoreText;

    private int _currentScore;
    private int _highScore;

    private void Awake()
    {
        _currentScore = GlobalVariables.Instance.CompletedOrders;
        _highScore = GlobalVariables.Instance.HighScore;
    }

    private void Start()
    {
        _gameOverStoryText.text = $"You completed {_currentScore} orders.";
        _gameOverEndlessText.text = $"You completed {_currentScore} orders.";
        _highScoreText.text = $"Your highest amount of orders completed is {_highScore}.";
    }
}
