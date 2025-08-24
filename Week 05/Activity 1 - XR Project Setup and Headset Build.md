# Activity 1: XR Project Setup and Headset Build

## Objective
Set up a Unity project specifically configured for Meta Quest 2/3 XR development, including project configuration, Meta All-in-One SDK installation, and building to the physical headset. 

## Prerequisites
- Unity 2022.3 LTS installed
- Meta Quest 2 or Quest 3 device (for final testing)
- USB-C cable for Quest 2/3 connection
- Meta Quest Developer Hub installed on your computer

## Reference Materials


**Official Documentation**: [Meta Unity Project Setup Guide](https://developers.meta.com/horizon/documentation/unity/unity-project-setup) - Complete setup instructions and troubleshooting from Meta.

## Instructions

### Step 1: Project Setup and Organization
1. **Create a new Unity project**:
   - Open Unity Hub
   - Click "New Project"
   - Choose "3D" template
   - Name it "Week5_XR_Project"
   - Select Unity 2022.3 LTS
   - Click "Create project"

2. **Save your scene immediately** (Ctrl+S) 
3. **Create an organized folder structure** in the Project window:
   - Right-click in Project window → Create → Folder
   - Create: "Scripts", "Materials", "Prefabs", "Scenes", "XR"

### Step 2: Basic Scene Setup
1. **Create a ground plane**:
   - GameObject → 3D Object → Plane
   - Rename it to "Ground"
   - Position at (0, 0, 0)
   - Scale to (10, 1, 10) for a large VR space

2. **Create a simple test object**:
   - GameObject → 3D Object → Cube
   - Rename it to "TestCube"
   - Position at (0, 1.5, 2) - at eye level for VR
   - Scale to (0.5, 0.5, 0.5)

3. **Add a rotating component**:
   - Select the TestCube
   - Add Component → Scripts → Rotator (from Week 2)
   - Set rotation speed to 30

### Step 3: Install XR Plugin Management
1. **Install XR Plugin Management package**:
   - In the Package Manager, search for "XR Plugin Management"
   - Or add by name: `com.unity.xr.management`
   - Click "Install"
   
   `The XR Plugin Management package is required to configure which XR plugins Unity should use. Without this package, you won't be able to access the XR Plugin Management settings in Project Settings.`

2. **Verify XR Plugin Management is installed**:
   - Look for "XR Plugin Management" in the Package Manager
   - This package should now be available

### Step 4: Install Unity OpenXR Plugin
1. **Install Unity OpenXR Plugin**:
   - In the Package Manager, search for "OpenXR Plugin"
   - Or add by name: `com.unity.xr.openxr`
   - Click "Install"
   
   `The Unity OpenXR Plugin provides the foundation for XR development and is required for Meta Quest development. This plugin enables Unity to communicate with XR devices through the OpenXR standard.`

2. **Verify OpenXR Plugin is installed**:
   - Look for "OpenXR Plugin" in the Package Manager
   - This package should now be available

### Step 5: Install Meta All-in-One SDK
1. **Open Package Manager**:
   - Window → Package Manager

2. **Install Meta XR All-In-One SDK**:
   - **Option A - Package Manager (Recommended)**:
     - In the Package Manager, click the "+" button
     - Select "Add package by name"
     - Enter: `com.meta.xr.sdk.all`
     - Click "Add"
   - **Option B - Asset Store**:
     - Go to [Meta XR All-In-One SDK on Asset Store](https://assetstore.unity.com/packages/tools/integration/meta-xr-all-in-one-sdk-269657)
     - Download and import the package
   - Accept any dependency installations when prompted
   
   `The Meta XR All-In-One SDK provides the complete Meta Building Blocks system, giving you access to pre-built XR interaction components that are specifically optimized for Quest devices and eliminate the need to code basic XR functionality from scratch.`

3. **Verify installation**:
   - Check that all Meta XR packages are installed
   - Look for "Meta XR All-in-One SDK" in the Package Manager
   
   `Ensuring all packages are properly installed prevents missing dependency errors and ensures you have access to the full Meta Building Blocks ecosystem.`

   ![](./images/2025-08-24%2017_38_09-Package%20Manager.png)

4. **Run the Project Setup Tool**:
   - After installation, Unity should automatically open the "Meta XR Project Setup Tool" window
   - If it doesn't open automatically, go to **Meta → Tools → Project Setup Tool**
   - Click "Fix All" to configure all necessary settings automatically
   - Click the Android Tab and click "Fix All" again
   - This tool will configure XR Plugin Management, Player Settings, and other required settings
   
   `The Project Setup Tool automatically configures your Unity project with all the necessary settings for Meta Quest development. This ensures compatibility and prevents common configuration errors that could cause build failures.`

### Step 7: Configure Project Settings for Quest 2/3 (optional)
1. **Open Project Settings**:
   - Edit → Project Settings

2. **Verify XR Plugin Management** (may already be configured by Project Setup Tool):
   - Select "XR Plugin Management" from the left sidebar
   - Check "Initialize XR on Startup"
   - In the "Plugin Providers" tab, check "OpenXR"
   - Ensure "OpenXR" is at the top of the plugin list (highest priority)
   
   `The Project Setup Tool should have already configured these settings, but it's good practice to verify them. Initializing XR on startup ensures your app is ready for VR from the moment it launches. Checking "OpenXR" tells Unity to use the OpenXR standard, which is required for Meta Quest development.`



### Step 7: Configure Build Settings
1. **Open Build Settings**:
   - File → Build Settings

2. **Switch Platform**:
   - Select "Android" from the platform list
   - Click "Switch Platform"
   - Wait for Unity to recompile (this may take several minutes)
   
   `Android is required for Quest development because both Quest 2 and Quest 3 run on Android. Unity needs to recompile all scripts and assets for the Android platform, which is why this step takes time.`

3. **Add your scene to build**:
   - Click "Add Open Scenes" to include your current scene
   - Ensure your scene is at index 0 in the build order
   
   `The scene at index 0 is the first scene that loads when your app starts. This should be your main scene that sets up the XR environment.`

### Step 8: Set Up Quest 2/3-Specific Settings (optional)
1. **Configure Quality Settings**:
   - Edit → Project Settings → Quality
   - Set "Anti Aliasing" to "Disabled" or "2x Multi Sampling"
   - Set "Texture Quality" to "Full Res"
   - Set "Anisotropic Textures" to "Per Texture"
   
   **Quest 2 vs Quest 3 Differences:**
   - **Quest 2**: Set "Anti Aliasing" to "Disabled" for best performance
   - **Quest 3**: Can use "2x Multi Sampling" for better visual quality
   
   `Both Quest 2 and Quest 3 have limited processing power compared to PC, so performance optimization is crucial. Quest 2 needs more aggressive optimization, while Quest 3 can handle slightly higher quality settings.`

### Step 9: Set Up Basic XR Controllers
1. **Open Building Blocks**:
   - In Unity, go to Meta → Tools → Building Blocks
   - This opens the Building Blocks window with pre-built XR components
   
   `The Building Blocks system provides pre-configured XR components that are optimized for Quest devices. This eliminates the need to manually set up XR controllers and ensures proper compatibility.`

2. **Select Core Building Blocks**:
   - In the Building Blocks window, click on the "Core" category
   - This contains the essential XR components you need
   
   `Core Building Blocks provide the fundamental XR functionality like camera rigs, input systems, and basic interactions.`

3. **Add Camera Rig to your scene**:
   - Drag the "Camera Rig" prefab from Core Building Blocks into your Hierarchy
   - Position it at (0, 0, 0) - this will be your VR camera position
   
   `The Camera Rig contains the main camera and defines where the player's head position is in the virtual world. It's the foundation for all VR interactions.`


### Step 10: Test Your XR Setup
1. **Test in Unity Editor**:
   - Click Play in Unity
   - Your scene should run normally
   - The rotating cube should be visible

2. **Check for XR errors**:
   - Look at the Console for any XR-related warnings
   - Ensure no critical errors are present

### Step 11: Build to Quest 2/3
1. **Open Build Settings**:
   - Go to File → Build Settings
   - Ensure "Android" is selected as the target platform 
   - If not, click "Android" then click "Switch Platform"
   
   `Android must be selected as the target platform because both Quest 2 and Quest 3 run on Android. If you see "PC, Mac & Linux Standalone" selected, you'll need to switch to Android first.`

2. **Prepare your Quest 2/3**:
   - Put on your Quest 2 or Quest 3 headset
   - Enable Developer Mode in the Meta Quest app (this should already be done)
   - Connect Quest 2/3 to your computer via USB-C cable
   - Allow USB debugging when prompted

3. **Build and Run**:

   - In Build Settings, click "Build And Run"
   - Choose a build location (e.g., "Week5_Builds" folder)
   - Wait for the build process to complete
   - The app should automatically install and launch on your Quest 3

3. **Test in VR**:
   - Look around your scene in VR
   - Verify the rotating cube is visible
   - Test basic head movement and tracking



## Troubleshooting Common Issues

### **Build Fails**
- Ensure Android SDK is properly installed
- Check that Unity is using the correct Android SDK path
- Verify all required packages are installed

### **App Won't Install on Quest 2/3**
- Check that Developer Mode is enabled
- Ensure USB debugging is allowed
- Try a different USB cable
- Restart both Unity and Quest 2/3



## Outcome
A properly configured Unity project ready for Meta Quest 2/3 XR development, with basic scene setup, understanding of XR-specific project settings, and successful build to the physical headset where you can see your rotating cube in VR.

## Save Your Work
**Don't forget to save your scene and project!**

## Next Steps
Once you've completed this activity, you'll be ready to develop in XR!
