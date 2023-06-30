using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type0Sub14Effect : EffectBase
{
    public override int EffectType
    {
        get { return 0; }
    }

    public override int SubEffectType
    {
        get { return 14; }
    }

    public float Unknown1;
    public float Unknown2;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        Unknown1 = effect.type0.Value.type0Sub14.Value.U0;
        Unknown2 = effect.type0.Value.type0Sub14.Value.U1;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewType0Effect = new SSFJsonHandler.Type0();

        NewType0Effect.SubType = SubEffectType;

        var NewType0Sub0Effect = new SSFJsonHandler.Type0Sub14();

        NewType0Sub0Effect.U0 = Unknown1;
        NewType0Sub0Effect.U1 = Unknown2;

        NewType0Effect.type0Sub14 = NewType0Sub0Effect;

        NewEffect.type0 = NewType0Effect;

        return NewEffect;
    }
}
