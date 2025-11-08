using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackRange = 2f;
    public float damage = 10f;
    public float attackRate = 1f;

    private float nextAttackTime = 0f;
    private Transform player;
    private PlayerHealth playerHealth;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player != null)
            playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange && Time.time >= nextAttackTime)
        {
            animator.SetBool("IsAttacking", true);
            nextAttackTime = Time.time + 1f / attackRate;
        }
        else
        {
            animator.SetBool("IsAttacking", false);
        }
    }

    // This function is called by the Animation Event
    public void DealDamage()
    {
        if (playerHealth == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log("Player took damage!");
        }
    }
}
