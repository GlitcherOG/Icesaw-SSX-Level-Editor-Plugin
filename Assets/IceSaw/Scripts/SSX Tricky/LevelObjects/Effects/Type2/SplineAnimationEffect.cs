using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineAnimationEffect : EffectBase
{
    public override int EffectType
    {
        get { return 2; }
    }

    public override int SubEffectType
    {
        get { return 1; }
    }

    public int SplineIndex;
    public int U1;
    public int U2;
    public int InstanceCount;
    public float AnimationSpeed;
    public float U5;
    public int U6;
    public float U7;
    public float R;
    public float G;
    public float B;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        SplineIndex = effect.type2.Value.SplineAnimation.Value.SplineIndex;
        U1 = effect.type2.Value.SplineAnimation.Value.U1;
        U2 = effect.type2.Value.SplineAnimation.Value.U2;
        InstanceCount = effect.type2.Value.SplineAnimation.Value.InstanceCount;
        AnimationSpeed = effect.type2.Value.SplineAnimation.Value.AnimationSpeed;
        U5 = effect.type2.Value.SplineAnimation.Value.U5;
        U6 = effect.type2.Value.SplineAnimation.Value.U6;
        U7 = effect.type2.Value.SplineAnimation.Value.U7;
        R = effect.type2.Value.SplineAnimation.Value.R;
        G = effect.type2.Value.SplineAnimation.Value.G;
        B = effect.type2.Value.SplineAnimation.Value.B;

    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewType0Effect = new SSFJsonHandler.Type2();

        NewType0Effect.SubType = SubEffectType;

        var NewType0Sub0Effect = new SSFJsonHandler.SplinePathAnimation();

        NewType0Sub0Effect.SplineIndex = SplineIndex;
        NewType0Sub0Effect.U1 = U1;
        NewType0Sub0Effect.U2 = U2;
        NewType0Sub0Effect.InstanceCount = InstanceCount;
        NewType0Sub0Effect.AnimationSpeed = AnimationSpeed;
        NewType0Sub0Effect.U5 = U5;
        NewType0Sub0Effect.U6 = U6;
        NewType0Sub0Effect.U7 = U7;
        NewType0Sub0Effect.R = R;
        NewType0Sub0Effect.G = G;
        NewType0Sub0Effect.B = B;

        NewType0Effect.SplineAnimation = NewType0Sub0Effect;

        NewEffect.type2 = NewType0Effect;

        return NewEffect;
    }
}
