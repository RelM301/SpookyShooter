using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyBehaivour : MonoBehaviour
{
    public int points = 5; // Set the number of points for this candy.
    public string enemyTag = "Enemy"; // Set the enemy tag in the Inspector.

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
        {
            // Destroy the candy and the enemy
            Destroy(gameObject);
            Destroy(collision.gameObject);

            // Update the score (assuming you have the ScoreManager attached to an object).
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.UpdateScore(points);
            }
        }
        else
        {
            // If the candy hits something else, destroy only the candy after a delay
            Destroy(gameObject, 7f); // Destroy the candy after 7 seconds
        }
    }
}
