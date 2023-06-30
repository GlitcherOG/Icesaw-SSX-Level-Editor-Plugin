using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostEffect : EffectBase
{
    public override int EffectType
    {
        get { return 0; }
    }

    public override int SubEffectType
    {
        get { return 7; }
    }

    public int Mode;
    public float Unknown1;
    public float Unknown2;
    public float BoostAmmount;
    public Vector3 BoostDir;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        Mode = effect.type0.Value.Boost.Value.Mode;
        Unknown1 = effect.type0.Value.Boost.Value.U1;
        Unknown2 = effect.type0.Value.Boost.Value.U2;
        BoostAmmount = effect.type0.Value.Boost.Value.BoostAmount;
        BoostDir = JsonUtil.ArrayToVector3(effect.type0.Value.Boost.Value.BoostDir);
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewType0Effect = new SSFJsonHandler.Type0();

        NewType0Effect.SubType = SubEffectType;

        var NewType0Sub0Effect = new SSFJsonHandler.BoostEffect();

        NewType0Sub0Effect.Mode = Mode;
        NewType0Sub0Effect.U1 = Unknown1;
        NewType0Sub0Effect.U2 = Unknown2;
        NewType0Sub0Effect.BoostAmount = BoostAmmount;
        NewType0Sub0Effect.BoostDir = JsonUtil.Vector3ToArray(BoostDir);

        NewType0Effect.Boost = NewType0Sub0Effect;

        NewEffect.type0 = NewType0Effect;

        return NewEffect;
    }
}
