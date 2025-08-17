# Activity 2: Object Selection and Throwing

## Objective
Create a scene where a player can move around, select objects by pressing space when near them, and throw selected objects by pressing space again. This activity builds on proximity detection and introduces object parenting and physics.

## Prerequisites
- Complete Week 02 and Week 03 activities
- Complete Week 04 Activity 1 (Proximity Reactions)
- Basic C# scripting knowledge

## Instructions

### Step 1: Project Setup
1. **Open your Week 4 Activity 1 scene**
2. **Save your scene** (Ctrl+S)
3. **Ensure you have**: Ground, Player with SimpleWASD script, and some objects

### Step 2: Create a Simple Grabbable Script
1. **Create a new script** called `Grabbable`
2. **Write this simple script**:
```csharp
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public bool isGrabbed = false;
    
    void Start()
    {
        // Make sure object has a Rigidbody
        if (GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }
    }
}
```

3. **Add this script to a cube** in your scene

### Step 3: Create the Player Script with Stubbed Functions
1. **Create a new script** called `PlayerGrabber`
2. **Write this script to set up the grab and throw functionality**:
```csharp
using UnityEngine;

public class PlayerGrabber : MonoBehaviour
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
        // TODO: Implement grab logic
        Debug.Log("Grab function called - not yet implemented");
    }
    
    void ThrowObject()
    {
        // TODO: Implement throw logic
        Debug.Log("Throw function called - not yet implemented");
    }
}
```

3. **Add this script to your Player**

### Step 4: Test the Basic Structure
1. **Enter Play mode**
2. **Move close to the grabbable object**
3. **Press space near the object**:
   - Watch the Console for debug messages
   - Verify that "attempting to grab object" appears
   - Verify that "Grab function called - not yet implemented" appears
4. **Press space again**:
   - Verify that "attempting to throw object" appears
   - Verify that "Throw function called - not yet implemented" appears

**Expected behavior**: You should see debug messages in the Console when pressing space, but no actual grabbing or throwing should occur yet.

### Step 5: Implement the Grab Function
1. **Update the GrabObject function** in your PlayerGrabber script:
```csharp
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
```

2. **Test the grab functionality**:
   - Enter Play mode
   - Move close to the GrabbableObject (within 3 units)
   - Press space
   - The object should now be attached to the player
   - Move around and verify the object follows you

### Step 6: Implement the Throw Function
1. **Update the ThrowObject function** in your PlayerGrabber script:
```csharp
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
        
        // Apply throwing force
        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
        }
        
        Debug.Log("Threw: " + grabbedObject.name);
        grabbedObject = null;
    }
}
```

2. **Test the complete grab and throw system**:
   - Enter Play mode
   - Move close to the GrabbableObject
   - Press space to grab it
   - Move around to position yourself
   - Press space again to throw it
   - The object should fly forward and fall with physics

### Step 7: Create Multiple Grabbable Objects
1. **Duplicate your GrabbableObject**:
   - Select the GrabbableObject in the Hierarchy
   - Press Ctrl+D (Windows) or Cmd+D (Mac) to duplicate
   - Position the duplicate at (7, 0.5, 0)
   - Rename it to "GrabbableObject2"

2. **Create a third object**:
   - GameObject → 3D Object → Sphere
   - Rename it to "GrabbableSphere"
   - Position it at (3, 0.5, 3)
   - Add the Grabbable script component

3. **Test with multiple objects**:
   - Enter Play mode
   - Try grabbing different objects
   - Notice that you can only grab one object at a time
   - Test throwing and then grabbing another object

### Step 8: Implement Raycast-Based Selection (Optional)
1. **Replace the GrabObject function** with raycast-based selection:
```csharp
void GrabObject()
{
    Ray ray = new Ray(transform.position, transform.forward);
    RaycastHit hit;
    
    if (Physics.Raycast(ray, out hit, grabDistance))
    {
        Grabbable grabbable = hit.collider.GetComponent<Grabbable>();
        if (grabbable != null && !grabbable.isGrabbed)
        {
            // Grab the object we're looking at
            grabbedObject = grabbable.gameObject;
            grabbable.isGrabbed = true;
            
            // Make object follow player
            grabbedObject.transform.SetParent(transform);
            grabbedObject.transform.localPosition = new Vector3(0, 1, 1);
            
            Debug.Log("Grabbed: " + grabbedObject.name + " using raycast");
        }
    }
}
```

2. **Test the raycast system**:
   - Enter Play mode
   - Look at a grabbable object (turn to face it)
   - Press space to grab it
   - The object should only be grabbed if you're looking directly at it
   - Try looking away and pressing space - nothing should happen

## Understanding the Core Concepts

### **Object Parenting**
- `transform.SetParent(transform)` makes the object a child of the player
- `transform.localPosition` positions the object relative to the player
- When the player moves, the child object moves with it

### **Physics and Throwing**
- `rb.AddForce()` applies force to make objects move
- `ForceMode.Impulse` gives instant force (like throwing)
- The object falls with gravity after being thrown

### **Simple State Management**
- `isGrabbed` boolean tracks if an object is currently held
- `grabbedObject` reference stores which object we're holding
- We can only hold one object at a time

## Extension Activities

### **Simple Improvements**
1. **Change throw direction**: Modify the throw force to go up: `transform.forward + transform.up`
2. **Add sound**: Play a sound when grabbing/throwing (use AudioSource)
3. **Visual feedback**: Change object color when grabbed

### **Velocity-Based Throwing**
**Challenge**: Replace the simple `AddForce` throwing with velocity-based throwing that feels more natural.

**Implementation**:
1. **Replace the throwing code** in your `ThrowObject()` function:
```csharp
// Instead of: rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
// Use this:

// Calculate throw velocity
Vector3 throwDirection = transform.forward;
float throwSpeed = 10f;

// Add upward component for natural arc
Vector3 throwVelocity = (throwDirection + Vector3.up * 0.3f) * throwSpeed;

// Apply velocity directly
rb.velocity = throwVelocity;
```

2. **Why this is better**:
   - Objects follow the exact velocity you specify
   - More predictable and controllable
   - Feels more natural and responsive

### **Advanced Challenges**
1. **Pull objects towards you**: Instead of grabbing, apply force towards the player
2. **Collision-based grabbing**: Automatically grab objects when you touch them
3. **Throw all objects in proximity**: Throw multiple objects away from you at once

### **First-Person Camera Movement**
**Challenge**: Make the camera follow the player's movement and rotation for a first-person experience.

**Implementation**:
1. **Find the Main Camera** in your scene (usually at the top of the Hierarchy)
2. **Drag the camera** to be a child of the Player object
3. **Position the camera** relative to the player:
   - Set the camera's Local Position to (0, 1.5, 0) - this puts it at eye level
   - The camera will now move and rotate with the player
4. **Test the system**:
   - Enter Play mode
   - Move with WASD and turn with A/D
   - The camera follows your movement and rotation
   - You now have a first-person view!

**Why this is useful**:
- **Better immersion**: See the world from the player's perspective
- **Easier object interaction**: Look directly at objects you want to grab
- **Realistic movement**: Camera follows your head movement
- **Foundation for VR**: Similar to how VR headsets work

**Advanced camera setup**:
- Try different camera heights: (0, 1.2, 0) for crouching, (0, 2.0, 0) for tall view
- Add camera smoothing by adjusting the camera's position gradually
- Experiment with different field of view (FOV) settings in the camera component

## Save Your Work
**Don't forget to save your scene and project!**
