using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrickyEffectHeader : MonoBehaviour
{

    public void LoadEffectList(SSFJsonHandler.EffectHeaderStruct EffectHeader)
    {
        gameObject.transform.name = EffectHeader.EffectName;
        for (int i = 0; i < EffectHeader.Effects.Count; i++)
        {
            LoadEffectData(EffectHeader.Effects[i]);
        }
    }


    public void LoadEffectData(SSFJsonHandler.Effect effect)
    {
        if (effect.MainType == 0)
        {
            if (effect.type0.Value.SubType == 0)
            {
                transform.AddComponent<Type0Sub0Effect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 2)
            {
                transform.AddComponent<DebounceEffect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 5)
            {
                transform.AddComponent<DeadNodeEffect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 6)
            {
                transform.AddComponent<CounterEffect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 7)
            {
                transform.AddComponent<BoostEffect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 10)
            {
                transform.AddComponent<UVScrollingEffect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 11)
            {
                transform.AddComponent<TextureFlipEffect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 12)
            {
                transform.AddComponent<FenceFlexEffect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 13)
            {
                transform.AddComponent<Type0Sub13Effect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 14)
            {
                transform.AddComponent<Type0Sub14Effect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 15)
            {
                transform.AddComponent<Type0Sub15Effect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 17)
            {
                transform.AddComponent<CrowdBoxEffect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 18)
            {
                transform.AddComponent<Type0Sub18Effect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 20)
            {
                transform.AddComponent<Type0Sub20Effect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 23)
            {
                transform.AddComponent<MovieScreenEffect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 24)
            {
                transform.AddComponent<Type0Sub24Effect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 256)
            {
                transform.AddComponent<Type0Sub256Effect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 257)
            {
                transform.AddComponent<Type0Sub257Effect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 258)
            {
                transform.AddComponent<Type0Sub258Effect>().LoadEffect(effect);
            }
            else
            {
                transform.AddComponent<EffectBase>().LoadEffect(effect);
            }
        }
        else if (effect.MainType == 2)
        {
            if (effect.type2.Value.SubType == 0)
            {
                transform.AddComponent<Type2Sub0Effect>().LoadEffect(effect);
            }
            else if (effect.type2.Value.SubType == 1)
            {
                transform.AddComponent<SplineAnimationEffect>().LoadEffect(effect);
            }
            else if (effect.type2.Value.SubType == 2)
            {
                transform.AddComponent<Type2Sub2Effect>().LoadEffect(effect);
            }
            else
            {
                transform.AddComponent<EffectBase>().LoadEffect(effect);
            }
        }
        else if (effect.MainType == 3)
        {
            transform.AddComponent<Type3Effect>().LoadEffect(effect);
        }
        else if (effect.MainType == 4)
        {
            transform.AddComponent<WaitEffect>().LoadEffect(effect);
        }
        else if (effect.MainType == 5)
        {
            transform.AddComponent<Type5Effect>().LoadEffect(effect);
        }
        else if (effect.MainType == 7)
        {
            transform.AddComponent<InstanceRunEffect>().LoadEffect(effect);
        }
        else if (effect.MainType == 8)
        {
            transform.AddComponent<SoundEffect>().LoadEffect(effect);
        }
        else if (effect.MainType == 9)
        {
            transform.AddComponent<Type9Effect>().LoadEffect(effect);
        }
        else if (effect.MainType == 13)
        {
            transform.AddComponent<Type13Effect>().LoadEffect(effect);
        }
        else if (effect.MainType == 14)
        {
            transform.AddComponent<MultiplierEffect>().LoadEffect(effect);
        }
        else if (effect.MainType == 17)
        {
            transform.AddComponent<Type17Effect>().LoadEffect(effect);
        }
        else if (effect.MainType == 18)
        {
            transform.AddComponent<Type18Effect>().LoadEffect(effect);
        }
        else if (effect.MainType == 21)
        {
            transform.AddComponent<FunctionRunEffect>().LoadEffect(effect);
        }
        else if (effect.MainType == 24)
        {
            transform.AddComponent<TeleportEffect>().LoadEffect(effect);
        }
        else if (effect.MainType == 25)
        {
            transform.AddComponent<SplineRunEffect>().LoadEffect(effect);
        }
        else
        {
            transform.AddComponent<EffectBase>().LoadEffect(effect);
        }
    }

    public void PostLoad(TrickyInstanceObject[] TempInstanceObjects, TrickyEffectHeader[] TempEffectHeader, TrickySplineObject[] TempSplineList, TrickyFunctionHeader[] TempFunctionList)
    {
        var TempEffects = GetEffects();

        for (int a = 0; a < TempEffects.Length; a++)
        {
            TempEffects[a].PostLoad(TempInstanceObjects, TempEffectHeader, TempSplineList, TempFunctionList);
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


    public EffectBase[] GetEffects()
    {
        return GetComponentsInChildren<EffectBase>(true);
    }

}
