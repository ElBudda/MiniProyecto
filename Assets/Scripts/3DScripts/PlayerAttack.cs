using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public Transform attackPoint;
    public float attackRange = 1.5f;
    public int damage = 50;
    public LayerMask enemyLayer;

    [Header("Animation")]
    public Animator animator;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("Attempting attack!");

        // Play attack animation
        if (animator != null)
        {
            animator.SetTrigger("attack");
        }
        else
        {
            Debug.LogWarning("Animator not assigned!");
        }

        // Find enemies in range
        Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
        Debug.Log($"Found {hits.Length} targets in range.");

        foreach (Collider hit in hits)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                Debug.Log($"Dealing damage to {hit.gameObject.name}");
                damageable.TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}


