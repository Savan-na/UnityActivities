using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.name);
        other.gameObject.SetActive(false);
    }
} 