using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextureFlipEffect : EffectBase
{
    public override int EffectType
    {
        get { return 0; }
    }

    public override int SubEffectType
    {
        get { return 11; }
    }

    public int Unknown1;
    public int Direction;
    public float Speed;
    public float Length;
    public int Unknown2;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        Unknown1 = effect.type0.Value.TextureFlip.Value.U0;
        Direction = effect.type0.Value.TextureFlip.Value.Direction;
        Speed = effect.type0.Value.TextureFlip.Value.Speed;
        Length = effect.type0.Value.TextureFlip.Value.Length;
        Unknown2 = effect.type0.Value.TextureFlip.Value.U4;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewType0Effect = new SSFJsonHandler.Type0();

        NewType0Effect.SubType = SubEffectType;

        var NewType0Sub0Effect = new SSFJsonHandler.TextureFlipEffect();

        NewType0Sub0Effect.U0 = Unknown1;
        NewType0Sub0Effect.Direction = Direction;
        NewType0Sub0Effect.Speed = Speed;
        NewType0Sub0Effect.Length = Length;
        NewType0Sub0Effect.U4 = Unknown2;

        NewType0Effect.TextureFlip = NewType0Sub0Effect;

        NewEffect.type0 = NewType0Effect;

        return NewEffect;
    }
}
