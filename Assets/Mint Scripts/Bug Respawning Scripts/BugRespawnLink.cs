using UnityEngine;

public class BugRespawnLink : MonoBehaviour
{
    [HideInInspector]
    public BugRespawner respawner;

    public void NotifyBugEaten()
    {
        if (respawner != null)
            respawner.BugWasEaten();
    }
}