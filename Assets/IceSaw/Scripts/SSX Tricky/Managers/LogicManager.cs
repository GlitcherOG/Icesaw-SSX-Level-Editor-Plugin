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
    GameObject PhysicsHolder;
    GameObject EffectHolder;
    GameObject FunctionHolder;

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

        PhysicsHolder = new GameObject("Physics");
        PhysicsHolder.transform.parent = this.transform;
        PhysicsHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        PhysicsHolder.transform.localEulerAngles = new Vector3(0, 0, 0);

        EffectHolder = new GameObject("Effects");
        EffectHolder.transform.parent = this.transform;
        EffectHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        EffectHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    public void LoadData(string path)
    {
        ssfJsonHandler = new SSFJsonHandler();
        ssfJsonHandler = SSFJsonHandler.Load(path + "\\SSFLogic.json");
        LoadEffectSlots(ssfJsonHandler.EffectSlots);
        LoadPhysics(ssfJsonHandler.PhysicsHeaders);
        LoadEffects(ssfJsonHandler.EffectHeaders);
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

    public void LoadPhysics(List<SSFJsonHandler.PhysicsHeader> physicsHeaders)
    {
        for (int i = 0; i < physicsHeaders.Count; i++)
        {
            var TempGameObject = new GameObject("Physics " + i);
            TempGameObject.transform.parent = EffectSlotHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
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

            for (int a = 0; a < effects[i].Effects.Count; a++)
            {
                GenerateEffectData(effects[i].Effects[a], TempGameObject, "Effect " + a);
            }
        }
    }

    public void GenerateEffectData(SSFJsonHandler.Effect effect, GameObject Parent, string name)
    {
        var TempGameObject = new GameObject(name);
        TempGameObject.transform.parent = Parent.transform;
        TempGameObject.transform.localScale = Vector3.one;
        TempGameObject.transform.localEulerAngles = Vector3.zero;

        if (effect.MainType == 0)
        {
            
        }
        else if (effect.MainType == 2)
        {

        }
        else if (effect.MainType == 3)
        {
            TempGameObject.AddComponent<Type3Effect>().LoadEffect(effect);
        }
        else if (effect.MainType == 4)
        {
            TempGameObject.AddComponent<WaitEffect>().LoadEffect(effect);
        }
        else if (effect.MainType == 5)
        {
            TempGameObject.AddComponent<Type5Effect>().LoadEffect(effect);
        }
        else if (effect.MainType == 7)
        {
            TempGameObject.AddComponent<InstanceRunEffect>().LoadEffect(effect);
        }
        else if (effect.MainType == 8)
        {
            TempGameObject.AddComponent<SoundEffect>().LoadEffect(effect);
        }
        else if (effect.MainType == 9)
        {
            TempGameObject.AddComponent<Type9Effect>().LoadEffect(effect);
        }
        else if (effect.MainType == 13)
        {
            TempGameObject.AddComponent<Type13Effect>().LoadEffect(effect);
        }
        else if (effect.MainType == 14)
        {
            TempGameObject.AddComponent<MultiplierEffect>().LoadEffect(effect);
        }
        else if (effect.MainType == 17)
        {
            TempGameObject.AddComponent<Type17Effect>().LoadEffect(effect);
        }
        else if (effect.MainType == 18)
        {
            TempGameObject.AddComponent<Type18Effect>().LoadEffect(effect);
        }
        else if (effect.MainType == 21)
        {
            TempGameObject.AddComponent<FunctionRunEffect>().LoadEffect(effect);
        }
        else if (effect.MainType == 24)
        {
            TempGameObject.AddComponent<TeleportEffect>().LoadEffect(effect);
        }
        else if (effect.MainType == 25)
        {
            TempGameObject.AddComponent<SplineRunEffect>().LoadEffect(effect);
        }
    }

    public void SaveData(string path)
    {
        ssfJsonHandler = new SSFJsonHandler();
        ssfJsonHandler.EffectSlots = SaveEffectSlots();
        ssfJsonHandler.PhysicsHeaders = SavePhysicsHeader();

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
        return EffectSlotHolder.GetComponentsInChildren<EffectSlotObject>();
    }

    public PhysicsObject[] GetPhysicsObjects()
    {
        return PhysicsHolder.GetComponentsInChildren<PhysicsObject>();
    }
}
