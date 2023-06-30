using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type13Effect : EffectBase
{
    public override int EffectType
    {
        get { return 13; }
    }

    public float Unknown;
    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        Unknown = effect.type13.Value;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;
        NewEffect.type13 = Unknown;

        return NewEffect;
    }
}
