using UnityEngine;
using UnityEngine.Audio;

public class EatObject : MonoBehaviour, Interactable
{
    public string promptMessage = "Press E to Eat";
    private TimerHealthBar timer;

    void Start()
    {
        timer = FindFirstObjectByType<TimerHealthBar>();
    }

    public void Interact()
    {
        Debug.Log("Interact pressed");
        BugManager manager = FindFirstObjectByType<BugManager>();

        if (manager != null)
            manager.StartBug(gameObject);  // Send bug type to the manager

        Destroy(gameObject);
    }

    public string GetPrompt()
    {
        return promptMessage;
    }
}