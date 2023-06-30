using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase : MonoBehaviour
{
    [HideInInspector]
    public int EffectType;

    public virtual void LoadEffect(SSFJsonHandler.Effect effect)
    {

    }

    public virtual SSFJsonHandler.Effect SaveEffect()
    {
        return new SSFJsonHandler.Effect();
    }
}
