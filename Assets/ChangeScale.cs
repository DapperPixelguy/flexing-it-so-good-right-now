using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ChangeScale : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private FirstPersonController movement;
    public float targetScale;
    public float scaleSpeed = 5f;

    void Start()
    {
        // This is 2 by default! This means you can go lower by 3 steps, and up by 3 steps.
        targetScale = Mathf.Clamp(transform.localScale.x, 0.5f, 3.5f); 
        movement = GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            targetScale = Mathf.Clamp(targetScale + 0.5f, 0.5f, 3f);
            movement.moveSpeed = Mathf.Clamp(movement.moveSpeed + 2f, 2f, 12f);            
            movement.jumpStrength = Mathf.Clamp(movement.jumpStrength + 2f, 2f, 10f);
            movement.gravity = Mathf.Clamp(movement.gravity - 4f, -28f, -8f);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            targetScale = Mathf.Clamp(targetScale - 0.5f, 0.5f, 3f);
            movement.moveSpeed = Mathf.Clamp(movement.moveSpeed - 2f, 2f, 12f);

            if (targetScale <= 2f)
            {
                movement.jumpStrength = Mathf.Clamp(movement.jumpStrength - 2f, 4f, 10f);
            }              
            movement.gravity = Mathf.Clamp(movement.gravity + 4f, -28f, -8f);
        }

        float currentScale = transform.localScale.x;
        float newScale = Mathf.Lerp(currentScale, targetScale, scaleSpeed * Time.deltaTime);

        transform.localScale = Vector3.one * newScale;
        movement.controller.height = newScale;
    }
}
