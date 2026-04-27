using UnityEngine;
using System.Collections;

public class BugRespawner : MonoBehaviour
{
    [Header("Respawn Settings")]
    public GameObject bugPrefab;   // prefab to respawn
    public float respawnDelay = 10f;

    private GameObject currentBug;

    void Start()
    {
        // First spawn when scene starts
        SpawnBug();
    }

    void SpawnBug()
    {
        currentBug = Instantiate(
            bugPrefab,
            transform.position,
            transform.rotation
        );

        // Tell the bug who spawned it
        BugRespawnLink link = currentBug.GetComponentInChildren<BugRespawnLink>();
        if (link != null)
            link.respawner = this;
    }

    public void BugWasEaten()
    {
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnBug();
    }
}