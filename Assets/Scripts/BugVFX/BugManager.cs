using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BugManager : MonoBehaviour
{
    [Header("Bugs")]
    public GameObject CBugPrefab; // Caterpillar
    public GameObject SBugPrefab; // Snail

    [Header("UI")]
    public RawImage SBugVFXOverlay;
    public RawImage CBugVFXOverlay;

    [Header("VFX Settings")]
    public float fadeDuration = 1f;      // How long the fade in/out takes
    public float holdDuration = 2f;      // How long the effect stays at full opacity

    private void Start()
    {
        // Ensure overlays begin fully transparent
        SetAlpha(SBugVFXOverlay, 0f);
        SetAlpha(CBugVFXOverlay, 0f);
    }

    public void StartBug(GameObject eatenBug)
    {
        if (SBugPrefab != null && eatenBug.name.Contains(SBugPrefab.name))
        {
            Debug.Log("Ate a Snail!");
            SnailVFX();
        }
        else if (CBugPrefab != null && eatenBug.name.Contains(CBugPrefab.name))
        {
            Debug.Log("Ate a Caterpillar!");
            CaterpillarVFX();
        }
        else
        {
            Debug.LogWarning("Ate an unknown bug: " + eatenBug.name);
        }
    }

    public void SnailVFX()
    {
        StartCoroutine(SFadeIn());
    }

    public void CaterpillarVFX()
    {
        StartCoroutine(CFadeIn());
    }

    // --- Snail Coroutines ---

    IEnumerator SFadeIn()
    {
        yield return StartCoroutine(FadeOverlay(SBugVFXOverlay, 0f, 1f, fadeDuration));
        yield return new WaitForSeconds(holdDuration);
        StartCoroutine(SFadeOut());
    }

    IEnumerator SFadeOut()
    {
        yield return StartCoroutine(FadeOverlay(SBugVFXOverlay, 1f, 0f, fadeDuration));
    }

    // --- Caterpillar Coroutines ---

    IEnumerator CFadeIn()
    {
        yield return StartCoroutine(FadeOverlay(CBugVFXOverlay, 0f, 1f, fadeDuration));
        yield return new WaitForSeconds(holdDuration);
        StartCoroutine(CFadeOut());
    }

    IEnumerator CFadeOut()
    {
        yield return StartCoroutine(FadeOverlay(CBugVFXOverlay, 1f, 0f, fadeDuration));
    }

    // --- Shared Utility ---

    // Reusable fade method so S and C coroutines aren't duplicating logic
    IEnumerator FadeOverlay(RawImage overlay, float fromAlpha, float toAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = overlay.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(fromAlpha, toAlpha, elapsed / duration);
            overlay.color = color;
            yield return null; // Wait one frame
        }

        // Snap to the final value to avoid floating point drift
        color.a = toAlpha;
        overlay.color = color;
    }

    private void SetAlpha(RawImage overlay, float alpha)
    {
        Color color = overlay.color;
        color.a = alpha;
        overlay.color = color;
    }
}