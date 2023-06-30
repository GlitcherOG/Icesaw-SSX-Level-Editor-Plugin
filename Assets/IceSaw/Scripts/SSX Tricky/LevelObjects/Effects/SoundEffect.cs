using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : EffectBase
{
    public override int EffectType
    {
        get { return 8; }
    }

    public int SoundPlayIndex;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        SoundPlayIndex = effect.SoundPlay.Value;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;
        NewEffect.SoundPlay = SoundPlayIndex;

        return NewEffect;
    }
}
