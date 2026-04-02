using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class RoomSwap : MonoBehaviour
{
    public bool clean;
    public bool swappable;
    public bool swapping;
    private FirstPersonController movement;
    private Stopwatch swapCooldown = new Stopwatch();
    public Image fadeImage;
    public Image VRFadeImage;
    public float fadeDuration = 0.25f;
    //private InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clean = true;
        swappable = true;
        movement = GetComponent<FirstPersonController>();
        swapCooldown.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.locked || !swappable)
        {
            return;
        }
        // VR controls - If B pressed, swap rooms. FIX THE BLACK FADE NOT APPEARING IN VR!
        InputDevice rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        bool bPressed = false;
        if (rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bPressed) && bPressed)
        {
            if (swapCooldown.ElapsedMilliseconds < 500)
            {
                return;
            }
            swapCooldown.Restart();

            StartCoroutine(SwapWithFade());
        }


        // Flatscreen controls - If G pressed, swap rooms.
        if (Input.GetKeyDown(KeyCode.G))
        {
            
            if (swapCooldown.ElapsedMilliseconds < 500) {
                return;
            }
            swapCooldown.Restart();

            StartCoroutine(SwapWithFade());
        }
    }
    IEnumerator SwapWithFade()
    {
        swapping = true;
        yield return StartCoroutine(Fade(0f, 1f));
        swapping = false;

        Vector3 pos = transform.position;

        if (clean)
        {
            pos.y += 14.145663f;
            clean = false;
        }
        else
        {
            pos.y -= 14.145663f;
            clean = true;
        }
        movement.controller.enabled = false;
        transform.position = pos;
        movement.controller.enabled = true;

        yield return StartCoroutine(Fade(1f, 0f));
    }

    IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;
        Color colour = fadeImage.color;

        while (elapsed < fadeDuration) {
            elapsed += Time.deltaTime;
            colour.a = Mathf.Lerp(from, to, elapsed / fadeDuration);
            fadeImage.color = colour;
            yield return null;
        }
        colour.a = to;
        fadeImage.color = colour;
    }
}