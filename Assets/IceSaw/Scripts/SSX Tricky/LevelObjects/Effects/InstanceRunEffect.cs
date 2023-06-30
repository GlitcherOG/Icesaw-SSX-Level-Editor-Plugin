using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceRunEffect : EffectBase
{
    public override int EffectType
    {
        get { return 7; }
    }

    public int InstanceIndex;
    public int EffectIndex;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        InstanceIndex = effect.Instance.Value.InstanceIndex;
        EffectIndex = effect.Instance.Value.EffectIndex;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewInstanceEffect = new SSFJsonHandler.InstanceEffect();

        NewInstanceEffect.InstanceIndex = InstanceIndex;
        NewInstanceEffect.EffectIndex = EffectIndex;

        NewEffect.Instance = NewInstanceEffect;

        return NewEffect;
    }
}
