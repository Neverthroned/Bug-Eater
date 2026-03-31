using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public Light spotLight;

    public Color normalColor = Color.yellow;
    public Color alertColor = Color.red;

    private void Start()
    {
        // Color switching functionality
        spotLight.color = normalColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks for player and changes spotlight color
        if (other.attachedRigidbody != null &&
            other.attachedRigidbody.CompareTag("Player"))
        {
            spotLight.color = alertColor;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Exit functionality
        if (other.attachedRigidbody != null &&
            other.attachedRigidbody.CompareTag("Player"))
        {
            spotLight.color = normalColor;
        }
    }
}