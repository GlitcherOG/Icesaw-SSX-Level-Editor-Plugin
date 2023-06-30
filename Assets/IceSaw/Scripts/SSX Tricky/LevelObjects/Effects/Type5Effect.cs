using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type5Effect : EffectBase
{
    public override int EffectType
    {
        get { return 5; }
    }

    public int Unknown1;
    public float Unknown2;
    public int Unknown3;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        Unknown1 = effect.type5.Value.U0;
        Unknown2 = effect.type5.Value.U1;
        Unknown3 = effect.type5.Value.U2;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewType9Effect = new SSFJsonHandler.Type5();

        NewType9Effect.U0 = Unknown1;
        NewType9Effect.U1 = Unknown2;
        NewType9Effect.U2 = Unknown3;

        NewEffect.type5 = NewType9Effect;

        return NewEffect;
    }
}
