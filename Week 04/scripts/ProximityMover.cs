using UnityEngine;

public class ProximityMover : MonoBehaviour
{
    public float proximityDistance = 3f;
    
    public bool moveAway = false;
    public float moveSpeed = 5f;
    public float moveDistance = 2f;
    
    private bool isPlayerNearby = false;
    private GameObject player;
    private Vector3 originalPosition;
    
    void Start()
    {     
        // Store original position for movement
        originalPosition = transform.position;
        
        // Find the player once and store the reference
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {    
        // Calculate distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        
        // Check if player is within proximity distance
        isPlayerNearby = distanceToPlayer <= proximityDistance;

        if (isPlayerNearby)
        {
            // Move away from player
            Vector3 directionFromPlayer = (transform.position - player.transform.position).normalized;
            Vector3 targetPosition = originalPosition + (directionFromPlayer * moveDistance);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Return to original position
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);
        }
        
    }
}
