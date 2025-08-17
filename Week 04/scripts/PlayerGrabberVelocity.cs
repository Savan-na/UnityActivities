using UnityEngine;

public class PlayerGrabberVelocity : MonoBehaviour
{
    public float grabDistance = 3f;
    private GameObject grabbedObject = null;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grabbedObject == null)
            {
                GrabObject();
            }
            else
            {
                ThrowObject();
            }
        }
    }
    
    void GrabObject()
    {
        // Find objects with Grabbable script
        Grabbable[] grabbables = FindObjectsOfType<Grabbable>();
        
        foreach (Grabbable grabbable in grabbables)
        {
            if (!grabbable.isGrabbed)
            {
                float distance = Vector3.Distance(transform.position, grabbable.transform.position);
                if (distance <= grabDistance)
                {
                    // Grab the first object we find
                    grabbedObject = grabbable.gameObject;
                    grabbable.isGrabbed = true;
                    
                    // Make object follow player
                    grabbedObject.transform.SetParent(transform);
                    grabbedObject.transform.localPosition = new Vector3(0, 1, 1);
                    
                    Debug.Log("Grabbed: " + grabbedObject.name);
                    break;
                }
            }
        }
    }
    
    void ThrowObject()
    {
        if (grabbedObject != null)
        {
            // Release object
            grabbedObject.transform.SetParent(null);
            
            // Get the Grabbable component and mark as not grabbed
            Grabbable grabbable = grabbedObject.GetComponent<Grabbable>();
            if (grabbable != null)
            {
                grabbable.isGrabbed = false;
            }
            
            // Apply throwing velocity (Meta Quest style!)
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Calculate throw velocity
                Vector3 throwDirection = transform.forward;
                float throwSpeed = 10f;
                
                // Add upward component for natural arc
                Vector3 throwVelocity = (throwDirection + Vector3.up * 0.3f) * throwSpeed;
                
                // Apply velocity directly
                rb.velocity = throwVelocity;
                
                Debug.Log("Threw: " + grabbedObject.name + " with velocity: " + throwVelocity);
            }
            
            grabbedObject = null;
        }
    }
}
