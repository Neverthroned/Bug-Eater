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
        {
            Debug.Log("Interact pressed");

            BugManager manager = FindFirstObjectByType<BugManager>();
            
            manager.StartBug();

            Destroy(gameObject);
        }
    }

    public string GetPrompt()
    {
        return promptMessage;
    }
}