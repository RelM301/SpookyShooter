using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool isGameActive = true;

    // Reference to other components or managers
    public ScoreManager scoreManager; // Replace with your actual ScoreManager
    public SpawnManager spawnManager; // Replace with your actual SpawnManager
    public PlayerMovement playerMovement; // Replace with your actual PlayerMovement
    public Shooter shooter; // Replace with your actual Shooter
    public HealthManager healthManager; // Replace with your actual HealthManager
    public Text gameOverScoreText;

    void Update()
    {
        if (isGameActive)
        {
            // Your game update logic goes here
            // For example, check for game over conditions
            if (healthManager.IsGameOver()) // Replace with the correct condition for game over
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        // Set the game state to inactive
        isGameActive = false;

        // Stop or pause various game components
        scoreManager.enabled = false; // Disable the ScoreManager script
        spawnManager.enabled = false; // Disable the SpawnManager script
        playerMovement.enabled = false; // Disable the PlayerMovement script
        shooter.enabled = false; // Disable the Shooter script
        gameOverScoreText.text = "Score: " + scoreManager.score.ToString();

        Time.timeScale = 0;
    }

    public void PauseGame() 
    {
        // Set the game state to inactive
        isGameActive = false;

        // Stop or pause various game components
        scoreManager.enabled = false; // Disable the ScoreManager script
        spawnManager.enabled = false; // Disable the SpawnManager script
        playerMovement.enabled = false; // Disable the PlayerMovement script
        shooter.enabled = false; // Disable the Shooter script

        Time.timeScale = 0;
    }

    public void ReturnToGame()
    {
        isGameActive = true;

        // Stop or pause various game components
        scoreManager.enabled = true; // Disable the ScoreManager script
        spawnManager.enabled = true; // Disable the SpawnManager script
        playerMovement.enabled = true; // Disable the PlayerMovement script
        shooter.enabled = true; // Disable the Shooter script
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1;
    }
}
