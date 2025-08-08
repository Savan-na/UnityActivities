# Activity 4: Raycasting in Unity

## Objective
Learn to use Unity's raycasting system to detect objects in the scene, create line-of-sight detection, and build interactive targeting systems.

## Prerequisites
- Complete Activities 1-3 to have a basic scene with debugging, UI, and math setup
- Understanding of Unity scripting, UI, and vector math concepts

## Instructions

### Step 1: Prepare Your Scene
1. Open your Unity project from Activity 3
2. Ensure you have your DebugCube, TargetCube, Platform, and FallingCube
3. Make sure your UI and math calculations are working

### Step 2: Create a Raycast Script
1. **Create a new script**:
   - In the Project window, right-click in the Scripts folder
   - Select `Create > C# Script`
   - Name it `SimpleRaycast`

2. **Write the simple raycast script**:
```csharp
using UnityEngine;

public class SimpleRaycast : MonoBehaviour
{
    public float raycastDistance = 10f;

    void Update()
    {
        // Create a ray from the object's position in the forward direction
        Ray ray = new Ray(transform.position, transform.forward);
        
        // Perform the raycast
        RaycastHit hit;
        bool hitDetected = Physics.Raycast(ray, out hit, raycastDistance);
        
        if (hitDetected)
        {
            // Log the name of the hit object
            Debug.Log("Hit: " + hit.transform.name);
        }
    }
    
    void OnDrawGizmos()
    {
        // Check if we hit something
        RaycastHit hit;
        bool hitDetected = Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance);
        
        // Draw the raycast line - red if no hit, green if hit
        Gizmos.color = hitDetected ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * raycastDistance);
        
        // Draw a wire sphere at the hit point if we hit something
        if (hitDetected)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(hit.point, 0.1f);
        }
    }
}
```

### Step 3: Set Up the Raycast
1. **Attach the script to your rotating cube**:
   - Select your DebugCube (the rotating one)
   - In the Inspector, click `Add Component`
   - Search for "SimpleRaycast" and add it

2. **Test the raycast**:
   - Click Play
   - You should see a red line extending from the rotating cube
   - When the ray hits an object, a green wire sphere appears at the hit point
   - Check the Console for hit messages

### Step 4: Experiment with the Raycast
1. **Move objects around**:
   - Move the TargetCube to different positions
   - Watch how the raycast detects it as the cube rotates
   - Notice when the green sphere appears and disappears

2. **Add more objects**:
   - Create additional cubes in the scene
   - Watch which objects the raycast hits as the cube rotates
   - Check the Console to see the names of hit objects

## Understanding Raycasting Concepts

### **What is Raycasting?**
- **Raycast**: A line that extends from a point in a direction to detect objects
- **Hit Detection**: Determines what objects the ray intersects with
- **Distance**: Measures how far the ray travels before hitting something
- **Hit Point**: The exact location where the ray hits an object

### **Common Uses**
- **Line of Sight**: Check if an object can see another object
- **Collision Detection**: Detect when objects are close to each other
- **UI Interaction**: Detect when the mouse clicks on UI elements
- **AI Targeting**: Help AI determine what to aim at

## Extension Activities

### **Experiment with Raycast Distance**
- Change the raycastDistance value in the Inspector
- Watch how the red line gets longer or shorter
- See how it affects what objects get detected

### **Mouse-Based Selection**
- Create a raycast from the camera through the mouse position
- Use it to select objects by clicking on them

**Hints:**
- Use `Camera.main.ScreenPointToRay(Input.mousePosition)` to create a ray from mouse position
- Check `Input.GetMouseButtonDown(0)` to detect mouse clicks
- Use `Physics.Raycast()` with the camera ray to detect objects
- Add visual feedback (change object color) when selected


## Outcome
A basic understanding of Unity's raycasting system, including hit detection and visual debugging. You should be able to use raycasting to detect objects in 3D space and understand how it can be used for line-of-sight detection and object targeting.

## Save Your Work
**Don't forget to save your scene and project!**
- Press Ctrl+S (Windows) or Cmd+S (Mac) to save your scene
- Go to File → Save Project to save all your work
