using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        PrefabsHolder.transform.hideFlags = HideFlags.HideInInspector;

        MaterialHolder = new GameObject("Materials");
        MaterialHolder.transform.parent = transform;
        MaterialHolder.transform.hideFlags = HideFlags.HideInInspector;
    }

    public void LoadData()
    {

    }

    public void LoadMaterials(string Path)
    {
        
    }

    public void LoadPrefabs(string Path)
    {

    }
}
