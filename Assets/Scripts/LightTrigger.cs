using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public Light spotLight;

    public Color normalColor = Color.yellow;
    public Color alertColor = Color.red;

    private void Start()
    {
        spotLight.color = normalColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.name);
        if (CompareTag(other.tag = "Player"))
        {

            Debug.Log("Triggered by: " + other.name);

            spotLight.color = alertColor;
            Debug.Log("Entered");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CompareTag(other.tag = "Player")) 
            {


            spotLight.color = normalColor;
        }
    }
}