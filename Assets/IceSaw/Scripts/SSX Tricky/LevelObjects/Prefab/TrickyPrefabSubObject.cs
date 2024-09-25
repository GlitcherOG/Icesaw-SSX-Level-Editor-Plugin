using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static SSXMultiTool.JsonFiles.Tricky.PrefabJsonHandler;

public class TrickyPrefabSubObject : MonoBehaviour
{
    public int ParentID;
    public int Flags;

    public ObjectAnimation Animation = new ObjectAnimation();

    public bool IncludeAnimation;
    public bool IncludeMatrix;

    public void LoadPrefabSubModel(PrefabJsonHandler.ObjectHeader objectHeader)
    {
        ParentID = objectHeader.ParentID;
        Flags = objectHeader.Flags;

        IncludeAnimation = objectHeader.IncludeAnimation;
        IncludeMatrix = objectHeader.IncludeMatrix;
        Animation = new ObjectAnimation();

        if (IncludeAnimation)
        {
            Animation.U1 = objectHeader.Animation.Value.U1;
            Animation.U2 = objectHeader.Animation.Value.U2;
            Animation.U3 = objectHeader.Animation.Value.U3;
            Animation.U4 = objectHeader.Animation.Value.U4;
            Animation.U5 = objectHeader.Animation.Value.U5;
            Animation.U6 = objectHeader.Animation.Value.U6;
            Animation.AnimationAction = objectHeader.Animation.Value.AnimationAction;
            Animation.AnimationEntries = new List<AnimationEntry>();
            for (int a = 0; a < objectHeader.Animation.Value.AnimationEntries.Count; a++)
            {
                var TempEntry = new AnimationEntry();
                TempEntry.AnimationMaths = new List<AnimationMath>();

                for (int b = 0; b < objectHeader.Animation.Value.AnimationEntries[a].AnimationMaths.Count; b++)
                {
                    var TempMaths = new AnimationMath();
                    TempMaths.Value1 = objectHeader.Animation.Value.AnimationEntries[a].AnimationMaths[b].Value1;
                    TempMaths.Value2 = objectHeader.Animation.Value.AnimationEntries[a].AnimationMaths[b].Value2;
                    TempMaths.Value3 = objectHeader.Animation.Value.AnimationEntries[a].AnimationMaths[b].Value3;
                    TempMaths.Value4 = objectHeader.Animation.Value.AnimationEntries[a].AnimationMaths[b].Value4;
                    TempMaths.Value5 = objectHeader.Animation.Value.AnimationEntries[a].AnimationMaths[b].Value5;
                    TempMaths.Value6 = objectHeader.Animation.Value.AnimationEntries[a].AnimationMaths[b].Value6;
                    TempEntry.AnimationMaths.Add(TempMaths);
                }
                Animation.AnimationEntries.Add(TempEntry);
            }
        }


        if (IncludeMatrix)
        {
            transform.localPosition = JsonUtil.ArrayToVector3(objectHeader.Position);
            transform.localScale = JsonUtil.ArrayToVector3(objectHeader.Scale);
            transform.localRotation = JsonUtil.ArrayToQuaternion(objectHeader.Rotation);
        }

        //Load MeshHeaders
        for (int i = 0; i < objectHeader.MeshData.Count; i++)
        {
            GameObject ChildMesh = new GameObject(i.ToString());

            ChildMesh.transform.parent = transform;
            ChildMesh.transform.localPosition = Vector3.zero;
            ChildMesh.transform.localScale = Vector3.one;
            ChildMesh.transform.localRotation = new Quaternion(0, 0, 0, 0);

            ChildMesh.AddComponent<PrefabMeshObject>().LoadPrefabMeshObject(objectHeader.MeshData[i]);
        }

    }

    public void PostLoad(TrickyMaterialObject[] MaterialObjects)
    {
        var TempMeshList = GetComponentsInChildren<PrefabMeshObject>();

        for (int i = 0; i < TempMeshList.Length; i++)
        {
            TempMeshList[i].PostLoad(MaterialObjects);
        }
    }

    public PrefabJsonHandler.ObjectHeader GeneratePrefabSubModel()
    {
        PrefabJsonHandler.ObjectHeader objectHeader = new PrefabJsonHandler.ObjectHeader();

        objectHeader.ParentID = ParentID;
        objectHeader.Flags = Flags;

        objectHeader.IncludeAnimation = IncludeAnimation;
        objectHeader.IncludeMatrix = IncludeMatrix;

        if(IncludeAnimation)
        {
            var NewAnimation = new PrefabJsonHandler.ObjectAnimation();
            NewAnimation.U1 = Animation.U1;
            NewAnimation.U2 = Animation.U2;
            NewAnimation.U3 = Animation.U3;
            NewAnimation.U4 = Animation.U4;
            NewAnimation.U5 = Animation.U5;
            NewAnimation.U6 = Animation.U6;
            NewAnimation.AnimationAction = Animation.AnimationAction;
            NewAnimation.AnimationEntries = new List<PrefabJsonHandler.AnimationEntry>();
            for (int a = 0; a < Animation.AnimationEntries.Count; a++)
            {
                var NewAnimEntry = new PrefabJsonHandler.AnimationEntry();
                NewAnimEntry.AnimationMaths = new List<PrefabJsonHandler.AnimationMath>();
                for (int b = 0; b < Animation.AnimationEntries[a].AnimationMaths.Count; b++)
                {
                    var TempMaths = new PrefabJsonHandler.AnimationMath();
                    TempMaths.Value1 = Animation.AnimationEntries[a].AnimationMaths[b].Value1;
                    TempMaths.Value2 = Animation.AnimationEntries[a].AnimationMaths[b].Value2;
                    TempMaths.Value3 = Animation.AnimationEntries[a].AnimationMaths[b].Value3;
                    TempMaths.Value4 = Animation.AnimationEntries[a].AnimationMaths[b].Value4;
                    TempMaths.Value5 = Animation.AnimationEntries[a].AnimationMaths[b].Value5;
                    TempMaths.Value6 = Animation.AnimationEntries[a].AnimationMaths[b].Value6;
                    NewAnimEntry.AnimationMaths.Add(TempMaths);
                }
                NewAnimation.AnimationEntries.Add(NewAnimEntry);
            }
            objectHeader.Animation = NewAnimation;
        }

        if (IncludeMatrix)
        {
            objectHeader.Position = JsonUtil.Vector3ToArray(transform.localPosition);
            objectHeader.Rotation = JsonUtil.QuaternionToArray(transform.localRotation);
            objectHeader.Scale = JsonUtil.Vector3ToArray(transform.localScale);
        }

        var TempMeshList = GetComponentsInChildren<PrefabMeshObject>();
        objectHeader.MeshData = new List<MeshHeader>();
        for (int i = 0; i < TempMeshList.Length; i++)
        {
            objectHeader.MeshData.Add(TempMeshList[i].GeneratePrefabMesh());
        }

        return objectHeader;
    }

    public GameObject GenerateSubObject()
    {
        GameObject MainObject = new GameObject(transform.name);
        MainObject.AddComponent<SelectParent>();
        var MeshObjectList = GetComponentsInChildren<PrefabMeshObject>();

        for (int a = 0; a < MeshObjectList.Length; a++)
        {
            GameObject ChildMesh = new GameObject(a.ToString());
            ChildMesh.transform.parent = MainObject.transform;
            ChildMesh.transform.localPosition = Vector3.zero;
            ChildMesh.transform.localScale = Vector3.one;
            ChildMesh.transform.localRotation = new Quaternion(0, 0, 0, 0);
            var TempMeshFilter = ChildMesh.AddComponent<MeshFilter>();
            var TempRenderer = ChildMesh.AddComponent<MeshRenderer>();
            //ChildMesh.AddComponent<SelectParent>();
            TempMeshFilter.mesh = MeshObjectList[a].mesh;
            TempRenderer.material = PrefabMeshObject.GenerateMaterial(MeshObjectList[a].TrickyMaterialObject, false);//MeshObjectList[a].material;
        }

        return MainObject;
    }

    public void ForceRegenMeshMat()
    {
        var TempMeshList = GetComponentsInChildren<PrefabMeshObject>();

        for (int i = 0; i < TempMeshList.Length; i++)
        {
            TempMeshList[i].GenerateModel();
        }
    }

    public PrefabMeshObject[] GetPrefabMesh()
    {
        return GetComponentsInChildren<PrefabMeshObject>();
    }

    [MenuItem("GameObject/Ice Saw/Prefab Sub Object", false, 102)]
    public static void CreatePrefabSubObject(MenuCommand menuCommand)
    {
        GameObject TempObject = new GameObject("Prefab Sub Object");
        if (menuCommand.context != null)
        {
            var AddToObject = (GameObject)menuCommand.context;
            TempObject.transform.parent = AddToObject.transform;
        }
        TempObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        TempObject.transform.localScale = new Vector3(1, 1, 1);
        Selection.activeGameObject = TempObject;
        TempObject.AddComponent<TrickyPrefabSubObject>();

    }

    [Serializable]
    public struct ObjectAnimation
    {
        public float U1;
        public float U2;
        public float U3;
        public float U4;
        public float U5;
        public float U6;

        public int AnimationAction;
        public List<AnimationEntry> AnimationEntries;
    }
    [Serializable]
    public struct AnimationEntry
    {
        public List<AnimationMath> AnimationMaths;
    }
    [Serializable]
    public struct AnimationMath
    {
        public float Value1;
        public float Value2;
        public float Value3;
        public float Value4;
        public float Value5;
        public float Value6;
    }
}
