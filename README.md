# duality-samples
This repository contains some sample projects made for the 2D game engine [Duality](https://github.com/AdamsLair/duality).
The samples in this repository use Duality 2.0.
Contributions to this repository are welcome.

## Samples
Each sample contains a Visual Studio Project file that can be included and built in your Duality ProjectPlugins solution. The Content directory in each sample contains any resources required to use the sample, such as: Scenes, Sounds, Prefabs et cetera. The contents of the Content directory can be placed in a Duality environment to use the sample.

All of the samples are also available for installation via the Duality Package Manager.

Currently, this repository consists of the following samples:
  * SceneTransitions sample
  * TopDownMovement sample
  * Gravity sample
  
  ### SceneTransitions
  This sample demonstrates Duality's scene system. It shows the difference between simply switching another scene and disposing the current scene and then switching to another scene. It also shows off scene reloading and scene saving. You can also add an object to a random position on the screen.
  ### TopDownMovement
  This sample shows a basic Transform-based top-down movement solution in Duality. It has one scene where you can move a square around.
  ### Gravity
  This sample showcases a simple way to create objects with local gravity in Duality. It contains one sample scene where you can spawn objects to observe how it works. The main component of this sample is the Gravitator, which is what causes the gravity (either attraction or repulsion).
