using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int maxHealth = 3; // Number of hits a zombie can take
    private int currentHealth;
    public ScoreManager scoreManager;
    public int scoreOnKill = 100;

    void Start()
    {
        if (scoreManager == null)
        {
            scoreManager = FindObjectOfType<ScoreManager>(); 
        }
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (scoreManager != null)
        {
            scoreManager.IncreaseScore(scoreOnKill); 
        }

        // Optional: Play death animation or effects here
        Destroy(gameObject);
    }
}
