using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Type2Sub2Effect : EffectBase
{
    public override int EffectType
    {
        get { return 2; }
    }

    public override int SubEffectType
    {
        get { return 2; }
    }

    public int Unknown1;
    public int Unknown2;
    public int Unknown3;
    public int Unknown4;
    public int Unknown5;
    public int Unknown6;
    public int Unknown7;
    public int Unknown8;
    public int Unknown9;
    public int Unknown10;
    public int Unknown11;
    public int Unknown12;
    public int Unknown13;
    public int Unknown14;
    public int Unknown15;
    public int Unknown16;
    public int Unknown17;
    public int Unknown18;
    public int Unknown19;
    public int Unknown20;
    public int Unknown21;
    public int Unknown22;
    public int Unknown23;
    public int Unknown24;
    public int Unknown25;
    public int Unknown26;
    public int Unknown27;
    public int Unknown28;
    public int Unknown29;
    public int Unknown30;
    public int Unknown31;
    public int Unknown32;
    public int Unknown33;
    public int Unknown34;
    public int Unknown35;
    public int Unknown36;
    public int Unknown37;
    public int Unknown38;
    public int Unknown39;
    public int Unknown40;
    public int Unknown41;
    public int Unknown42;
    public int Unknown43;
    public int Unknown44;
    public int Unknown45;
    public int Unknown46;
    public int Unknown47;
    public int Unknown48;
    public int Unknown49;
    public int Unknown50;
    public int Unknown51;


    public override void LoadEffect(SSFJsonHandler.Effect effect)
    {
        Unknown1 = effect.type2.Value.type2Sub2.Value.U0;
        Unknown2 = effect.type2.Value.type2Sub2.Value.U1;
        Unknown3 = effect.type2.Value.type2Sub2.Value.U2;
        Unknown4 = effect.type2.Value.type2Sub2.Value.U3;
        Unknown5 = effect.type2.Value.type2Sub2.Value.U4;
        Unknown6 = effect.type2.Value.type2Sub2.Value.U5;
        Unknown7 = effect.type2.Value.type2Sub2.Value.U6;
        Unknown8 = effect.type2.Value.type2Sub2.Value.U7;
        Unknown9 = effect.type2.Value.type2Sub2.Value.U8;
        Unknown10 = effect.type2.Value.type2Sub2.Value.U9;
        Unknown11 = effect.type2.Value.type2Sub2.Value.U10;
        Unknown12 = effect.type2.Value.type2Sub2.Value.U11;
        Unknown13 = effect.type2.Value.type2Sub2.Value.U12;
        Unknown14 = effect.type2.Value.type2Sub2.Value.U13;
        Unknown15 = effect.type2.Value.type2Sub2.Value.U14;
        Unknown16 = effect.type2.Value.type2Sub2.Value.U15;
        Unknown17 = effect.type2.Value.type2Sub2.Value.U16;
        Unknown18 = effect.type2.Value.type2Sub2.Value.U17;
        Unknown19 = effect.type2.Value.type2Sub2.Value.U18;
        Unknown20 = effect.type2.Value.type2Sub2.Value.U19;
        Unknown21 = effect.type2.Value.type2Sub2.Value.U20;
        Unknown22 = effect.type2.Value.type2Sub2.Value.U21;
        Unknown23 = effect.type2.Value.type2Sub2.Value.U22;
        Unknown24 = effect.type2.Value.type2Sub2.Value.U23;
        Unknown25 = effect.type2.Value.type2Sub2.Value.U24;
        Unknown26 = effect.type2.Value.type2Sub2.Value.U25;
        Unknown27 = effect.type2.Value.type2Sub2.Value.U26;
        Unknown28 = effect.type2.Value.type2Sub2.Value.U27;
        Unknown29 = effect.type2.Value.type2Sub2.Value.U28;
        Unknown30 = effect.type2.Value.type2Sub2.Value.U29;
        Unknown31 = effect.type2.Value.type2Sub2.Value.U30;
        Unknown32 = effect.type2.Value.type2Sub2.Value.U31;
        Unknown33 = effect.type2.Value.type2Sub2.Value.U32;
        Unknown34 = effect.type2.Value.type2Sub2.Value.U33;
        Unknown35 = effect.type2.Value.type2Sub2.Value.U34;
        Unknown36 = effect.type2.Value.type2Sub2.Value.U35;
        Unknown37 = effect.type2.Value.type2Sub2.Value.U36;
        Unknown38 = effect.type2.Value.type2Sub2.Value.U37;
        Unknown39 = effect.type2.Value.type2Sub2.Value.U38;
        Unknown40 = effect.type2.Value.type2Sub2.Value.U39;
        Unknown41 = effect.type2.Value.type2Sub2.Value.U40;
        Unknown42 = effect.type2.Value.type2Sub2.Value.U41;
        Unknown43 = effect.type2.Value.type2Sub2.Value.U42;
        Unknown44 = effect.type2.Value.type2Sub2.Value.U43;
        Unknown45 = effect.type2.Value.type2Sub2.Value.U44;
        Unknown46 = effect.type2.Value.type2Sub2.Value.U45;
        Unknown47 = effect.type2.Value.type2Sub2.Value.U46;
        Unknown48 = effect.type2.Value.type2Sub2.Value.U47;
        Unknown49 = effect.type2.Value.type2Sub2.Value.U48;
        Unknown50 = effect.type2.Value.type2Sub2.Value.U49;
        Unknown51 = effect.type2.Value.type2Sub2.Value.U50;
    }

    public override SSFJsonHandler.Effect SaveEffect()
    {
        var NewEffect = new SSFJsonHandler.Effect();

        NewEffect.MainType = EffectType;

        var NewType0Effect = new SSFJsonHandler.Type2();

        NewType0Effect.SubType = SubEffectType;

        var NewType0Sub0Effect = new SSFJsonHandler.Type2Sub2();

        NewType0Sub0Effect.U0 = Unknown1;
        NewType0Sub0Effect.U1 = Unknown2;
        NewType0Sub0Effect.U2 = Unknown3;
        NewType0Sub0Effect.U3 = Unknown4;
        NewType0Sub0Effect.U4 = Unknown5;
        NewType0Sub0Effect.U5 = Unknown6;
        NewType0Sub0Effect.U6 = Unknown7;
        NewType0Sub0Effect.U7 = Unknown8;
        NewType0Sub0Effect.U8 = Unknown9;
        NewType0Sub0Effect.U9 = Unknown10;
        NewType0Sub0Effect.U10 = Unknown11;
        NewType0Sub0Effect.U11 = Unknown12;
        NewType0Sub0Effect.U12 = Unknown13;
        NewType0Sub0Effect.U13 = Unknown14;
        NewType0Sub0Effect.U14 = Unknown15;
        NewType0Sub0Effect.U15 = Unknown16;
        NewType0Sub0Effect.U16 = Unknown17;
        NewType0Sub0Effect.U17 = Unknown18;
        NewType0Sub0Effect.U18 = Unknown19;
        NewType0Sub0Effect.U19 = Unknown20;
        NewType0Sub0Effect.U20 = Unknown21;
        NewType0Sub0Effect.U21 = Unknown22;
        NewType0Sub0Effect.U22 = Unknown23;
        NewType0Sub0Effect.U23 = Unknown24;
        NewType0Sub0Effect.U24 = Unknown25;
        NewType0Sub0Effect.U25 = Unknown26;
        NewType0Sub0Effect.U26 = Unknown27;
        NewType0Sub0Effect.U27 = Unknown28;
        NewType0Sub0Effect.U28 = Unknown29;
        NewType0Sub0Effect.U29 = Unknown30;
        NewType0Sub0Effect.U30 = Unknown31;
        NewType0Sub0Effect.U31 = Unknown32;
        NewType0Sub0Effect.U32 = Unknown33;
        NewType0Sub0Effect.U33 = Unknown34;
        NewType0Sub0Effect.U34 = Unknown35;
        NewType0Sub0Effect.U35 = Unknown36;
        NewType0Sub0Effect.U36 = Unknown37;
        NewType0Sub0Effect.U37 = Unknown38;
        NewType0Sub0Effect.U38 = Unknown39;
        NewType0Sub0Effect.U39 = Unknown40;
        NewType0Sub0Effect.U40 = Unknown41;
        NewType0Sub0Effect.U41 = Unknown42;
        NewType0Sub0Effect.U42 = Unknown43;
        NewType0Sub0Effect.U43 = Unknown44;
        NewType0Sub0Effect.U44 = Unknown45;
        NewType0Sub0Effect.U45 = Unknown46;
        NewType0Sub0Effect.U46 = Unknown47;
        NewType0Sub0Effect.U47 = Unknown48;
        NewType0Sub0Effect.U48 = Unknown49;
        NewType0Sub0Effect.U49 = Unknown50;
        NewType0Sub0Effect.U50 = Unknown51;

        NewType0Effect.type2Sub2 = NewType0Sub0Effect;

        NewEffect.type2 = NewType0Effect;

        return NewEffect;
    }
}
