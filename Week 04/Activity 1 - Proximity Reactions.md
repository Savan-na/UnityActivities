# Activity 1: Proximity-Based Object Reactions

## Objective
Create a scene where objects react to player proximity by changing color (highlighting) or moving away from the player. This activity builds on collision detection and introduces proximity-based interactions.

## Prerequisites
- Complete Week 02 and Week 03 activities
- Understanding of Unity components (Rigidbody, Collider, Renderer)
- Basic C# scripting knowledge
- Familiarity with collision detection systems

## Instructions

### Step 1: Project Setup
1. **Open Unity and create a new 3D project**
2. **Save your scene immediately** (Ctrl+S) 
3. **Create an organized folder structure** in the Project window:
   - Right-click in Project window → Create → Folder
   - Name it "Scripts"

### Step 2: Create the Basic Scene
1. **Create a ground plane**:
   - GameObject → 3D Object → Plane
   - Rename it to "Ground"
   - Position at (0, 0, 0)
   - Scale to (2, 1, 2) for more space

2. **Create the Player**:
   - GameObject → 3D Object → Capsule
   - Rename it to "Player"
   - Position at (0, 1, 0)
   - Add a Rigidbody component
   - Add a SimpleWASD script (see script below)
   - **Important**: In the Rigidbody component, check "Freeze Rotation" on X, Y, and Z axes to keep the capsule upright and prevent unwanted rotation from collisions

3. **Create the SimpleWASD script**:
   - In the Project window, right-click in the Scripts folder
   - Select `Create > C# Script`
   - Name it `SimpleWASD`
   - Write this script:
```csharp
using UnityEngine;

public class SimpleWASD : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        Vector3 movement = Vector3.zero;
        
        // W and S for forward/backward movement
        if (Input.GetKey(KeyCode.W)) movement.z += 1f;
        if (Input.GetKey(KeyCode.S)) movement.z -= 1f;
        
        // A and D for rotation (turning left and right)
        if (Input.GetKey(KeyCode.A)) transform.Rotate(0, -90f * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.D)) transform.Rotate(0, 90f * Time.deltaTime, 0);
        
        // For efficiency, we check to make sure that we need to move before calling Translate
        if (movement != Vector3.zero)
        {
            transform.Translate(movement.normalized * moveSpeed * Time.deltaTime, Space.Self);
        }
    }
    
    void OnDrawGizmos()
    {
        // Draw a debug ray pointing forward from the player
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * 3f);
    }
}
```

4. **Create a highlighting object**:
   - GameObject → 3D Object → Cube
   - Rename it to "HighlightObject"
   - Position at (3, 0.5, 0)

### Step 4: Create the Basic Proximity Highlighting Script
1. **Create a new script**:
   - In the Project window, right-click in the Scripts folder
   - Select `Create > C# Script`
   - Name it `ProximityHighlighter`

2. **Write the basic proximity highlighting script**:
```csharp
using UnityEngine;

public class ProximityHighlighter : MonoBehaviour
{
    public float proximityDistance = 3f;
    
    public Color normalColor = Color.white;
    public Color highlightColor = Color.yellow;
    
    private Renderer objectRenderer;
    private bool isPlayerNearby = false;
    
    private GameObject player;
    
    void Start()
    {
        // Get the renderer component for color changes
        objectRenderer = GetComponent<Renderer>();
        
        // Set initial color
        objectRenderer.material.color = normalColor;
        
        // Find the player once and store the reference
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        // Calculate distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        
        // Check if player is within proximity distance
        bool wasNearby = isPlayerNearby;
        isPlayerNearby = distanceToPlayer <= proximityDistance;
        
        // Handle proximity highlighting
        if (isPlayerNearby && !wasNearby)
        {
            // Player just entered proximity - highlight the object
            objectRenderer.material.color = highlightColor;
            Debug.Log(gameObject.name + " is now highlighted!");
        }
        else if (!isPlayerNearby && wasNearby)
        {
            // Player just left proximity - return to normal color
            objectRenderer.material.color = normalColor;
            Debug.Log(gameObject.name + " returned to normal color");
        }
    }
}
```

### Step 5: Configure the Highlighting Object
1. **Set up the HighlightObject**:
   - Select the HighlightObject in the Hierarchy
   - Add the ProximityHighlighter script component
   - In the Inspector, configure:
     - Proximity Distance: 3
     - Normal Color: White
     - Highlight Color: Yellow

### Step 6: Tag the Player
1. **Add the Player tag**:
   - Select the Player object in the Hierarchy
   - In the Inspector, click the Tag dropdown
   - Select "Player" (or create it if it doesn't exist)

### Step 7: Test the Proximity Highlighting System
1. **Enter Play mode**: Click the Play button
2. **Move the player around** using WASD or arrow keys
3. **Test proximity highlighting**:
   - Move close to the HighlightObject (within 3 units)
   - Watch it change from white to yellow
   - Move away from the object
   - Observe it return to its original white color



## Understanding the Code

### **Basic Proximity Highlighting (ProximityHighlighter)**
- Uses `Vector3.Distance()` to calculate distance between objects
- Compares distance to `proximityDistance` threshold
- Tracks state changes to trigger highlighting only when entering/leaving proximity
- Accesses the object's `Renderer` component to modify material colors
- Provides immediate visual feedback when player approaches

### **Understanding the Renderer Component**
The `Renderer` component is what makes 3D objects visible in Unity. It controls:
- **Material**: The visual appearance (color, texture, shader)
- **Mesh**: The 3D shape of the object
- **Visibility**: Whether the object is shown or hidden

**How we use it:**
- `GetComponent<Renderer>()` gets the Renderer component from our object
- `objectRenderer.material.color` changes the color of the object's material
- This gives us instant visual feedback when the player approaches

### **State Change Detection with Flags**
The script uses two boolean flags to detect when the player enters or leaves the proximity zone:

**`isPlayerNearby`**: Current state (true = player is close, false = player is far)  
**`wasNearby`**: Previous state (what the state was in the last frame)  
**`!`**: The `not` operator inverts the boolean logic. `!true = false`

**How it works:**
1. **Store previous state**: `bool wasNearby = isPlayerNearby;` (saves current state before updating)
2. **Update current state**: `isPlayerNearby = distanceToPlayer <= proximityDistance;` (checks if player is now close)
3. **Detect changes**:
   - `isPlayerNearby && !wasNearby` = Player just entered proximity (was far, now close)
   - `!isPlayerNearby && wasNearby` = Player just left proximity (was close, now far)

**Why this matters:**
Without flags, the object would change color every frame while the player is nearby, causing unnecessary updates. With flags, the color only changes when the player actually enters or leaves the zone - a much more efficient and logical system.

### **Understanding Unity Tags**
Tags are labels you can assign to GameObjects to identify them in your code. Think of them as sticky notes that help your scripts find specific objects.

**How tags work:**
- **Tag Assignment**: In the Inspector, click the Tag dropdown and select or create a tag
- **Finding Objects**: Use `GameObject.FindGameObjectWithTag("Player")` to locate objects with specific tags
- **Performance**: Tags are much faster than searching through all objects in the scene

**Why we use "Player" tag:**
- **Efficient Reference**: Instead of manually dragging the player object into every script
- **Dynamic Finding**: The script automatically finds the player even if you rename or move it
- **Scene Independence**: Works in any scene that has an object tagged as "Player"

**Alternative approaches:**
- **Manual Reference**: Drag the player object into a public field in the Inspector
- **Find by Name**: Use `GameObject.Find("PlayerName")` (slower and less reliable)
- **Tag System**: Use `FindGameObjectWithTag("Player")` (fastest and most reliable)

**Best practices:**
- Use descriptive tag names: "Player", "Enemy", "Collectible", "Obstacle"
- Keep tag names consistent across your project
- Tags are case-sensitive, so "Player" ≠ "player"

## Extension Activities

### **Expand Your Scene**
1. **Add more highlighting objects**:
   - Duplicate existing objects
   - Create new primitive shapes (Sphere, Cylinder)
   - Configure different proximity distances
   - Try different highlight colors

2. **Experiment with different settings**:
   - Try different proximity distances
   - Test various colors
   - Adjust highlight timing
   - Create color gradients

### **Make objects move away from the player**
**Challenge**: Add movement behavior so objects move away when the player gets too close, then return to their original position when the player leaves.

**Logic to implement**:
1. Store the object's starting position when the script begins
2. When the player is nearby, calculate a direction away from the player
3. Move the object in that direction using `Vector3.MoveTowards()`
4. When the player leaves, move the object back to its original position

**Key code you'll need**:
- Store position: `originalPosition = transform.position;`
- Calculate direction away: `(transform.position - player.transform.position).normalized`
- Move object: `transform.position = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime)`

### **Add Sound Effects**
**Challenge**: Make objects play a sound when the player enters or leaves their proximity zone.

**Logic to implement**:
1. Add an AudioSource component to your object
2. Assign an audio clip to play when proximity changes
3. Play the sound at the same time the color changes

**Key code you'll need**:
- Add variables: `public AudioSource audioSource;` and `public AudioClip proximitySound;`
- Add and reference AudioSource component: `GetComponent<AudioSource>()`
- Play sound: `audioSource.PlayOneShot(proximitySound)`
- Check if components exist: `if (audioSource && proximitySound)`

### **Create Different Reaction Types**
**Challenge**: Add rotation and scaling effects when objects are highlighted, making them more dynamic.

**Logic to implement**:
1. Add boolean flags to enable/disable rotation and scaling
2. When player is nearby, rotate the object around its Y-axis
3. When player is nearby, scale the object up; when they leave, scale back down
4. Use the existing `isPlayerNearby` flag to control when effects happen

**Key code you'll need**:
- Add variables: `public bool rotateOnProximity = false;` and `public float rotationSpeed = 90f;`
- Rotate object: `transform.Rotate(Vector3.up * speed * Time.deltaTime)`
- Scale object: `transform.localScale = Vector3.one * multiplier`
- Reset scale: `transform.localScale = Vector3.one`


## Save Your Work
**Don't forget to save your scene and project!**
- Press Ctrl+S (Windows) or Cmd+S (Mac) to save your scene
- Go to File → Save Project to save all your work
