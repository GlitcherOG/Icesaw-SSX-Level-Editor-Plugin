using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static SSXMultiTool.JsonFiles.Tricky.SSFJsonHandler;

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

    [ContextMenu("Goto Instance")]
    public void GotoInstance()
    {
        var TempList = TrickyWorldManager.Instance.GetInstanceList();

        if (TempList.Length - 1 >= InstanceIndex)
        {
            Selection.activeObject = TempList[InstanceIndex].gameObject;
        }
    }

    [ContextMenu("Goto Effect")]
    public void GotoEffect()
    {
        var TempList = TrickyLogicManager.Instance.GetEffectObjects();

        if (TempList.Length - 1 >= EffectIndex)
        {
            Selection.activeObject = TempList[EffectIndex];
        }
    }
}
