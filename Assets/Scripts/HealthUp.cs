using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    private int healthAmount = 7; // The amount of health to add when collected.

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // The player collided with the health pickup; add health to the player.
            HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.AddHealth(healthAmount); // Create a method in HealthManager to add health.
                Destroy(gameObject); // Destroy the health pickup after collecting it.
            }
        }
    }
}
