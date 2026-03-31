using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadTrigger : MonoBehaviour, Interactable
{
    [Header("Prompt")]
    [SerializeField] private string interactPrompt = "Press E to talk";

    // Whether the player is close enough (inside the trigger collider)
    private bool playerInRange = false;

    // When player in range give interact prompt (and allow player to interact)
    public string GetPrompt()
    {
        return playerInRange ? interactPrompt : string.Empty;
    }

    // Interact and open keypad
    public void Interact()
    {
        Debug.Log("Interact pressed");

        KeypadManager manager = FindFirstObjectByType<KeypadManager>();

        if (manager.IsOpen())
            manager.ExitKeypad();
        else
            manager.StartKeypad();
    }
}
