using UnityEngine;

public class CaterpillarAudioTrigger : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float minVolume = 0.2f;
    [SerializeField] private float maxVolume = 1f;

    [Header("Distance Settings")]
    [SerializeField] private float triggerDistance = 50f;
    [SerializeField] private float minDistance = 2f; // Distance at which max volume is reached

    private Transform playerTransform;
    private bool isAudioPlaying = false;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    private void Update()
    {
        if (playerTransform == null || audioSource == null)
            return;

        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= triggerDistance)
        {
            if (!isAudioPlaying)
            {
                audioSource.Play();
                isAudioPlaying = true;
            }

            float volume = CalculateVolume(distance);
            audioSource.volume = volume;
        }
        else
        {
            if (isAudioPlaying)
            {
                audioSource.Stop();
                isAudioPlaying = false;
            }
        }
    }

    private float CalculateVolume(float distance)
    {
        if (distance <= minDistance)
        {
            return maxVolume;
        }

        if (distance >= triggerDistance)
        {
            return minVolume;
        }

        float normalizedDistance = (distance - minDistance) / (triggerDistance - minDistance);
        float volume = Mathf.Lerp(maxVolume, minVolume, normalizedDistance);

        return volume;
    }
}
