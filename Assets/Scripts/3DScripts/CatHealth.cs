using UnityEngine;

public class CatHealth : MonoBehaviour, IDamageable
{
    public int health = 100;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"Cat took {amount} damage. Health: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Cat died!");
        Destroy(gameObject);
    }
}

