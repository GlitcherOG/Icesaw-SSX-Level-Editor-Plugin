using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type0Sub2Effect : EffectBase
{
    public override int EffectType
    {
        get { return 0; }
    }

    public override int SubEffectType
    {
        get { return 2; }
    }

    public int Unknown;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        Unknown = effect.type0.Value.type0Sub2.Value;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewType0Effect = new SSFJsonHandler.Type0();

        NewType0Effect.SubType = SubEffectType;
        NewType0Effect.type0Sub2 = Unknown;

        NewEffect.type0 = NewType0Effect;

        return NewEffect;
    }
}
