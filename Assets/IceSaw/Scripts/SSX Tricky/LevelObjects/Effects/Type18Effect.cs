using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type18Effect : EffectBase
{
    public override int EffectType
    {
        get { return 18; }
    }

    public float Unknown;
    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        Unknown = effect.type18.Value;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;
        NewEffect.type18 = Unknown;

        return NewEffect;
    }
}
