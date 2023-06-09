using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using static SSXMultiTool.JsonFiles.Tricky.PrefabJsonHandler;

[System.Serializable]
[SelectionBase]
public class PrefabObject : MonoBehaviour
{
    public int Unknown3;
    public float AnimTime;

    public bool SkyboxModel;
    public GameObject GeneratePrefab()
    {
        GameObject MainObject = new GameObject(transform.name);
        MainObject.transform.hideFlags = HideFlags.HideInHierarchy;

        var TempList = GetComponentsInChildren<PrefabSubObject>();

        for (int i = 0; i < TempList.Length; i++)
        {
            var TempModel = TempList[i].GenerateSubObject();
            TempModel.transform.parent = MainObject.transform;
            TempModel.transform.localPosition = TempList[i].transform.localPosition;
            TempModel.transform.localScale = TempList[i].transform.localScale;
            TempModel.transform.localRotation = TempList[i].transform.localRotation;
        }

        return MainObject;
    }

    public void LoadPrefab(PrefabJsonHandler.PrefabJson prefabJson, bool Skybox = false)
    {
        SkyboxModel = Skybox;

        if (!Skybox)
        {
            transform.name = prefabJson.PrefabName;
        }
        Unknown3 = prefabJson.Unknown3;
        AnimTime = prefabJson.AnimTime;
        
        for (int i = 0; i < prefabJson.PrefabObjects.Count; i++)
        {
            GameObject ChildMesh = new GameObject(i.ToString());

            ChildMesh.transform.parent = transform;
            ChildMesh.transform.localPosition = Vector3.zero;
            ChildMesh.transform.localScale = Vector3.one;
            ChildMesh.transform.localRotation = new Quaternion(0,0,0,0);

            ChildMesh.AddComponent<PrefabSubObject>().LoadPrefabSubModel(prefabJson.PrefabObjects[i]);
        }

    }

    public PrefabJsonHandler.PrefabJson GeneratePrefabs(bool Skybox = false)
    {
        PrefabJsonHandler.PrefabJson prefabJson = new PrefabJson();

        if(!Skybox)
        {
            prefabJson.PrefabName = transform.name;
        }

        prefabJson.Unknown3 = Unknown3;
        prefabJson.AnimTime = AnimTime;
        prefabJson.PrefabObjects = new List<PrefabJsonHandler.ObjectHeader>();

        var TempList = GetComponentsInChildren<PrefabSubObject>();

        for (int i = 0; i < TempList.Length; i++)
        {
            prefabJson.PrefabObjects.Add(TempList[i].GeneratePrefabSubModel());
        }

        return prefabJson;
    }

    public void ForceReloadMeshMat()
    {
        var TempHeader = GetComponentsInChildren<PrefabSubObject>();

        for (int i = 0; i < TempHeader.Length; i++)
        {
            TempHeader[i].ForceRegenMeshMat();
        }
    }
}
