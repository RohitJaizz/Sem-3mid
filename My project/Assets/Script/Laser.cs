using UnityEngine;

public class Laser : MonoBehaviour
{
    public float damage = 25f;
    public float life = 2f;

    void Start()
    {
        Destroy(gameObject, life);
    }

    void OnTriggerEnter(Collider other)
    {
        // Avoid hitting the gun or shooter
        if (other.CompareTag("Enemy"))
        {
            var health = other.GetComponentInParent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            // optional: create impact VFX here

            // Destroy laser on hit (or let it pass-through if you want piercing)
            Destroy(gameObject);
        }
    }
}
