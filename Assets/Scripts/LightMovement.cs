using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Simple wandering AI using Unity's NavMeshAgent.
/// Attach this script to a GameObject with a NavMeshAgent component.
/// Ensure a NavMesh is baked in your scene.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class WanderingAI : MonoBehaviour
{
    [Header("Wandering Settings")]
    [Tooltip("Maximum distance from the current position to pick a random point.")]
    public float wanderRadius = 10f;

    [Tooltip("Time to wait before picking a new destination.")]
    public float wanderDelay = 100f;

    [Tooltip("Movement speed of the AI.")]
    public float moveSpeed = 100f;

    // Reference to player's transform
    public Transform player;

    //private float spawn = GameObject.FindGameObjectWithTag("LightSpawner").transform;
    public Transform spawn;
    
    // Distance at which the enemy will start chasing
    public float chaseRange = 10f;

    //Distance the light will travel before returning to spawn
    public float movementRange = 100f;

    private NavMeshAgent agent;
    private float timer;

    private Vector3 wanderTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        timer = wanderDelay;

        // Automatically find the player if not assigned
        if (player == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (spawn == null && GameObject.FindGameObjectWithTag("LightSpawner") != null)
        {
            spawn = GameObject.FindGameObjectWithTag("LightSpawner").transform;
        }

        wanderTarget = transform.position; // initialize to starting position
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (player == null)
        {
            Debug.Log("Player is null!");
            return;
        }

        // Calculate distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        // Calculate distance to light spawn
        float distanceToSpawn = Vector3.Distance(transform.position, spawn.position);

        if (distanceToSpawn > movementRange)
        {
            ReturnToSpawn();
        }
        // Chase if within range
        else if (distanceToPlayer <= chaseRange)
        {
            ChasePlayer();
        }
        else if (timer >= wanderDelay)
        {
            wanderTarget = RandomNavSphere(transform.position, wanderRadius, NavMesh.AllAreas);
            agent.SetDestination(wanderTarget);
            timer = 0;
        }

        if (distanceToPlayer <= chaseRange)
        {
            Debug.Log("AHHHHHHHH");
            moveSpeed = 50;
        }
        else 
        {
            moveSpeed = 10f;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;
        randomDirection += origin;

        NavMeshHit navHit;
        if (NavMesh.SamplePosition(randomDirection, out navHit, dist, layermask))
        {
            return navHit.position;
        }

        // If no valid point found, return original position
        return origin;
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void ReturnToSpawn()
    {
        agent.SetDestination(spawn.position);
    }

    // Draw chase range and movement range in editor for easy debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, movementRange);
    }
}