using UnityEngine;
using UnityEngine.XR;

public class ChangeScaleVR : MonoBehaviour
{
    private CharacterController controller;
    public float targetScale;
    public float scaleSpeed = 5f;
    private bool yPressedLastFrame = false;
    private bool xPressedLastFrame = false;

    // Movement parameters (adjust these in inspector or reference your actual movement script)
    public float moveSpeed = 6f;
    public float jumpStrength = 6f;
    public float gravity = -18f;
    public bool flipped = false;

    void Start()
    {
        targetScale = Mathf.Clamp(transform.localScale.x, 0.5f, 3.5f);
        controller = GetComponent<CharacterController>();

        if (controller == null)
        {
            Debug.LogError("CharacterController not found on XR Origin!");
        }
    }

    void Update()
    {
        InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        // Y Button - Scale Up
        bool yPressed = false;
        if (leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out yPressed) && yPressed && !yPressedLastFrame)
        {
            targetScale = Mathf.Clamp(targetScale + 0.5f, 0.5f, 3f);
            moveSpeed = Mathf.Clamp(moveSpeed + 2f, 2f, 12f);
            jumpStrength = Mathf.Clamp(jumpStrength + 2f, 2f, 10f);

            if (!flipped)
            {
                gravity = Mathf.Clamp(gravity - 4f, -28f, -8f);
            }
            else
            {
                gravity = Mathf.Clamp(gravity + 4f, 8f, 28f);
            }
        }
        yPressedLastFrame = yPressed;

        // X Button - Scale Down
        bool xPressed = false;
        if (leftController.TryGetFeatureValue(CommonUsages.primaryButton, out xPressed) && xPressed && !xPressedLastFrame)
        {
            targetScale = Mathf.Clamp(targetScale - 0.5f, 0.5f, 3f);
            moveSpeed = Mathf.Clamp(moveSpeed - 2f, 2f, 12f);

            if (targetScale <= 2f)
            {
                jumpStrength = Mathf.Clamp(jumpStrength - 2f, 4f, 10f);
            }

            if (!flipped)
            {
                gravity = Mathf.Clamp(gravity + 4f, -28f, -8f);
            }
            else
            {
                gravity = Mathf.Clamp(gravity - 4f, 8f, 28f);
            }
        }
        xPressedLastFrame = xPressed;

        // Smoothly interpolate to target scale
        float currentScale = transform.localScale.x;
        float newScale = Mathf.Lerp(currentScale, targetScale, scaleSpeed * Time.deltaTime);

        transform.localScale = Vector3.one * newScale;

        //if (controller != null)
        //{
        //    controller.height = newScale * 1.8f; // Multiply by base height
        //    controller.radius = newScale * 0.5f; // Scale radius too for collision
        //}
    }
}