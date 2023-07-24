using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    [ContextMenu("Goto Persistant Effect")]
    public void GotoPersistantEffect()
    {
        var TempList = LogicManager.Instance.GetEffectObjects();

        if (TempList.Length - 1 >= PersistantEffectSlot)
        {
            Selection.activeObject = TempList[PersistantEffectSlot];
        }
    }

    [ContextMenu("Goto Collision Effect")]
    public void GotoCollisionEffect()
    {
        var TempList = LogicManager.Instance.GetEffectObjects();

        if (TempList.Length - 1 >= CollisionEffectSlot)
        {
            Selection.activeObject = TempList[CollisionEffectSlot];
        }
    }

    [ContextMenu("Goto Slot 3 Effect")]
    public void GotoSlot3Effect()
    {
        var TempList = LogicManager.Instance.GetEffectObjects();

        if (TempList.Length - 1 >= Slot3)
        {
            Selection.activeObject = TempList[Slot3];
        }
    }

    [ContextMenu("Goto Slot 4 Effect")]
    public void GotoSlot4Effect()
    {
        var TempList = LogicManager.Instance.GetEffectObjects();

        if (TempList.Length - 1 >= Slot4)
        {
            Selection.activeObject = TempList[Slot4];
        }
    }

    [ContextMenu("Goto Effect Trigger Effect")]
    public void GotoEffectTriggerffect()
    {
        var TempList = LogicManager.Instance.GetEffectObjects();

        if (TempList.Length - 1 >= EffectTriggerSlot)
        {
            Selection.activeObject = TempList[EffectTriggerSlot];
        }
    }

    [ContextMenu("Goto Slot 6 Effect")]
    public void GotoSlot5Effect()
    {
        var TempList = LogicManager.Instance.GetEffectObjects();

        if (TempList.Length - 1 >= Slot6)
        {
            Selection.activeObject = TempList[Slot6];
        }
    }

    [ContextMenu("Goto Slot 7 Effect")]
    public void GotoSlot6Effect()
    {
        var TempList = LogicManager.Instance.GetEffectObjects();

        if (TempList.Length - 1 >= Slot7)
        {
            Selection.activeObject = TempList[Slot7];
        }
    }

    [MenuItem("GameObject/Ice Saw/Effect Slot", false, 12)]
    public static void CreateEffectSlot(MenuCommand menuCommand)
    {
        GameObject TempObject = new GameObject("Effect Slot");
        TempObject.AddComponent<EffectSlotObject>();
        if (menuCommand.context != null)
        {
            var AddToObject = (GameObject)menuCommand.context;

            TempObject.transform.parent = AddToObject.transform;
        }

    }
}
