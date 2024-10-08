using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase : MonoBehaviour
{
    [HideInInspector]
    public virtual int EffectType
    {
        get { return -1; }
    }
    [HideInInspector]
    public virtual int SubEffectType
    {
        get { return -1; }
    }

    public virtual void LoadEffect(SSFJsonHandler.Effect effect)
    {

    }

    public virtual void PostLoad(TrickyInstanceObject[] TempInstanceObjects, TrickyEffectHeader[] TempEffectHeader, TrickySplineObject[] TempListSplines, TrickyFunctionHeader[] TempFunctionList)
    {

    }

    public virtual SSFJsonHandler.Effect SaveEffect()
    {
        return new SSFJsonHandler.Effect();
    }
}
