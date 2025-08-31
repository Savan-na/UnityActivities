# Meta XR All-in-One SDK v78 — Building Blocks Guide (Unity)

This guide explains Meta’s **Building Blocks** (in **Meta XR All-in-One SDK v78**) 

---

## What are Building Blocks?

**Building Blocks** are **preconfigured sets of GameObjects and components** that you can drop into your Unity scene. They are designed to help you get started quickly with XR features. Each component inside a block has a specific use and function, and you should aim to understand what each does as you continue building your projects.

They save time by wiring up common systems like:

* Camera rigs
* Hand & controller tracking
* Grabbing, poking, ray pointing
* Locomotion & teleport
* Passthrough / MR
* Anchors, Haptics, Voice

You’ll find them in Unity under:

```
Meta → Tools → Building Blocks
```

**Reference:** [Meta XR All-in-One SDK v78 (Unity Asset Store)](https://assetstore.unity.com/packages/tools/integration/meta-xr-all-in-one-sdk-269657) — latest version **78.0.0** (Aug 30, 2025).

---

## The Essential Building Blocks

Here are the most useful blocks students should start with:

### 1. Camera Rig

* Creates the **OVRCameraRig**.
* Adds **OVRManager** with Quest-specific options (eye/face/body tracking, floor offset, etc.).
* Always add this first.
* [Unity XR Camera Rig](https://developers.meta.com/horizon/documentation/unity/unity-ovrcamerarig)

### 2. Hand Tracking & Controller Tracking

* Enable hands and/or Touch controllers.
* Adds **Interactors** (grab, poke, ray).
* [Interaction SDK Docs](https://developers.meta.com/horizon/documentation/unity/unity-isdk-interaction-sdk-overview)

### 3. Grab / Poke / Ray Interaction

* Lets you:

  * Grab objects
  * Poke pressable buttons
  * Use rays for distant pointing
* Uses **Interactors, Interactables, Transformers**.


### 4. Locomotion / Teleport

* Add teleport pads & smooth locomotion.
* Useful for moving around larger scenes.

### 5. Passthrough

* Lets you see your real room in VR.
* Provides room meshes & placement helpers.


### 6. Spatial Anchors

* Save and restore object positions in your space.
* Great for persistence between sessions.


### 7. Haptics

* Add vibration feedback with **haptic clips**.


### 8. Voice

* Add voice commands & intents.


---

## Key Concept: Interactors, Interactables, Transformers

When you add interaction-type blocks, you’ll see these components repeat:

* **Interactor** → Lives on the hand or controller (grabbing hand, poke finger, ray pointer).
* **Interactable** → Lives on the object (grabbable cube, pressable button).
* **Transformer** → Defines how objects behave when grabbed (move, rotate, scale, two-hand behavior).


---

## Example: Inside Two Common Building Blocks

### A) Controller Tracking Block

When you add the **Controller Tracking** building block, Unity sets up:

* **Controller Prefabs** (left and right hand models).
* **Tracked Pose Driver** (keeps the prefab in sync with the real controller position).
* **Interactors** on each controller:

  * **Grab Interactor** (for picking up objects).
  * **Ray Interactor** (for pointing and UI interaction).
  * **Poke Interactor** (for pressing buttons).
* **OVRInputModule** (to route controller input events into Unity’s EventSystem).

This block ensures that your Touch controllers are visible and fully functional in your scene.

### B) Grab Interaction Block

When you add the **Grab Interaction** building block, Unity sets up:

* A **Grabbable prefab** you can attach to objects.
* Components on the object:

  * **Rigidbody** (so it can move with physics).
  * **Grab Interactable** (makes it possible to pick up).
  * **Transformers**:

    * **GrabFreeTransformer** (move/rotate when held).
    * **TwoGrabFreeTransformer** (scale/rotate with two hands).
* Optional constraints like min/max scale or movement limits.

Together, this block shows the relationship between **Interactor (hand/controller)** and **Interactable (object)**, with Transformers deciding the behavior of the object while grabbed.

---

## FAQ

**Q: Do Building Blocks lock me in?**
A: No. They are just prefab setups. You can inspect & modify them.

**Q: Hands or controllers?**
A: Use both. Blocks support both; let users choose.

**Q: OpenXR or Oculus XR plugin?**
A: Use **OpenXR + Unity OpenXR: Meta** (current supported path).

---


## References

* [Meta XR All-in-One SDK v78 (Asset Store)](https://assetstore.unity.com/packages/tools/integration/meta-xr-all-in-one-sdk-269657)
* [Unity Meta SDK Docs](https://developers.meta.com/horizon/develop/unity)
