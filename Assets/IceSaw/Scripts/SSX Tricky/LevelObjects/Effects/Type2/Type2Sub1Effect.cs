using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type2Sub1Effect : EffectBase
{
    public override int EffectType
    {
        get { return 2; }
    }

    public override int SubEffectType
    {
        get { return 1; }
    }

    public int Unknown1;
    public int Unknown2;
    public int Unknown3;
    public int Unknown4;
    public float Unknown5;
    public float Unknown6;
    public int Unknown7;
    public int Unknown8;
    public int Unknown9;
    public int Unknown10;
    public int Unknown11;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        Unknown1 = effect.type2.Value.type2Sub1.Value.U0;
        Unknown2 = effect.type2.Value.type2Sub1.Value.U1;
        Unknown3 = effect.type2.Value.type2Sub1.Value.U2;
        Unknown4 = effect.type2.Value.type2Sub1.Value.U3;
        Unknown5 = effect.type2.Value.type2Sub1.Value.U4;
        Unknown6 = effect.type2.Value.type2Sub0.Value.U5;
        Unknown7 = effect.type2.Value.type2Sub1.Value.U6;
        Unknown8 = effect.type2.Value.type2Sub1.Value.U7;
        Unknown9 = effect.type2.Value.type2Sub1.Value.U8;
        Unknown10 = effect.type2.Value.type2Sub1.Value.U9;
        Unknown11 = effect.type2.Value.type2Sub1.Value.U10;

    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewType0Effect = new SSFJsonHandler.Type2();

        NewType0Effect.SubType = SubEffectType;

        var NewType0Sub0Effect = new SSFJsonHandler.Type2Sub1();

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
        NewType0Sub0Effect.U10 = Unknown11;

        NewType0Effect.type2Sub1 = NewType0Sub0Effect;

        NewEffect.type2 = NewType0Effect;

        return NewEffect;
    }
}
