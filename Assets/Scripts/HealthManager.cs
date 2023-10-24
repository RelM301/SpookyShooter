using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public GameObject gameOverPanel;
    public int maxHealth = 100;
    private int currentHealth;
    private bool isGameOver = false;
    public AudioSource damageSound;
    public AudioSource recover;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = 1f;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void TakeDamage(int damage)
    {
        if (!isGameOver)
        {
            currentHealth -= damage;
            float healthPercentage = (float)currentHealth / maxHealth;
            healthBar.fillAmount = healthPercentage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isGameOver = true;
                gameOverPanel.SetActive(true);
            }
        }

        damageSound.PlayOneShot(damageSound.clip);
    }

    public void AddHealth(int amount)
    {
        if (!isGameOver)
        {
            currentHealth += amount;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
            float healthPercentage = (float)currentHealth / maxHealth;
            healthBar.fillAmount = healthPercentage;
        }

        recover.PlayOneShot(recover.clip);
    }
}
