using UnityEngine;

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
         if (timer != null)
        {
            timer.ResetTimer();
        }
        
        Destroy(gameObject);
    }

    public string GetPrompt()
    {
        return promptMessage;
    }
}