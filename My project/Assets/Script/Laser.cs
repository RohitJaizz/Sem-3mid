using UnityEngine;

public class Laser : MonoBehaviour
{
    public float damage = 25f;   // Amount of damage laser deals
    public float life = 2f;      // Lifetime of the laser in seconds

    void Start()
    {
        // Automatically destroy laser after its lifetime expires
        Destroy(gameObject, life);
    }

    void OnTriggerEnter(Collider other)
    {
        // Only hit objects tagged "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Try to find an EnemyHealth component (in case it's on parent)
            EnemyHealth health = other.GetComponentInParent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(damage); // Pass float damage
            }

            // Optional: add particle or hit effect here

            // Destroy laser after hitting an enemy
            Destroy(gameObject);
        }
    }
}
