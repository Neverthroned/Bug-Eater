using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLightDetector : MonoBehaviour
{
    public TimerHealthBar timer;
    public string endingSceneName = "EndingScene";
    private bool endingTriggered = false;

    void Start()
    {
        timer = FindFirstObjectByType<TimerHealthBar>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<LightTrigger>() != null ||
            other.transform.root.CompareTag("LightSpawner"))
        {
            if (timer != null)
            {
                timer.StartMetabolism();
                timer.EnterLightHazard();
            }

            TryTriggerEnding();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<LightTrigger>() != null ||
            other.transform.root.CompareTag("LightSpawner"))
        {
            if (timer != null)
                timer.ExitLightHazard();
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