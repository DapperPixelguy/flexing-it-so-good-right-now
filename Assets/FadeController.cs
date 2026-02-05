using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance;

    public Image fadeImage;
    public float fadeDuration = 0.25f;
    public float exitRotation = 0f;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
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

    public IEnumerator FadeInOut(float fadeInDelay, float fadeOutDelay, System.Action onFadeOutComplete)
    {
        yield return StartCoroutine(Fade(0f, 1f, fadeInDelay));
        onFadeOutComplete?.Invoke();
        yield return StartCoroutine(Fade(1f, 0f, fadeOutDelay));
    }
}
