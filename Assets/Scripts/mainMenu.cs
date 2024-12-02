using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{

    public TextMeshProUGUI highScoreText;

    int highScore = PlayerPrefs.GetInt("HighScore", 0);

    void Start()
    {
        // Load the high score from PlayerPrefs
        int highScore = PlayerPrefs.GetInt("HighScore", 0); 

        // Update the UI with the high score
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))  
        {
            ResetHighScore();
        }

    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");  
        highScore = 0;  
        UpdateHighScoreUI(); 
    }

    void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    public void playGame()
    {
        SceneManager.LoadScene("map_selection");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
