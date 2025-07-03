using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public Transform attackPoint;
    public float attackRange = 1.5f;
    public int damage = 5;
    public float attackCooldown = 0.6f;
    public float windUpTime = 0.2f;
    public LayerMask enemyLayer;

    [Header("Animation")]
    public Animator animator;

    private bool isAttacking = false;
    private PlayerMovement3d movement;

    void Start()
    {
        movement = GetComponent<PlayerMovement3d>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        if (movement != null)
            movement.canMove = false;

        if (animator != null)
            animator.SetTrigger("attack");

        yield return new WaitForSeconds(windUpTime);

        DoDamageCheck();

        yield return new WaitForSeconds(attackCooldown - windUpTime);

        if (movement != null)
            movement.canMove = true;

        isAttacking = false;
    }

    void DoDamageCheck()
    {
        Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
        Debug.Log($"Found {hits.Length} targets in range.");

        foreach (Collider hit in hits)
        {
            IDamageable damageable = hit.GetComponentInParent<IDamageable>();
            if (damageable != null)
            {
                Debug.Log($"Dealing {damage} damage to {hit.name}");
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



