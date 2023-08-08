using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TrickyViewOptions
{
    [MenuItem("Ice Saw View/Show All",false,10)]
    public static void ShowAll()
    {
        //Grab Patches
        PatchObject[] patchObject = WorldManager.Instance.GetPatchList();
        for (int i = 0; i < patchObject.Length; i++)
        {
            patchObject[i].gameObject.SetActive(true);
        }

        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        //Grab Instances
        for (int i = 0; i < instanceObjects.Length; i++)
        {
            instanceObjects[i].gameObject.SetActive(true);
        }

        SplineObject[] splineObjects = WorldManager.Instance.GetSplineList();
        for (int i = 0; i < splineObjects.Length; i++)
        {
            splineObjects[i].gameObject.SetActive(true);
        }

        PathFileManager.Instance.AIPHolder.SetActive(true);
        PathFileManager.Instance.SOPHolder.SetActive(true);
    }

    [MenuItem("Ice Saw View/Race Only", false, 10)]
    public static void RaceOnly()
    {
        //Grab Patches
        PatchObject[] patchObject = WorldManager.Instance.GetPatchList();
        for (int i = 0; i < patchObject.Length; i++)
        {
            if (patchObject[i].TrickOnlyPatch)
            {
                patchObject[i].gameObject.SetActive(false);
            }
            else
            {
                patchObject[i].gameObject.SetActive(true);
            }
        }

        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        //Grab Instances
        for (int i = 0; i < instanceObjects.Length; i++)
        {
            if (instanceObjects[i].LTGState == 2)
            {
                instanceObjects[i].gameObject.SetActive(false);
            }
            else
            {
                instanceObjects[i].gameObject.SetActive(true);
            }
        }

        SplineObject[] splineObjects = WorldManager.Instance.GetSplineList();
        for (int i = 0; i < splineObjects.Length; i++)
        {
            splineObjects[i].gameObject.SetActive(true);
        }

        //Run Effect
        if(LogicManager.Instance==null)
        {
            Debug.Log("NotNull");
        }

        var FunctionList = LogicManager.Instance.GetFunctionList();
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "RaceMode")
            {
                RunFunction(i, instanceObjects, splineObjects);
            }
        }

        PathFileManager.Instance.SOPHolder.SetActive(false);
        PathFileManager.Instance.AIPHolder.SetActive(true);
    }

    [MenuItem("Ice Saw View/Showoff Only", false, 10)]
    public static void ShowOffOnly()
    {
        //Grab Patches
        PatchObject[] patchObject = WorldManager.Instance.GetPatchList();
        for (int i = 0; i < patchObject.Length; i++)
        {
            patchObject[i].gameObject.SetActive(true);
        }

        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        //Grab Instances
        for (int i = 0; i < instanceObjects.Length; i++)
        {
            if (instanceObjects[i].LTGState == 1)
            {
                instanceObjects[i].gameObject.SetActive(false);
            }
            else
            {
                instanceObjects[i].gameObject.SetActive(true);
            }
        }

        SplineObject[] splineObjects = WorldManager.Instance.GetSplineList();
        for (int i = 0; i < splineObjects.Length; i++)
        {
            splineObjects[i].gameObject.SetActive(true);
        }

        //Run Effect
        var FunctionList = LogicManager.Instance.GetFunctionList();
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "ShowoffMode")
            {
                RunFunction(i, instanceObjects, splineObjects);
            }
        }

        PathFileManager.Instance.SOPHolder.SetActive(true);
        PathFileManager.Instance.AIPHolder.SetActive(false);
    }

    [MenuItem("Ice Saw View/Freeride Only", false, 10)]
    public static void FreerideOnly()
    {
        //Grab Patches
        PatchObject[] patchObject = WorldManager.Instance.GetPatchList();
        for (int i = 0; i < patchObject.Length; i++)
        {
            if (patchObject[i].TrickOnlyPatch)
            {
                patchObject[i].gameObject.SetActive(false);
            }
            else
            {
                patchObject[i].gameObject.SetActive(true);
            }
        }

        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        //Grab Instances
        for (int i = 0; i < instanceObjects.Length; i++)
        {
            if (instanceObjects[i].LTGState == 2)
            {
                instanceObjects[i].gameObject.SetActive(false);
            }
            else
            {
                instanceObjects[i].gameObject.SetActive(true);
            }
        }

        SplineObject[] splineObjects = WorldManager.Instance.GetSplineList();
        for (int i = 0; i < splineObjects.Length; i++)
        {
            splineObjects[i].gameObject.SetActive(true);
        }

        //Run Effect

        var FunctionList = LogicManager.Instance.GetFunctionList();
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "FreerideMode")
            {
                RunFunction(i, instanceObjects, splineObjects);
            }
        }

        PathFileManager.Instance.AIPHolder.SetActive(true);
        PathFileManager.Instance.SOPHolder.SetActive(false);
    }

    [MenuItem("Ice Saw View/Functions/RaceMode")]
    public static void FunctionRunRace()
    {
        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        SplineObject[] splineObjects = WorldManager.Instance.GetSplineList();
        var FunctionList = LogicManager.Instance.GetFunctionList();
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "RaceMode")
            {
                RunFunction(i, instanceObjects, splineObjects);
            }
        }
    }
    [MenuItem("Ice Saw View/Functions/ShowOff")]
    public static void FunctionRunShowOff()
    {
        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        SplineObject[] splineObjects = WorldManager.Instance.GetSplineList();
        var FunctionList = LogicManager.Instance.GetFunctionList();
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "ShowoffMode")
            {
                RunFunction(i, instanceObjects, splineObjects);
            }
        }
    }
    [MenuItem("Ice Saw View/Functions/FreeRideMode")]
    public static void FunctionRunFreeRide()
    {
        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        SplineObject[] splineObjects = WorldManager.Instance.GetSplineList();
        var FunctionList = LogicManager.Instance.GetFunctionList();
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "FreerideMode")
            {
                RunFunction(i, instanceObjects, splineObjects);
            }
        }
    }

    [MenuItem("Ice Saw View/Functions/StartCountDown")]
    public static void FunctionRunCountDown()
    {
        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        SplineObject[] splineObjects = WorldManager.Instance.GetSplineList();
        var FunctionList = LogicManager.Instance.GetFunctionList();
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "StartCountDown")
            {
                RunFunction(i, instanceObjects, splineObjects);
            }
        }
    }

    [MenuItem("Ice Saw View/Functions/EndCountDown")]
    public static void FunctionRunEndCountDown()
    {
        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        SplineObject[] splineObjects = WorldManager.Instance.GetSplineList();
        var FunctionList = LogicManager.Instance.GetFunctionList();
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "EndCountDown")
            {
                RunFunction(i, instanceObjects, splineObjects);
            }
        }
    }

    [MenuItem("Ice Saw View/Functions/NoCountDown")]
    public static void FunctionRunNoCountDown()
    {
        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        SplineObject[] splineObjects = WorldManager.Instance.GetSplineList();
        var FunctionList = LogicManager.Instance.GetFunctionList();
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "NoCountDown")
            {
                RunFunction(i, instanceObjects, splineObjects);
            }
        }
    }


    public static void RunFunction(int Position, InstanceObject[] InstanceList, SplineObject[] splineObjects)
    {
        var Function = LogicManager.Instance.GetFunctionList()[Position];

        for (int i = 0; i < Function.Effects.Count; i++)
        {
            if (Function.Effects[i].MainType==21)
            {
                RunFunction(Function.Effects[i].FunctionRunIndex.Value, InstanceList, splineObjects);
            }
            if(Function.Effects[i].MainType == 7)
            {
                RunEffectInstance(Function.Effects[i].Instance.Value.EffectIndex, Function.Effects[i].Instance.Value.InstanceIndex, InstanceList);
            }
            if (Function.Effects[i].MainType == 25)
            {
                RunEffectSpline(Function.Effects[i].Spline.Value.Effect, Function.Effects[i].Spline.Value.SplineIndex, splineObjects);
            }
        }
    }

    public static void RunEffectInstance(int Effect, int Instance, InstanceObject[] InstanceList)
    {
        var TempInstance = InstanceList[Instance];
        var EffectHeader = LogicManager.Instance.GetEffectHeadersList()[Effect];
        for (int i = 0; i < EffectHeader.Effects.Count; i++)
        {
            if (EffectHeader.Effects[i].MainType==0)
            {
                if(EffectHeader.Effects[i].type0.Value.SubType==5)
                {
                    TempInstance.gameObject.SetActive(false);
                }
            }
        }
    }

    public static void RunEffectSpline(int Effect, int Spline, SplineObject[] splineObjects)
    {
        if(Effect==0)
        {
            splineObjects[Spline].gameObject.SetActive(false);
        }
        else if(Effect == 1)
        {
            splineObjects[Spline].gameObject.SetActive(true);
        }
    }

    [MenuItem("Ice Saw View/Toggle Lightmap", false, 200)]
    public static void LightmapToggle()
    {
        LevelManager.Instance.LightmapMode = !LevelManager.Instance.LightmapMode;

        var TempPatchList = WorldManager.Instance.GetPatchList();

        for (int i = 0; i < TempPatchList.Length; i++)
        {
            TempPatchList[i].ToggleLightingMode(LevelManager.Instance.LightmapMode);
        }
    }

    [MenuItem("Ice Saw View/Toggle Instance Models", false, 200)]
    public static void TogglePrefabModels()
    {
        WorldManager.Instance.ShowInstanceModels = !WorldManager.Instance.ShowInstanceModels;

        var TempList = WorldManager.Instance.GetInstanceList();

        for (int i = 0; i < TempList.Length; i++)
        {
            TempList[i].RefreshHiddenModels();
        }
    }

    [MenuItem("Ice Saw View/Toggle Collision Models", false, 200)]
    public static void ToggleCollisionModels()
    {
        WorldManager.Instance.ShowCollisionModels = !WorldManager.Instance.ShowCollisionModels;

        var TempList = WorldManager.Instance.GetInstanceList();

        for (int i = 0; i < TempList.Length; i++)
        {
            TempList[i].RefreshHiddenModels();
        }
    }

    [MenuItem("Ice Saw View/Toggle Invisable Instances", false, 200)]
    public static void VisablityToggle()
    {
        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        bool Active = false;
        bool Test = false;

        for (int i = 0; i < instanceObjects.Length; i++)
        {
            if (!instanceObjects[i].Visable)
            {
                if(Test==false)
                {
                    Test = true;
                    Active = !instanceObjects[i].gameObject.activeInHierarchy;
                }

                instanceObjects[i].gameObject.SetActive(Active);
            }
        }
    }
}
