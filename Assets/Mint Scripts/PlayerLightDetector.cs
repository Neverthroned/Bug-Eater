using UnityEngine;

public class PlayerLightDetector : MonoBehaviour
{
    public TimerHealthBar timer;

    void Start()
    {
        timer = FindFirstObjectByType<TimerHealthBar>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if this collider belongs to a LightSpawner parent
        if (other.GetComponentInParent<LightTrigger>() != null ||
            other.transform.root.CompareTag("LightSpawner"))
        {
            if (timer != null)
                timer.StartMetabolism();
                timer.EnterLightHazard();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<LightTrigger>() != null ||
            other.transform.root.CompareTag("LightSpawner"))
        {
            if (timer != null)
                timer.ExitLightHazard();
        }
    }
}