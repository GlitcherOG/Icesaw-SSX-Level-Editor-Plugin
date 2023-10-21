using SSXMultiTool.JsonFiles.SSXOG;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OGPrefabObject : MonoBehaviour
{
    public bool SkyboxModel;

    public int U1;
    public int U2;
    public int U3;
    public int U4;

    public GameObject GeneratePrefab()
    {
        GameObject MainObject = new GameObject(transform.name);
        MainObject.transform.hideFlags = HideFlags.HideInHierarchy;

        var TempList = GetComponentsInChildren<OGPrefabSubModel>();

        for (int i = 0; i < TempList.Length; i++)
        {
            GameObject ChildMesh = new GameObject(transform.name);
            ChildMesh.transform.parent = MainObject.transform;
            ChildMesh.transform.localPosition = TempList[i].transform.localPosition;
            ChildMesh.transform.localScale = TempList[i].transform.localScale;
            ChildMesh.transform.localRotation = TempList[i].transform.localRotation;
            var TempMeshFilter = ChildMesh.AddComponent<MeshFilter>();
            var TempRenderer = ChildMesh.AddComponent<MeshRenderer>();
            TempMeshFilter.mesh = TempList[i].mesh;
            TempRenderer.material = TempList[i].material;

        }

        return MainObject;
    }

    public void LoadPrefab(PrefabJsonHandler.PrefabJson prefabJson, bool Skybox = false)
    {
        SkyboxModel = Skybox;

        if (!Skybox)
        {
            //transform.name = prefabJson.nam;
        }

        U1 = prefabJson.U1;
        U2 = prefabJson.U2;
        U3 = prefabJson.U3;
        U4 = prefabJson.U4;



        for (int i = 0; i < prefabJson.models.Count; i++)
        {
            GameObject ChildMesh = new GameObject(i.ToString());

            ChildMesh.transform.parent = transform;
            ChildMesh.transform.localPosition = Vector3.zero;
            ChildMesh.transform.localScale = Vector3.one;
            ChildMesh.transform.localRotation = new Quaternion(0, 0, 0, 0);

            ChildMesh.AddComponent<OGPrefabSubModel>().LoadSubModel(prefabJson.models[i]);
        }

    }

    //public PrefabJsonHandler.PrefabJson GeneratePrefabs(bool Skybox = false)
    //{
    //    PrefabJsonHandler.PrefabJson prefabJson = new PrefabJson();

    //    if (!Skybox)
    //    {
    //        prefabJson.PrefabName = transform.name;
    //    }

    //    prefabJson.Unknown3 = Unknown3;
    //    prefabJson.AnimTime = AnimTime;
    //    prefabJson.PrefabObjects = new List<PrefabJsonHandler.ObjectHeader>();

    //    var TempList = GetComponentsInChildren<PrefabSubObject>();

    //    for (int i = 0; i < TempList.Length; i++)
    //    {
    //        prefabJson.PrefabObjects.Add(TempList[i].GeneratePrefabSubModel());
    //    }

    //    return prefabJson;
    //}

    //public PrefabSubObject[] GetPrefabSubObject()
    //{
    //    return GetComponentsInChildren<PrefabSubObject>();
    //}

    public void ForceReloadMeshMat()
    {
        var TempHeader = GetComponentsInChildren<OGPrefabSubModel>();

        for (int i = 0; i < TempHeader.Length; i++)
        {
            TempHeader[i].GenerateModel();
        }
    }


    [MenuItem("GameObject/Ice Saw/Prefab Object", false, 101)]
    public static void CreatePatch(MenuCommand menuCommand)
    {
        GameObject TempObject = new GameObject("PrefabObject");
        if (menuCommand.context != null)
        {
            var AddToObject = (GameObject)menuCommand.context;
            TempObject.transform.parent = AddToObject.transform;
        }
        TempObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        TempObject.transform.localScale = new Vector3(1, 1, 1);
        Selection.activeGameObject = TempObject;
        TempObject.AddComponent<OGPrefabObject>();

    }

    public string[] GetTextureNames()
    {
        List<string> TextureNames = new List<string>();
        var TempList = GetComponentsInChildren<OGPrefabSubModel>();

        for (int i = 0; i < TempList.Length; i++)
        {
            var TempSubModel = TempList[i].MaterialID;
            TextureNames.Add(OGPrefabManager.Instance.GetMaterialObject(TempSubModel).TexturePath);
        }
        return TextureNames.ToArray();
    }
    [ContextMenu("Test If Used")]
    public void TestIfUsed()
    {
        var TempList = OGWorldManager.Instance.GetInstanceList();

        int ID = this.transform.GetSiblingIndex();
        int Used = 0;
        string Instance = "";

        for (int i = 0; i < TempList.Length; i++)
        {
            if(TempList[i].PrefabID == ID)
            {
                Instance += i + ", ";
                Used++;
            }
        }

        Debug.Log(Used + "(" + Instance + ")");
    }
}
