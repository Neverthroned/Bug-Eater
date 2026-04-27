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
    public float sHoldDuration = 2f;      // How long the effect stays at full opacity
    public float cHoldDuration = 2f;

    private void Start()
    {
        // Ensure overlays begin fully transparent
        SetAlpha(SBugVFXOverlay, 0f);
        SetAlpha(CBugVFXOverlay, 0f);
    }

    public void StartBug(GameObject eatenBug)
    {
        GameObject root = eatenBug.transform.root.gameObject;

        if (root.CompareTag("Snail"))
        if (SBugPrefab != null && eatenBug.name.Contains(SBugPrefab.name))
        {
            Debug.Log("Ate a Snail!");
            SnailVFX();
        }
        else if (root.CompareTag("Caterpillar"))
        {
            Debug.Log("Ate a Caterpillar!");
            CaterpillarVFX();
        }
        else
        {
            Debug.LogWarning("Ate an unknown bug: " + root.name);

        }

        // Destroy the whole bug (not just the child collider)
        Destroy(root);
    }

    public void SnailVFX()
    {
        // VFX logic here
        StartCoroutine(SFadeIn());
    }

    public void CaterpillarVFX()
    {
        // VFX logic here
        StartCoroutine(CFadeIn());
    }

    // --- Snail Coroutines ---

    IEnumerator SFadeIn()
    {
        yield return StartCoroutine(FadeOverlay(SBugVFXOverlay, 0f, 1f, fadeDuration));
        yield return new WaitForSeconds(sHoldDuration);
        StartCoroutine(SFadeOut());
    }

    IEnumerator SFadeOut()
    {
        yield return StartCoroutine(FadeOverlay(SBugVFXOverlay, 1f, 0f, fadeDuration));
    }

    // --- Caterpillar Coroutines ---

    IEnumerator CFadeIn()
    {
        yield return StartCoroutine(FadeOverlay(CBugVFXOverlay, 0f, 0.2f, fadeDuration));
        yield return new WaitForSeconds(cHoldDuration);
        StartCoroutine(CFadeOut());
    }

    IEnumerator CFadeOut()
    {
        yield return StartCoroutine(FadeOverlay(CBugVFXOverlay, 0.2f, 0f, fadeDuration));
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