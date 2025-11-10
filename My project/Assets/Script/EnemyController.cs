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
            // Move toward player but ignore Y-axis
            Vector3 dir = (player.position - transform.position).normalized;
            dir.y = 0f;

            transform.position += dir * moveSpeed * Time.deltaTime;
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

           // Optional: stick to ground
RaycastHit hit;
if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 5f))
{
    transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
}


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
        yield return new WaitForSeconds(0.5f); // animation hit timing
        // player.GetComponent<PlayerHealth>()?.TakeDamage(attackDamage);
        yield return new WaitForSeconds(0.5f); // end delay
        animator.SetBool("IsAttacking", false);
    }

    public void Die()
    {
        isDead = true;
        animator.SetBool("IsDead", true);
        GetComponent<CharacterController>().enabled = false;
        this.enabled = false;
    }
}
