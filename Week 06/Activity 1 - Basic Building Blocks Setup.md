# Activity 1: Basic Building Blocks Setup

## Objective
Set up a new Unity project with Meta SDK building blocks and create a simple passthrough game where users can interact with objects using hands or controllers. This activity follows the video tutorial and establishes the foundation for the advanced interactions in subsequent activities.

## Prerequisites
- Complete Week 5 activities to get set up with XR
- Unity 2022.3 LTS installed
- Meta Quest 2/3 device (for final testing)
- USB-C cable for Quest 2/3 connection

## Reference Materials
**YouTube Tutorial**: [How to Build a Meta Quest Game in Just a Few Clicks](https://www.youtube.com/watch?v=paVX3Pm4Yq4) - This video demonstrates the complete building blocks setup process.

**Official Documentation**: [Meta Building Blocks Documentation](https://developers.meta.com/horizon/documentation/unity/bb-overview/) - Complete reference for all available building blocks.

## Instructions

### Step 1: Project Setup and Organization
1. **Create a new Unity project**:
   - Open Unity Hub
   - Click "New Project"
   - Choose "3D" template
   - Name it "Week6_Studio" (or anything else)
   - Click "Create project"

2. **Save your scene immediately** (Ctrl+S) and name it "Week6_Studio_Scene"
3. **Create an organized folder structure** in the Project window:
   - Right-click in Project window → Create → Folder
   - Create: "Scripts", "Materials", "Scenes"

### Step 2: Install Meta XR All-in-One SDK and other required packages
1. **Open Package Manager**:
   - Window → Package Manager

2. **Install Meta XR All-In-One SDK**:
   - In the Package Manager, click the "+" button in the top left
   - Select "Add package by name"
   - Enter: `com.meta.xr.sdk.all`
   - Click "Add"
   - Accept any dependency installations when prompted
   
   `The Meta XR All-In-One SDK provides the complete Meta Building Blocks system, giving you access to pre-built XR interaction components that are specifically optimized for Quest devices.`

3. **Restart Unity Editor** if prompted
4. **Verify installation**:
   - Check that all Meta XR packages are installed
   - Look for "Meta XR All-in-One SDK" in the Package Manager, as well as a bunch of other Meta XR Packages

5. **Install XR Plugin Management package**:
   - In the Package Manager, search for "XR Plugin Management"
   - Or add by name: `com.unity.xr.management`
   - Click "Install"
   
6. **Install Unity OpenXR Plugin**:
   - In the Package Manager, search for "OpenXR Plugin"
   - Or add by name: `com.unity.xr.openxr`
   - Click "Install"
   


### Step 3: Configure Project for Meta Quest
1. **Open Meta Project Setup Tool**:
   - Go to Meta → Tools → Project Setup Tool

2. **Configure for Desktop**:
   - Select "Desktop" platform tab (usually selected by default)
   - Click "Fix All" to configure desktop settings
   - Click "Apply All" to save the changes
   
3. **Configure for Android**:
   - Select "Android" platform tab
   - Click "Fix All" to automatically configure project settings
   - Click "Apply All" to save the changes
   
   `This automatically configures your project with the correct settings for Meta Quest development, including minimum API level, target architecture, and XR plugin management.`

4. **Switch your platform to Android**:
   - Open File → Build settings...
   - Select Android
   - Click Switch Platform

**~At this point, you are set up and ready to build for XR with Meta Quest SDK~**

### Step 4: Add Core Building Blocks
1. **Open Building Blocks**:
   - Go to Meta → Tools → Building Blocks
   - This opens the Building Blocks window

2. **Add Camera Rig**:
   - From the Core category, drag "Camera Rig" into your scene
   - Position it at (0, 1.5, 0)
   - See that it has added "[BuildingBlock] Camera Rig" to your scene.
   
   `The Camera Rig provides the essential XR camera system with left and right hand tracking, forming the foundation for all XR interactions.`

3. **Add Interactions Rig**:
   - From the Interaction category, drag "Interactions Rig" into your scene
   - This automatically adds controller and hand support.
   - See that it has added "[BuildingBlock] OVRInteractionComprehensive" to your camera Rig.
   
   `This Building Block provides a comprehensive control scheme that allows for both hand and controller interactions. Controller Tracking provides visual representation of your controllers in VR, making it easier to understand hand positioning and orientation. Hand Tracking allows users to interact with objects using their actual hands, providing more natural and intuitive interactions.`


### Step 5: Add Passthrough and Interaction Features
1. **Add Passthrough**:
   - Drag "Passthrough" from the Core category into your scene
   - This enables the real-world view through the headset
   
   `Passthrough allows users to see the real world while wearing the headset, creating a mixed reality experience.`

2. **Add Grab Interaction**:
   - Drag "Grab Interaction" from the Interaction category into your scene
   - Position it at (0.2, 1.5, 2) - at eye level for VR
   
   `The Grab Interaction is a pre-configured cube that can be grabbed and manipulated with hands or controllers.`

3. **Add another Grab Interaction**:
   - Drag another "Grab Interaction" from the Interaction category into your scene
   - Position it at (-0.2, 1.5, 2)
   
   `This second Grab Interaction provides another object to interact with.`

### Step 6: Create a Simple Environment

1. **Create a ground surface**:
   - GameObject → 3D Object → Plane
   - Rename it to "Ground"
   - Position at (0, 0, 0)
   - Disable the Mesh Renderer (using the checkbox in the inspector) to make it invisible

   `With interactions enabled (including movement using the left controller stick), the player needs a ground so they don't fall into the void.`

2. **Create a table surface**:
   - GameObject → 3D Object → Cube
   - Rename it to "Table"
   - Position at (0, 0.5, 0.75)
   - Scale to (1, 0.05, 0.5)

3. **Position objects on the table**:
   - Move the first Grab Interaction to (0.2, 0.6, 0.75)
   - Move the second Grab Interaction to (-0.2, 0.6, 0.75)
   
   `These positions place the objects nicely on the table surface, spaced apart for easy interaction.`

### Step 7: Test Your Setup
1. **Enter Play Mode**:
   - Click the Play button in Unity
   - Your scene should now run.
   - Interact with the objects!

## Expected Results
By the end of this activity, you should have:
- A Unity project properly configured for Meta Quest development
- A scene with Camera Rig and Interactions
- Passthrough functionality enabled
- Basic grabbable objects in your scene
- A simple environment to interact with

## Troubleshooting
- **Building Blocks not appearing**: Ensure Meta XR All-in-One SDK is properly installed
- **XR Plugin Management errors**: Use the Meta Project Setup Tools to fix configuration issues
- **Hand tracking not working**: Verify that hand tracking is enabled in the OVR Manager component on your Camera Rig

## Next Steps
This activity establishes the foundation for Activity 2 and 3. Make sure everything is working correctly before proceeding, as the subsequent activities build upon this setup.

## Extension Activities
- **Enable physics interactions**: Uncheck "Is Trigger" on the Collider component and "Is Kinematic" on the Rigidbody component of the Grab Interaction cubes to allow them to interact with physics and be thrown around
- **Customize the environment**: Add more objects, change materials, or create a more complex scene
