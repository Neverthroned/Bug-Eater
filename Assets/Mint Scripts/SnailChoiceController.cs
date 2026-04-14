using UnityEngine;
using UnityEngine.SceneManagement;

IEnumerator ReturnAfterDialogue()
{
    // Wait until dialogue closes
    while (dialogueManager.IsOpen())
        yield return null;

    // Load previous scene
    SceneManager.LoadScene(GameManager.Instance.returnSceneName);
}

public class SnailChoiceController : MonoBehaviour
{
    [Header("Dialogues")]
    public Dialogue introDialogue;     // first time talking
    public Dialogue yesDialogue;       // player eats snail
    public Dialogue noDialogue;        // player refuses

    [Header("References")]
    public ChoicePanelController choicePanel;
    private DialogueManager dialogueManager;

    // 0 = first meeting
    // 1 = refused before (loop state)
    // 2 = eaten (finished)
    private int snailState = 0;

    //bool for checking if the snail scene already played, for repeat attempts.
    private bool introPlayed = false;

    void Start()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
    }

    // Called by DialogueTrigger instead of normal StartDialogue
    public void StartSnailConversation()
    {
        if (GameManager.Instance.snailIntroSeen)
        {
            // Skip straight to choice if already talked before
            choicePanel.SetActive(true);
            return;
        }

        dialogueManager.StartDialogue(introDialogue);
        StartCoroutine(WaitForDialogueThenShowChoice());
    }

    // YES button will call this
    public void ChooseYes()
    {
        GameManager.Instance.snailIntroSeen = true;
        choicePanel.SetActive(false);

        dialogueManager.StartDialogue(yesDialogue);
        Destroy(gameObject, 2f); // snail eaten 
    }

    // NO button will call this
    public void ChooseNo()
    {
        GameManager.Instance.snailIntroSeen = true;
        choicePanel.SetActive(false);

        dialogueManager.StartDialogue(noDialogue);
        StartCoroutine(ReturnAfterDialogue());
    }
}

    void EatSnail()
    {
        Destroy(gameObject);
    }
}