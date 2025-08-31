# Activity 2: Thumbs Up Gesture Detection

## Objective
Build upon your Activity 1 project to detect left hand thumbs up gestures and make one interactable cube turn red when the gesture is performed. This activity uses Meta's hand pose recognition system with the OVRInteractionsComprehensive building block to create gesture-based interactions.

## Prerequisites
- Complete Activity 1: Basic Building Blocks Setup
- Working Unity project with Meta SDK building blocks
- Scene with Camera Rig, Interactions Rig, and Grab Interaction objects

## Reference Materials
**Official Documentation**: [Meta Unity ISDK Building Hand Pose Recognizer](https://developers.meta.com/horizon/documentation/unity/unity-isdk-building-hand-pose-recognizer) - Complete guide to implementing hand pose recognition.

**Building Blocks Reference**: [Meta Building Blocks Documentation](https://developers.meta.com/horizon/documentation/unity/bb-overview/) - Reference for all available building blocks.

**Component Documentation**: See individual component summaries below for detailed information about each building block component.

## Instructions

### Step 1: Prepare Your Scene
1. **Ensure your scene is ready**:
   - You should have the Camera Rig with Interactions Rig from Activity 1
   - Grab Interaction objects should be present and functional
   - The OVRInteractionsComprehensive component should be visible in your Camera Rig

2. **Verify the Interactions Rig setup**:
   - Select your Camera Rig in the hierarchy
   - Look for the "[BuildingBlock] OVRInteractionComprehensive" component
   - This should have LeftInteractions and RightInteractions sections
   - Under LeftInteractions, you should see Features and Hand components
   - The full path should be: `[BuildingBlock] OVRInteractionComprehensive > LeftInteractions > Features` and `[BuildingBlock] OVRInteractionComprehensive > LeftInteractions (Hand)`

### Step 2: Create the Thumbs Up Pose Recognition System
1. **Create an empty GameObject for left hand pose recognition**:
   - Right-click in the Hierarchy → Create Empty
   - Rename it to "ThumbsUpPoseLeft"
   - This will contain all the components needed for thumbs up detection

2. **Add the required components to ThumbsUpPoseLeft**:
   - Select ThumbsUpPoseLeft in the Hierarchy
   - In the Inspector, click "Add Component"
   - Add these components in order:
     - `ShapeRecognizerActiveState`
     - `TransformRecognizerActiveState`
     - `ActiveStateGroup`
     - `ActiveStateSelector`
     - `SelectorUnityEventWrapper`

### Step 3: Configure Shape Recognition
1. **Set up the ShapeRecognizerActiveState component**:
   - Select ThumbsUpPoseLeft
   - In the ShapeRecognizerActiveState component:
     - Set **Hand** to `[BuildingBlock] OVRInteractionComprehensive > LeftInteractions (Hand)`
     - Set **Finger Feature State Provider** to `[BuildingBlock] OVRInteractionComprehensive > LeftInteractions > Features`

> **ShapeRecognizerActiveState Documentation**: 
> 
> **Purpose**: Detects specific finger configurations by comparing current hand finger states to predefined shapes.
> 
> **How it works**: Monitors the tracked hand's fingers and becomes active when the hand adopts a pose that matches any of the specified shapes (like thumbs up or closed fist).
> 
> **Key features**: 
> - Compares finger states from `IFingerFeatureStateProvider` to shape requirements
> - Supports multiple shape configurations for flexible pose detection
> - Automatically activates native recognition components for optimal performance

2. **Configure the shapes for thumbs up**: 
   - In the **Shapes** property, click the "+" button twice to add two elements
   - Set **Element 0** to `FingersAllClosedShapeRecognizer`
     - Click the circle in the property field and search for "FingersAllClosed"
     - Note: You may need to unhide this asset in the Assets selection window
   - Set **Element 1** to `ThumbUpShapeRecognizer`
     - Click the circle in the property field and search for "ThumbUp"

### Step 4: Configure Transform Recognition
1. **Set up the TransformRecognizerActiveState component**:
        - In the TransformRecognizerActiveState component:
       - Set **Hand** to `[BuildingBlock] OVRInteractionComprehensive > LeftInteractions (Hand)`
                - Set **Transform Feature State Provider** to `[BuildingBlock] OVRInteractionComprehensive > LeftInteractions > Features`

> **TransformRecognizerActiveState**: 
> 
> **Purpose**: Detects hand orientation and transform states in 3D space (like palm facing up, wrist orientation, etc.).
> 
> **How it works**: Monitors the hand's transform features and becomes active when the hand adopts the required orientation states.
> 
> **Key features**: 
> - Tracks hand orientation relative to the user (palm towards face, wrist up, etc.)
> - Uses `TransformConfig` to define acceptable orientation thresholds
> - Works in conjunction with shape recognition for complete pose detection

2. **Configure the pose orientation**:
   - In **Transform Feature Configs**, select `Wrist Up`
   - Under **Transform Config**, set **Up Vector Type** to `World`
   - Set **Feature Thresholds** to `DefaultTransformFeatureStateThresholds`
     - Click the circle in the property field and search for "DefaultTransformFeatureStateThresholds"

### Step 5: Combine Shapes and Orientation
1. **Configure the ActiveStateGroup component**:
   - In the ActiveStateGroup component, in the **Shapes** property:
     - Click the "+" button twice to add two elements
     - Set **Element 0** to the ThumbsUpPoseLeft GameObject
       - Select `ShapeRecognizerActiveState` from the list
     - Set **Element 1** to the ThumbsUpPoseLeft GameObject
       - Select `TransformRecognizerActiveState` from the list

> **ActiveStateGroup**: 
> 
> **Purpose**: Combines multiple active states using logical operators (AND, OR, XOR) to create complex interaction conditions.
> 
> **How it works**: Evaluates all child active states and applies the specified logic operator to determine the group's overall active state.
> 
> **Key features**: 
> - Supports AND (all states must be active), OR (any state can be active), and XOR (exactly one state must be active)
> - Perfect for combining shape recognition with transform recognition for complete pose detection
> - Automatically manages state transitions and updates

### Step 6: Track the Pose State
1. **Configure the ActiveStateSelector component**:
   - In the ActiveStateSelector component:
     - Set **Active State** to the ThumbsUpPoseLeft GameObject
     - Select `ActiveStateGroup` from the list
     - This tracks the combined state of shapes and orientation

> **ActiveStateSelector**: 
> 
> **Purpose**: Converts an `IActiveState` into an `ISelector` that can trigger selection events when the active state changes.
> 
> **How it works**: Monitors the provided active state and raises `WhenSelected()` and `WhenUnselected()` events based on state changes.
> 
> **Key features**: 
> - Bridges the gap between pose detection and interaction selection
> - Automatically handles state transitions and event firing
> - Can replace traditional selection mechanisms (trigger pulls, pinch poses) with custom active states

### Step 7: Set Up Event Handling
1. **Configure the SelectorUnityEventWrapper component**:
   - In the SelectorUnityEventWrapper component:
     - Set **Selector** to the ThumbsUpPoseLeft GameObject
     - This component will handle the events when thumbs up is detected/stopped

> **SelectorUnityEventWrapper**: 
> 
> **Purpose**: Connects Unity's event system to Meta SDK selectors, allowing you to trigger Unity events when selection states change.
> 
> **How it works**: Listens to the provided selector's `WhenSelected()` and `WhenUnselected()` events and converts them to Unity events that can be configured in the Inspector.
> 
> **Key features**: 
> - Enables visual feedback, audio, and other Unity-based responses to gesture detection
> - Provides a clean interface between Meta SDK and Unity's event system
> - Supports both automatic and manual event configuration

### Step 8: Create the Material Swapping System
1. **Create two materials**:
   - In the Project window, right-click → Create → Material
   - Name the material "NormalMaterial"
   - Set NormalMaterial color to Green
   - Create another material
   - Name the second material "SelectedMaterial"
   - Set SelectedMaterial color to red

2. **Create a MaterialSwapper script**:
   - In the Project window, right-click in Scripts folder → Create → C# Script
   - Name it "MaterialSwapper"

3. **Write the MaterialSwapper script**:
```csharp
using UnityEngine;

public class MaterialSwapper : MonoBehaviour
{
    public GameObject targetObject;
    
    public Material materialA;
    public Material materialB;
    
    private Renderer targetRenderer;
    
    void Start()
    {
      targetRenderer = targetObject.GetComponent<Renderer>();
      targetRenderer.material = materialA;
    }
    
    public void SwapToMaterialB()
    {
      targetRenderer.material = materialB;
      Debug.Log("Swapped to Material B");
    }
    
    public void SwapToMaterialA()
    {
      targetRenderer.material = materialA;
      Debug.Log("Swapped to Material A");
    }
}
```

### Step 9: Connect the System
1. **Add MaterialSwapper to a cube**:
   - Select one of your Grab Interaction cubes
   - Add the MaterialSwapper script as a component
   - In the MaterialSwapper component:
     - Set **Target Object** to the cube GameObject
     - Set **Material A** to your NormalMaterial
     - Set **Material B** to your SelectedMaterial

2. **Connect the events**:
   - Select ThumbsUpPoseLeft in the hierarchy
   - In the SelectorUnityEventWrapper component:
     - In **When Selected()**, click the "+" button
     - Drag the cube with MaterialSwapper to the field
     - Select `MaterialSwapper.SwapToMaterialB()` from the function list
     - In **When Unselected()**, click the "+" button
     - Drag the same cube to the field
     - Select `MaterialSwapper.SwapToMaterialA()` from the function list

### Step 10: Test Your Setup
1. **Enter Play Mode**:
   - Click the Play button in Unity
   - Your scene should run with gesture recognition active

2. **Test the gesture detection**:
   - Perform a left hand thumbs up gesture
   - The selected cube should change to the SelectedMaterial (red)
   - Stop the gesture and the cube should return to the NormalMaterial (green)

## Expected Results
By the end of this activity, you should have:
- A working left hand thumbs up gesture detection system
- One cube that changes color when the gesture is performed
- A complete hand pose recognition setup using Meta's building blocks
- Understanding of how to connect gesture detection to visual feedback

## Troubleshooting
- **Components not found**: Ensure you're using Meta SDK v78 and have the Interactions Rig building block with "OVRInteractionsComprehensive"
- **Events not firing**: Verify all component connections are correct, especially the Hand and Feature State Provider references
- **Material not changing**: Check that the MaterialSwapper script is properly attached and materials are assigned


## Extension Activities
- **Right hand implementation**: Repeat the setup for the right hand by creating ThumbsUpPoseRight
- **Different hand poses**: Experiment with other hand poses like peace sign or pointing
- **Multiple cube targets**: Apply MaterialSwapper to multiple cubes with different gesture effects

## Component System Overview
This activity demonstrates how Meta's building blocks work together to create complex gesture recognition:

### **Data Flow Architecture**:
```
Hand Tracking → Shape Recognition → Transform Recognition → Active State Group → Selector → Unity Events → Visual Feedback
```

### **Component Responsibilities**:
1. **ShapeRecognizerActiveState**: Detects finger configurations (thumbs up, closed fist)
2. **TransformRecognizerActiveState**: Detects hand orientation (palm up, wrist position)
3. **ActiveStateGroup**: Combines both recognizers using AND logic
4. **ActiveStateSelector**: Converts pose detection to selection events
5. **SelectorUnityEventWrapper**: Bridges Meta SDK to Unity's event system
6. **MaterialSwapper**: Provides visual feedback through material changes

