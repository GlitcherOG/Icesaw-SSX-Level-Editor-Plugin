using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TrickyViewOptions
{
    [MenuItem("Ice Saw View/Show All")]
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
    }

    [MenuItem("Ice Saw View/Race Only")]
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

        //Run Effect
        var FunctionList = LogicManager.Instance.ssfJsonHandler.Functions;
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "RaceMode")
            {
                RunFunction(i, instanceObjects);
            }
        }

    }

    [MenuItem("Ice Saw View/Showoff Only")]
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

        //Run Effect

        var FunctionList = LogicManager.Instance.ssfJsonHandler.Functions;
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "ShowoffMode")
            {
                RunFunction(i, instanceObjects);
            }
        }
    }

    [MenuItem("Ice Saw View/Freeride Only")]
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

        //Run Effect

        var FunctionList = LogicManager.Instance.ssfJsonHandler.Functions;
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "FreerideMode")
            {
                RunFunction(i, instanceObjects);
            }
        }
    }

    [MenuItem("Ice Saw View/Functions/RaceMode")]
    public static void FunctionRunRace()
    {
        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        var FunctionList = LogicManager.Instance.ssfJsonHandler.Functions;
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "RaceMode")
            {
                RunFunction(i, instanceObjects);
            }
        }
    }
    [MenuItem("Ice Saw View/Functions/ShowOff")]
    public static void FunctionRunShowOff()
    {
        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        var FunctionList = LogicManager.Instance.ssfJsonHandler.Functions;
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "ShowoffMode")
            {
                RunFunction(i, instanceObjects);
            }
        }
    }
    [MenuItem("Ice Saw View/Functions/FreeRideMode")]
    public static void FunctionRunFreeRide()
    {
        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        var FunctionList = LogicManager.Instance.ssfJsonHandler.Functions;
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "FreerideMode")
            {
                RunFunction(i, instanceObjects);
            }
        }
    }

    [MenuItem("Ice Saw View/Functions/StartCountDown")]
    public static void FunctionRunCountDown()
    {
        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        var FunctionList = LogicManager.Instance.ssfJsonHandler.Functions;
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "StartCountDown")
            {
                RunFunction(i, instanceObjects);
            }
        }
    }

    [MenuItem("Ice Saw View/Functions/EndCountDown")]
    public static void FunctionRunEndCountDown()
    {
        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        var FunctionList = LogicManager.Instance.ssfJsonHandler.Functions;
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "EndCountDown")
            {
                RunFunction(i, instanceObjects);
            }
        }
    }

    [MenuItem("Ice Saw View/Functions/NoCountDown")]
    public static void FunctionRunNoCountDown()
    {
        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        var FunctionList = LogicManager.Instance.ssfJsonHandler.Functions;
        for (int i = 0; i < FunctionList.Count; i++)
        {
            if (FunctionList[i].FunctionName == "NoCountDown")
            {
                RunFunction(i, instanceObjects);
            }
        }
    }


    public static void RunFunction(int Position, InstanceObject[] InstanceList)
    {
        var Function = LogicManager.Instance.ssfJsonHandler.Functions[Position];

        for (int i = 0; i < Function.Effects.Count; i++)
        {
            if (Function.Effects[i].MainType==21)
            {
                RunFunction(Function.Effects[i].FunctionRunIndex.Value, InstanceList);
            }
            if(Function.Effects[i].MainType == 7)
            {
                RunEffectInstance(Function.Effects[i].Instance.Value.EffectIndex, Function.Effects[i].Instance.Value.InstanceIndex, InstanceList);
            }
            if (Function.Effects[i].MainType == 25)
            {
                RunEffectSpline(Function.Effects[i].Spline.Value.Effect, Function.Effects[i].Spline.Value.SplineIndex);
            }
        }
    }

    public static void RunEffectInstance(int Effect, int Instance, InstanceObject[] InstanceList)
    {
        var TempInstance = InstanceList[Instance];
        var EffectHeader = LogicManager.Instance.ssfJsonHandler.EffectHeaders[Effect];
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

    public static void RunEffectSpline(int Effect, int Spline)
    {
        //0 - off
        //1 - on
    }
}
