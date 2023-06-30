using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Type0Sub20Effect : EffectBase
{
    public override int EffectType
    {
        get { return 0; }
    }

    public override int SubEffectType
    {
        get { return 20; }
    }

    public int Unknown1;
    public float Unknown2;
    public float Unknown3;
    public int Unknown4;
    public int Unknown5;
    public int Unknown6;
    public float Unknown7;
    public float Unknown8;
    public float Unknown9;
    public float Unknown10;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        Unknown1 = effect.type0.Value.type0Sub20.Value.U0;
        Unknown2 = effect.type0.Value.type0Sub20.Value.U1;
        Unknown3 = effect.type0.Value.type0Sub20.Value.U2;
        Unknown4 = effect.type0.Value.type0Sub20.Value.U3;
        Unknown5 = effect.type0.Value.type0Sub20.Value.U4;
        Unknown6 = effect.type0.Value.type0Sub20.Value.U5;
        Unknown7 = effect.type0.Value.type0Sub20.Value.U6;
        Unknown8 = effect.type0.Value.type0Sub20.Value.U7;
        Unknown9 = effect.type0.Value.type0Sub20.Value.U8;
        Unknown10 = effect.type0.Value.type0Sub20.Value.U9;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewType0Effect = new SSFJsonHandler.Type0();

        NewType0Effect.SubType = SubEffectType;

        var NewType0Sub0Effect = new SSFJsonHandler.Type0Sub20();

        NewType0Sub0Effect.U0 = Unknown1;
        NewType0Sub0Effect.U1 = Unknown2;
        NewType0Sub0Effect.U2 = Unknown3;
        NewType0Sub0Effect.U3 = Unknown4;
        NewType0Sub0Effect.U4 = Unknown5;
        NewType0Sub0Effect.U5 = Unknown6;
        NewType0Sub0Effect.U6 = Unknown7;
        NewType0Sub0Effect.U7 = Unknown8;
        NewType0Sub0Effect.U8 = Unknown9;
        NewType0Sub0Effect.U9 = Unknown10;

        NewType0Effect.type0Sub20 = NewType0Sub0Effect;

        NewEffect.type0 = NewType0Effect;

        return NewEffect;
    }
}
