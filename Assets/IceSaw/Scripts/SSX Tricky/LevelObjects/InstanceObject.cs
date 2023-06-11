using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using UnityEditor;

public class InstanceObject : MonoBehaviour
{
    public string InstanceName;

    bool IsLoaded = false;
    public bool UpdateXYZ = false;

    public Vector3 rotation;
    public Vector3 scale;
    public Vector3 InstancePosition;

    public Vector4 Unknown5; //Something to do with lighting
    public Vector4 Unknown6; //Lighting Continued?
    public Vector4 Unknown7; 
    public Vector4 Unknown8;
    public Vector4 Unknown9; //Some Lighting Thing
    public Vector4 Unknown10;
    public Vector4 Unknown11;
    public Vector4 RGBA;

    public int ModelID;
    public int PrevInstance; //Next Connected Model 
    public int NextInstance; //Prev Connected Model

    public int UnknownInt26;
    public int UnknownInt27;
    public int UnknownInt28;
    public int ModelID2;
    public int UnknownInt30;
    public int UnknownInt31;
    public int UnknownInt32;

    public int LTGState;
    

    GameObject Prefab;
    public List<MeshCollider> colliders;

    public string Effects;

    public MaterialJsonHandler.MaterialsJson Mat = new MaterialJsonHandler.MaterialsJson(); 

    public void LoadInstance(InstanceJsonHandler.InstanceJson instance)
    {
        InstanceName = instance.InstanceName;

        rotation = JsonUtil.ArrayToQuaternion(instance.Rotation).eulerAngles;
        scale = JsonUtil.ArrayToVector3(instance.Scale);
        InstancePosition = JsonUtil.ArrayToVector3(instance.Location);

        Unknown5 = JsonUtil.ArrayToVector4(instance.Unknown5);
        Unknown6 = JsonUtil.ArrayToVector4(instance.Unknown6);
        Unknown7 = JsonUtil.ArrayToVector4(instance.Unknown7);
        Unknown8 = JsonUtil.ArrayToVector4(instance.Unknown8);
        Unknown9 = JsonUtil.ArrayToVector4(instance.Unknown9);
        Unknown10 = JsonUtil.ArrayToVector4(instance.Unknown10);
        Unknown11 = JsonUtil.ArrayToVector4(instance.Unknown11);
        Unknown11 = JsonUtil.ArrayToVector4(instance.Unknown11);
        RGBA = JsonUtil.ArrayToVector4(instance.RGBA);


        ModelID = instance.ModelID;
        PrevInstance = instance.PrevInstance;
        NextInstance = instance.NextInstance;

        var TempPos = InstancePosition;
        var TempScale = scale;

        transform.localPosition = InstancePosition;
        transform.localEulerAngles = rotation;
        transform.localScale = scale;


        UnknownInt26 = instance.UnknownInt26;
        UnknownInt27 = instance.UnknownInt27;
        UnknownInt28 = instance.UnknownInt28;
        ModelID2 = instance.ModelID2;
        UnknownInt30 = instance.UnknownInt30;
        UnknownInt31 = instance.UnknownInt31;
        UnknownInt32 = instance.UnknownInt32;

        LTGState = instance.LTGState;

        LoadPrefabs();

        IsLoaded = true;

    }

    public void LoadPrefabs()
    {
        if(Prefab!=null)
        {
            Destroy(Prefab);
        }

        if (ModelID != -1)
        {
            Prefab = Instantiate(PrefabManager.Instance.GetPrefabGameObject(ModelID));
            Prefab.transform.parent = transform;
            Prefab.transform.localRotation = new Quaternion(0, 0, 0, 0);
            Prefab.transform.localPosition = new Vector3(0, 0, 0);
            Prefab.transform.localScale = new Vector3(1, 1, 1);
        }
        //Generate Collisions

    }

    public InstanceJsonHandler.InstanceJson GenerateInstance()
    {
        InstanceJsonHandler.InstanceJson TempInstance = new InstanceJsonHandler.InstanceJson();
        TempInstance.InstanceName = InstanceName;

        TempInstance.Location = JsonUtil.Vector3ToArray(InstancePosition);
        TempInstance.Scale = JsonUtil.Vector3ToArray(scale);
        TempInstance.Rotation = JsonUtil.QuaternionToArray(Quaternion.Euler(rotation));

        TempInstance.Unknown5 = JsonUtil.Vector4ToArray(Unknown5);
        TempInstance.Unknown6 = JsonUtil.Vector4ToArray(Unknown6);
        TempInstance.Unknown7 = JsonUtil.Vector4ToArray(Unknown7);
        TempInstance.Unknown8 = JsonUtil.Vector4ToArray(Unknown8);
        TempInstance.Unknown9 = JsonUtil.Vector4ToArray(Unknown9);
        TempInstance.Unknown10 = JsonUtil.Vector4ToArray(Unknown10);
        TempInstance.Unknown11 = JsonUtil.Vector4ToArray(Unknown11);
        TempInstance.RGBA = JsonUtil.Vector4ToArray(RGBA);

        TempInstance.ModelID = ModelID;
        TempInstance.PrevInstance = PrevInstance;
        TempInstance.NextInstance = NextInstance;

        IsLoaded = false;
        transform.localScale = scale;
        transform.localPosition = InstancePosition;

        transform.localPosition = InstancePosition * TrickyProjectWindow.Scale;
        transform.localScale = scale * TrickyProjectWindow.Scale;

        IsLoaded = true;

        TempInstance.UnknownInt26 = UnknownInt26;
        TempInstance.UnknownInt27 = UnknownInt27;
        TempInstance.UnknownInt28 = UnknownInt28;
        TempInstance.ModelID2 = ModelID2;
        TempInstance.UnknownInt30 = UnknownInt30;
        TempInstance.UnknownInt31 = UnknownInt31;
        TempInstance.UnknownInt32 = UnknownInt32;

        TempInstance.LTGState = LTGState;

        return TempInstance;
    }

    public void SetUpdateMeshes(int NewMeshID)
    {
        int Test = ModelID;
        try 
        {
            ModelID = NewMeshID;
            LoadPrefabs();
            UpdateXYZ = true;
        }
        catch
        {
            ModelID = Test;
        }
    }
}
