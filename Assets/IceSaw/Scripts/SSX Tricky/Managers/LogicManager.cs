using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LogicManager : MonoBehaviour
{
    public static LogicManager Instance;
    [SerializeField]
    public SSFJsonHandler ssfJsonHandler;

    GameObject EffectSlotHolder;

    //GameObject Effects;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void GenerateEmptyObjects()
    {
        EffectSlotHolder = new GameObject("Effect Slots");
        EffectSlotHolder.transform.parent = this.transform;
        EffectSlotHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        EffectSlotHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    public void LoadData(string path)
    {
        ssfJsonHandler = new SSFJsonHandler();
        ssfJsonHandler = SSFJsonHandler.Load(path + "\\SSFLogic.json");
        LoadEffectSlots(ssfJsonHandler.EffectSlots);
    }

    public void LoadEffectSlots(List<SSFJsonHandler.EffectSlotJson> effectSlotJson)
    {
        for (int i = 0; i < effectSlotJson.Count; i++)
        {
            var TempGameObject = new GameObject("Effect Slot " + i);
            TempGameObject.transform.parent = EffectSlotHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            var TempInstance = TempGameObject.AddComponent<EffectSlotObject>();
            TempInstance.LoadEffectSlot(effectSlotJson[i]);
        }
    }

    public void SaveData(string path)
    {
        ssfJsonHandler = new SSFJsonHandler();
        ssfJsonHandler.EffectSlots = SaveEffectSlots();
    }

    public List<SSFJsonHandler.EffectSlotJson> SaveEffectSlots()
    {
        var TempList = GetEffectSlotsList();
        var NewEffectSlotList = new List<SSFJsonHandler.EffectSlotJson>();

        for (int i = 0; i < TempList.Length; i++)
        {
            NewEffectSlotList.Add(TempList[i].SaveEffectSlot());
        }
        return NewEffectSlotList;
    }

    public EffectSlotObject[] GetEffectSlotsList()
    {
        return EffectSlotHolder.GetComponentsInChildren<EffectSlotObject>();
    }


}
