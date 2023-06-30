using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadNodeEffect : EffectBase
{
    public override int EffectType
    {
        get { return 0; }
    }

    public override int SubEffectType
    {
        get { return 5; }
    }

    public int DeadNodeMode;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        DeadNodeMode = effect.type0.Value.DeadNodeMode.Value;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewType0Effect = new SSFJsonHandler.Type0();

        NewType0Effect.SubType = SubEffectType;
        NewType0Effect.DeadNodeMode = DeadNodeMode;

        NewEffect.type0 = NewType0Effect;

        return NewEffect;
    }
}
