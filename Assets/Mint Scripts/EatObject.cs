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
        Debug.Log("Bug eaten");

        BugManager manager = FindFirstObjectByType<BugManager>();

        if (timerHealthBar != null)
        {
            timerHealthBar.StartMetabolism();
            timerHealthBar.ResetTimer();
        }

        if (manager != null)
            manager.StartBug(gameObject);


        PlayerAudio.Instance.PlayEatSound();
        Destroy(transform.root.gameObject);
    }

    public string GetPrompt()
    {
        return promptMessage;
    }
}