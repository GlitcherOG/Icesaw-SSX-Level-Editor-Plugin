using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static SSXMultiTool.JsonFiles.Tricky.PrefabJsonHandler;

[ExecuteInEditMode]
public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance;
    GameObject PrefabsHolder;
    GameObject MaterialHolder;

    public void SetStatic()
    {
        if (Instance == null)
            Instance = this;
    }

    public void GenerateEmptyObjects()
    {
        transform.hideFlags = HideFlags.HideInInspector;

        PrefabsHolder = new GameObject("Prefabs");
        PrefabsHolder.transform.parent = transform;
        PrefabsHolder.transform.hideFlags = HideFlags.HideInInspector;

        MaterialHolder = new GameObject("Materials");
        MaterialHolder.transform.parent = transform;
        MaterialHolder.transform.hideFlags = HideFlags.HideInInspector;
    }

    public void LoadData(string Path)
    {
        SetStatic();
        LoadMaterials(Path + "\\Materials.json");
        LoadPrefabs(Path + "\\Prefabs.json");
    }

    public void LoadMaterials(string Path)
    {
        MaterialJsonHandler materialJsonHandler = new MaterialJsonHandler();
        materialJsonHandler = MaterialJsonHandler.Load(Path);

        for (int i = 0; i < materialJsonHandler.Materials.Count; i++)
        {
            GameObject gameObject = new GameObject("Materials " + i);
            gameObject.transform.hideFlags = HideFlags.HideInInspector;
            gameObject.transform.parent = MaterialHolder.transform;
            gameObject.transform.localPosition = new Vector3(0, 0, 0);
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            MaterialObject materialObject = gameObject.AddComponent<MaterialObject>();
            materialObject.LoadMaterial(materialJsonHandler.Materials[i]);

        }
    }

    public void LoadPrefabs(string Path)
    {
        float XPosition = 0;
        float ZPosition = 0;
        int X = 0;

        PrefabJsonHandler PrefabJson = PrefabJsonHandler.Load(Path);
        int WH = (int)Mathf.Sqrt(PrefabJson.Prefabs.Count);
        for (int i = 0; i < PrefabJson.Prefabs.Count; i++)
        {
            var TempModelJson = PrefabJson.Prefabs[i];
            GameObject gameObject = new GameObject("Prefab " + i);
            gameObject.transform.hideFlags = HideFlags.HideInInspector;
            gameObject.transform.parent = PrefabsHolder.transform;
            gameObject.transform.localPosition = new Vector3(XPosition, -ZPosition, 0);
            gameObject.transform.localEulerAngles = new Vector3(0,0, 0);
            gameObject.transform.localScale = new Vector3(1,1,1);
            PrefabObject mObject = gameObject.AddComponent<PrefabObject>();
            mObject.LoadPrefab(TempModelJson);

            if(X!=WH)
            {
                XPosition += 10000;
                X++;
            }
            else
            {
                XPosition = 0;
                X = 0;
                ZPosition += 10000;
            }
        }
    }

    public MaterialObject GetMaterialObject(int A)
    {
        MaterialObject[] TempObject = MaterialHolder.transform.GetComponentsInChildren<MaterialObject>(); 

        return TempObject[A];
    }

    public GameObject GetPrefabGameObject(int A)
    {
        PrefabObject[] TempObject = PrefabsHolder.transform.GetComponentsInChildren<PrefabObject>();
        return TempObject[A].gameObject;
    }
}
