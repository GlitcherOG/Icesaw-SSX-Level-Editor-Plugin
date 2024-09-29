using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TrickySkyboxPrefabObject : TrickyPrefabBase
{
    public override ObjectType Type
    {
        get { return ObjectType.SkyboxPrefab; }
    }

    public GameObject GeneratePrefab()
    {
        GameObject MainObject = new GameObject(transform.name);
        MainObject.transform.hideFlags = HideFlags.HideInHierarchy;
        var TempList = GetComponentsInChildren<TrickyPrefabSkyboxSubObject>();

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
        transform.name = prefabJson.PrefabName;
        Unknown3 = prefabJson.Unknown3;
        AnimTime = prefabJson.AnimTime;

        for (int i = 0; i < prefabJson.PrefabObjects.Count; i++)
        {
            GameObject ChildMesh = new GameObject(i.ToString());

            ChildMesh.transform.parent = transform;
            ChildMesh.transform.localPosition = Vector3.zero;
            ChildMesh.transform.localScale = Vector3.one;
            ChildMesh.transform.localRotation = new Quaternion(0, 0, 0, 0);

            ChildMesh.AddComponent<TrickyPrefabSkyboxSubObject>().LoadPrefabSubModel(prefabJson.PrefabObjects[i]);
        }

    }

    public PrefabJsonHandler.PrefabJson GeneratePrefabs(bool Skybox = false)
    {
        PrefabJsonHandler.PrefabJson prefabJson = new PrefabJsonHandler.PrefabJson();

        prefabJson.PrefabName = transform.name;

        prefabJson.Unknown3 = Unknown3;
        prefabJson.AnimTime = AnimTime;
        prefabJson.PrefabObjects = new List<PrefabJsonHandler.ObjectHeader>();

        var TempList = GetComponentsInChildren<TrickyPrefabSkyboxSubObject>();

        for (int i = 0; i < TempList.Length; i++)
        {
            prefabJson.PrefabObjects.Add(TempList[i].GeneratePrefabSubModel());
        }

        return prefabJson;
    }

    public TrickyPrefabSkyboxSubObject[] GetPrefabSubObject()
    {
        return GetComponentsInChildren<TrickyPrefabSkyboxSubObject>();
    }

    public void PostLoad(TrickySkyboxMaterialObject[] MaterialObjects)
    {
        var TempList = GetComponentsInChildren<TrickyPrefabSkyboxSubObject>();

        for (int i = 0; i < TempList.Length; i++)
        {
            TempList[i].PostLoad(MaterialObjects);
        }
    }

    [MenuItem("GameObject/Ice Saw/Skybox Prefab Object", false, 101)]
    public static void CreatePatch(MenuCommand menuCommand)
    {
        GameObject TempObject = new GameObject("SkyboxPrefabObject");
        if (menuCommand.context != null)
        {
            var AddToObject = (GameObject)menuCommand.context;
            TempObject.transform.parent = AddToObject.transform;
        }
        TempObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        TempObject.transform.localScale = new Vector3(1, 1, 1);
        Selection.activeGameObject = TempObject;
        TempObject.AddComponent<TrickySkyboxPrefabObject>();
    }
}
