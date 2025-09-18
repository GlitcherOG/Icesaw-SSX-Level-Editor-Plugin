using SSXMultiTool.JsonFiles.SSX3;
using SSXMultiTool.Utilities;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static SSXMultiTool.JsonFiles.SSX3.MDRJsonHandler;

[ExecuteInEditMode]
[SelectionBase]
public class SSX3PrefabObject : MonoBehaviour
{
    public int TrackID;
    public int RID;

    public int U3;
    public int U4;

    public float U6;
    public float U7;
    public float U8;
    public float U9;

    public List<int> U12;


    public void LoadPrefab(MDRJsonHandler.MainModelHeader model)
    {
        TrackID = model.TrackID;
        RID = model.RID;

        U3 = model.U3;
        U4 = model.U4;

        U6 = model.U6;
        U7 = model.U7;
        U8 = model.U8;
        U9 = model.U9;

        U12 = model.U12;

        for (int i = 0; i < model.ModelObjects.Count; i++)
        {
            GameObject ChildMesh = new GameObject(i.ToString());

            ChildMesh.transform.parent = transform;
            ChildMesh.transform.localPosition = Vector3.zero;
            ChildMesh.transform.localScale = Vector3.one;
            ChildMesh.transform.localRotation = new Quaternion(0, 0, 0, 0);

            ChildMesh.AddComponent<SSX3PrefabMeshObject>().LoadPrefab(model.ModelObjects[i]);
        }
    }
}