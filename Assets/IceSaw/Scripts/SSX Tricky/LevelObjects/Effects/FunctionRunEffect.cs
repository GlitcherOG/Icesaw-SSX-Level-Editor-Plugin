using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FunctionRunEffect : EffectBase
{
    public override int EffectType
    {
        get { return 21; }
    }

    public TrickyFunctionHeader FunctionObject;
    int FunctionIndex;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        FunctionIndex = effect.FunctionRunIndex.Value;
    }

    public override void PostLoad(TrickyInstanceObject[] TempInstanceObjects, TrickyEffectHeader[] TempEffectHeader, TrickySplineObject[] TempListSplines, TrickyFunctionHeader[] TempFunctionList)
    {
        if (TempFunctionList.Length - 1 >= FunctionIndex && FunctionIndex != -1)
        {
            FunctionObject = TempFunctionList[FunctionIndex];
        }
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        if(FunctionObject!=null)
        {
            NewEffect.FunctionRunIndex = TrickyLevelManager.Instance.dataManager.GetFunctionID(FunctionObject);
        }
        else
        {
            NewEffect.FunctionRunIndex = -1;
        }

        return NewEffect;
    }
}
