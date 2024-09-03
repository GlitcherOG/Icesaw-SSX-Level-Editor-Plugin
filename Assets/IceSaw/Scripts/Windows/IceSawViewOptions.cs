using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IceSawViewOptions
{
    [MenuItem("Ice Saw View/Show All",false,10)]
    public static void ShowAll()
    {
        //Grab Patches
        TrickyPatchObject[] patchObject = TrickyWorldManager.Instance.GetPatchList();
        for (int i = 0; i < patchObject.Length; i++)
        {
            patchObject[i].gameObject.SetActive(true);
        }

        TrickyInstanceObject[] instanceObjects = TrickyWorldManager.Instance.GetInstanceList();
        //Grab Instances
        for (int i = 0; i < instanceObjects.Length; i++)
        {
            instanceObjects[i].gameObject.SetActive(true);
        }

        TrickySplineObject[] splineObjects = TrickyWorldManager.Instance.GetSplineList();
        for (int i = 0; i < splineObjects.Length; i++)
        {
            splineObjects[i].gameObject.SetActive(true);
        }

        TrickyPathFileManager.Instance.AIPHolder.SetActive(true);
        TrickyPathFileManager.Instance.SOPHolder.SetActive(true);
    }

    [MenuItem("Ice Saw View/Race Only", false, 10)]
    public static void RaceOnly()
    {
        //Grab Patches
        TrickyPatchObject[] patchObject = TrickyWorldManager.Instance.GetPatchList();
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

        TrickyInstanceObject[] instanceObjects = TrickyWorldManager.Instance.GetInstanceList();
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

        TrickySplineObject[] splineObjects = TrickyWorldManager.Instance.GetSplineList();
        for (int i = 0; i < splineObjects.Length; i++)
        {
            splineObjects[i].gameObject.SetActive(true);
        }

        //Run Effect
        if(LogicManager.Instance==null)
        {
            Debug.Log("IceSaw - NotNull");
        }

        var FunctionList = LogicManager.Instance.GetFunctionList();
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "RaceMode")
            {
                RunFunction(i, instanceObjects, splineObjects);
            }
        }

        TrickyPathFileManager.Instance.SOPHolder.SetActive(false);
        TrickyPathFileManager.Instance.AIPHolder.SetActive(true);
    }

    [MenuItem("Ice Saw View/Showoff Only", false, 10)]
    public static void ShowOffOnly()
    {
        //Grab Patches
        TrickyPatchObject[] patchObject = TrickyWorldManager.Instance.GetPatchList();
        for (int i = 0; i < patchObject.Length; i++)
        {
            patchObject[i].gameObject.SetActive(true);
        }

        TrickyInstanceObject[] instanceObjects = TrickyWorldManager.Instance.GetInstanceList();
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

        TrickySplineObject[] splineObjects = TrickyWorldManager.Instance.GetSplineList();
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

        TrickyPathFileManager.Instance.SOPHolder.SetActive(true);
        TrickyPathFileManager.Instance.AIPHolder.SetActive(false);
    }

    [MenuItem("Ice Saw View/Freeride Only", false, 10)]
    public static void FreerideOnly()
    {
        //Grab Patches
        TrickyPatchObject[] patchObject = TrickyWorldManager.Instance.GetPatchList();
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

        TrickyInstanceObject[] instanceObjects = TrickyWorldManager.Instance.GetInstanceList();
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

        TrickySplineObject[] splineObjects = TrickyWorldManager.Instance.GetSplineList();
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

        TrickyPathFileManager.Instance.AIPHolder.SetActive(true);
        TrickyPathFileManager.Instance.SOPHolder.SetActive(false);
    }

    [MenuItem("Ice Saw View/Functions/RaceMode")]
    public static void FunctionRunRace()
    {
        TrickyInstanceObject[] instanceObjects = TrickyWorldManager.Instance.GetInstanceList();
        TrickySplineObject[] splineObjects = TrickyWorldManager.Instance.GetSplineList();
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
        TrickyInstanceObject[] instanceObjects = TrickyWorldManager.Instance.GetInstanceList();
        TrickySplineObject[] splineObjects = TrickyWorldManager.Instance.GetSplineList();
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
        TrickyInstanceObject[] instanceObjects = TrickyWorldManager.Instance.GetInstanceList();
        TrickySplineObject[] splineObjects = TrickyWorldManager.Instance.GetSplineList();
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
        TrickyInstanceObject[] instanceObjects = TrickyWorldManager.Instance.GetInstanceList();
        TrickySplineObject[] splineObjects = TrickyWorldManager.Instance.GetSplineList();
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
        TrickyInstanceObject[] instanceObjects = TrickyWorldManager.Instance.GetInstanceList();
        TrickySplineObject[] splineObjects = TrickyWorldManager.Instance.GetSplineList();
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
        TrickyInstanceObject[] instanceObjects = TrickyWorldManager.Instance.GetInstanceList();
        TrickySplineObject[] splineObjects = TrickyWorldManager.Instance.GetSplineList();
        var FunctionList = LogicManager.Instance.GetFunctionList();
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "NoCountDown")
            {
                RunFunction(i, instanceObjects, splineObjects);
            }
        }
    }


    public static void RunFunction(int Position, TrickyInstanceObject[] InstanceList, TrickySplineObject[] splineObjects)
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

    public static void RunEffectInstance(int Effect, int Instance, TrickyInstanceObject[] InstanceList)
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

    public static void RunEffectSpline(int Effect, int Spline, TrickySplineObject[] splineObjects)
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
        if(OGLevelManager.Instance !=null)
        {
            OGLevelManager.Instance.LightmapMode = !OGLevelManager.Instance.LightmapMode;

            var TempPatchList = OGWorldManager.Instance.GetPatchList();

            for (int i = 0; i < TempPatchList.Length; i++)
            {
                TempPatchList[i].ToggleLightingMode(OGLevelManager.Instance.LightmapMode);
            }
        }
        else
        if (TrickyLevelManager.Instance != null)
        {
            TrickyLevelManager.Instance.LightmapMode = !TrickyLevelManager.Instance.LightmapMode;

            var TempPatchList = TrickyWorldManager.Instance.GetPatchList();

            for (int i = 0; i < TempPatchList.Length; i++)
            {
                TempPatchList[i].ToggleLightingMode(TrickyLevelManager.Instance.LightmapMode);
            }

            var TempInstanceList = TrickyWorldManager.Instance.GetInstanceList();

            for (int i = 0; i < TempInstanceList.Length; i++)
            {
                TempInstanceList[i].ToggleLightingMode(TrickyLevelManager.Instance.LightmapMode);
            }
        }
    }

    [MenuItem("Ice Saw View/Toggle Instance Models", false, 200)]
    public static void TogglePrefabModels()
    {
        if(OGWorldManager.Instance!=null)
        {
            OGWorldManager.Instance.ShowInstanceModels = !OGWorldManager.Instance.ShowInstanceModels;

            var TempList = OGWorldManager.Instance.GetInstanceList();

            for (int i = 0; i < TempList.Length; i++)
            {
                TempList[i].RefreshHiddenModels();
            }
        }
        else if (TrickyWorldManager.Instance != null)
        {
            TrickyWorldManager.Instance.ShowInstanceModels = !TrickyWorldManager.Instance.ShowInstanceModels;

            var TempList = TrickyWorldManager.Instance.GetInstanceList();

            for (int i = 0; i < TempList.Length; i++)
            {
                TempList[i].RefreshHiddenModels();
            }
        }
    }

    [MenuItem("Ice Saw View/Toggle Collision Models", false, 200)]
    public static void ToggleCollisionModels()
    {
        if(OGWorldManager.Instance != null)
        {
            OGWorldManager.Instance.ShowCollisionModels = !OGWorldManager.Instance.ShowCollisionModels;

            var TempList = OGWorldManager.Instance.GetInstanceList();

            for (int i = 0; i < TempList.Length; i++)
            {
                TempList[i].RefreshHiddenModels();
            }
        }
        else if (TrickyWorldManager.Instance != null)
        {
            TrickyWorldManager.Instance.ShowCollisionModels = !TrickyWorldManager.Instance.ShowCollisionModels;

            var TempList = TrickyWorldManager.Instance.GetInstanceList();

            for (int i = 0; i < TempList.Length; i++)
            {
                TempList[i].RefreshHiddenModels();
            }
        }
    }

    [MenuItem("Ice Saw View/Toggle Invisable Instances", false, 200)]
    public static void VisablityToggle()
    {
        if (OGLevelManager.Instance != null)
        {
            OGInstanceObject[] instanceObjects = OGWorldManager.Instance.GetInstanceList();
            bool Active = false;
            bool Test = false;

            for (int i = 0; i < instanceObjects.Length; i++)
            {
                if (!instanceObjects[i].Visable)
                {
                    if (Test == false)
                    {
                        Test = true;
                        Active = !instanceObjects[i].gameObject.activeInHierarchy;
                    }

                    instanceObjects[i].gameObject.SetActive(Active);
                }
            }
        }
        else
        if (TrickyWorldManager.Instance != null)
        {
            TrickyInstanceObject[] instanceObjects = TrickyWorldManager.Instance.GetInstanceList();
            bool Active = false;
            bool Test = false;

            for (int i = 0; i < instanceObjects.Length; i++)
            {
                if (!instanceObjects[i].Visable)
                {
                    if (Test == false)
                    {
                        Test = true;
                        Active = !instanceObjects[i].gameObject.activeInHierarchy;
                    }

                    instanceObjects[i].gameObject.SetActive(Active);
                }
            }
        }
    }

    [MenuItem("Ice Saw View/Get Scene Camera Cords")]
    public static void GetSceneCameraCords()
    {
        if (OGLevelManager.Instance != null)
        {
            var List = SceneView.GetAllSceneCameras();
            for (int i = 0; i < List.Length; i++)
            {
                Debug.Log("Icesaw - Scene Camera " + i + "\nUnity Cords:" + List[i].transform.position + " Tricky Cords:" + OGLevelManager.Instance.transform.InverseTransformPoint(List[i].transform.position));
            }
        }
        else
        if (TrickyLevelManager.Instance!=null)
        {
            var List = SceneView.GetAllSceneCameras();
            for (int i = 0; i < List.Length; i++)
            {
                Debug.Log("Icesaw - Scene Camera " + i + "\nUnity Cords:" + List[i].transform.position + " Tricky Cords:" + TrickyLevelManager.Instance.transform.InverseTransformPoint(List[i].transform.position));
            }
        }
    }
}
