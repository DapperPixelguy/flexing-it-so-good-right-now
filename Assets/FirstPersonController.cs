using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float gravity = -9.81f;
    public float scaleSpeed = 5f;


    private bool clean = false;
    private float targetScale;
    private float xRotation = 0f;

    private CharacterController controller;
    private Vector3 velocity;
    private Transform cameraTransform;

    void Start()
    {
        targetScale = Mathf.Clamp(transform.localScale.x, 0.5f, 5f);
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        cameraTransform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        cameraTransform.parent = transform; // Attach camera to player
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
      
        // Allow jumping

        if (controller.isGrounded && Input.GetKey(KeyCode.Space))
        {
            print(velocity);
            velocity.y += 10;
            print(velocity);
        }

        // Player movement
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        controller.Move(move * Time.deltaTime);


        // Apply gravity
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Camera rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    

        if (Input.GetKeyDown(KeyCode.E))
        {
            targetScale += 0.5f;            
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            targetScale -= 0.5f;            
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Vector3 pos = transform.position;
            if (clean)
            {
                pos.y = -4.173007f;
                clean = false;
            }else
            {
                pos.y = -18.31867f;
                clean = true;
            }
                // Set Y to exactly 5
                controller.enabled = false;  // Temporarily disable CharacterController
            transform.position = pos;    // Move object
            controller.enabled = true;   // Re-enable CharacterController
            velocity.y = 0f;             // Reset vertical speed to avoid instant falling
        }

        float currentScale = transform.localScale.x;
        float newScale = Mathf.Lerp(currentScale, targetScale, scaleSpeed * Time.deltaTime);
        transform.localScale = Vector3.one * newScale;
        controller.height = newScale;
    }
}