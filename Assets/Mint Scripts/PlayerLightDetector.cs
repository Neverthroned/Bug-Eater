using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLightDetector : MonoBehaviour
{
    public TimerHealthBar timer;
    public string endingSceneName = "EndingScene";
    private bool endingTriggered = false;

    //audio for enemy

    public AudioSource enemyLightAudio;
    private int lightZones = 0; // counts how many lights we are inside

    void Start()
    {
        timer = FindFirstObjectByType<TimerHealthBar>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<LightTrigger>() != null ||
            other.transform.root.CompareTag("LightSpawner"))
        {
            lightZones++;   // count how many lights we are inside

            if (timer != null)
            {
                timer.StartMetabolism();
                timer.EnterLightHazard();
            }

            // start sound ONLY when entering first light
            if (lightZones == 1 && !enemyLightAudio.isPlaying)
                enemyLightAudio.Play();

            TryTriggerEnding();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<LightTrigger>() != null ||
            other.transform.root.CompareTag("LightSpawner"))
        {
            lightZones = Mathf.Max(0, lightZones - 1);

            if (timer != null)
                timer.ExitLightHazard();

            // stop sound ONLY when leaving last light
            if (lightZones == 0)
                enemyLightAudio.Stop();
        }
    }

    void TryTriggerEnding()
    {
        if (endingTriggered) return;

        if (GameManager.Instance != null &&
            GameManager.Instance.saidNoToSnail)
        {
            endingTriggered = true;
            SceneManager.LoadScene(endingSceneName);
        }
    }
}