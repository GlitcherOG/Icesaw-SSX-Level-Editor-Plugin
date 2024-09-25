using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickyFunctionHeader : TrickyEffectHeaderBase
{
    public void LoadFunction(SSFJsonHandler.Function EffectHeader)
    {
        gameObject.transform.name = EffectHeader.FunctionName;
        for (int i = 0; i < EffectHeader.Effects.Count; i++)
        {
            LoadEffectData(EffectHeader.Effects[i]);
        }
    }

    public SSFJsonHandler.Function GenerateFunction()
    {
        var NewHeader = new SSFJsonHandler.Function();

        NewHeader.FunctionName = transform.name;
        NewHeader.Effects = new List<SSFJsonHandler.Effect>();

        var TempEffects = GetEffects();

        for (int a = 0; a < TempEffects.Length; a++)
        {
            NewHeader.Effects.Add(TempEffects[a].SaveEffect());
        }

        return NewHeader;
    }
}
