using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovieScreenEffect : EffectBase
{
    public override int EffectType
    {
        get { return 0; }
    }

    public override int SubEffectType
    {
        get { return 23; }
    }

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        //You do nothing cause you can do nothing 
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewType0Effect = new SSFJsonHandler.Type0();

        NewType0Effect.SubType = SubEffectType;

        NewEffect.type0 = NewType0Effect;

        return NewEffect;
    }
}
