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
        targetScale = Mathf.Clamp(transform.localScale.x, 0.5f, 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            targetScale = Mathf.Clamp(targetScale + 0.5f, 0.5f, 3.5f);

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            targetScale = Mathf.Clamp(targetScale - 0.5f, 0.5f, 3.5f);
        }

        float currentScale = transform.localScale.x;
        float newScale = Mathf.Lerp(currentScale, targetScale, scaleSpeed * Time.deltaTime);

        transform.localScale = Vector3.one * newScale;
        movement.controller.height = newScale;
    }
}
