using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static SSXMultiTool.JsonFiles.Tricky.SSFJsonHandler;

public class InstanceRunEffect : EffectBase
{
    public override int EffectType
    {
        get { return 7; }
    }

    public TrickyInstanceObject InstanceObject;
    public TrickyEffectHeader EffectHeaderObject;

    int InstanceIndex;
    int EffectIndex;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        InstanceIndex = effect.Instance.Value.InstanceIndex;
        EffectIndex = effect.Instance.Value.EffectIndex;
    }

    public override void PostLoad(TrickyInstanceObject[] TempInstanceObjects, TrickyEffectHeader[] TempEffectHeader, TrickySplineObject[] TempListSplines, TrickyFunctionHeader[] TempFunctionList)
    {
        if (TempInstanceObjects.Length - 1 >= InstanceIndex && InstanceIndex != -1)
        {
            InstanceObject = TempInstanceObjects[InstanceIndex];
        }

        if (TempEffectHeader.Length - 1 >= EffectIndex && EffectIndex != -1)
        {
            EffectHeaderObject = TempEffectHeader[EffectIndex];
        }
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewInstanceEffect = new SSFJsonHandler.InstanceEffect();

        if(InstanceObject != null)
        {
            NewInstanceEffect.InstanceIndex = InstanceObject.transform.GetSiblingIndex();
        }    
        else
        {
            NewInstanceEffect.InstanceIndex = -1;
        }

        if (EffectHeaderObject != null)
        {
            NewInstanceEffect.EffectIndex = EffectHeaderObject.transform.GetSiblingIndex();
        }
        else
        {
            NewInstanceEffect.EffectIndex = -1;
        }

        NewEffect.Instance = NewInstanceEffect;

        return NewEffect;
    }

    [ContextMenu("Goto Instance")]
    public void GotoInstance()
    {
        var TempList = TrickyWorldManager.Instance.GetInstanceList();

        if (TempList.Length - 1 >= InstanceIndex)
        {
            Selection.activeObject = TempList[InstanceIndex].gameObject;
        }
    }

    [ContextMenu("Goto Effect")]
    public void GotoEffect()
    {
        var TempList = TrickyLogicManager.Instance.GetEffectObjects();

        if (TempList.Length - 1 >= EffectIndex)
        {
            Selection.activeObject = TempList[EffectIndex];
        }
    }
}
