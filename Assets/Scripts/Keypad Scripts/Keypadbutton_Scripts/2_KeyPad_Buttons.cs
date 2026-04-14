using UnityEngine;

public class KeyPad2Buttons : MonoBehaviour
{

    public string value = "1";
    public KeypadManager keypadManager;

    //this helps handle the cool key button glow
    KeypadButtonGlow glow;

    void Start()
    {
        glow = GetComponent<KeypadButtonGlow>();
    }


    public void OnButtonPressed()
    {
        Debug.Log("Pressed: " + value);
        glow?.PressedGlow();

        if (keypadManager != null)
        {
            keypadManager.PressKey(value);
        }
        else
        {
            Debug.LogWarning("KeypadManager is not assigned on " + gameObject.name);
        }
    }
}
