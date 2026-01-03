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
public class SSX3PrefabMeshObject : MonoBehaviour
{
    public int ParentID;

    public UnknownS2 unknownS2;
    public UnknownS3 unknownS3;

    public string ModelPath;


    public void LoadPrefab(MDRJsonHandler.ModelObject model)
    {
        ParentID = model.ParentID;

        transform.localPosition = JsonUtil.ArrayToVector3(model.Position);
        transform.localRotation = JsonUtil.ArrayToQuaternion(model.Rotation);
        transform.localScale = JsonUtil.ArrayToVector3(model.Scale);

        if(transform.localScale == Vector3.zero)
        {
            transform.localScale = Vector3.one;
        }

        unknownS2 = model.unknownS2;
        unknownS3 = model.unknownS3;

        ModelPath = model.ModelPath;
    }
}