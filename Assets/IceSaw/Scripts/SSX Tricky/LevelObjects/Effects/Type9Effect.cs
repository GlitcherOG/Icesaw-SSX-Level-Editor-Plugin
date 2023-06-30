using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type9Effect : EffectBase
{
    public override int EffectType
    {
        get { return 9; }
    }

    public int Unknown1;
    public float Unknown2;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        Unknown1 = effect.type9.Value.U0;
        Unknown2 = effect.type9.Value.U1;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewType9Effect = new SSFJsonHandler.Type9();

        NewType9Effect.U0 = Unknown1;
        NewType9Effect.U1 = Unknown2;

        NewEffect.type9 = NewType9Effect;

        return NewEffect;
    }
}
