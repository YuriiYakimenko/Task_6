using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth = 5;
    public Image gameOverPanel;

    public void Awake()
    {
        gameOverPanel.gameObject.SetActive(false);
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Player died");
        Time.timeScale = 0f;
        gameOverPanel.gameObject.SetActive(true);
    }
}