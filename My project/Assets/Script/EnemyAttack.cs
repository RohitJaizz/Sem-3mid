using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackRange = 2.5f;
    public float damage = 10f;
    public float attackRate = 1f;

    private float nextAttackTime = 0f;
    private Transform player;
    private PlayerHealth playerHealth;
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        // Find correct player with PlayerHealth attached
        GameObject[] taggedPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obj in taggedPlayers)
        {
            PlayerHealth foundHealth = obj.GetComponent<PlayerHealth>();
            if (foundHealth != null)
            {
                player = obj.transform;
                playerHealth = foundHealth;
                break;
            }
        }

        if (player == null)
            Debug.LogWarning("⚠️ No Player with PlayerHealth found!");
        else
            Debug.Log("✅ Found player: " + player.name);
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

    // 👇 Called by animation event
    public void DealDamage()
    {
        if (playerHealth == null || player == null)
        {
            Debug.LogWarning("⚠️ DealDamage() called but playerHealth is missing!");
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            playerHealth.TakeDamage((int)damage);
            Debug.Log($"💥 Player took {damage} damage!");

        }
        else
        {
            Debug.Log("⚠️ Attack missed — player out of range.");
        }
    }
}
