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
        DialogueManager manager = FindFirstObjectByType<DialogueManager>();
        EatObject eater = GetComponent<EatObject>();

        if (manager.IsOpen())
        {
            manager.DisplayNextSentence();
            return;
        }

        // Snail with choice system
        if (snailController != null && showChoiceAfterDialogue)
        {
            snailController.StartSnailConversation();
            return;
        }

        // Everything else: dialogue then eat
        if (eater != null)
        {
            manager.StartDialogue(dialogue, () =>
            {
                eater.Interact();
                FindFirstObjectByType<PlayerInteraction>()?.EndInteraction();
            });
            return;
        }

        // fallback
        manager.StartDialogue(dialogue);
    }
}