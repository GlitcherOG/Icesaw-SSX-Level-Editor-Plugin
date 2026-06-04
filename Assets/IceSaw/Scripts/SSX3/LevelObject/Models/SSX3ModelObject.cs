using SSXMultiTool.JsonFiles.SSX3;
using SSXMultiTool.Utilities;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static SSXMultiTool.JsonFiles.SSX3.MDRJsonHandler;

[ExecuteInEditMode]
[SelectionBase]
public class SSX3ModelObject : MonoBehaviour
{
    public int TrackID;
    public int RID;

    public int U3;
    public int U4;

    public float U6;

    public List<ObjectID> U12;


    public void LoadModel(MDRJsonHandler.MainModelHeader model)
    {
        transform.name = model.Name;

        TrackID = model.TrackID;
        RID = model.RID;

        U3 = model.U3;
        U4 = model.U4;

        U6 = model.U6;

        U12 = model.U12;

        for (int i = 0; i < model.ModelObjects.Count; i++)
        {
            GameObject ChildMesh = new GameObject(i.ToString());

            ChildMesh.transform.parent = transform;
            ChildMesh.transform.localPosition = Vector3.zero;
            ChildMesh.transform.localScale = Vector3.one;
            ChildMesh.transform.localRotation = new Quaternion(0, 0, 0, 0);

            ChildMesh.AddComponent<SSX3ModelMeshObject>().LoadPrefab(model.ModelObjects[i]);

        }
    }

    public GameObject GemerateModel()
    {
        GameObject MainObject = new GameObject(transform.name);
        MainObject.transform.hideFlags = HideFlags.HideInHierarchy;
        var TempList = GetComponentsInChildren<SSX3ModelMeshObject>();

        for (int i = 0; i < TempList.Length; i++)
        {
            GameObject ChildMesh = new GameObject(i.ToString());
            ChildMesh.transform.parent = MainObject.transform;
            ChildMesh.transform.localPosition = TempList[i].transform.localPosition;
            ChildMesh.transform.localScale = TempList[i].transform.localScale;
            ChildMesh.transform.localRotation = Quaternion.identity; //TempList[i].transform.localRotation;
            var TempMeshFilter = ChildMesh.AddComponent<MeshFilter>();
            var TempRenderer = ChildMesh.AddComponent<MeshRenderer>();
            //ChildMesh.AddComponent<SelectParent>();
            TempMeshFilter.mesh = TempList[i].mesh;
            TempRenderer.material = SSX3ModelMeshObject.GenerateMaterial(U12[0].RID, this.gameObject);
        }

        return MainObject;
    }
}