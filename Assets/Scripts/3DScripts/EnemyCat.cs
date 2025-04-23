using UnityEngine;

public class EnemyCat : MonoBehaviour
{
    public Transform player; // assign in Inspector
    public float detectionRange = 5f;
    public float moveSpeed = 2f;
    public float attackRange = 1.2f;

    private bool isChasing = false;

    void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.position);

        if (distToPlayer <= detectionRange)
        {
            isChasing = true;
        }

        if (isChasing)
        {
            // Move towards player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Optional: Flip sprite or billboard
        }

        // Ready to attack
        if (distToPlayer <= attackRange)
        {
            Debug.Log("CAT ATTACKS");
            // TODO: Trigger attack or damage later
        }
    }
}

