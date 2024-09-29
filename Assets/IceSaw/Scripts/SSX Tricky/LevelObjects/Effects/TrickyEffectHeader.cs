using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TrickyBaseObject;

public class TrickyEffectHeader : TrickyEffectHeaderBase
{
    public override ObjectType Type
    {
        get { return ObjectType.Effect; }
    }
    public void LoadEffectList(SSFJsonHandler.EffectHeaderStruct EffectHeader)
    {
        gameObject.transform.name = EffectHeader.EffectName;
        for (int i = 0; i < EffectHeader.Effects.Count; i++)
        {
            LoadEffectData(EffectHeader.Effects[i]);
        }
    }


    public SSFJsonHandler.EffectHeaderStruct GenerateEffectHeader()
    {
        var NewHeader = new SSFJsonHandler.EffectHeaderStruct();

        NewHeader.EffectName = transform.name;
        NewHeader.Effects = new List<SSFJsonHandler.Effect>();

        var TempEffects = GetEffects();

        for (int a = 0; a < TempEffects.Length; a++)
        {
            NewHeader.Effects.Add(TempEffects[a].SaveEffect());
        }

        return NewHeader;
    }
}
