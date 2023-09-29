using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebounceEffect : EffectBase
{
    public override int EffectType
    {
        get { return 0; }
    }

    public override int SubEffectType
    {
        get { return 2; }
    }

    public int Debounce;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        Debounce = effect.type0.Value.Debounce.Value;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewType0Effect = new SSFJsonHandler.Type0();

        NewType0Effect.SubType = SubEffectType;
        NewType0Effect.Debounce = Debounce;

        NewEffect.type0 = NewType0Effect;

        return NewEffect;
    }
}
