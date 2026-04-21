using UnityEngine;

public class BugManager : MonoBehaviour
{
    [Header("Bugs")]
    public GameObject CBugPrefab; // Caterpillar
    public GameObject SBugPrefab; // Snail

    public void StartBug(GameObject eatenBug)
    {
        GameObject root = eatenBug.transform.root.gameObject;

        if (root.CompareTag("Snail"))
        {
            Debug.Log("Ate a Snail!");
            SnailVFX();
        }
        else if (root.CompareTag("Caterpillar"))
        {
            Debug.Log("Ate a Caterpillar!");
            CaterpillarVFX();
        }
        else
        {
            Debug.LogWarning("Ate an unknown bug: " + root.name);

        }

        // Destroy the whole bug (not just the child collider)
        
    }

    public void SnailVFX()
    {
        // VFX logic here
    }

    public void CaterpillarVFX()
    {
        // VFX logic here
    }
}