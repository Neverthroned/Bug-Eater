using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightCullingManager : MonoBehaviour
{
    public Transform target; // player or camera
    public float enableDistance = 30f;
    public float disableDistance = 40f; // hysteresis to prevent flicker
    public int maxLightsEnabled = 50; // hard cap

    [Header("Exclusion Settings")]
    public string alwaysOnTag = "AlwaysOn";

    private List<Light> allLights = new List<Light>();
    private List<Light> activeLights = new List<Light>();


    void Start()
    {
        // Find all lights in scene
        Light[] lights = FindObjectsOfType<Light>();

        foreach (var l in lights)
        {
            // Skip lights with the AlwaysOn tag
            if (l.CompareTag("AlwaysOn"))
                continue;

            allLights.Add(l);
        }
    }

    void Update()
    {
        if (target == null) return;

        activeLights.Clear();

        // Ensure tagged lights are ALWAYS enabled
        Light[] taggedLights = GameObject.FindGameObjectsWithTag(alwaysOnTag)
                                         .Select(go => go.GetComponent<Light>())
                                         .Where(l => l != null)
                                         .ToArray();

        foreach (var l in taggedLights)
        {
            if (!l.enabled)
                l.enabled = true;
        }

        // Sort lights by distance to target
        allLights.Sort((a, b) =>
        {
            float distA = Vector3.SqrMagnitude(a.transform.position - target.position);
            float distB = Vector3.SqrMagnitude(b.transform.position - target.position);
            return distA.CompareTo(distB);
        });

        int enabledCount = 0;

        foreach (var light in allLights)
        {
            float dist = Vector3.Distance(light.transform.position, target.position);

            bool shouldEnable =
                dist < enableDistance &&
                enabledCount < maxLightsEnabled;

            // Hysteresis to reduce flickering
            if (light.enabled)
            {
                if (dist > disableDistance || enabledCount >= maxLightsEnabled)
                {
                    light.enabled = false;
                }
                else
                {
                    activeLights.Add(light);
                    enabledCount++;
                }
            }
            else
            {
                if (shouldEnable)
                {
                    light.enabled = true;
                    activeLights.Add(light);
                    enabledCount++;
                }
            }
        }
    }
}