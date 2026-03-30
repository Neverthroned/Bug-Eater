using UnityEngine;

public class KeyPad2Buttons : MonoBehaviour
{

    public string value = "1";

    public void OnButtonPressed()
    {
        Debug.Log("Pressed: " + value);
    }
}
