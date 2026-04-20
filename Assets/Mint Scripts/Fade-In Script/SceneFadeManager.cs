using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFadeManager : MonoBehaviour
{
    public static SceneFadeManager Instance;

    public Image fadeImage;
    public float fadeDuration = 1f;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetAlpha(1f); // start black AFTER load
        StartCoroutine(FadeIn());
    }


    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeIn()
    {
        float t = fadeDuration;

        while (t > 0f)
        {
            t -= Time.unscaledDeltaTime;
            SetAlpha(t / fadeDuration);
            yield return null;
        }

        SetAlpha(0f);
    }

    IEnumerator FadeOut(string sceneName)
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            SetAlpha(t / fadeDuration);
            yield return null;
        }

        SetAlpha(1f);

        yield return null; // let frame finish

        SceneManager.LoadScene(sceneName);

    }

    void SetAlpha(float alpha)
    {
        Color c = fadeImage.color;
        c.a = alpha;
        fadeImage.color = c;
    }
}
