# Activity 5: Getting Set Up for XR

## Video Reference
For a detailed walkthrough of this process, watch: [Unity XR Setup for Meta Quest](https://www.youtube.com/watch?v=nRhcJDYCegU)

This video provides an excellent step-by-step guide that aligns with the process outlined below.

## Objective
Learn to set up a Unity project for Meta Quest 2/3 XR development, including project configuration and basic scene setup.

## Prerequisites
- Unity 2022.3 LTS installed
- Understanding of basic Unity concepts from previous activities
- Meta Quest 2 or 3 device (optional for testing)

## Instructions

### Step 1: Create a New Unity Project
1. **Open Unity Hub**
2. **Create a new project**:
   - Click "New Project"
   - Choose "3D" template
   - Name it "XR_Setup_Project"
   - Select Unity 2022.3 LTS
   - Click "Create project"

### Step 2: Set Up the Basic Scene
1. **Create a rotating cube**:
   - GameObject → 3D Object → Cube
   - Rename it to "XR_Cube"
   - Position it at (0, 1, 0)
   - Add the DebugRotator script from Activity 1
   - Set rotation speed to 30

2. **Add a ground plane**:
   - GameObject → 3D Object → Plane
   - Rename it to "Ground"
   - Position it at (0, 0, 0)
   - Scale it to (5, 1, 5) for a larger ground

3. **Test the basic scene**:
   - Click Play to verify the cube rotates
   - Stop the scene when satisfied

### Step 3: Install XR Plugin Management
1. **Open Package Manager**:
   - Window → Package Manager

2. **Install XR Plugin Management**:
   - Click the "+" button
   - Select "Add package by name"
   - Enter "com.unity.xr.management"
   - Click "Add"

3. **Install Oculus XR Plugin**:
   - In Package Manager, search for "Oculus XR Plugin"
   - Click "Install"
   - Accept any dependency installations

### Step 4: Configure Project Settings for Quest
1. **Open Project Settings**:
   - Edit → Project Settings

2. **Configure XR Plugin Management**:
   - Select "XR Plugin Management" from the left sidebar
   - Check "Initialize XR on Startup"
   - In the "Plugin Providers" tab, check "Oculus"

3. **Configure Player Settings**:
   - Select "Player" from the left sidebar
   - In "Other Settings":
     - Set "Scripting Backend" to "IL2CPP"
     - Set "Target Architectures" to "ARM64"
   - In "Publishing Settings":
     - Set "Package Name" to "com.yourname.xrproject"

### Step 5: Configure Build Settings
1. **Open Build Settings**:
   - File → Build Settings

2. **Switch Platform**:
   - Select "Android" from the platform list
   - Click "Switch Platform"
   - Wait for Unity to recompile

3. **Configure Android Settings**:
   - In Project Settings → Player → Android
   - Set "Minimum API Level" to "Android 10.0 (API level 29)"
   - Set "Target API Level" to "Automatic (highest installed)"

### Step 6: Set Up Quest-Specific Settings
1. **Configure Oculus Settings**:
   - In Project Settings → XR Plugin Management → Oculus
   - Check "Low Overhead Mode"
   - Set "Dash Support" to "Supported"
   - Set "Low Overhead Mode" to "Enabled"

2. **Configure Quality Settings**:
   - Edit → Project Settings → Quality
   - Set "Anti Aliasing" to "Disabled" or "2x Multi Sampling"
   - Set "Texture Quality" to "Full Res"

### Step 7: Set Up XR Simulator (Windows)
1. **Install XR Simulator**:
   - In Package Manager, search for "XR Simulator"
   - Click "Install" if available
   - This allows you to test XR functionality without a Quest device

2. **Configure Simulator Settings**:
   - In Project Settings → XR Plugin Management → Oculus
   - Check "Enable XR Simulator" if available
   - Set "Simulator Mode" to "Enabled"

3. **Test with Simulator**:
   - Click Play in Unity
   - Use keyboard and mouse to simulate VR interaction
   - WASD keys for movement, mouse for looking around
   - Spacebar for primary action (like controller trigger)

### Step 8: Test Your XR Setup
1. **Build and Test** (if you have a Quest device):
   - Connect your Quest to your computer via USB
   - Enable Developer Mode on your Quest
   - In Build Settings, click "Build And Run"
   - The app should install and run on your Quest

2. **Test in Unity Editor with Simulator** (if no Quest device):
   - Click Play in Unity
   - Use the XR Simulator controls to navigate
   - Your rotating cube should be visible in the simulated VR environment
   - Test how objects look and behave in the simulated VR space

## Understanding XR Development Setup

### **Why These Settings Matter**
- **IL2CPP**: Required for Quest apps (faster than Mono)
- **ARM64**: Quest uses ARM64 architecture
- **Android API 29+**: Required for Quest 2/3 compatibility
- **Oculus Plugin**: Enables Quest-specific features

### **Key XR Concepts**
- **XR Plugin Management**: Unity's system for managing VR/AR plugins
- **Build Target**: Android is required for Quest development
- **Performance Settings**: Quest has limited processing power
- **Package Name**: Unique identifier for your app
- **XR Simulator**: Allows testing VR functionality without physical hardware

## Extension Activities

### **Add More Interactive Objects**
- Create additional cubes, spheres, and cylinders
- Add different rotation speeds and directions
- Test how objects look and perform in XR

### **Experiment with XR Settings**
- Try different quality settings to see performance impact
- Test different anti-aliasing options
- Experiment with texture quality settings

### **Prepare for Hand Tracking**
- Add objects that could be grabbed in VR
- Position objects at hand height (around Y = 1.5)
- Think about how objects would feel in VR

## Outcome
A properly configured Unity project ready for Meta Quest XR development, with basic scene setup and understanding of XR-specific project settings.

## Save Your Work
**Don't forget to save your scene and project!**
- Press Ctrl+S (Windows) or Cmd+S (Mac) to save your scene
- Go to File → Save Project to save all your work
