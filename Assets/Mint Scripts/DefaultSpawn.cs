using UnityEngine;

public class DefaultSpawn : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.returnPosition == Vector3.zero)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = transform.position;
        }
    }
}
