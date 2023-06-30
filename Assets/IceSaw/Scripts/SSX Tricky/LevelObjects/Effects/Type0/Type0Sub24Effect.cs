using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type0Sub24Effect : EffectBase
{
    public override int EffectType
    {
        get { return 0; }
    }

    public override int SubEffectType
    {
        get { return 24; }
    }

    public int Unknown1;
    public int Unknown2;
    public float Unknown3;
    public float Unknown4;
    public int Unknown5;
    public int Unknown6;
    public int Unknown7;
    public float Unknown8;
    public float Unknown9;
    public float Unknown10;
    public float Unknown11;
    public float Unknown12;
    public float Unknown13;
    public float Unknown14;
    public float Unknown15;
    public float Unknown16;
    public float Unknown17;
    public float Unknown18;
    public float Unknown19;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        Unknown1 = effect.type0.Value.type0Sub24.Value.U0;
        Unknown2 = effect.type0.Value.type0Sub24.Value.U1;
        Unknown3 = effect.type0.Value.type0Sub24.Value.U2;
        Unknown4 = effect.type0.Value.type0Sub24.Value.U3;
        Unknown5 = effect.type0.Value.type0Sub24.Value.U4;
        Unknown6 = effect.type0.Value.type0Sub24.Value.U5;
        Unknown7 = effect.type0.Value.type0Sub24.Value.U6;
        Unknown8 = effect.type0.Value.type0Sub24.Value.U7;
        Unknown9 = effect.type0.Value.type0Sub24.Value.U8;
        Unknown10 = effect.type0.Value.type0Sub24.Value.U9;
        Unknown11 = effect.type0.Value.type0Sub24.Value.U10;
        Unknown12 = effect.type0.Value.type0Sub24.Value.U11;
        Unknown13 = effect.type0.Value.type0Sub24.Value.U12;
        Unknown14 = effect.type0.Value.type0Sub24.Value.U13;
        Unknown15 = effect.type0.Value.type0Sub24.Value.U14;
        Unknown16 = effect.type0.Value.type0Sub24.Value.U15;
        Unknown17 = effect.type0.Value.type0Sub24.Value.U16;
        Unknown18 = effect.type0.Value.type0Sub24.Value.U17;
        Unknown19 = effect.type0.Value.type0Sub24.Value.U18;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewType0Effect = new SSFJsonHandler.Type0();

        NewType0Effect.SubType = SubEffectType;

        var NewType0Sub0Effect = new SSFJsonHandler.Type0Sub24();

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
        NewType0Sub0Effect.U11 = Unknown12;
        NewType0Sub0Effect.U12 = Unknown13;
        NewType0Sub0Effect.U13 = Unknown14;
        NewType0Sub0Effect.U14 = Unknown15;
        NewType0Sub0Effect.U15 = Unknown16;
        NewType0Sub0Effect.U16 = Unknown17;
        NewType0Sub0Effect.U17 = Unknown18;
        NewType0Sub0Effect.U18 = Unknown19;

        NewType0Effect.type0Sub24 = NewType0Sub0Effect;

        NewEffect.type0 = NewType0Effect;

        return NewEffect;
    }
}
