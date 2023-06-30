using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterEffect : EffectBase
{
    public override int EffectType
    {
        get { return 0; }
    }

    public override int SubEffectType
    {
        get { return 6; }
    }

    public int Count;
    public float Unknown1;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        Count = effect.type0.Value.Counter.Value.Count;
        Unknown1 = effect.type0.Value.Counter.Value.U1;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewType0Effect = new SSFJsonHandler.Type0();

        NewType0Effect.SubType = SubEffectType;

        var NewType0Sub0Effect = new SSFJsonHandler.CounterEffect();

        NewType0Sub0Effect.Count = Count;
        NewType0Sub0Effect.U1 = Unknown1;

        NewType0Effect.Counter = NewType0Sub0Effect;

        NewEffect.type0 = NewType0Effect;

        return NewEffect;
    }
}
