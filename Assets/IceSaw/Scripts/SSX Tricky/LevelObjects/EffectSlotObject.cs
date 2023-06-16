using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSlotObject : MonoBehaviour
{
    public int PersistantEffectSlot;
    public int CollisionEffectSlot;
    public int Slot3;
    public int Slot4;
    public int EffectTriggerSlot;
    public int Slot6;
    public int Slot7;

    public void LoadEffectSlot(SSFJsonHandler.EffectSlotJson effectSlot)
    {
        transform.name = effectSlot.EffectSlotName;

        PersistantEffectSlot = effectSlot.PersistantEffectSlot;
        CollisionEffectSlot = effectSlot.CollisionEffectSlot;
        Slot3 = effectSlot.Slot3;
        Slot4 = effectSlot.Slot4;
        EffectTriggerSlot = effectSlot.EffectTriggerSlot;
        Slot6 = effectSlot.Slot6;
        Slot7 = effectSlot.Slot7;
    }

    public SSFJsonHandler.EffectSlotJson SaveEffectSlot()
    {
        var TempEffectslot = new SSFJsonHandler.EffectSlotJson();

        TempEffectslot.EffectSlotName = transform.name;
        TempEffectslot.PersistantEffectSlot = PersistantEffectSlot;
        TempEffectslot.CollisionEffectSlot = CollisionEffectSlot;
        TempEffectslot.Slot3 = Slot3;
        TempEffectslot.Slot4 = Slot4;
        TempEffectslot.EffectTriggerSlot = EffectTriggerSlot;
        TempEffectslot.Slot6 = Slot6;
        TempEffectslot.Slot7 = Slot7;

        return TempEffectslot;
    }
}
