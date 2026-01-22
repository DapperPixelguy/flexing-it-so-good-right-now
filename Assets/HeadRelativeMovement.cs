using UnityEngine;

public class HeadRelativeMovement : MonoBehaviour
{
    public Transform head;      // XR Camera (HMD) or Main Camera
    public float speed = 2f;

    void Update()
    {
        float input = Input.GetAxis("Vertical"); // W/S or joystick Y
        if (Mathf.Abs(input) < 0.01f) return;

        // Get forward direction of head
        Vector3 forward = head.forward;

        // Remove vertical component
        forward.y = 0f;
        forward.Normalize();

        transform.position += forward * input * speed * Time.deltaTime;
    }
}
