# Activity 4 - Creating a Custom Transformer

## Overview
In this activity, you'll learn how to create a custom transformer script that extends the Meta SDK's interaction system. You'll build a minimal implementation step-by-step, starting with basic movement and then adding extra functionality from there. 

## Prerequisites
- Complete Activity 1, 2, and 3
- Have a working XR project with Meta SDK building blocks
- Basic understanding of C# scripting in Unity

## Learning Objectives
- Understand the structure of Meta SDK transformer scripts
- Learn how to implement the ITransformer interface
- Create a minimal custom transformer for object movement
- Add custom visual effects (rainbow color cycling) to the transformer
- Connect custom transformers to grab interactable objects

## Activity Steps

### Step 1: Create a CustomTransformer.cs Script
1. In your Project window, navigate to the **Scripts** folder
2. Right-click in the Scripts folder and select **Create → C# Script**
3. Name the script **CustomTransformer**
4. Double-click to open it in your code editor


5. Replace the default code with this minimal structure:

```csharp
// Import Unity's core functionality
using UnityEngine;
// Import Meta SDK's interaction system for ITransformer and IGrabbable
using Oculus.Interaction;

public class CustomTransformer : MonoBehaviour, ITransformer
{
    // Reference to the grabbable object this transformer is attached to
    // Set by the Meta SDK when Initialize() is called
    private IGrabbable _grabbable;
    
    // Stores the hand position and rotation when grabbing first started
    // Used to calculate how much the hand has moved since grab began
    private Pose _startGrabPose;
    
    // Stores the object's position when grabbing first started
    // Used as a reference point for movement calculations
    private Vector3 _startObjectPos;
    
    // The distance between the hand and object when grabbing started
    // This offset is preserved during movement to maintain natural feel
    private Vector3 _grabOffset;

    // Called once by the Meta SDK when this transformer is connected to a grabbable object
    // This is where you set up references and prepare for interaction
    public void Initialize(IGrabbable grabbable)
    {
        // Store reference to the grabbable object for later use
        _grabbable = grabbable;
    }

    // Called when the user first starts grabbing the object
    // Use this to capture initial positions and set up any starting state
    public void BeginTransform()
    {
        // Called when grab starts
    }

    // Called every frame while the object is being grabbed
    // This is where you update the object's position, rotation, or other properties
    public void UpdateTransform()
    {
        // Called every frame while grabbed
    }

    // Called when the user stops grabbing the object
    // Use this to clean up any temporary changes or restore original state
    public void EndTransform()
    {
        // Called when grab ends
    }

    public void EndTransform()
    {
        // Called when grab ends
    }
}
```

3. Save the script and return to Unity

### Step 3: Connect to a Grab Interactable Object
1. **Add a new Grab Interaction cube** to your scene:
   - Go to **Meta → Tools → Building Blocks**
   - Drag a **Grab Interaction** building block into your scene
   - Position it at a different location from your existing cubes (e.g., at `(2, 1.5, 0.75)`)
   - This gives you a clean slate to work with

2. **Add your CustomTransformer script** to the new cube:
   - Select the new Grab Interaction cube
   - In the Inspector, click **Add Component**
   - Search for and add your **CustomTransformer** script

3. **Connect the transformer** to the Grabbable component:
   - With the cube still selected, find the **Grabbable** component
   - In the **Optionals > One Grab Transformer** field, drag the cube (which now has your CustomTransformer script) into this field

### Step 4: Add Code to Move the Grabbed Object
1. Return to your **CustomTransformer.cs** script
2. **Update the `BeginTransform()` method** to store initial positions when grabbing starts:
   - Store the hand's grab pose
   - Store the object's starting position  
   - Calculate the initial grab offset between hand and object

3. **Update the `UpdateTransform()` method** to move the object:
   - Get the current hand position
   - Calculate target position using the stored offset
   - Move the object to follow the hand while preserving the offset

4. **Replace the entire script content** with this updated version:

```csharp
using UnityEngine;
using Oculus.Interaction;

public class CustomTransformer : MonoBehaviour, ITransformer
{
    private IGrabbable _grabbable;
    private Pose _startGrabPose;
    private Vector3 _startObjectPos;
    private Vector3 _grabOffset;

    public void Initialize(IGrabbable grabbable)
    {
        _grabbable = grabbable;
    }

    public void BeginTransform()
    {
        var grabPoint = _grabbable.GrabPoints[0];
        _startGrabPose = grabPoint;
        _startObjectPos = transform.position;
        _grabOffset = _startObjectPos - _startGrabPose.position; // Calculate initial offset
    }

    public void UpdateTransform()
    {
        var grabPoint = _grabbable.GrabPoints[0];
        Vector3 targetPos = grabPoint.position + _grabOffset; // Preserve offset while moving
        transform.position = targetPos; // Move object to follow hand
    }

    public void EndTransform()
    {
        // Called when grab ends
    }
}
```

5. Save the script

### Step 5: Test Basic Movement
1. Enter Play mode
2. Grab the cube with one hand
3. Move your hand around - the cube should follow your hand while maintaining its initial offset
4. The object won't rotate because we haven't programmed that. 
5. Exit Play mode

### Step 6: Add Rainbow Color Cycling

We are going to add a custom behaviour to this transformer when the user moves the grabbed object. We are going to make the object's color cycle through different colors as they move it up or down.  To do this we get a reference to the renderer where the colors are set, create and assign a material we will dynamically change the color of, and then change that color when the user grabs and moves the object in the y direction. 

1. Return to your **CustomTransformer.cs** script
2. **Add new variables** for color cycling:
   - Add a Renderer reference to access the object's material and modify its color
   - Add a variable to store the starting color hue so we can cycle from the object's current color

3. **Update the `Initialize()` method** to set up color functionality:
   - Get the renderer component from the object to access its material
   - Create a unique material instance to avoid affecting other objects that share the same material

4. **Update the `BeginTransform()` method** to capture starting color:
   - Extract the current material color and convert it from RGB to HSV format
   - Store the starting hue value so rainbow cycling begins from the object's current color
   - This prevents the object from always starting at red when grabbed

5. **Update the `UpdateTransform()` method** to add rainbow cycling:
   - Calculate the total distance the hand has moved since grabbing started
   - Map vertical (Y-axis) movement to color changes using HSV color space
   - Convert the new hue value back to RGB and apply it to the object's material
   - Use Mathf.Repeat to create continuous color cycling through the rainbow spectrum

```csharp
using UnityEngine;
using Oculus.Interaction;

public class CustomTransformer : MonoBehaviour, ITransformer
{
    private IGrabbable _grabbable;
    private Pose _startGrabPose;
    private Vector3 _startObjectPos;
    private Vector3 _grabOffset;
    
    // Variables for rainbow color cycling functionality
    private Renderer _renderer;
    private float _startHue;

    public void Initialize(IGrabbable grabbable)
    {
        _grabbable = grabbable;
        // Get renderer component and create unique material instance
        _renderer = GetComponent<Renderer>();
        _renderer.material = new Material(_renderer.material);
    }

    public void BeginTransform()
    {
        var grabPoint = _grabbable.GrabPoints[0];
        _startGrabPose = grabPoint;
        _startObjectPos = transform.position;
        _grabOffset = _startObjectPos - _startGrabPose.position;
        
        // Store starting color hue for rainbow cycling
        Color rgb = _renderer.material.color;
        Color.RGBToHSV(rgb, out float h, out _, out _);
        _startHue = h;
    }

    public void UpdateTransform()
    {
        var grabPoint = _grabbable.GrabPoints[0];
        Vector3 targetPos = grabPoint.position + _grabOffset;
        transform.position = targetPos;
        
        // Rainbow color cycling based on Y movement
        Vector3 delta = grabPoint.position - _startGrabPose.position;
        float hueDelta = delta.y * 2.0f; // Only Y movement affects color
        float hue = Mathf.Repeat(_startHue + hueDelta, 1f);
        _renderer.material.color = Color.HSVToRGB(hue, 1f, 1f);
    }

    public void EndTransform()
    {
        // Called when grab ends
    }
}
```




6. Save the script

### Step 7: Test Rainbow Color Cycling
1. Enter Play mode
2. Grab the cube with one hand
3. Move your hand up and down (Y-axis movement)
4. The cube should change colors through the rainbow spectrum as you move vertically
5. Moving up cycles through colors, moving down cycles backwards
6. Horizontal movement (X and Z) won't affect the color

## How It Works

### ITransformer Interface
The **ITransformer** interface is Meta SDK's way of allowing custom control over how objects behave when grabbed. It provides three key methods:

- **`Initialize(IGrabbable grabbable)`**: Called once when the transformer is connected to a grabbable object
- **`BeginTransform()`**: Called when the user starts grabbing the object
- **`UpdateTransform()`**: Called every frame while the object is being grabbed
- **`EndTransform()`**: Called when the user stops grabbing the object

### Movement System
The movement system preserves the initial offset between the hand and object:
1. **Store initial positions** when grabbing begins
2. **Calculate offset** between hand and object
3. **Apply offset** to maintain the same relative position during movement
4. **Update position** every frame to follow the hand

### Color Cycling System
The rainbow color system uses HSV color space:
1. **Convert RGB to HSV** to get the hue component
2. **Calculate movement delta** from the starting position
3. **Map Y movement to hue change** (vertical movement = color change)
4. **Convert back to RGB** for the material color
5. **Use Mathf.Repeat** to cycle through the 0-1 hue range

## Troubleshooting

### Common Issues
1. **Object doesn't move**: Check that the CustomTransformer is assigned to the One Grab Transformer field
2. **Colors don't change**: Ensure the object has a Renderer component and material
3. **Movement feels jittery**: The current implementation is direct - you can add smoothing with Vector3.Lerp
4. **Script errors**: Make sure you're using the correct namespace `using Oculus.Interaction;`

### Debug Tips
- Add `Debug.Log()` statements in each method to verify they're being called
- Check the Console for any compilation errors
- Verify the script component is properly attached to the object
- Ensure the transformer is assigned in the Grabbable component

## Extension Activities

### Advanced Movement
1. **Limit movement**: Make the grabbed object only move up and down. 
2. **Add rotation**: Implement rotation following in the UpdateTransform method
3. **Smooth movement**: Use Vector3.Lerp for smoother, less jittery movement

### Enhanced Color Effects
1. **Multi-axis color cycling**: Use X and Z movement for different color effects
2. **Color intensity**: Vary saturation and value based on movement speed
3. **Color transitions**: Add smooth color blending between states

### Custom Behaviors
1. **Scale effects**: Add scaling based on hand distance
2. **Audio feedback**: Play sounds based on movement or color changes
3. **Particle effects**: Spawn particles with colors matching the object

## Key Concepts

- **Interface implementation**: Using ITransformer to integrate with Meta SDK
- **Grab lifecycle**: Understanding when different methods are called
- **Position preservation**: Maintaining object-hand relationships during interaction
- **HSV color space**: Working with hue, saturation, and value for color manipulation
- **Custom transformers**: Extending the building block system with custom behaviors

