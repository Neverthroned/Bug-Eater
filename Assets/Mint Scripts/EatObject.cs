using UnityEngine;

public class EatObject : MonoBehaviour, IInteractable
{
    public string promptMessage = "Press E to Eat";

    public void Interact()
    {
        Destroy(gameObject);
    }

    public string GetPrompt()
    {
        return promptMessage;
    }
}