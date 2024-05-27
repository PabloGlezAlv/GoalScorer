using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI actualScoreText;
    [SerializeField]
    TextMeshProUGUI recordScoreText;

    int currentScore = 0;

    int bestScore = 0;
    void Awake()
    {
        actualScoreText.text = currentScore.ToString();
        recordScoreText.text = bestScore.ToString();
    }

    void OnEnable()
    {
        actualScoreText.text = currentScore.ToString();
        recordScoreText.text = bestScore.ToString();
    }
    public void ResetScore()
    {
        currentScore = 0;
    }

    public void ResetGame()
    {
        if(currentScore > bestScore)
        {
            bestScore = currentScore;
            recordScoreText.text = bestScore.ToString();
        }

        currentScore = 0;
        actualScoreText.text = currentScore.ToString();
    }

    public void AddScore(int score)
    {
        currentScore += score;
        actualScoreText.text = currentScore.ToString();
    }
}
