# Activity 4: Detecting Interactions Between Objects

## Objective
Learn to use Unity's collision detection system to create interactive objects and trigger events when objects touch each other.

## Prerequisites
- Complete Activity 3 to understand player movement
- Basic understanding of Unity components (Rigidbody, Collider)
- A scene with a player object that can move around

## Instructions

### Step 1: Prepare Your Scene
1. **Open your scene from Activity 3** with the player object
2. **Ensure you have space to test** collision interactions
3. **Save your scene** (Ctrl+S) before adding new components

### Step 2: Create Interactive Objects
1. **Create a Sphere**: GameObject → 3D Object → Sphere
2. **Rename it "Player"**: Select the Sphere, name it "Player"
3. **Add a Rigidbody component**:
   - Select the Player object
   - In Inspector, click "Add Component"
   - Search for "Rigidbody" and add it
   - This enables physics-based movement and collision detection
4. **Create a Cube**: GameObject → 3D Object → Cube
5. **Rename it "Collectible"**: Select the Cube, name it "Collectible"
6. **Position the objects**: Place the Collectible near the Player's starting position

### Step 3: Configure Collision Detection
1. **Set up the Collectible as a Trigger**:
   - Select the Collectible object
   - In Inspector, find the Box Collider component
   - Check the "Is Trigger" checkbox
   - This makes it detect collisions without physical blocking
2. **Verify Player has a Collider**:
   - Select the Player object
   - Ensure it has a Sphere Collider component
   - The Rigidbody will handle collision detection automatically

### Step 4: Create the CollisionHandler Script
1. **Create a new script**: Right-click in Project window → Create → C# Script
2. **Name it "CollisionHandler"**: Double-click to rename if needed
3. **Open the script** and replace all code with:

```csharp
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public bool destroyOnCollision = true;
    public string collisionMessage = "Object collected!";

    void OnTriggerEnter(Collider other)
    {
        // Log the collision event
        Debug.Log("Collided with: " + other.name);
        
        // Display a message in the console
        Debug.Log(collisionMessage);
        
        // Destroy or deactivate the other object
        if (destroyOnCollision)
        {
            other.gameObject.SetActive(false);
        }
    }
}
```

### Step 5: Attach and Configure the Script
1. **Attach the script**: Select the Player object, drag the CollisionHandler script onto it
2. **Configure in Inspector**:
   - Set `Destroy On Collision` to true (default)
   - Customize `Collision Message` if desired
3. **Save the script** (Ctrl+S) and return to Unity

### Step 6: Test the Collision System
1. **Enter Play mode**: Click the Play button
2. **Move the Player** using WASD or arrow keys
3. **Collide with the Collectible**: Move the Player into the Cube
4. **Observe the results**:
   - Check the Console for debug messages
   - Watch the Collectible disappear
   - Verify collision detection is working

### Step 7: Expand Your Scene
1. **Add more collectibles**:
   - Duplicate the Collectible (Ctrl+D)
   - Position them around the scene
   - Each will disappear when touched
2. **Test with multiple objects**: Move around and collect all objects
3. **Observe Console output**: Each collision generates a debug message

## Understanding the Code

### **OnTriggerEnter()**
- Called automatically when this object enters a trigger collider
- `Collider other` parameter contains information about the collided object
- Only works when this object has a Rigidbody and the other has a trigger collider

### **Debug.Log()**
- Outputs messages to Unity's Console window
- Useful for debugging and understanding what's happening
- Can display variable values and object names

### **gameObject.SetActive(false)**
- Deactivates the specified GameObject
- Makes it invisible and non-interactive
- Alternative to `Destroy()` which completely removes the object

### **Public Variables**
- `destroyOnCollision`: Toggle whether objects are destroyed on collision
- `collisionMessage`: Customizable message displayed in Console


## Extension Activities

### **Add Visual Feedback**
Modify the script to change the Player's color on collision:

```csharp
// Add to OnTriggerEnter method
Renderer playerRenderer = GetComponent<Renderer>();
playerRenderer.material.color = Color.green;
```

### **Create Collectible Types**
Add different types of collectibles with different behaviors:

```csharp
// Check the object's tag or name
if (other.CompareTag("PowerUp"))
{
    // Special behavior for power-ups
    Debug.Log("Power-up collected!");
}
```

### **Add Score System**
Create a simple scoring system:

```csharp
// Add at class level
public int score = 0;

// Add to OnTriggerEnter method
score += 10;
Debug.Log("Score: " + score);
```

### **Create Respawn System**
Make collectibles respawn after a delay:

```csharp
// Instead of SetActive(false), use:
StartCoroutine(RespawnObject(other.gameObject));

// Add this method to the class
IEnumerator RespawnObject(GameObject obj)
{
    obj.SetActive(false);
    yield return new WaitForSeconds(3f);
    obj.SetActive(true);
}
```

## Outcome
A functional collision detection system where the player can interact with objects in the scene. Objects disappear when touched, and debug messages provide feedback about collision events. This foundation can be expanded to create more complex interaction systems. 