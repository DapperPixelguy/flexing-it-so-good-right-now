using Unity.VisualScripting;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float gravity = -20f;

    public Vector3 velocity { get; set; }
    public float jumpStrength = 10f;


    private float xRotation = 0f;

    public CharacterController controller;
    private Transform cameraTransform;

    void Start()
    {
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
            //velocity.y += 10;
            velocity = new Vector3(velocity.x, jumpStrength, velocity.z);
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
            //velocity.y = -2f;
            velocity = new Vector3(velocity.x, -2f, velocity.z);
        }
        //velocity.y += gravity * Time.deltaTime;
        velocity = new Vector3(velocity.x, velocity.y + gravity * Time.deltaTime, velocity.z);
        controller.Move(velocity * Time.deltaTime);

        // Camera rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);      
    }
}