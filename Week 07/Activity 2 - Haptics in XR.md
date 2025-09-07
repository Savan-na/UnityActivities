# Activity 2: Haptics in XR

## Objective
Learn how to implement haptic feedback in XR using Meta SDK building blocks and custom haptic files. This activity demonstrates how to create tactile feedback that responds to user interactions, enhancing the immersive experience of XR applications through touch sensations.

## Prerequisites
- Complete Activity 1: XR Poke and Spatial Audio
- Working Unity project with Meta SDK building blocks
- Scene with Camera Rig and Interactions Rig
- Meta Quest 2/3 device (haptics only work with controllers)

## Haptics in XR

**Haptic feedback** (also called tactile feedback) provides users with physical sensations through vibrations, forces, or other tactile stimuli. In XR, haptics are crucial for:

- **Immersion**: Making virtual objects feel "real" through touch
- **Feedback**: Confirming interactions and providing status information
- **Accessibility**: Providing alternative feedback for users with visual or auditory impairments
- **Realism**: Simulating the feel of different materials, textures, and interactions

## Instructions

### Step 1: Set Up Your Scene
1. **Start with your existing XR scene**:
   - Use the scene from Activity 1, or create a new scene with basic XR setup
   - Ensure you have Camera Rig and Interactions Rig from previous activities
   - Make sure your scene is properly configured for XR development

2. **Create a simple environment**:
   - Add a ground plane if you don't have one

### Step 2: Create a Grabbable Object
1. **Create a sphere**:
   - GameObject → 3D Object → Sphere
   - Rename it to "HapticSphere"
   - Position it at (0, 1.5, 2) - at eye level for VR
   - Scale it to (0.3, 0.3, 0.3) for comfortable grabbing

2. **Add Grabbable Interaction**:
   - From the Interaction category in Building Blocks, drag "Grab Interaction" onto the sphere
   - This makes the sphere grabbable with hands or controllers
   
   `The Grab Interaction building block automatically adds all necessary components for grabbing, including colliders, rigidbody, and interaction logic.`

### Step 3: Add Haptics Building Block
1. **Add Haptics to the sphere**:
   - From the Haptics category in Building Blocks, drag "Haptics" onto the HapticSphere
   - This adds haptic feedback capabilities to the object.

2. **Examine the Haptic Source component**:
   - Select the HapticSphere
   - Look at the **Haptic Source** component in the Inspector
   - Notice the various settings including:
     - **Haptic Clip**: The haptic pattern file to play
     - **Amplitude**: Volume/intensity of the haptic feedback
     - **Frequency**: Speed of the haptic pattern
     - **Duration**: How long the haptic plays

   `The Haptic Source component manages haptic playback and provides methods to play, stop, and control haptic feedback. It can use both built-in haptic patterns and custom haptic files. It share's similar structure to an Adudio Source. Both can Play() and Stop().`

### Step 4: Download and Add Haptic Files
1. **Download haptic files**:
   - Go to the [Meta Haptics Studio Examples](https://github.com/oculus-samples/haptics-studio-examples/tree/main/1%20Phanto/Phanto%20Haptics)
   - Download a `.haptics` file from the repository [like this nice simple one](https://github.com/oculus-samples/haptics-studio-examples/blob/main/1%20Phanto/Phanto%20Haptics/UI/UI_WindowOpen.haptic)
   - Other common files to download:
     - `Phanto_Impact.haptics` - A sharp impact sensation
     - `Phanto_Soft.haptics` - A gentle, soft sensation
     - `Phanto_Texture.haptics` - A textured, rough sensation

2. **Import haptic files into Unity**:
   - Copy the downloaded `.haptics` files into your project's Assets folder
   - Create a "Haptics" folder in your Assets to organize them
   - Unity will automatically import the haptic files

3. **Assign haptic file to the Haptic Source**:
   - Select the HapticSphere
   - In the **Haptic Source** component:
     - Drag one of your downloaded haptic files to the **Haptic Clip** field
     - Check **Loop** to loop the haptic vibration.
     - Set **Amplitude** to 1.0 for full intensity
     - Set **Frequency** to 1.0 for normal speed

### Step 5: Add Interactable Unity Event Wrapper
1. **Add the Event Wrapper**:
   - Select the HapticSphere
   - Add Component → Search for "Interactable Unity Event Wrapper"
   - This provides Unity events for all interaction states

2. **Connect the components**:
   - In the **Interactable Unity Event Wrapper** component:
     - Set **Interactable View** to the HapticSphere's child "[BuildingBlock] HandGrabInstallationRoutine"
     - Select **Grab Interactable** from the resulting popup window.

### Step 6: Connect Haptic Events
1. **Set up grab haptics**:
   - In the **Interactable Unity Event Wrapper** component:
     - In **When Selected()**, click the "+" button
     - Drag the HapticSphere to the field
     - Select `HapticSource > Play()` from the function list
   
   `This means when you grab the sphere (select it), the haptic feedback will start playing.`

2. **Set up release haptics**:
   - In **When Unselected()**, click the "+" button
     - Drag the HapticSphere to the field
     - Select `HapticSource > Stop()` from the function list
   
   `This means when you release the sphere (unselect it), the haptic feedback will stop.`

### Step 7: Test Your Haptic Setup
1. **Enter Play Mode**:
   - Click the Play button in Unity
   - Your scene should run with haptic feedback active
   - In VR, grab the sphere with your controller
   - You should feel haptic feedback in your controller when you grab it
   - The haptic should continue while you're holding the sphere
   - When you release the sphere, the haptic should stop
   - Try grabbing and releasing multiple times to feel the haptic pattern

## Troubleshooting
- **No haptic feedback**: Ensure your Quest controllers are connected and the haptic file is properly assigned
- **Haptic too weak/strong**: Adjust the Amplitude setting in the Haptic Source component
- **Haptic not stopping**: Check that the Stop() function is properly connected to the When Unselected() event
- **Haptic file not found**: Verify the haptic file is imported into Unity and assigned to the Haptic Clip field

## Extension Activities
- **Different haptics for different objects**: Create multiple objects with different haptic patterns to compare sensations
- **Proximity haptics**: Add haptic feedback when the controller gets close to an object (using distance-based triggers)
- **Haptic intensity based on interaction**: Modify haptic intensity based on how hard you grab or how fast you move
- **Multiple haptic sources**: Create objects that trigger different haptic patterns on different hands

## Advanced Haptic Techniques

### Custom Haptic Patterns
You can create your own haptic patterns using Meta's Haptics Studio:
1. Download [Meta Haptics Studio](https://developers.meta.com/horizon/resources/haptics-studio)
2. Create custom haptic patterns
3. Export as `.haptics` files
4. Import into Unity


### Haptic Feedback for Different Materials
Different haptic patterns can simulate different materials:
- **Metal**: Sharp, crisp haptics with quick attacks
- **Fabric**: Soft, gentle haptics with smooth transitions
- **Wood**: Medium intensity with some texture
- **Glass**: Sharp, high-frequency haptics

## Key Concepts Learned
- **Haptic Feedback**: Creating tactile sensations in XR applications
- **Haptic Files**: Using custom haptic patterns for realistic feedback
- **Event-Driven Haptics**: Connecting haptic feedback to user interactions
- **Haptic Design**: Understanding how different haptic patterns create different sensations
- **Controller Integration**: Using Quest controller haptic capabilities effectively

## Resources

### Official Documentation
- [Meta Unity Haptics SDK](https://developers.meta.com/horizon/documentation/unity/unity-haptics-sdk) - Complete guide to implementing haptic feedback in Unity
- [Meta Unity Event Wrappers](https://developers.meta.com/horizon/documentation/unity/unity-haptics-sdk)

### Haptic Resources
- [Meta Haptics Studio](https://developers.meta.com/horizon/resources/haptics-studio)
- [Haptics Studio Examples](https://github.com/oculus-samples/haptics-studio-examples)
