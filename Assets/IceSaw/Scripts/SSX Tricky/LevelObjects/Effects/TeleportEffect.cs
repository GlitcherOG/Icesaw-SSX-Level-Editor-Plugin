using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TeleportEffect : EffectBase
{
    public override int EffectType
    {
        get { return 24; }
    }
    
    public TrickyInstanceObject InstanceObject;
    int TeleportInstanceIndex;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        TeleportInstanceIndex = effect.TeleportInstanceIndex.Value;
    }

    public override void PostLoad(TrickyInstanceObject[] TempInstanceObjects, TrickyEffectHeader[] TempEffectHeader, TrickySplineObject[] TempListSplines, TrickyFunctionHeader[] TempFunctionList)
    {
        if (TempInstanceObjects.Length - 1 >= TeleportInstanceIndex && TeleportInstanceIndex != -1)
        {
            InstanceObject = TempInstanceObjects[TeleportInstanceIndex];
        }
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        if (InstanceObject != null)
        {
            NewEffect.TeleportInstanceIndex = InstanceObject.transform.GetSiblingIndex();
        }
        else
        {
            NewEffect.TeleportInstanceIndex = -1;
        }

        return NewEffect;
    }
}
