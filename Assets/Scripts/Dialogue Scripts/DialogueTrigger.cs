using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, Interactable
{
    [Header("Dialogue")]
    public Dialogue dialogue;

    [Header("Prompt")]
    [SerializeField] private string interactPrompt = "Press E to talk";

    // Whether the player is close enough (inside the trigger collider)
    private bool playerInRange = false;


    public string GetPrompt()
    {
        return playerInRange ? interactPrompt : string.Empty;
    }

    public void Interact()
    {
        Debug.Log("Interact pressed");

        DialogueManager manager = FindFirstObjectByType<DialogueManager>();

        // Opens dialogue, if dialogue is already open, display the next sentence upon interact button being pressed
        if (manager.IsOpen())
            manager.DisplayNextSentence();
        else
            manager.StartDialogue(dialogue);
    }
}