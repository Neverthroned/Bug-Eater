using UnityEngine;

public class NarrativeTrigger : MonoBehaviour, Interactable
{
    [Header("Prompt")]
    [SerializeField] private string interactPrompt = "Press E to talk";

    [Header("Narrative")]
    [SerializeField] private Sprite narrativeImage;

    [Header("Audio")]
    [SerializeField] private AudioClip interactSound;

    private AudioSource audioSource;

    // Whether the player is close enough (inside the trigger collider)
    private bool playerInRange = false;

    // When player in range give interact prompt (and allow player to interact)
    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public string GetPrompt()
    {
        return playerInRange ? interactPrompt : string.Empty;
    }

    // Interact and open keypad
    public void Interact()
    {
        Debug.Log("Interact pressed");

        NarrativeManager manager = FindFirstObjectByType<NarrativeManager>();

        if (manager.IsOpen())
            manager.ExitNarrative();
        else
        {
            if (interactSound != null)
                audioSource.PlayOneShot(interactSound);

            manager.StartNarrative(narrativeImage);
        }
    }
}