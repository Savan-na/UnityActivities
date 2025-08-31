# Week 6: Advanced XR Interactions and Custom Transformers

Week 6 builds upon the XR fundamentals from Week 5, introducing advanced interaction systems including gesture recognition, two-handed object manipulation, and custom transformer development. This week focuses on creating sophisticated XR experiences using Meta's Building Blocks system and extending it with custom code.

## Learning Progression

1. **Building Blocks Theory** - Understand the fundamental concepts and architecture of Meta's XR system
2. **Advanced Setup** - Configure comprehensive XR projects with multiple interaction types
3. **Gesture Recognition** - Implement hand pose detection and gesture-based interactions
4. **Two-Handed Manipulation** - Create intuitive scaling and manipulation systems
5. **Custom Transformers** - Extend the Meta SDK with custom interaction behaviors

## Theory Foundation

**Before starting the activities, read [BuildingBlocks.md](BuildingBlocks.md) for a deeper understanding of the Building Blocks concept and architecture.**

This document explains:
- What Building Blocks are and how they work
- The essential Building Blocks for XR development
- Key concepts like Interactors, Interactables, and Transformers
- How to understand and modify Building Block components
- Best practices for XR development workflow

## Activities

All activities are located in the `Week 06/` directory:

- **[Activity 1](Activity%201%20-%20Basic%20Building%20Blocks%20Setup.md)** - Basic Building Blocks Setup
  - Complete XR project setup with Meta SDK
  - Core Building Blocks integration (Camera Rig, Interactions Rig)
  - Passthrough and basic interaction systems
  - Project configuration for both Desktop and Android platforms

- **[Activity 2](Activity%202%20-%20Thumbs%20Up%20Gesture%20Detection.md)** - Thumbs Up Gesture Detection
  - Hand pose recognition using Meta's ShapeRecognizerActiveState
  - Transform recognition for orientation detection
  - Gesture-based object interactions
  - Advanced Building Block component configuration

- **[Activity 3](Activity%203%20-%20Two-Handed%20Object%20Scaling.md)** - Two-Handed Object Scaling
  - Multi-hand object manipulation
  - GrabFreeTransformer configuration and constraints
  - Uniform and non-uniform scaling systems
  - Transformer constraint management

- **[Activity 4](Activity%204%20-%20Creating%20a%20Custom%20Transformer.md)** - Creating a Custom Transformer
  - Custom transformer script development
  - ITransformer interface implementation
  - Visual effects integration
  - Custom interaction behavior creation

## C# Scripts

The following Unity-compatible C# scripts are included in the `Scripts/` directory:

- **[CustomTransformer.cs](Scripts/CustomTransformer.cs)** - Base template for custom transformer development
- **[MaterialSwapper.cs](Scripts/MaterialSwapper.cs)** - Utility for dynamic material changes
- **[NonUniformGrabFreeTransformer.cs](Scripts/NonUniformGrabFreeTransformer.cs)** - Advanced scaling transformer with non-uniform capabilities

## Key Technologies

### Meta XR Building Blocks
- **Camera Rig** - Essential XR camera system with hand tracking
- **Interactions Rig** - Comprehensive interaction system for hands and controllers
- **Grab Interaction** - Pre-configured object grabbing and manipulation
- **Shape Recognition** - Hand pose detection and gesture recognition
- **Transform Recognition** - Hand orientation and movement detection

### Advanced XR Concepts
- **Multi-hand Interactions** - Simultaneous hand tracking and coordination
- **Gesture Recognition** - Real-time hand pose analysis and response
- **Custom Transformers** - Extensible interaction behavior system
- **Constraint Management** - Fine-grained control over object manipulation
