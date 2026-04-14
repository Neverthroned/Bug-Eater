using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeypadButtonGlow : MonoBehaviour
{
    public Image glowImage;
    public float fadeSpeed = 8f;

    //make em a little more illuminated
    RectTransform rect;
    Vector3 originalScale;
    public float pressedScale = 1.35f;

    private Coroutine fadeRoutine;

    void Awake()
    {
        rect = glowImage.rectTransform;
        originalScale = rect.localScale;

        if (glowImage != null)
            glowImage.color = new Color(0, 1, 0, 0); // invisible green
    }

    public void PressedGlow()
    {
        SetGlowColor(Color.green);
        FadeTo(0.25f);
    }

    public void ResetGlow()
    {
        FadeTo(0f);
    }

    public void ErrorGlow()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);
        StartCoroutine(ErrorFlash());
    }

    IEnumerator ErrorFlash()
    {
        // force red + visible immediately
        FadeTo(0.35f);   // softer red
        glowImage.color = new Color(1, 0, 0, 0.9f);

        yield return new WaitForSeconds(0.4f);
        FadeTo(0f);

        // fully reset back to invisible (removes green memory)
        glowImage.color = new Color(0, 1, 0, 0);
    }

    void SetGlowColor(Color color)
    {
        glowImage.color = new Color(color.r, color.g, color.b, glowImage.color.a);
    }

    void FadeTo(float targetAlpha)
    {
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(FadeRoutine(targetAlpha));
    }

    IEnumerator FadeRoutine(float targetAlpha)
    {
        float startAlpha = glowImage.color.a;
        Vector3 startScale = rect.localScale;
        Vector3 targetScale = targetAlpha > 0 ? originalScale * pressedScale : originalScale;

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * fadeSpeed;

            float a = Mathf.Lerp(startAlpha, targetAlpha, t);
            glowImage.color = new Color(glowImage.color.r, glowImage.color.g, glowImage.color.b, a);

            rect.localScale = Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }
    }
}