# AugmentedVirtuality
Digital Representations of Real World Objects in VR Applications

### Table of contents 
- [1. Idea](#1-idea)
- [2. Methodology](#2-methodology)
  * [Object detection](#object-detection)
  * [Implementation](#implementation)
- [3. Virtual Object](#3-virtual-object)
- [4. Results](#4-results)

## 1. Idea 
While wearing a VR headset, one is blind to the real surroundings and may bump into something or have difficulty finding and interacting with real-world objects such as chairs or cups. Taking off the VR headset is a nuisance and destroys immersion.  
The idea of this work is to enable VR users to interact with real-world objects by putting similar virtual object in their place. To do this, the real-world object must first be detected by a computer vision model and then the virtual representation has to be placed, rotated and scaled so that it resembles the real-world object as much as possible.  

## 2. Methodology 
### Object detection 
To detect the real-world objects Google's [MediaPipe Objectron](https://google.github.io/mediapipe/solutions/objectron.html) will be used, which detects
objects in 2D images, estimates their poses and predicts 3D bounding boxes around the objects in real-time.  
![Example](https://google.github.io/mediapipe/images/mobile/objectron_chair_android_gpu.gif)
![Example2](https://google.github.io/mediapipe/images/mobile/objectron_cup_android_gpu.gif)  
The Objectron dataset consists of over 14k annotated video clips of common objects (chairs, cups, cameras and shoes), collected from a geo-diverse sample (covering ten countries across five continents). The model trained on this dataset creates 3D bounding boxes, which describe the object's position, orientation and dimensions. [Objectron Paper](https://arxiv.org/abs/2012.09988)  

### Implementation 
As a virtual reality game engine [Unity](https://unity.com/unity/features/vr) will be used. To implement MediaPipe in Unity the [MediaPipe Unity Plugin](https://github.com/homuler/MediapipeUnityPlugin) ports the MediaPipe API (C++) one by one to C# so that it can be called from Unity.  

## 3. Virtual Object
The virtual object [script](https://github.com/JHoster/AugmentedVirtuality/blob/main/Assets/VirtualObject.cs) positions, rotates and scales any virtual representation of the real-world object.

## 4. Results 
