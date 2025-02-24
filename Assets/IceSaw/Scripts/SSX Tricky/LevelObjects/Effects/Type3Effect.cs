using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type3Effect : EffectBase
{
    public override int EffectType
    {
        get { return 3; }
    }

    public int Unknown1;
    public float Unknown2;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        Unknown1 = effect.type3.Value.U0;
        Unknown2 = effect.type3.Value.U1;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewType3Effect = new SSFJsonHandler.Type3();

        NewType3Effect.U0 = Unknown1;
        NewType3Effect.U1 = Unknown2;

        NewEffect.type3 = NewType3Effect;

        return NewEffect;
    }
}
