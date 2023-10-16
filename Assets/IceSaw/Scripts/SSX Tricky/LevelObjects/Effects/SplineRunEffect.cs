using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SplineRunEffect : EffectBase
{
    public override int EffectType
    {
        get { return 25; }
    }

    public int SplineIndex;
    public int Effect;

    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        SplineIndex = effect.Spline.Value.SplineIndex;
        Effect = effect.Spline.Value.Effect;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewInstanceEffect = new SSFJsonHandler.SplineEffect();

        NewInstanceEffect.SplineIndex = SplineIndex;
        NewInstanceEffect.Effect = Effect;

        NewEffect.Spline = NewInstanceEffect;

        return NewEffect;
    }

    [ContextMenu("Goto Spline")]
    public void GotoSpline()
    {
        var TempList = TrickyWorldManager.Instance.GetSplineList();

        if (TempList.Length - 1 >= SplineIndex)
        {
            Selection.activeObject = TempList[SplineIndex].gameObject;
        }
    }
}
