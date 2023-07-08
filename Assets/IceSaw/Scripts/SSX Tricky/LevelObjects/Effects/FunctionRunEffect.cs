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

    public int FunctionID;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        FunctionID = effect.FunctionRunIndex.Value;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;
        NewEffect.FunctionRunIndex = FunctionID;

        return NewEffect;
    }

    [ContextMenu("Goto Function")]
    public void GotoFunction()
    {
        var TempList = LogicManager.Instance.GetFunctionObjects();

        if(TempList.Length-1>=FunctionID)
        {
            Selection.activeObject = TempList[FunctionID];
        }
    }
}
