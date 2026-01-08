using UnityEngine;

public class GravityFlip : MonoBehaviour
{
    private FirstPersonController movement;
    private ChangeScale scale;
    private bool flipped;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = GetComponent<FirstPersonController>();
        scale = GetComponent<ChangeScale>();
        flipped = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            movement.flipped = !movement.flipped;
            movement.gravity = -movement.gravity;
            movement.jumpStrength = -movement.jumpStrength;
            movement.cameraTransform.position = new Vector3(transform.position.x, transform.position.y + (movement.flipped ? -0.75f * scale.targetScale : 0.75f * scale.targetScale), transform.position.z);
            movement.velocity = new Vector3(
                    movement.velocity.x,
                    movement.flipped ? 5f : -5f,
                    movement.velocity.z
                );
                movement.cameraTransform.localRotation = Quaternion.Euler(0f, 0f, 180f);
                
        }
    }
}
