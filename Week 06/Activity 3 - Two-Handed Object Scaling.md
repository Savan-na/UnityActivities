# Activity 3 - Two-Handed Object Scaling

## Overview
In this activity, you'll learn how to implement two-handed object scaling using Meta SDK building blocks. This will allow users to grab an object with both hands or controllers and scale it by moving their hands apart or together, creating an intuitive stretching effect.

## Prerequisites
- Complete Activity 1 and Activity 2
- Have a working XR project with Meta SDK building blocks
- Basic understanding of Unity's Transform system

## Learning Objectives
- Understand how to use Meta SDK's GrabFreeTransformer for object manipulation
- Learn about transformer constraints and their effects
- Explore custom transformer scripts for specialized behaviors
- Implement two-handed scaling interactions

## Activity Steps

### Step 1: Add GrabFreeTransformer to Grab Interaction Cube
1. In your Hierarchy, locate one of the **Grab Interaction** cubes from Activity 1
2. Select the cube and look at its Inspector
3. Add the **GrabFreeTransformer** component to the cube by clicking **Add Component** in the Inspector and searching for "GrabFreeTransformer"
4. In the **Grab Interaction** component, you'll see two optional fields:
   - **One Grab Transformer**: For single-hand interactions
   - **Two Grab Transformer**: For two-handed interactions
5. Drag the cube (with the GrabFreeTransformer component) into the **Two Grab Transformer** field

### Step 2: Configure Transformer Constraints
1. With the cube still selected, find the **GrabFreeTransformer** component in the Inspector
2. Expand the **Constraints** section to see the available options:

#### Position Constraints
- **X Position**: Controls whether the object can move along the X-axis
- **Y Position**: Controls whether the object can move along the Y-axis  
- **Z Position**: Controls whether the object can move along the Z-axis

#### Rotation Constraints
- **X Rotation**: Controls whether the object can rotate around the X-axis
- **Y Rotation**: Controls whether the object can rotate around the Y-axis
- **Z Rotation**: Controls whether the object can rotate around the Z-axis

#### Scale Constraints
- **X Scale**: Controls whether the object can scale along the X-axis
- **Y Scale**: Controls whether the object can scale along the Y-axis
- **Z Scale**: Controls whether the object can scale along the Z-axis

3. For scaling functionality, ensure **X Scale**, **Y Scale**, and **Z Scale** are all **enabled** (checked)
4. You can disable **Position** and **Rotation** constraints if you only want scaling behavior
5. Update the Min/Max values of each scaling axis to set minimum and maximum scaling constraints (0.1 to 3 is a good range)

### Step 3: Test Two-Handed Scaling
1. Run your Program on the headset.
2. Grab the cube with one hand
3. While holding with the first hand, grab the same cube with your other hand
4. Move your hands apart to scale the object larger
5. Move your hands closer together to scale the object smaller
6. The object should maintain its center point between your hands while scaling

### Step 4: Extension - Non-Uniform Scaling
1. Select the second **Grab Interaction** cube in your scene
2. Add the **NonUniformGrabFreeTransformer** script from the Scripts folder in Week 06 activites to this cube [NonUniformGrabFreeTransformer.cs](Scripts/NonUniformGrabFreeTransformer.cs)
3. Configure the NonUniformGrabFreeTransformer component:
   - **Uncheck Uniform Scaling** to allow different scaling on each axis
   - **Check Lock Rotation** to prevent the object from rotating while scaling
   - **Check Limit Scaling** to enable scale constraints
   - Set up scaling constraints as in Step 2 (X, Y, Z Scale enabled, Min/Max values set)
4. Test how non-uniform scaling differs from uniform scaling

## How It Works

### GrabFreeTransformer
The **GrabFreeTransformer** is a Meta SDK component that handles object transformation when grabbed by multiple interactors (hands/controllers). It automatically calculates the appropriate scale, rotation, and position based on the relative positions of the grabbing hands.

**Key Features:**
- **Multi-hand support**: Automatically detects when multiple hands are grabbing the same object
- **Constraint system**: Fine-grained control over which transformations are allowed
- **Smooth interpolation**: Provides smooth, natural-feeling object manipulation
- **Physics integration**: Works seamlessly with Unity's physics system

### NonUniformGrabFreeTransformer
The **NonUniformGrabFreeTransformer** is a Cusom transformer that extends the functionality of the built in GrabFreeTransformer.cs


## Troubleshooting

### Common Issues
1. **Object doesn't scale**: Check that Scale constraints are enabled in the GrabFreeTransformer and min/max values are not the same.
2. **Scaling feels unnatural**: Ensure both hands are properly grabbing the object
3. **Performance issues**: Consider reducing the complexity of objects being scaled

### Debug Tips
- Use the Scene view to see the grab points and transformation calculations
- Check the Console for any error messages related to the transformer
- Verify that both hands are within the grab range of the object

## Extension Activities

### Advanced Scaling
1. **Directional scaling**: Enable only X and Y scale constraints for 2D scaling
2. **Locked rotation**: Disable all rotation constraints for pure scaling behavior
3. **Position locking**: Lock position constraints to keep the object in place while scaling

### Custom Behaviors
 **Haptic feedback**: Integrate haptic feedback when scaling reaches certain thresholds


## Resources

- [Meta SDK Documentation - GrabFreeTransformer](https://developers.meta.com/horizon/reference/interaction/v78/class_oculus_interaction_grab_free_transformer)

