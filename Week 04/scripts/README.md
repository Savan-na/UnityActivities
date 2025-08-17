# Week 04 Activity 1 Scripts

This folder contains the C# scripts for the Proximity-Based Object Reactions activity.

## Scripts

### **ProximityHighlighter.cs** - Basic Proximity Highlighting
- **Use this for**: Learning the core proximity highlighting concept
- **Features**: 
  - Changes object color when player approaches
  - Configurable proximity distance and colors
  - Performance optimized (finds player once in Start)
- **Best for**: Beginners learning proximity detection and visual feedback

### **ProximityMover.cs** - Movement Extension
- **Use this for**: Adding movement behavior to your highlighting system
- **Features**: 
  - Minimal implementation focused only on movement
  - Objects move away from player when approached
  - Returns to original position when player leaves
- **Best for**: Users who want to add movement without other features

### **ProximitySound.cs** - Sound Effects Extension
- **Use this for**: Adding audio feedback to proximity changes
- **Features**: 
  - Minimal implementation focused only on sound
  - Plays sound when player enters proximity zone
  - Requires AudioSource component and audio clip
- **Best for**: Users who want to add audio feedback without other features

### **ProximityEffects.cs** - Visual Effects Extension
- **Use this for**: Adding rotation and scaling effects
- **Features**: 
  - Minimal implementation focused only on visual effects
  - Objects rotate when player is nearby
  - Objects scale up when nearby, return to normal when player leaves
- **Best for**: Users who want to add visual effects without other features

## How to Use

1. **Choose the appropriate script** based on your needs:
   - Start with **ProximityHighlighter** for basic highlighting
   - Use **ProximityMover** to add movement
   - Use **ProximitySound** to add audio feedback
   - Use **ProximityEffects** to add rotation/scaling
2. **Attach the script** to a GameObject in your scene
3. **Configure the settings** in the Inspector:
   - Proximity Distance: How close the player needs to be
   - Normal Color: Default object color
   - Highlight Color: Color when player is nearby
   - Extension-specific settings (movement speed, audio clips, etc.)
4. **Test in Play mode**

## Requirements

- Unity 2022.3 or later
- Player object must have "Player" tag
- Object must have a Renderer component (automatically added to 3D objects)

### Extension-Specific Requirements:
- **ProximityMover**: No additional requirements
- **ProximitySound**: Add AudioSource component to object, assign audio clip
- **ProximityEffects**: No additional requirements

## Learning Notes

- The script demonstrates proximity detection using `Vector3.Distance()`
- Shows how to use boolean flags to detect state changes
- Teaches component access and material manipulation
- Extension activities in the main document provide conceptual challenges for further learning
