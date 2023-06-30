using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEffect : EffectBase
{
    public override int EffectType
    {
        get { return 24; }
    }

    public int TeleportInstanceIndex;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        TeleportInstanceIndex = effect.TeleportInstanceIndex.Value;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;
        NewEffect.TeleportInstanceIndex = TeleportInstanceIndex;

        return NewEffect;
    }
}
