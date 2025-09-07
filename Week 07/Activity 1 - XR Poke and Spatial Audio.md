# Activity 1: XR Poke and Spatial Audio

## Objective
Learn how to use Unity Event Wrappers with Meta SDK building blocks to create interactive audio experiences. This activity demonstrates how to connect poke interactions with spatial audio systems, creating immersive XR experiences where audio responds to user interactions and moves realistically in 3D space.

## Prerequisites
- Complete Week 5 and Week 6 activities to get set up with XR
- Unity 2022.3 LTS installed
- Meta Quest 2/3 device (for final testing)
- Working scene with Camera Rig and Interactions Rig

## Theory: Event Wrappers

**Event Wrappers** in Unity are components that provide a bridge between Meta SDK's interaction system and Unity's event system. They allow you to:

- Connect any type of interactable (including grabbable objects) to Unity events
- Trigger custom functions when interactions occur (select, unselect, hover, etc.)
- Create complex interaction chains without writing custom interaction code
- Use Unity's Inspector to visually connect events to functions

The **Interactable Unity Event Wrapper** is particularly powerful because it can be added to any interactable object and provides events for all interaction states, making it easy to create responsive XR experiences.

## Instructions

### Step 1: Set Up Your Scene
1. **Start with your existing XR scene**:
   - Use the scene from Week 6 activities, or create a new scene with basic XR setup
   - Ensure you have Camera Rig and Interactions Rig from previous activities
   - Make sure your scene is properly configured for XR development

2. **Create a simple environment**:
   - Add a ground plane if you don't have one
   - Create a few basic objects to interact with
   - Position objects at comfortable VR interaction distances (1-2 meters away)

### Step 2: Add Spatial Audio Building Block
1. **Open Building Blocks**:
   - Go to Meta → Tools → Building Blocks

2. **Add Spatial Audio**:
   - From the Audio category, drag "Spatial Audio" into your scene
   - This creates a spatial audio system that supports 3D positional audio
   
   `Spatial Audio provides realistic 3D sound positioning, where audio sources appear to come from specific locations in 3D space. This creates immersive audio experiences where sounds move naturally as you move around in VR.`

3. **Configure the Audio Source**:
   - Select the Spatial Audio object in your hierarchy
   - In the Audio Source component:
     - Add an audio clip to the **AudioClip** field (click the circle in the input and there should be some samples)
     - Check **Play On Awake** to start the audio immediately
     - Check **Loop** to make the audio repeat continuously


4. **Test spatial audio**:
   - Enter Play Mode
   - Move around in VR to hear how the sound changes position
   - Notice how the audio appears to come from the specific location of the audio source

### Step 3: Add Poke Interaction Building Block
1. **Add Poke Interaction**:
   - From the Interaction category, drag "Poke Interaction" into your scene
   - Position it at a comfortable distance for interaction (1-2 meters away)
   - This creates a poke-able object that responds to finger touches

2. **Examine the Poke Interactable component**:
   - Select the Poke Interaction object
   - Look at the **Poke Interactable** component in the Inspector
   - Notice the various settings for poke behavior, including:
     - **Poke Threshold**: How far the finger needs to penetrate
     - **Enter Hover Distance**: When hover state begins
     - **Exit Hover Distance**: When hover state ends

   `The Poke Interactable component handles all the physics and collision detection for poke interactions. It automatically detects when fingers or controllers penetrate the object and manages the interaction states.`

### Step 4: Add Unity Event Wrapper
1. **Add Pointable Unity Event Wrapper**:
   - Select the Poke Interaction object's child "[BuildingBlock] ISDK_PokeInteraction"
   - Add Component → Search for "Pointable Unity Event Wrapper"
   - This component provides Unity events for all interaction states

2. **Connect the components**:
   - In the **Pointable Unity Event Wrapper** component:
     - Set **Pointable** to the Poke Interaction object (which is the Poke interaction object's child, "[BuildingBlock] ISDK_PokeInteraction")
   
   `The Pointable Unity Event Wrapper acts as a bridge between Meta SDK's interaction system and Unity's event system. It converts Meta SDK interaction events into Unity events that you can connect to custom functions.`

### Step 5: Create Audio Player Script
1. **Create a new script**:
   - In your Scripts folder, create a new C# script
   - Name it "AudioPlayer"

2. **Write the AudioPlayer script**:
```csharp
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
      
    public void PlayAudio()
    {
        audioSource.Play();
    }
   
    public void StopAudio()
    {
        audioSource.Stop();
    }
}
```

### Step 6: Connect the Audio System
1. **Add AudioPlayer to the poke object**:
   - Select the Poke Interaction object
   - Add the AudioPlayer script as a component
   - In the AudioPlayer component:
     - Assign your spatial audio source to the **Audio Source** field

2. **Connect the events**:
   - Select the Poke Interaction object
   - In the **Pointable Unity Event Wrapper** component:
     - In **When Selected()**, click the "+" button
     - Drag the Poke Interaction object to the field
     - Select `AudioPlayer > PlayAudio()` from the function list
   - Back in the Audio Source component:
     - UnCheck **Play On Awake** to not start the audio immediately
     - UnCheck **Loop** to make the audio not repeat continuously
   
   `This connection means that when you poke the object (select it), the PlayAudio() function will be called, triggering the spatial audio.`

### Step 7: Test Your Setup
1. **Enter Play Mode**:
   - Click the Play button in Unity
   - Your scene should run with both spatial audio and poke interaction active
   - **Test the interaction**
   
## Troubleshooting
- **No audio playing**: Check that the Audio Source has an audio clip assigned and the volume is not muted
- **Poke not working**: Verify that the Pointable Unity Event Wrapper is properly connected to the Poke Interactable component

## Extension Activities
- **Multiple audio sources**: Add a second audio source and poke button in different locations to create a multi-location audio experience
- **Grabbable audio**: Add a grabbable object, attach an Interactable Unity Event Wrapper to it, and make grabbing the object trigger a sound
- **Audio feedback**: Add visual feedback (like color changes) that happens simultaneously with the audio
