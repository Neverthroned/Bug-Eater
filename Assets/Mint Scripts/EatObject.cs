using UnityEngine;

public class EatObject : MonoBehaviour, IInteractable
{
    public string promptMessage = "Press E to Eat";

    private TimerHealthBar timer;

    void Start()
    {
        timer = FindObjectOfType<TimerHealthBar>();
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