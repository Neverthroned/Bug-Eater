using UnityEngine;
using System.Collections;

public class PlayerSpawnFromGameManager : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        if (GameManager.Instance == null)
            yield break;

        Vector3 spawn = GameManager.Instance.returnPosition;

        // IMPORTANT FIX
        if (spawn == Vector3.zero)
        {
            Debug.Log("No saved position — using safe spawn.");
            yield break; // DO NOT MOVE PLAYER
        }

        transform.position = spawn + Vector3.up * 1f;
    }
}