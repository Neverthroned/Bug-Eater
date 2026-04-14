using UnityEngine;

public class BugManager : MonoBehaviour
{
    [Header("Bugs")]
    public GameObject CBugPrefab; // Caterpillar
    public GameObject SBugPrefab; // Snail

    public void StartBug(GameObject eatenBug)
    {
        // Always get the ROOT object (important!)
        GameObject rootBug = eatenBug.transform.root.gameObject;

        // Identify type based on prefab name match
        if (rootBug.name.Contains(SBugPrefab.name))
        {
            Debug.Log("Ate a Snail!");
            SnailVFX();
        }
        else if (rootBug.name.Contains(CBugPrefab.name))
        {
            Debug.Log("Ate a Caterpillar!");
            CaterpillarVFX();
        }
        else
        {
            Debug.LogWarning("Ate an unknown bug: " + rootBug.name);
        }

        // Destroy the whole bug (not just the child collider)
        Destroy(rootBug);
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