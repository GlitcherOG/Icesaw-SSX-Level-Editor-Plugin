using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class LogicManager : MonoBehaviour
{
    public static LogicManager Instance;

    GameObject EffectSlotHolder;
    GameObject PhysicsHolder;
    GameObject EffectHolder;
    GameObject FunctionHolder;

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
            TempGameObject.transform.hideFlags = HideFlags.HideInInspector;

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
            TempGameObject.transform.hideFlags = HideFlags.HideInInspector;

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
            TempGameObject.transform.hideFlags = HideFlags.HideInInspector;

            for (int a = 0; a < effects[i].Effects.Count; a++)
            {
                GenerateEffectData(effects[i].Effects[a], TempGameObject, "Effect " + a);
            }
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
            TempGameObject.transform.hideFlags = HideFlags.HideInInspector;

            for (int a = 0; a < effects[i].Effects.Count; a++)
            {
                GenerateEffectData(effects[i].Effects[a], TempGameObject, "Effect " + a);
            }
        }
    }

    public void GenerateEffectData(SSFJsonHandler.Effect effect, GameObject Parent, string name)
    {

        if (effect.MainType == 0)
        {
            if (effect.type0.Value.SubType == 0)
            {
                Parent.AddComponent<Type0Sub0Effect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 2)
            {
                Parent.AddComponent<Type0Sub2Effect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 5)
            {
                Parent.AddComponent<DeadNodeEffect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 6)
            {
                Parent.AddComponent<CounterEffect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 7)
            {
                Parent.AddComponent<BoostEffect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 10)
            {
                Parent.AddComponent<UVScrollingEffect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 11)
            {
                Parent.AddComponent<TextureFlipEffect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 12)
            {
                Parent.AddComponent<FenceFlexEffect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 13)
            {
                Parent.AddComponent<Type0Sub13Effect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 14)
            {
                Parent.AddComponent<Type0Sub14Effect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 15)
            {
                Parent.AddComponent<Type0Sub15Effect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 17)
            {
                Parent.AddComponent<CrowdBoxEffect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 18)
            {
                Parent.AddComponent<Type0Sub18Effect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 20)
            {
                Parent.AddComponent<Type0Sub20Effect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 23)
            {
                Parent.AddComponent<MovieScreenEffect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 24)
            {
                Parent.AddComponent<Type0Sub24Effect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 256)
            {
                Parent.AddComponent<Type0Sub256Effect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 257)
            {
                Parent.AddComponent<Type0Sub257Effect>().LoadEffect(effect);
            }
            else if (effect.type0.Value.SubType == 258)
            {
                Parent.AddComponent<Type0Sub258Effect>().LoadEffect(effect);
            }
            else
            {
                Parent.AddComponent<EffectBase>().LoadEffect(effect);
            }
        }
        else if (effect.MainType == 2)
        {
            if (effect.type2.Value.SubType == 0)
            {
                Parent.AddComponent<Type2Sub0Effect>().LoadEffect(effect);
            }
            else if (effect.type2.Value.SubType == 1)
            {
                Parent.AddComponent<Type2Sub1Effect>().LoadEffect(effect);
            }
            else if (effect.type2.Value.SubType == 2)
            {
                Parent.AddComponent<Type2Sub2Effect>().LoadEffect(effect);
            }
            else
            {
                Parent.AddComponent<EffectBase>().LoadEffect(effect);
            }
        }
        else if (effect.MainType == 3)
        {
            Parent.AddComponent<Type3Effect>().LoadEffect(effect);
        }
        else if (effect.MainType == 4)
        {
            Parent.AddComponent<WaitEffect>().LoadEffect(effect);
        }
        else if (effect.MainType == 5)
        {
            Parent.AddComponent<Type5Effect>().LoadEffect(effect);
        }
        else if (effect.MainType == 7)
        {
            Parent.AddComponent<InstanceRunEffect>().LoadEffect(effect);
        }
        else if (effect.MainType == 8)
        {
            Parent.AddComponent<SoundEffect>().LoadEffect(effect);
        }
        else if (effect.MainType == 9)
        {
            Parent.AddComponent<Type9Effect>().LoadEffect(effect);
        }
        else if (effect.MainType == 13)
        {
            Parent.AddComponent<Type13Effect>().LoadEffect(effect);
        }
        else if (effect.MainType == 14)
        {
            Parent.AddComponent<MultiplierEffect>().LoadEffect(effect);
        }
        else if (effect.MainType == 17)
        {
            Parent.AddComponent<Type17Effect>().LoadEffect(effect);
        }
        else if (effect.MainType == 18)
        {
            Parent.AddComponent<Type18Effect>().LoadEffect(effect);
        }
        else if (effect.MainType == 21)
        {
            Parent.AddComponent<FunctionRunEffect>().LoadEffect(effect);
        }
        else if (effect.MainType == 24)
        {
            Parent.AddComponent<TeleportEffect>().LoadEffect(effect);
        }
        else if (effect.MainType == 25)
        {
            Parent.AddComponent<SplineRunEffect>().LoadEffect(effect);
        }
        else
        {
            Parent.AddComponent<EffectBase>().LoadEffect(effect);
        }
    }

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

    public EffectSlotObject[] GetEffectSlotsList()
    {
        return EffectSlotHolder.GetComponentsInChildren<EffectSlotObject>(true);
    }

    public PhysicsObject[] GetPhysicsObjects()
    {
        return PhysicsHolder.GetComponentsInChildren<PhysicsObject>(true);
    }

    public List<SSFJsonHandler.EffectHeaderStruct> GetEffectHeadersList()
    {
        int ChildList = EffectHolder.transform.childCount;

        List<SSFJsonHandler.EffectHeaderStruct> HeaderList = new List<SSFJsonHandler.EffectHeaderStruct>();

        for (int i = 0; i < ChildList; i++)
        {
            var TempObject = EffectHolder.transform.GetChild(i);
            var NewHeader = new SSFJsonHandler.EffectHeaderStruct();

            NewHeader.EffectName = TempObject.name;
            NewHeader.Effects = new List<SSFJsonHandler.Effect>();

            var TempEffects = TempObject.GetComponentsInChildren<EffectBase>(true);

            for (int a = 0; a < TempEffects.Length; a++)
            {
                NewHeader.Effects.Add(TempEffects[a].SaveEffect());
            }
            HeaderList.Add(NewHeader);
        }

        return HeaderList;
    }

    public List<SSFJsonHandler.Function> GetFunctionList()
    {
        int ChildList = FunctionHolder.transform.childCount;

        List<SSFJsonHandler.Function> HeaderList = new List<SSFJsonHandler.Function>();

        for (int i = 0; i < ChildList; i++)
        {
            var TempObject = FunctionHolder.transform.GetChild(i);
            var NewHeader = new SSFJsonHandler.Function();

            NewHeader.FunctionName = TempObject.name;
            NewHeader.Effects = new List<SSFJsonHandler.Effect>();

            var TempEffects = TempObject.GetComponentsInChildren<EffectBase>(true);

            for (int a = 0; a < TempEffects.Length; a++)
            {
                NewHeader.Effects.Add(TempEffects[a].SaveEffect());
            }
            HeaderList.Add(NewHeader);
        }

        return HeaderList;
    }

    public GameObject[] GetFunctionObjects()
    {
        int ChildList = FunctionHolder.transform.childCount;
        List<GameObject> FunctionList = new List<GameObject>();
        for (int i = 0; i < ChildList; i++)
        {
            FunctionList.Add(FunctionHolder.transform.GetChild(i).gameObject);
        }

        return FunctionList.ToArray();
    }

    public GameObject[] GetEffectObjects()
    {
        int ChildList = EffectHolder.transform.childCount;
        List<GameObject> FunctionList = new List<GameObject>();
        for (int i = 0; i < ChildList; i++)
        {
            FunctionList.Add(EffectHolder.transform.GetChild(i).gameObject);
        }

        return FunctionList.ToArray();
    }
}
