using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class TeleportBlock : MonoBehaviour
{

    public Transform teleportTarget;
    public Image fadeImage;
    public float fadeDuration = 0.25f;

    private bool isTeleporting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (!isTeleporting && other.CompareTag("Player"))
        {
            StartCoroutine(Teleport(other));
        }
    }


    IEnumerator Teleport(Collider other)
    {
        CharacterController controller = other.GetComponent<CharacterController>();

        if (controller != null)
        {

            yield return StartCoroutine(Fade(0f, 1f, 0f));

            controller.enabled = false;
            other.transform.position = teleportTarget.position;
            controller.enabled = true;

            StartCoroutine(Fade(1f, 0f, 0.25f));

        }
        else
        {
            other.transform.position = teleportTarget.position;
        }
    }

    IEnumerator Fade(float from, float to, float wait)
    {
        yield return new WaitForSeconds(wait);
        float elapsed = 0f;
        Color colour = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            colour.a = Mathf.Lerp(from, to, elapsed / fadeDuration);
            fadeImage.color = colour;
            yield return null;
        }
        colour.a = to;
        fadeImage.color = colour;
    }
}
