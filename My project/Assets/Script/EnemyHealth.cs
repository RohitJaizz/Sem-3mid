using UnityEngine;
using System; // Required for Action

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;

    // Event to notify when this enemy dies
    public event Action onEnemyDeath;

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // Call event so spawner knows this enemy is dead
        onEnemyDeath?.Invoke();

        // Destroy this enemy
        Destroy(gameObject);
    }
}
