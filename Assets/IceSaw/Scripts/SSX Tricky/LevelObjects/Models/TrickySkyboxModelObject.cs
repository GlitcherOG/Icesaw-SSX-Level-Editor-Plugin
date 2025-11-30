using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TrickySkyboxModelObject : TrickyModelBase
{
    public override ObjectType Type
    {
        get { return ObjectType.SkyboxPrefab; }
    }

    public GameObject GeneratePrefab()
    {
        GameObject MainObject = new GameObject(transform.name);
        MainObject.transform.hideFlags = HideFlags.HideInHierarchy;
        var TempList = GetComponentsInChildren<TrickyModelSkyboxSubObject>();

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

    public void LoadPrefab(ModelJsonHandler.ModelJson prefabJson, bool Skybox = false)
    {
        transform.name = prefabJson.ModelName;
        Unknown3 = prefabJson.Unknown3;
        AnimTime = prefabJson.AnimTime;

        for (int i = 0; i < prefabJson.ModelObjects.Count; i++)
        {
            GameObject ChildMesh = new GameObject(i.ToString());

            ChildMesh.transform.parent = transform;
            ChildMesh.transform.localPosition = Vector3.zero;
            ChildMesh.transform.localScale = Vector3.one;
            ChildMesh.transform.localRotation = new Quaternion(0, 0, 0, 0);

            ChildMesh.AddComponent<TrickyModelSkyboxSubObject>().LoadPrefabSubModel(prefabJson.ModelObjects[i]);
        }

    }

    public ModelJsonHandler.ModelJson GeneratePrefabs(bool Skybox = false)
    {
        ModelJsonHandler.ModelJson prefabJson = new ModelJsonHandler.ModelJson();

        prefabJson.ModelName = transform.name;

        prefabJson.Unknown3 = Unknown3;
        prefabJson.AnimTime = AnimTime;
        prefabJson.ModelObjects = new List<ModelJsonHandler.ObjectHeader>();

        var TempList = GetComponentsInChildren<TrickyModelSkyboxSubObject>();

        for (int i = 0; i < TempList.Length; i++)
        {
            prefabJson.ModelObjects.Add(TempList[i].GeneratePrefabSubModel());
        }

        return prefabJson;
    }

    public TrickyModelSkyboxSubObject[] GetPrefabSubObject()
    {
        return GetComponentsInChildren<TrickyModelSkyboxSubObject>();
    }

    public void PostLoad(TrickySkyboxMaterialObject[] MaterialObjects)
    {
        var TempList = GetComponentsInChildren<TrickyModelSkyboxSubObject>();

        for (int i = 0; i < TempList.Length; i++)
        {
            TempList[i].PostLoad(MaterialObjects);
        }
    }

    [MenuItem("GameObject/Ice Saw/Skybox Model Object", false, 101)]
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
        TempObject.AddComponent<TrickySkyboxModelObject>();
    }
}
