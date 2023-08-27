# IceSaw SSX Level Editor Plugin 

## Requires
- SSX Multitool V0.2 Exported Level
- Recommended Unity Version 2021.3.25f1
- Newtonsoft Unity Package (com.unity.nuget.newtonsoft-json)

## Install Instructions
- Download the correct unity version
- Create a new project
- Go to the package manager, click the plus icon and then giturl inputting "com.unity.nuget.newtonsoft-json"
- Import the Icesaw Package
 
## Current Todo
 - Proper Spline Editing
 - Proper Patch Editing
 - Custom Inspectors for Buttons (Figured Out Now Add To All)
 - Proper Get Pos Index System
 - Live Character/Bone Reading
 - Clean up Particle Prefabs To much unneeded data
 - Fix Camera Rotations
 - Hash Generation When Saving if none present
 - Easy way to add models
 - add newtonsoft json auto get
 - Object warning for if its going over many ltg groups
 - bbox override in editor
 - if object is to big for grids in ltg make it so it will use origin point
 - Make clicking lightmap update lightmap mode
 - Fix Spline Undo
 
 - Settings page
 - Save Settings
 -	Save Images
 -	Save Models
 
 - Reload models Instead of making it remove and add all models. have it check for new images and update old models
 - Check to ensure no names are matching on change (? Cant do in run time only on saving)
 - Add option to remove unused models and textures
 - Options to convert unity model into instance/prefab
 - Add Model Exporting from Unity
 - Fix Mesh Loading (Its just waisting memory and will cause issues with saving back)
