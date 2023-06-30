using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : EffectBase
{
    public override int EffectType
    {
        get { return 14; }
    }

    public float MultiplierScore;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        MultiplierScore = effect.MultiplierScore.Value;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;
        NewEffect.MultiplierScore = MultiplierScore;

        return NewEffect;
    }
}
