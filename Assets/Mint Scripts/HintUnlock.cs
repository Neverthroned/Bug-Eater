using UnityEngine;

public class HintUnlock : MonoBehaviour
{
    public int hintIndex; // which keypad hint this unlocks
    private bool hasBeenRead = false;

    KeypadManager keypadManager;

    void Start()
    {
        keypadManager = FindFirstObjectByType<KeypadManager>();
    }

    public void UnlockHint()
    {
        if (hasBeenRead) return;

        hasBeenRead = true;
        keypadManager?.UnlockHint(hintIndex);

        Debug.Log("Player discovered keypad hint " + hintIndex);
    }
}