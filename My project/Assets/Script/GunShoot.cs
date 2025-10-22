using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [Header("Laser Settings")]
    public GameObject laserPrefab;
    public Transform firePoint;
    public float laserSpeed = 50f;
    public float laserLife = 2f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootLaser();
        }
    }

    void ShootLaser()
    {
        if (laserPrefab == null || firePoint == null) return;

        GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = laser.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = laser.AddComponent<Rigidbody>();
            rb.useGravity = false;
        }

        rb.linearVelocity = firePoint.forward * laserSpeed;
        Destroy(laser, laserLife);
    }
}
