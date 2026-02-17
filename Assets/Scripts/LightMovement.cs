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
    
    // Distance at which the enemy will start chasing
    public float chaseRange = 10f;

    private NavMeshAgent agent;
    private float timer;

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
    }

    void Update()
    {
        timer += Time.deltaTime;

        // If AI has reached its destination or timer expired, pick a new point
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (timer >= wanderDelay)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, NavMesh.AllAreas);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }

        if (player == null) return;

        // Calculate distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Chase if within range
        if (distanceToPlayer <= chaseRange)
        {
            ChasePlayer();
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
        // Direction toward the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Move enemy toward player
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Optional: Rotate enemy to face player
        transform.LookAt(player);
    }

    // Draw chase range in editor for easy debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}