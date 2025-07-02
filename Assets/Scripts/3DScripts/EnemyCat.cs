using UnityEngine;

public class EnemyCat : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 5f;
    public float moveSpeed = 2f;
    public float attackRange = 1.2f;
    public int attackDamage = 1;
    public float attackCooldown = 2f;

    private bool isChasing = false;
    private float lastAttackTime = -999f;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.position);

        if (distToPlayer <= detectionRange)
        {
            isChasing = true;
        }

        if (isChasing)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        if (isChasing && distToPlayer <= attackRange)
        {
            if (Time.time - lastAttackTime > attackCooldown)
            {
                lastAttackTime = Time.time;
                Attack();
            }
        }
    }

    void Attack()
    {
        Debug.Log("CAT ATTACKS!");
        player.GetComponent<PlayerHealthSystem>().TakeDamage(attackDamage);
    }
}

