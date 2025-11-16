using UnityEngine;

public class CameraIntroHover : MonoBehaviour
{
    public Transform gameplayCamPosition;   // Final gameplay camera position
    public float introHeight = 20f;         // How high above the player it starts
    public float hoverTime = 2f;            // How long to hover at the top
    public float moveDownTime = 2f;         // How long the descent takes

    private Vector3 startPos;
    private Quaternion startRot;
    private float timer = 0f;
    private bool movingDown = false;

    void Start()
    {
        // Save the final camera position
        startPos = gameplayCamPosition.position;
        startRot = gameplayCamPosition.rotation;

        // Move camera above the gameplay position
        transform.position = startPos + new Vector3(0, introHeight, 0);

        // Look at the gameplay position
        transform.LookAt(startPos);
    }

    void Update()
    {
        // Hover at the top for some time
        if (!movingDown)
        {
            timer += Time.deltaTime;

            if (timer >= hoverTime)
            {
                timer = 0f;
                movingDown = true; // start descending
            }
            return;
        }

        // Now move down to gameplay position
        if (movingDown && timer < moveDownTime)
        {
            timer += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, timer / moveDownTime);

            transform.position = Vector3.Lerp(transform.position, startPos, t);
            transform.rotation = Quaternion.Lerp(transform.rotation, startRot, t);
        }
        else if (movingDown)
        {
            // Snap to final camera location
            transform.position = startPos;
            transform.rotation = startRot;

            // Disable script when done
            enabled = false;
        }
    }
}
