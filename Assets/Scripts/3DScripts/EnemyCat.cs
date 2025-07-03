using System.Collections;
using UnityEngine;

public class EnemyCat : MonoBehaviour, IDamageable
{
    [Header("References")]
    public Transform player;
    private Rigidbody rb;

    [Header("Detection and Movement")]
    public float detectionRange = 8f;
    public float moveSpeed = 2.5f;

    [Header("Attack Range Settings")]
    public float preferredLungeRange = 2.5f;
    public float lungeBuffer = 0.3f;
    public float attackRange = 1.5f;

    [Header("Attack Settings")]
    public int attackDamage = 1;
    public float telegraphTime = 0.8f;
    public float recoverTime = 0.5f;

    [Header("Adaptive Lunge Settings")]
    public float lungeForceMultiplier = 1.2f;
    public float minLungeForce = 4f;
    public float maxLungeForce = 12f;
    public float maxCancelRange = 8f;

    [Header("Health")]
    public int health = 5;
    public float knockbackForce = 5f;

    private enum CatState { Idle, Chasing, Telegraphing, Lunging, Recovering }
    private CatState currentState = CatState.Idle;

    // Stored for lunge
    private Vector3 storedLungeDirection;
    private float storedPlayerDistance;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void Update()
    {
        if (player == null) return;

        switch (currentState)
        {
            case CatState.Idle:
                CheckForPlayer();
                break;
            case CatState.Chasing:
                ChasePlayer();
                break;
                // Telegraphing, Lunging are coroutines
        }
    }

    void CheckForPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionRange)
        {
            currentState = CatState.Chasing;
            Debug.Log("[CAT] STATE CHANGE: Idle → Chasing");
        }
    }

    void ChasePlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > detectionRange)
        {
            currentState = CatState.Idle;
            Debug.Log("[CAT] STATE CHANGE: Chasing → Idle");
            return;
        }

        if (distance > preferredLungeRange + lungeBuffer)
        {
            // Move closer
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            Debug.Log("[CAT] Moving closer to preferred range...");
        }
        else if (distance < preferredLungeRange - lungeBuffer)
        {
            // Optional: back up
            Vector3 direction = (transform.position - player.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            Debug.Log("[CAT] Backing up to maintain range...");
        }
        else
        {
            Debug.Log("[CAT] In lunge range! Preparing to telegraph.");
            StartCoroutine(TelegraphAttack());
        }
    }

    IEnumerator TelegraphAttack()
    {
        currentState = CatState.Telegraphing;
        Debug.Log("[CAT] STATE CHANGE: Chasing → Telegraphing");
        Debug.Log("[CAT] Telegraphing...");

        // Lock direction and distance at start
        storedLungeDirection = (player.position - transform.position).normalized;
        storedPlayerDistance = Vector3.Distance(transform.position, player.position);
        Debug.Log($"[CAT] Locked direction. Snapshot distance: {storedPlayerDistance}");

        yield return new WaitForSeconds(telegraphTime);

        float currentDistance = Vector3.Distance(transform.position, player.position);
        if (currentDistance > maxCancelRange)
        {
            Debug.Log("[CAT] Player escaped too far! Cancelling attack.");
            currentState = CatState.Chasing;
            yield break;
        }

        StartCoroutine(DoLunge());
    }

    IEnumerator DoLunge()
    {
        currentState = CatState.Lunging;
        Debug.Log("[CAT] STATE CHANGE: Telegraphing → Lunging");

        float calculatedLungeForce = storedPlayerDistance * lungeForceMultiplier;
        calculatedLungeForce = Mathf.Clamp(calculatedLungeForce, minLungeForce, maxLungeForce);
        Debug.Log($"[CAT] Calculated lunge force: {calculatedLungeForce}");

        rb.AddForce(storedLungeDirection * calculatedLungeForce, ForceMode.VelocityChange);
        Debug.Log("[CAT] Lunging!");

        yield return new WaitForSeconds(0.2f); // Impact window

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            Debug.Log("[CAT] Lunge HIT the player!");
            player.GetComponent<PlayerHealthSystem>()?.TakeDamage(attackDamage);
        }
        else
        {
            Debug.Log("[CAT] Lunge MISSED!");
        }

        yield return new WaitForSeconds(recoverTime);
        currentState = CatState.Chasing;
        Debug.Log("[CAT] STATE CHANGE: Recovering → Chasing");
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"[CAT] Took {amount} damage. Health now: {health}");

        if (player != null && rb != null)
        {
            Vector3 knockDirection = (transform.position - player.position).normalized;
            rb.AddForce(knockDirection * knockbackForce, ForceMode.Impulse);
            Debug.Log("[CAT] Knocked back!");
        }

        if (currentState == CatState.Telegraphing)
        {
            StopAllCoroutines();
            Debug.Log("[CAT] INTERRUPTED during telegraph!");
            currentState = CatState.Chasing;
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("[CAT] DIED!");
        Destroy(gameObject);
    }
}
