using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FenceFlexEffect : EffectBase
{
    public override int EffectType
    {
        get { return 0; }
    }

    public override int SubEffectType
    {
        get { return 12; }
    }
    public int Unknown1;
    public float FlexAmmount;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        Unknown1 = effect.type0.Value.Fence.Value.U0;
        FlexAmmount = effect.type0.Value.Fence.Value.FlexAmmount;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewType0Effect = new SSFJsonHandler.Type0();

        NewType0Effect.SubType = SubEffectType;

        var NewType0Sub0Effect = new SSFJsonHandler.FenceFlex();

        NewType0Sub0Effect.U0 = Unknown1;
        NewType0Sub0Effect.FlexAmmount = FlexAmmount;

        NewType0Effect.Fence = NewType0Sub0Effect;

        NewEffect.type0 = NewType0Effect;

        return NewEffect;
    }
}
