using UnityEngine;

public class IsoCameraFollow : MonoBehaviour
{
    [Header("References")]
    public Transform target; // Assign your Player root here

    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(10f, 15f, -10f); // Isometric offset
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Fixed camera position relative to player
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Always look at the player
        transform.LookAt(target.position + Vector3.up * 1.5f); // Look slightly above the ground
    }
}
