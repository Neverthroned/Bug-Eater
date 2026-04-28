using UnityEngine;
using UnityEngine.Audio;

public class EatObject : MonoBehaviour, Interactable
{
    public string promptMessage = "Press E to Eat";
    public TimerHealthBar timerHealthBar;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip eatSound;

    void Start()
    {
        timerHealthBar = FindFirstObjectByType<TimerHealthBar>();
    }

    public void Interact()
    {
        Debug.Log("Bug eaten");

        if (audioSource != null && eatSound != null)
            audioSource.PlayOneShot(eatSound);

        BugManager manager = FindFirstObjectByType<BugManager>();

        if (timerHealthBar != null)
        {
            timerHealthBar.StartMetabolism();
            timerHealthBar.ResetTimer();
        }

        if (manager != null)
            manager.StartBug(transform.root.gameObject);

        FindFirstObjectByType<PlayerInteraction>()?.EndInteraction();

        Destroy(transform.root.gameObject);
    }

    public string GetPrompt()
    {
        return promptMessage;
    }
}