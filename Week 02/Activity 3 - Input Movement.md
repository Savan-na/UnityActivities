# Activity 3: Use Inputs to Make an Object Move

## Objective
Learn to use Unity's Input system to create player-controlled movement, building on the movement concepts from Activity 2b.

## Prerequisites
- Complete Activity 2b to understand basic movement with `Translate`
- Basic understanding of Unity Editor and C# scripting
- A scene with objects to navigate around (from Activity 1)

## Instructions

### Step 1: Prepare Your Scene
1. **Open your scene from Activity 1** or create a simple test environment
2. **Ensure you have objects to navigate around** (buildings, walls, obstacles)
3. **Save your scene** (Ctrl+S) before adding new scripts

### Step 1.5: Configure Input Settings
1. **Open Input Manager**: Edit → Project Settings → Input Manager
2. **Check Horizontal Axis**:
   - Expand "Axes" → "Horizontal"
   - Verify "Name" is set to "Horizontal"
   - Check "Type" is set to "Key or Mouse Button"
   - Verify "Positive Button" includes "d" and "right"
   - Verify "Negative Button" includes "a" and "left"
   - Set "Gravity" to 3 and "Dead" to 0.001
3. **Check Vertical Axis**:
   - Expand "Axes" → "Vertical"
   - Verify "Name" is set to "Vertical"
   - Check "Type" is set to "Key or Mouse Button"
   - Verify "Positive Button" includes "w" and "up"
   - Verify "Negative Button" includes "s" and "down"
   - Set "Gravity" to 3 and "Dead" to 0.001
4. **Test the setup**: In Play mode, the player should NOT move when no keys are pressed

### Step 2: Create the Player Object
1. **Create a Capsule**: GameObject → 3D Object → Capsule
2. **Rename it properly**: Select the Capsule, name it "Player"
3. **Position it appropriately**: Place it at a starting position like (0, 1, 0)

### Step 3: Create the PlayerMovement Script
1. **Create a new script**: Right-click in Project window → Create → C# Script
2. **Name it "PlayerMovement"**: Double-click to rename if needed
3. **Open the script** and replace all code with:

```csharp
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;

    void Update()
    {
        // Get input values (-1 to 1)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        // Create movement vector (no Y movement for ground-based movement)
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        
        // Apply movement using Transform
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}
```

### Step 4: Attach and Configure the Script
1. **Attach the script**: Select the Player object, drag the PlayerMovement script onto it
2. **Configure in Inspector**:
   - Set `Move Speed` to 5 (or adjust as needed)
3. **Save the script** (Ctrl+S) and return to Unity

### Step 5: Test the Movement
1. **Enter Play mode**: Click the Play button
2. **Test movement controls**:
   - **WASD keys**: W (forward), S (backward), A (left), D (right)
   - **Arrow keys**: Up, Down, Left, Right arrows
   - **Gamepad**: Left stick (if connected)
3. **Observe the movement**: The player should move smoothly around the scene
4. **Exit Play mode**: Click the Play button again

### Step 6: Adjust and Optimize
1. **Fine-tune the movement**:
   - Adjust `Move Speed` in the Inspector while in Play mode
   - Test different values to find the right feel
2. **Test with your scene objects**:
   - Navigate around buildings, walls, and obstacles
   - Ensure movement feels natural and responsive

## Understanding the Code

### **Input.GetAxis()**
- Returns a value between -1 and 1 based on key press
- Returns 0 when no keys are pressed (if configured correctly)
- Provides smooth input (not just on/off)
- Supports multiple input methods (keyboard, gamepad)
- "Horizontal" = A/D or Left/Right arrows
- "Vertical" = W/S or Up/Down arrows

### **Vector3 Movement**
- `new Vector3(horizontalInput, 0f, verticalInput)`: Creates 3D movement vector
- `0f` for Y-axis keeps movement on the ground plane
- `.normalized`: Prevents faster diagonal movement

### **Movement Methods**
- **Transform.Translate()**: Direct position changes (no physics)

### **Public Variables**
- `moveSpeed`: Adjustable in Inspector for easy tuning



## Extension Activities

### **Add Rotation to Movement**
Modify the script to make the player face the direction of movement:

```csharp
// Add this inside the Update() method after movement
if (movement != Vector3.zero)
{
    transform.rotation = Quaternion.LookRotation(movement);
}
```

### **Add Jumping**
Add vertical movement with the Space key:

```csharp
// Add to the movement vector
if (Input.GetKeyDown(KeyCode.Space))
{
    movement.y = 1f;
}
```

### **Add Running**
Implement sprint functionality with Shift key:

```csharp
// Modify movement speed based on input
float currentSpeed = moveSpeed;
if (Input.GetKey(KeyCode.LeftShift))
{
    currentSpeed *= 2f;
}
```

### **Create a Camera Follow**
Add a camera that follows the player:
1. Create an empty GameObject named "CameraHolder"
2. Make the Main Camera a child of CameraHolder
3. Position the camera behind and above the player
4. Make CameraHolder follow the player's position

## Outcome
A player object that responds smoothly to keyboard input, with configurable movement speed. The player can navigate around the scene using WASD or arrow keys. 