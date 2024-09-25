using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class TrickyLogicManager : MonoBehaviour
{
    public static TrickyLogicManager Instance;

    public GameObject EffectSlotHolder;
    public GameObject PhysicsHolder;
    public GameObject EffectHolder;
    public GameObject FunctionHolder;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
    }

    public void GenerateEmptyObjects()
    {
        EffectSlotHolder = new GameObject("Effect Slots");
        EffectSlotHolder.transform.parent = this.transform;
        EffectSlotHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        EffectSlotHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
        EffectSlotHolder.transform.hideFlags = HideFlags.HideInInspector;

        PhysicsHolder = new GameObject("Physics");
        PhysicsHolder.transform.parent = this.transform;
        PhysicsHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        PhysicsHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
        PhysicsHolder.transform.hideFlags = HideFlags.HideInInspector;

        EffectHolder = new GameObject("Effects");
        EffectHolder.transform.parent = this.transform;
        EffectHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        EffectHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
        EffectHolder.transform.hideFlags = HideFlags.HideInInspector;

        FunctionHolder = new GameObject("Functions");
        FunctionHolder.transform.parent = this.transform;
        FunctionHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        FunctionHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
        FunctionHolder.transform.hideFlags = HideFlags.HideInInspector;
    }
    #region Load Data
    public void LoadData(string path)
    {
        SSFJsonHandler ssfJsonHandler = new SSFJsonHandler();
        ssfJsonHandler = SSFJsonHandler.Load(path + "\\SSFLogic.json");
        LoadEffectSlots(ssfJsonHandler.EffectSlots);
        LoadPhysics(ssfJsonHandler.PhysicsHeaders);
        LoadEffects(ssfJsonHandler.EffectHeaders);
        LoadFunctions(ssfJsonHandler.Functions);
    }

    public void LoadEffectSlots(List<SSFJsonHandler.EffectSlotJson> effectSlotJson)
    {
        for (int i = 0; i < effectSlotJson.Count; i++)
        {
            var TempGameObject = new GameObject("Effect Slot " + i);
            TempGameObject.transform.parent = EffectSlotHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            //TempGameObject.transform.hideFlags = HideFlags.HideInInspector;

            var TempInstance = TempGameObject.AddComponent<EffectSlotObject>();
            TempInstance.LoadEffectSlot(effectSlotJson[i]);
        }
    }

    public void LoadPhysics(List<SSFJsonHandler.PhysicsHeader> physicsHeaders)
    {
        for (int i = 0; i < physicsHeaders.Count; i++)
        {
            var TempGameObject = new GameObject("Physics " + i);
            TempGameObject.transform.parent = PhysicsHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            //TempGameObject.transform.hideFlags = HideFlags.HideInInspector;
            var TempInstance = TempGameObject.AddComponent<PhysicsObject>();
            TempInstance.LoadPhysics(physicsHeaders[i]);
        }
    }

    public void LoadEffects(List<SSFJsonHandler.EffectHeaderStruct> effects)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            var TempGameObject = new GameObject(effects[i].EffectName);
            TempGameObject.transform.parent = EffectHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            //TempGameObject.transform.hideFlags = HideFlags.HideInInspector;
            TempGameObject.AddComponent<TrickyEffectHeader>().LoadEffectList(effects[i]);
        }
    }

    public void LoadFunctions(List<SSFJsonHandler.Function> effects)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            var TempGameObject = new GameObject(effects[i].FunctionName);
            TempGameObject.transform.parent = FunctionHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            //TempGameObject.transform.hideFlags = HideFlags.HideInInspector;
            TempGameObject.AddComponent<TrickyFunctionHeader>().LoadFunction(effects[i]);
        }
    }

    #endregion

    public void PostLoad()
    {
        var TempListEffectSlot = GetEffectSlotsList();
        var TempListEffectHeaders = GetEffectObjects();
        var TempListFunctionHeaders = GetFunctionObjects();

        var TempInstanceList = TrickyWorldManager.Instance.GetInstanceList();
        var TempListSplines = TrickyWorldManager.Instance.GetSplineList();

        for (int i = 0; i < TempListEffectSlot.Length; i++)
        {
            TempListEffectSlot[i].PostLoad(TempListEffectHeaders);
        }

        for (int i = 0; i < TempListEffectHeaders.Length; i++)
        {
            TempListEffectHeaders[i].PostLoad(TempInstanceList, TempListEffectHeaders, TempListSplines, TempListFunctionHeaders);
        }

        for (int i = 0; i < TempListFunctionHeaders.Length; i++)
        {
            TempListFunctionHeaders[i].PostLoad(TempInstanceList, TempListEffectHeaders, TempListSplines, TempListFunctionHeaders);
        }

        //TempListEffectSlot = null;
        //TempListEffectHeaders = null;
    }

    #region Save Data
    public void SaveData(string path)
    {
        SSFJsonHandler ssfJsonHandler = new SSFJsonHandler();
        ssfJsonHandler.EffectSlots = SaveEffectSlots();
        ssfJsonHandler.PhysicsHeaders = SavePhysicsHeader();
        ssfJsonHandler.EffectHeaders = GetEffectHeadersList();
        ssfJsonHandler.Functions = GetFunctionList();
        ssfJsonHandler.CreateJson(path + "\\SSFLogic.json");
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

    public List<SSFJsonHandler.PhysicsHeader> SavePhysicsHeader()
    {
        var TempList = GetPhysicsObjects();
        var NewPhysicsHeaders = new List<SSFJsonHandler.PhysicsHeader>();

        for (int i = 0; i < TempList.Length; i++)
        {
            NewPhysicsHeaders.Add(TempList[i].GeneratePhysics());
        }

        return NewPhysicsHeaders;
    }

    public List<SSFJsonHandler.EffectHeaderStruct> GetEffectHeadersList()
    {
        var EffectObjects = GetEffectObjects();

        List<SSFJsonHandler.EffectHeaderStruct> HeaderList = new List<SSFJsonHandler.EffectHeaderStruct>();

        for (int i = 0; i < EffectObjects.Length; i++)
        {
            HeaderList.Add(EffectObjects[i].GenerateEffectHeader());
        }

        return HeaderList;
    }

    public List<SSFJsonHandler.Function> GetFunctionList()
    {
        var EffectObjects = GetFunctionObjects();

        List<SSFJsonHandler.Function> HeaderList = new List<SSFJsonHandler.Function>();

        for (int i = 0; i < EffectObjects.Length; i++)
        {
            HeaderList.Add(EffectObjects[i].GenerateFunction());
        }

        return HeaderList;
    }
    #endregion

    public TrickyFunctionHeader[] GetFunctionObjects()
    {
        return FunctionHolder.GetComponentsInChildren<TrickyFunctionHeader>(true);
    }

    public TrickyEffectHeader[] GetEffectObjects()
    {
        return EffectHolder.GetComponentsInChildren<TrickyEffectHeader>(true);
    }
    public EffectSlotObject[] GetEffectSlotsList()
    {
        return EffectSlotHolder.GetComponentsInChildren<EffectSlotObject>(true);
    }

    public PhysicsObject[] GetPhysicsObjects()
    {
        return PhysicsHolder.GetComponentsInChildren<PhysicsObject>(true);
    }

}
