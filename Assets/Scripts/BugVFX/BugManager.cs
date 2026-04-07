using UnityEngine;

public class BugManager : MonoBehaviour
{
    [Header("Bugs")]
    public GameObject CBugPrefab; // Caterpillar
    public GameObject SBugPrefab; // Snail

    public void StartBug(GameObject eatenBug)
    {
        // Compare the eaten bug's name to known prefab names
        // Unity appends "(Clone)" to instantiated prefabs, so we use Contains
        if (SBugPrefab != null )
        {
            Debug.Log("Ate a Snail!");
            SnailVFX();
        }
        else if (CBugPrefab != null && eatenBug.name.Contains(CBugPrefab.name))
        {
            Debug.Log("Ate a Caterpillar!");
            CaterpillarVFX();
        }
        else
        {
            Debug.LogWarning("Ate an unknown bug: " + eatenBug.name);
        }
    }

    public void SnailVFX()
    {
        // VFX logic here later
    }

    public void CaterpillarVFX()
    {
        // VFX logic here later
    }
}