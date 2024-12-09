using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform ball; // The target to follow
    public Vector3 offset; // Offset for the camera's position relative to the ball

    void Start()
    {
        // Set a default offset if none is provided
        if (offset == Vector3.zero)
        {
            offset = new Vector3(0f, 10f, -10f);
        }
    }

    void LateUpdate()
    {
        // Ensure the ball is assigned before trying to follow it
        if (ball != null)
        {
            transform.position = ball.position + offset;
            transform.LookAt(ball);
        }
    }
}
