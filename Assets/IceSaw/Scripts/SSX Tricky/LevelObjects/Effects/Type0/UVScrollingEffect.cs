using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVScrollingEffect : EffectBase
{
    public override int EffectType
    {
        get { return 0; }
    }

    public override int SubEffectType
    {
        get { return 10; }
    }

    public int Unknown1;
    public float Unknown2;
    public float Unknown3;
    public float Unknown4;
    public float Unknown5;
    public int Unknown6;


    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        Unknown1 = effect.type0.Value.UVScroll.Value.U0;
        Unknown2 = effect.type0.Value.UVScroll.Value.U1;
        Unknown3 = effect.type0.Value.UVScroll.Value.U2;
        Unknown4 = effect.type0.Value.UVScroll.Value.U3;
        Unknown5 = effect.type0.Value.UVScroll.Value.U4;
        Unknown6 = effect.type0.Value.UVScroll.Value.U5;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewType0Effect = new SSFJsonHandler.Type0();

        NewType0Effect.SubType = SubEffectType;

        var NewType0Sub0Effect = new SSFJsonHandler.UVScrolling();

        NewType0Sub0Effect.U0 = Unknown1;
        NewType0Sub0Effect.U1 = Unknown2;
        NewType0Sub0Effect.U2 = Unknown3;
        NewType0Sub0Effect.U3 = Unknown4;
        NewType0Sub0Effect.U4 = Unknown5;
        NewType0Sub0Effect.U5 = Unknown6;

        NewType0Effect.UVScroll = NewType0Sub0Effect;

        NewEffect.type0 = NewType0Effect;

        return NewEffect;
    }
}
