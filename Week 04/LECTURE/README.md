# Lecture Example: Hiking App Prototype

This folder contains a Unity project example from the lecture demonstrating a simple horizontal prototype of an XR version of a hiking app. The project builds out the scene from primitives and adds simple player movement with dynamic waypoints.

## Project Overview

The HikingExample project showcases:
- **Scene Construction**: Building environments from Unity primitives
- **Player Movement**: WASD controls with mouse look and jumping
- **Dynamic Waypoints**: Proximity-based waypoint visibility system
- **Material System**: Basic terrain and object materials
- **Prefab System**: Reusable tree objects

## Asset Contents

### Scripts (`Assets/Scripts/`)

#### [ProximityShowHide.cs](HikingExample/Assets/Scripts/ProximityShowHide.cs)
A proximity detection system that shows/hides waypoint objects based on player distance.

#### [SimpleWASDController.cs](HikingExample/Assets/Scripts/SimpleWASDController.cs)
A simple first-person character controller with jump.

### Textures (`Assets/Textures/`)

#### [grass.jpg](HikingExample/Assets/Textures/grass.jpg)
A high-resolution grass texture (2.1MB) used for terrain and ground surfaces. This texture provides realistic ground appearance and can be applied to materials for various terrain types.

