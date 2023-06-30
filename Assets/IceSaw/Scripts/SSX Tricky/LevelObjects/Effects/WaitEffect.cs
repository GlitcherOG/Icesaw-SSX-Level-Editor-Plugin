using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitEffect : EffectBase
{
    public float WaitTime;
    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        EffectType = effect.MainType;
        WaitTime = effect.WaitTime.Value;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;
        NewEffect.WaitTime = WaitTime;

        return NewEffect;
    }

}
