using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SSXMultiTool.JsonFiles.Tricky.PrefabJsonHandler;

[ExecuteInEditMode]
public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance;
    WorldManager WorldManager { get {  return WorldManager.Instance; } }
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
        PrefabsHolder.transform.localScale = new Vector3 (1, 1, 1) * TrickyProjectWindow.Scale;
        PrefabsHolder.transform.eulerAngles = new Vector3(-90, 0, 0);
        PrefabsHolder.transform.hideFlags = HideFlags.HideInInspector;

        MaterialHolder = new GameObject("Materials");
        MaterialHolder.transform.parent = transform;
        MaterialHolder.transform.hideFlags = HideFlags.HideInInspector;
    }

    public void LoadData()
    {
        string LoadPath = TrickyProjectWindow.CurrentPath;


        LoadPrefabs(LoadPath + "\\Prefabs.json");
    }

    public void LoadMaterials(string Path)
    {
        
    }

    public void LoadPrefabs(string Path)
    {
        PrefabJsonHandler PrefabJson = PrefabJsonHandler.Load(Path);
        for (int i = 0; i < PrefabJson.Prefabs.Count; i++)
        {
            var TempModelJson = PrefabJson.Prefabs[i];
            GameObject gameObject = new GameObject("Prefab " + i);
            gameObject.transform.hideFlags = HideFlags.HideInInspector;
            gameObject.transform.parent = PrefabsHolder.transform;
            gameObject.transform.localEulerAngles = new Vector3(0,0,0);
            gameObject.transform.localScale = new Vector3(1,1,1);
            PrefabObject mObject = gameObject.AddComponent<PrefabObject>();
            mObject.LoadPrefab(TempModelJson);
        }
    }
}
