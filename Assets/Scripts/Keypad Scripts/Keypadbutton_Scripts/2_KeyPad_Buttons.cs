using UnityEngine;

public class KeyPad2Buttons : MonoBehaviour
{

    public string value = "1";
    public KeypadManager keypadManager;

    public void OnButtonPressed()
    {
        Debug.Log("Pressed: " + value);

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
