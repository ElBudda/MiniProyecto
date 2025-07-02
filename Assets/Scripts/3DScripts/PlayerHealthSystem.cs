using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthSystem : MonoBehaviour
{
    public static PlayerHealthSystem Instance;

    public int maxHealth = 100;
    public int currentHealth;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"Took {amount} damage. Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void FullHeal()
    {
        currentHealth = maxHealth;
        Debug.Log("Fully healed!");
    }

    void Die()
    {
        Debug.Log("You Died!");
        // TODO: Handle death (restart, game over)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

