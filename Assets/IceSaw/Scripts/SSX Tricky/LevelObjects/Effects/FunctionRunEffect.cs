using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
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
}
