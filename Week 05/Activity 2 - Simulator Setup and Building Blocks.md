# Activity 2: XR Simulator Setup

## Objective
Set up the XR Simulator and add basic interaction building blocks to your scene. You'll learn to test XR projects on your computer and add grab interactions using Meta's Building Blocks system.

## Prerequisites
- Complete Activity 1: XR Project Setup and Headset Build
- Unity project with Meta All-in-One SDK installed
- Working scene with Camera Rig and rotating cube

## Reference Materials
**YouTube Tutorial**: [XR Simulator Setup and Usage](https://www.youtube.com/watch?v=RRsxYtuO2iM) - Detailed explanation of the simulator functionality.

**Official Documentation**: [Meta XR Simulator Introduction](https://developers.meta.com/horizon/documentation/unity/xrsim-intro) - Complete simulator overview and features.

## Instructions

### Step 1: Check if XR Simulator is Installed
1. **Open Package Manager**:
   - Window → Package Manager

2. **Look for XR Simulator**:
   - Search for "XR Simulator" in the Package Manager
   - Check if it's already installed
   
   `The XR Simulator should have been installed automatically when you installed the Meta All-in-One SDK in Activity 1. If it's missing, you'll need to install it manually.`

### Step 2: Install XR Simulator (if missing)
1. **Install from Package Manager**:
   - In Package Manager, search for "XR Simulator"
   - Click "Install" if found
   
   **OR**
   
2. **Install from Asset Store** (if not in Package Manager):
   - Go to [Meta XR Simulator on Asset Store](https://assetstore.unity.com/packages/tools/integration/meta-xr-simulator-195361)
   - Download and import the package
   
   `The XR Simulator provides a way to test XR projects on your computer using keyboard and mouse input, simulating head movement and basic interactions.`

### Step 3: Configure Your Scene for Simulator
1. **Ensure Camera Rig is present**:
   - Your scene should already have the Camera Rig from Activity 1
   - If not, add it using Meta → Tools → Building Blocks → Core → Camera Rig
   
   `The Camera Rig is essential for the simulator to work properly. It provides the XR Origin and camera components that the simulator can control.`

2. **Verify scene setup**:
   - Camera Rig at position (0, 0, 0)
   - Rotating cube visible in the scene
   - Ground plane for reference
   
   `A properly set up scene ensures the simulator can provide an accurate VR experience preview.`

### Step 4: Test with XR Simulator
1. **Enter Play Mode**:
   - Click Meta → Meta XR Simulator → Activate
   - Click the Play button in Unity
   - Your scene should now run in XR Simulator mode
   
   `The simulator automatically activates when you enter Play Mode with an XR-enabled scene.`

2. **Simulator Controls**:
   - **Mouse**: Look around (right-click and drag)
   - **WASD**: Move around the scene
   - **R**: Move up
   - **F**: Move down
   - **U**: Squeeze Trigger
   
   `These controls simulate basic VR movement and head tracking, allowing you to navigate your XR scene on your computer.`

3. **Test your scene**:
   - Look around using the mouse
   - Move around using WASD keys
   - Verify the rotating cube is visible and rotating
   - Test that movement feels smooth and responsive
   
   `This simulates the basic VR experience you'll have on the actual Quest headset.`

### Step 5: Add Interaction Building Blocks
1. **Open Building Blocks**:
   - Go to Meta → Tools → Building Blocks
   - Click on the "Core" category first
   
   `The Core Building Blocks provide the essential components needed for all XR interactions.`

2. **Add Interaction Rig**:
   - Drag the "Interaction Rig" from the Core category into your scene
   - Position it as a child of your Camera Rig (it should do this by default)
   
   `The Interaction Rig is essential - it provides the hand tracking and interaction system that all other interaction blocks depend on.`

3. **Add Grab Interaction**:
   - Go to the "Interaction" category in Building Blocks
   - Drag the "Grab Interaction" block into your scene
   - Position it near your rotating cube
   
   `The Grab Interaction block is a small blue cube that comes fully configured and ready to grab.`

4. **Test the Pre-configured Grabbable Object**:
   - Enter Play Mode
   - Use the simulator controls to approach the blue cube
   - Press **U** (squeeze trigger) to grab it
   - Move around while holding the cube
   - Release **U** to drop it
   
   `This tests the grab interaction system with a properly configured object.`



## Troubleshooting Common Issues

### **Simulator Not Working**
- Ensure XR Plugin Management is properly configured
- Check that OpenXR is enabled in Project Settings
- Verify the Camera Rig is present in your scene

### **Movement Not Working**
- Check that you're in Play Mode
- Ensure the scene has focus (click in the Game view)
- Verify no other input systems are interfering

### **Scene Not Loading**
- Check that your scene is added to Build Settings
- Ensure all required packages are installed
- Restart Unity if issues persist

### **Grab Interaction Not Working**
- Verify the Interaction Rig is a child of the Camera Rig
- Check that the Grab Interaction block is properly positioned in your scene
- Ensure you're using the **U** key (squeeze trigger) to grab
- Try restarting the simulator if interactions feel unresponsive

## Extension Activities

### **Test on Physical Headset**
- Build your project to your Quest 2/3 headset
- Test the grab interaction in real VR
- Compare the experience to the simulator
- Note any differences in performance or feel

### **Experiment with Different Objects**
- Try grabbing different objects in your scene
- Test how different object sizes feel when grabbed
- Experiment with object weights and physics

### **Make Your Rotating Cube Grabbable**

Follow the steps outlined here:  
https://developers.meta.com/horizon/documentation/unity/unity-isdk-create-grabbable-object

## Outcome
A working XR Simulator setup with grab interaction building blocks. You can now test your XR project on your computer, including basic VR movement and grab interactions using Meta's pre-configured Building Blocks.

## Save Your Work
**Don't forget to save your scene and project!**

## Next Steps
Once you've completed this activity successfully:
- Play with Building blocks and see what is possible!
- Read the documentation and see what's possible!