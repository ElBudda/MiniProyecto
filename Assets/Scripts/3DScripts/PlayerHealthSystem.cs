using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealthSystem : MonoBehaviour
{
    public static PlayerHealthSystem Instance;

    public float knockbackForce = 3f;
    public float knockbackDuration = 0.3f;

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

        // Trigger movement knockback lock
        PlayerMovement3d movement = GetComponent<PlayerMovement3d>();
        if (movement != null)
        {
            movement.isKnockedBack = true;
            Debug.Log("Player movement locked for knockback!");
        }

        // Compute knockback direction from cat
        EnemyCat cat = FindObjectOfType<EnemyCat>();
        if (cat != null)
        {
            Vector3 knockDirection = (transform.position - cat.transform.position).normalized;
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(knockDirection * knockbackForce, ForceMode.Impulse);
                Debug.Log($"Player knocked back with force {knockbackForce}!");
            }

            // Start unlock coroutine
            StartCoroutine(EndKnockback(movement));
        }

        // Check for death
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator EndKnockback(PlayerMovement3d movement)
    {
        yield return new WaitForSeconds(knockbackDuration);
        if (movement != null)
        {
            movement.isKnockedBack = false;
            Debug.Log("Player knockback ended, movement unlocked.");
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

        if (GameUIManager.Instance != null)
            GameUIManager.Instance.ShowDeathScreen();
    }
}

