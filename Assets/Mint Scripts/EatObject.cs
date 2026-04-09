using UnityEngine;
using UnityEngine.Audio;

public class EatObject : MonoBehaviour, Interactable
{
    public string promptMessage = "Press E to Eat";
    public TimerHealthBar timerHealthBar;

    void Start()
    {
        timerHealthBar = FindFirstObjectByType<TimerHealthBar>();
    }

    public void Interact()
    {
        Debug.Log("Interact pressed");
        BugManager manager = FindFirstObjectByType<BugManager>();

        //resets the timer
        if (timerHealthBar != null)
        timerHealthBar.StartMetabolism();
        timerHealthBar.ResetTimer();

        if (manager != null)
            manager.StartBug(gameObject);  // Send bug type to the manager

        Destroy(gameObject);


    }

    public string GetPrompt()
    {
        return promptMessage;
    }
}