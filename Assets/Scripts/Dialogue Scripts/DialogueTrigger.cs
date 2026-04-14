using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, Interactable
{
    [Header("Dialogue")]
    public Dialogue dialogue;

    [Header("Prompt")]
    [SerializeField] private string interactPrompt = "Press E to talk";

    //for dialogue_choices
    [Header("Optional Choice After Dialogue")]
    public bool showChoiceAfterDialogue = false;
    public ChoicePanelController choicePanel;


    // for a very special snail
    [Header("Optional Snail Controller")]
    public SnailChoiceController snailController;

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

        if (manager.IsOpen())
        {
            manager.DisplayNextSentence();
            return;
        }

        // If this NPC has a snail controller, use it instead
        if (snailController != null)
        {
            snailController.StartSnailConversation();
            return;
        }

        // normal NPC behaviour
        manager.StartDialogue(dialogue);
    }
}