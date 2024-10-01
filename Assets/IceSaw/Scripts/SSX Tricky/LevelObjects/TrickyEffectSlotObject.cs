using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EffectSlotObject : TrickyBaseObject
{
    public override ObjectType Type
    {
        get { return ObjectType.EffectSlot; }
    }

    public TrickyEffectHeader PersistantEffectSlot;
    public TrickyEffectHeader CollisionEffectSlot;
    public TrickyEffectHeader Slot3;
    public TrickyEffectHeader Slot4;
    public TrickyEffectHeader EffectTriggerSlot;
    public TrickyEffectHeader Slot6;
    public TrickyEffectHeader Slot7;

    int PersistantEffectSlotIndex;
    int CollisionEffectSlotIndex;
    int Slot3Index;
    int Slot4Index;
    int EffectTriggerSlotIndex;
    int Slot6Index;
    int Slot7Index;

    public void LoadEffectSlot(SSFJsonHandler.EffectSlotJson effectSlot)
    {
        transform.name = effectSlot.EffectSlotName;

        PersistantEffectSlotIndex = effectSlot.PersistantEffectSlot;
        CollisionEffectSlotIndex = effectSlot.CollisionEffectSlot;
        Slot3Index = effectSlot.Slot3;
        Slot4Index = effectSlot.Slot4;
        EffectTriggerSlotIndex = effectSlot.EffectTriggerSlot;
        Slot6Index = effectSlot.Slot6;
        Slot7Index = effectSlot.Slot7;
    }

    public void PostLoad(TrickyEffectHeader[] EffectBaseList)
    {
        if (EffectBaseList.Length - 1 >= PersistantEffectSlotIndex && PersistantEffectSlotIndex != -1)
        {
            PersistantEffectSlot = EffectBaseList[PersistantEffectSlotIndex];
        }

        if (EffectBaseList.Length - 1 >= CollisionEffectSlotIndex && CollisionEffectSlotIndex != -1)
        {
            CollisionEffectSlot = EffectBaseList[CollisionEffectSlotIndex];
        }

        if (EffectBaseList.Length - 1 >= Slot3Index && Slot3Index != -1)
        {
            Slot3 = EffectBaseList[Slot3Index];
        }

        if (EffectBaseList.Length - 1 >= Slot4Index && Slot4Index != -1)
        {
            Slot4 = EffectBaseList[Slot4Index];
        }

        if (EffectBaseList.Length - 1 >= EffectTriggerSlotIndex && EffectTriggerSlotIndex != -1)
        {
            EffectTriggerSlot = EffectBaseList[EffectTriggerSlotIndex];
        }

        if (EffectBaseList.Length - 1 >= Slot6Index && Slot6Index != -1)
        {
            Slot6 = EffectBaseList[Slot6Index];
        }

        if (EffectBaseList.Length - 1 >= Slot7Index && Slot7Index != -1)
        {
            Slot7 = EffectBaseList[Slot7Index];
        }
    }

    public SSFJsonHandler.EffectSlotJson SaveEffectSlot()
    {
        var TempEffectslot = new SSFJsonHandler.EffectSlotJson();

        TempEffectslot.EffectSlotName = transform.name;

        if (PersistantEffectSlot != null)
        {
            TempEffectslot.PersistantEffectSlot = TrickyLevelManager.Instance.dataManager.GetEffectHeaderID(PersistantEffectSlot);
        }
        else
        {
            TempEffectslot.PersistantEffectSlot = -1;
        }

        if (CollisionEffectSlot != null)
        {
            TempEffectslot.CollisionEffectSlot = TrickyLevelManager.Instance.dataManager.GetEffectHeaderID(CollisionEffectSlot);
        }
        else
        {
            TempEffectslot.CollisionEffectSlot = -1;
        }

        if (Slot3 != null)
        {
            TempEffectslot.Slot3 = TrickyLevelManager.Instance.dataManager.GetEffectHeaderID(Slot3);
        }
        else
        {
            TempEffectslot.Slot3 = -1;
        }

        if (Slot4 != null)
        {
            TempEffectslot.Slot4 = TrickyLevelManager.Instance.dataManager.GetEffectHeaderID(Slot4);
        }
        else
        {
            TempEffectslot.Slot4 = -1;
        }

        if (EffectTriggerSlot != null)
        {
            TempEffectslot.EffectTriggerSlot = TrickyLevelManager.Instance.dataManager.GetEffectHeaderID(EffectTriggerSlot);
        }
        else
        {
            TempEffectslot.EffectTriggerSlot = -1;
        }

        if (Slot6 != null)
        {
            TempEffectslot.Slot6 = TrickyLevelManager.Instance.dataManager.GetEffectHeaderID(Slot6);
        }
        else
        {
            TempEffectslot.Slot6 = -1;
        }

        if (Slot7 != null)
        {
            TempEffectslot.Slot7 = TrickyLevelManager.Instance.dataManager.GetEffectHeaderID(Slot7);
        }
        else
        {
            TempEffectslot.Slot7 = -1;
        }

        return TempEffectslot;
    }

    [MenuItem("GameObject/Ice Saw/Effect Slot", false, 303)]
    public static void CreateEffectSlot(MenuCommand menuCommand)
    {
        GameObject TempObject = new GameObject("Effect Slot");
        if (menuCommand.context != null)
        {
            var AddToObject = (GameObject)menuCommand.context;
            TempObject.transform.parent = AddToObject.transform;
        }
        TempObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        TempObject.transform.localScale = new Vector3(1, 1, 1);
        Selection.activeGameObject = TempObject;
        TempObject.AddComponent<EffectSlotObject>();
    }
}
