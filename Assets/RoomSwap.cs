using UnityEngine;
using UnityEngine.InputSystem.XR;

public class RoomSwap : MonoBehaviour
{
    private bool clean;
    private FirstPersonController movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clean = true;
        movement = GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Vector3 pos = transform.position;
            if (clean)
            {
                //pos.y = -4.173007f;
                pos.y += 14.145663f;
                clean = false;
            }
            else
            {
                //pos.y = -18.31867f;
                pos.y -= 14.145663f;
                clean = true;
            }
            // Set Y to exactly 5
            movement.controller.enabled = false;  // Temporarily disable CharacterController
            transform.position = pos;    // Move object
            movement.controller.enabled = true;   // Re-enable CharacterController
            //movement.velocity = new Vector3(movement.velocity.x, 0f, movement.velocity.z);             // Reset vertical speed to avoid instant falling
        }
    }
}