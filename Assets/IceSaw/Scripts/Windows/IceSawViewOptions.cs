using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IceSawViewOptions
{
    [MenuItem("Ice Saw/View/Show All",false,10)]
    public static void ShowAll()
    {
        var TempDataManager = TrickyLevelManager.Instance.dataManager;
        TempDataManager.RefreshObjectList();

        //Grab Patches
        TrickyPatchObject[] patchObject = TempDataManager.trickyPatchObjects.ToArray();
        for (int i = 0; i < patchObject.Length; i++)
        {
            patchObject[i].gameObject.SetActive(true);
        }

        TrickyInstanceObject[] instanceObjects = TempDataManager.trickyInstances.ToArray();
        //Grab Instances
        for (int i = 0; i < instanceObjects.Length; i++)
        {
            instanceObjects[i].gameObject.SetActive(true);
        }

        TrickySplineObject[] splineObjects = TempDataManager.trickySplineObjects.ToArray();
        for (int i = 0; i < splineObjects.Length; i++)
        {
            splineObjects[i].gameObject.SetActive(true);
        }

        TempDataManager.trickyGeneralPaths.gameObject.SetActive(true);
        TempDataManager.trickyShowoffPaths.gameObject.SetActive(true);
    }

    [MenuItem("Ice Saw/View/Race Only", false, 10)]
    public static void RaceOnly()
    {
        var TempDataManager = TrickyLevelManager.Instance.dataManager;
        TempDataManager.RefreshObjectList();

        //Grab Patches
        TrickyPatchObject[] patchObject = TempDataManager.trickyPatchObjects.ToArray();
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

        TrickyInstanceObject[] instanceObjects = TempDataManager.trickyInstances.ToArray();
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

        TrickySplineObject[] splineObjects = TempDataManager.trickySplineObjects.ToArray();
        for (int i = 0; i < splineObjects.Length; i++)
        {
            splineObjects[i].gameObject.SetActive(true);
        }

        ////Run Effect
        //if(TrickyLogicManager.Instance==null)
        //{
        //    Debug.Log("IceSaw - NotNull");
        //}

        var FunctionList = TempDataManager.trickyFunctionHeaders.ToArray();
        for (int i = 0; i < FunctionList.Length; i++)
        {
            if (FunctionList[i].transform.name == "RaceMode")
            {
                RunFunction(FunctionList[i], TempDataManager);
            }
        }

        TempDataManager.trickyGeneralPaths.gameObject.SetActive(false);
        TempDataManager.trickyShowoffPaths.gameObject.SetActive(true);
    }

    [MenuItem("Ice Saw/View/Showoff Only", false, 10)]
    public static void ShowOffOnly()
    {
        var TempDataManager = TrickyLevelManager.Instance.dataManager;
        TempDataManager.RefreshObjectList();

        //Grab Patches
        TrickyPatchObject[] patchObject = TempDataManager.trickyPatchObjects.ToArray();
        for (int i = 0; i < patchObject.Length; i++)
        {
            patchObject[i].gameObject.SetActive(true);
        }

        TrickyInstanceObject[] instanceObjects = TempDataManager.trickyInstances.ToArray();
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

        TrickySplineObject[] splineObjects = TempDataManager.trickySplineObjects.ToArray();
        for (int i = 0; i < splineObjects.Length; i++)
        {
            splineObjects[i].gameObject.SetActive(true);
        }

        //Run Effect
        var FunctionList = TempDataManager.trickyFunctionHeaders.ToArray();
        for (int i = 0; i < FunctionList.Length; i++)
        {
            if (FunctionList[i].transform.name == "ShowoffMode")
            {
                RunFunction(FunctionList[i], TempDataManager);
            }
        }

        TempDataManager.trickyGeneralPaths.gameObject.SetActive(true);
        TempDataManager.trickyShowoffPaths.gameObject.SetActive(false);
    }

    [MenuItem("Ice Saw/View/Freeride Only", false, 10)]
    public static void FreerideOnly()
    {
        var TempDataManager = TrickyLevelManager.Instance.dataManager;
        TempDataManager.RefreshObjectList();

        //Grab Patches
        TrickyPatchObject[] patchObject = TempDataManager.trickyPatchObjects.ToArray();
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

        TrickyInstanceObject[] instanceObjects = TempDataManager.trickyInstances.ToArray();
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

        TrickySplineObject[] splineObjects = TempDataManager.trickySplineObjects.ToArray();
        for (int i = 0; i < splineObjects.Length; i++)
        {
            splineObjects[i].gameObject.SetActive(true);
        }

        //Run Effect

        var FunctionList = TempDataManager.trickyFunctionHeaders.ToArray();
        for (int i = 0; i < FunctionList.Length; i++)
        {
            if (FunctionList[i].transform.name == "FreerideMode")
            {
                RunFunction(FunctionList[i], TempDataManager);
            }
        }

        TempDataManager.trickyGeneralPaths.gameObject.SetActive(true);
        TempDataManager.trickyShowoffPaths.gameObject.SetActive(false);
    }

    [MenuItem("Ice Saw/View/Functions/RaceMode")]
    public static void FunctionRunRace()
    {
        var TempDataManager = TrickyLevelManager.Instance.dataManager;
        TempDataManager.RefreshObjectList();

        TrickyInstanceObject[] instanceObjects = TempDataManager.trickyInstances.ToArray();
        TrickySplineObject[] splineObjects = TempDataManager.trickySplineObjects.ToArray();
        var FunctionList = TempDataManager.trickyFunctionHeaders.ToArray();
        for (int i = 0; i < FunctionList.Length; i++)
        {
            if (FunctionList[i].transform.name == "RaceMode")
            {
                RunFunction(FunctionList[i], TempDataManager);
            }
        }
    }
    [MenuItem("Ice Saw/View/Functions/ShowOff")]
    public static void FunctionRunShowOff()
    {
        var TempDataManager = TrickyLevelManager.Instance.dataManager;
        TempDataManager.RefreshObjectList();

        TrickyInstanceObject[] instanceObjects = TempDataManager.trickyInstances.ToArray();
        TrickySplineObject[] splineObjects = TempDataManager.trickySplineObjects.ToArray();
        var FunctionList = TempDataManager.trickyFunctionHeaders.ToArray();
        for (int i = 0; i < FunctionList.Length; i++)
        {
            if (FunctionList[i].transform.name == "ShowoffMode")
            {
                RunFunction(FunctionList[i], TempDataManager);
            }
        }
    }
    [MenuItem("Ice Saw/View/Functions/FreeRideMode")]
    public static void FunctionRunFreeRide()
    {
        var TempDataManager = TrickyLevelManager.Instance.dataManager;
        TempDataManager.RefreshObjectList();

        TrickyInstanceObject[] instanceObjects = TempDataManager.trickyInstances.ToArray();
        TrickySplineObject[] splineObjects = TempDataManager.trickySplineObjects.ToArray();
        var FunctionList = TempDataManager.trickyFunctionHeaders.ToArray();
        for (int i = 0; i < FunctionList.Length; i++)
        {
            if (FunctionList[i].transform.name == "FreerideMode")
            {
                RunFunction(FunctionList[i], TempDataManager);
            }
        }
    }

    [MenuItem("Ice Saw/View/Functions/StartCountDown")]
    public static void FunctionRunCountDown()
    {
        var TempDataManager = TrickyLevelManager.Instance.dataManager;
        TempDataManager.RefreshObjectList();

        TrickyInstanceObject[] instanceObjects = TempDataManager.trickyInstances.ToArray();
        TrickySplineObject[] splineObjects = TempDataManager.trickySplineObjects.ToArray();
        var FunctionList = TempDataManager.trickyFunctionHeaders.ToArray();
        for (int i = 0; i < FunctionList.Length; i++)
        {
            if (FunctionList[i].transform.name == "StartCountDown")
            {
                RunFunction(FunctionList[i], TempDataManager);
            }
        }
    }

    [MenuItem("Ice Saw/View/Functions/EndCountDown")]
    public static void FunctionRunEndCountDown()
    {
        var TempDataManager = TrickyLevelManager.Instance.dataManager;
        TempDataManager.RefreshObjectList();

        TrickyInstanceObject[] instanceObjects = TempDataManager.trickyInstances.ToArray();
        TrickySplineObject[] splineObjects = TempDataManager.trickySplineObjects.ToArray();
        var FunctionList = TempDataManager.trickyFunctionHeaders.ToArray();
        for (int i = 0; i < FunctionList.Length; i++)
        {
            if (FunctionList[i].transform.name == "EndCountDown")
            {
                RunFunction(FunctionList[i], TempDataManager);
            }
        }
    }

    [MenuItem("Ice Saw/View/Functions/NoCountDown")]
    public static void FunctionRunNoCountDown()
    {
        var TempDataManager = TrickyLevelManager.Instance.dataManager;
        TempDataManager.RefreshObjectList();

        TrickyInstanceObject[] instanceObjects = TempDataManager.trickyInstances.ToArray();
        TrickySplineObject[] splineObjects = TempDataManager.trickySplineObjects.ToArray();
        var FunctionList = TempDataManager.trickyFunctionHeaders.ToArray();
        for (int i = 0; i < FunctionList.Length; i++)
        {
            if (FunctionList[i].transform.name == "NoCountDown")
            {
                RunFunction(FunctionList[i], TempDataManager);
            }
        }
    }


    public static void RunFunction(TrickyFunctionHeader trickyFunctionHeader, DataManager dataManager)
    {
        var Function = trickyFunctionHeader.GenerateFunction();

        for (int i = 0; i < Function.Effects.Count; i++)
        {
            if (Function.Effects[i].MainType==21)
            {
                RunFunction(trickyFunctionHeader.GetComponent<FunctionRunEffect>().FunctionObject, dataManager);
            }
            if(Function.Effects[i].MainType == 7)
            {
                RunEffectInstance(Function.Effects[i].Instance.Value.EffectIndex, Function.Effects[i].Instance.Value.InstanceIndex, dataManager);
            }
            if (Function.Effects[i].MainType == 25)
            {
                RunEffectSpline(Function.Effects[i].Spline.Value.Effect, Function.Effects[i].Spline.Value.SplineIndex, dataManager);
            }
        }
    }

    public static void RunEffectInstance(int Effect, int Instance, DataManager dataManager)
    {
        var TempInstance = dataManager.trickyInstances[Instance];
        var EffectHeader = dataManager.trickyEffectHeaders[Effect].GenerateEffectHeader();

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

    public static void RunEffectSpline(int Effect, int Spline, DataManager dataManager)
    {
        var TempSpline = dataManager.trickySplineObjects[Spline];
        var TempEffect = dataManager.trickyEffectHeaders[Effect].GenerateEffectHeader();

        if (Effect==0)
        {
            TempSpline.gameObject.SetActive(false);
        }
        else if(Effect == 1)
        {
            TempSpline.gameObject.SetActive(true);
        }
    }

    [MenuItem("Ice Saw/View/Toggle Lightmap", false, 200)]
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

            var TempInstanceList = OGWorldManager.Instance.GetInstanceList();

            for (int i = 0; i < TempInstanceList.Length; i++)
            {
                TempInstanceList[i].ToggleLightingMode(OGLevelManager.Instance.LightmapMode);
            }
        }
        else
        if (TrickyLevelManager.Instance != null)
        {
            var TempDataManager = TrickyLevelManager.Instance.dataManager;
            TempDataManager.RefreshObjectList();

            TrickyLevelManager.Instance.LightmapMode = !TrickyLevelManager.Instance.LightmapMode;

            var TempPatchList = TempDataManager.trickyPatchObjects.ToArray();

            for (int i = 0; i < TempPatchList.Length; i++)
            {
                TempPatchList[i].ToggleLightingMode(TrickyLevelManager.Instance.LightmapMode);
            }

            var TempInstanceList = TempDataManager.trickyInstances.ToArray();

            for (int i = 0; i < TempInstanceList.Length; i++)
            {
                TempInstanceList[i].ToggleLightingMode(TrickyLevelManager.Instance.LightmapMode);
            }
        }
        else
        if (SSX3WorldManager.Instance != null)
        {
            //var TempDataManager = SSX3LevelManager.Instance.dataManager;
            //TempDataManager.RefreshObjectList();

            SSX3WorldManager.Instance.LightmapMode = !SSX3WorldManager.Instance.LightmapMode;

            var TempPatchList = SSX3WorldManager.Instance.GetPatchList();

            for (int i = 0; i < TempPatchList.Length; i++)
            {
                TempPatchList[i].ToggleLightingMode(SSX3WorldManager.Instance.LightmapMode);
            }
        }
    }

    [MenuItem("Ice Saw/View/Toggle Instance Models", false, 200)]
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
        else if (TrickyLevelManager.Instance != null)
        {
            var TempDataManager = TrickyLevelManager.Instance.dataManager;
            TempDataManager.RefreshObjectList();

            TrickyLevelManager.Instance.ShowInstanceModels = !TrickyLevelManager.Instance.ShowInstanceModels;

            var TempList = TempDataManager.trickyInstances;

            for (int i = 0; i < TempList.Count; i++)
            {
                TempList[i].RefreshHiddenModels();
            }
        }
    }

    [MenuItem("Ice Saw/View/Toggle Collision Models", false, 200)]
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
        else if (TrickyLevelManager.Instance != null)
        {
            var TempDataManager = TrickyLevelManager.Instance.dataManager;
            TempDataManager.RefreshObjectList();

            TrickyLevelManager.Instance.ShowCollisionModels = !TrickyLevelManager.Instance.ShowCollisionModels;

            var TempList = TempDataManager.trickyInstances;

            for (int i = 0; i < TempList.Count; i++)
            {
                TempList[i].RefreshHiddenModels();
            }
        }
    }

    [MenuItem("Ice Saw/View/Toggle Invisable Instances", false, 200)]
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
        if (TrickyLevelManager.Instance != null)
        {
            var TempDataManager = TrickyLevelManager.Instance.dataManager;
            TempDataManager.RefreshObjectList();

            TrickyInstanceObject[] instanceObjects = TempDataManager.trickyInstances.ToArray();
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

    [MenuItem("Ice Saw/View/Get Scene Camera Cords")]
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
