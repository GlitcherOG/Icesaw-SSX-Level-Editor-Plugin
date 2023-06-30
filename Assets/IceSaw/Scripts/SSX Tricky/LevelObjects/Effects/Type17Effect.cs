using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type17Effect : EffectBase
{
    public override int EffectType
    {
        get { return 17; }
    }

    public float Unknown;
    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        Unknown = effect.type17.Value;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;
        NewEffect.type17 = Unknown;

        return NewEffect;
    }
}
