using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TrickyPrefabSubBase : TrickyBaseObject
{
    public int ParentID;
    public int Flags;

    public ObjectAnimation Animation = new ObjectAnimation();

    public bool IncludeAnimation;
    public bool IncludeMatrix;

    public void ForceRegenMeshMat()
    {
        var TempMeshList = GetComponentsInChildren<PrefabMeshObject>();

        for (int i = 0; i < TempMeshList.Length; i++)
        {
            TempMeshList[i].GenerateModel();
        }
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
