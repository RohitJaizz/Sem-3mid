using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    public event Action onEnemyDeath; // 👈 Add this line

    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetBool("IsDead", true);

        onEnemyDeath?.Invoke(); // 👈 Notify spawner
        Destroy(gameObject, 3f); // Destroy after death animation
    }
}
