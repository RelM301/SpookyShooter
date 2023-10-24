using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // Reference to the Text UI element to display the score.
    public int score = 0; // Initialize the score to 0.

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
}
