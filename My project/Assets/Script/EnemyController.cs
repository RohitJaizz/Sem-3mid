using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float stoppingDistance = 2f;
    public float attackRate = 1f;
    public int attackDamage = 10;

    private Transform player;
    private Animator animator;
    private float nextAttackTime = 0f;
    private bool isDead = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stoppingDistance)
        {
            // Move toward player
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

            animator.SetBool("IsAttacking", false);
            animator.SetFloat("Speed", 1f);
        }
        else
        {
            // Attack player
            animator.SetFloat("Speed", 0f);

            if (Time.time >= nextAttackTime)
            {
                StartCoroutine(Attack());
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    IEnumerator Attack()
    {
        animator.SetBool("IsAttacking", true);

        // Wait for animation to hit moment
        yield return new WaitForSeconds(0.5f); // adjust to animation timing

        // Here you can damage the player
        // player.GetComponent<PlayerHealth>()?.TakeDamage(attackDamage);

        yield return new WaitForSeconds(0.5f); // wait till animation ends
        animator.SetBool("IsAttacking", false);
    }

    public void Die()
    {
        isDead = true;
        animator.SetBool("IsDead", true);
        GetComponent<CharacterController>().enabled = false;
        this.enabled = false; // stop logic
    }
}
