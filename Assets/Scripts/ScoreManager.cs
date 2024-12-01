using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public int highScore = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0); 
        UpdateScoreUI();  
    }
    // Method to increase the score
    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreUI();

        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
        }
    }
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);  
        PlayerPrefs.Save(); 
    }

    // Update the score UI
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;  
        }
    }

    // Method to reset the high score to zero
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");  
        highScore = 0;  
        UpdateScoreUI();  
    }
}
