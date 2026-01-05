using SSXMultiTool.JsonFiles.SSX3;
using SSXMultiTool.Utilities;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static SSXMultiTool.JsonFiles.SSX3.MDRJsonHandler;

[ExecuteInEditMode]
[SelectionBase]
public class SSX3ModelMeshObject : MonoBehaviour
{
    public int ParentID;

    public UnknownS2 unknownS2;
    public UnknownS3 unknownS3;

    public string ModelPath;

    [HideInInspector]
    public Mesh mesh;

    [HideInInspector]
    public Material material;

    [HideInInspector]
    public MeshFilter meshFilter;
    [HideInInspector]
    public MeshRenderer meshRenderer;

    [ContextMenu("Add Missing Components")]
    public void AddMissingComponents()
    {
        if (meshFilter != null)
        {
            DestroyImmediate(meshFilter);
        }
        if (meshRenderer != null)
        {
            DestroyImmediate(meshRenderer);
        }

        meshFilter = transform.AddComponent<MeshFilter>();
        meshRenderer = transform.AddComponent<MeshRenderer>();

        meshFilter.hideFlags = HideFlags.HideInInspector;
        meshRenderer.hideFlags = HideFlags.HideInInspector;
    }

    public void LoadPrefab(MDRJsonHandler.ModelObject model)
    {
        AddMissingComponents();

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

        LoadMesh();
    }

    public void LoadMesh()
    {
        if (ModelPath != "")
        {
            var ssx3LevelManager = SSX3LevelManager.GetLevelManager(this.gameObject);

            mesh = ssx3LevelManager.GetMesh(ModelPath);
        }

        material = new Material(Shader.Find("Standard"));

        material.color = Color.gray; // Set the material color to gray
        material.SetFloat("_Smoothness", 0.5f); // Adjust smoothness

        meshFilter.sharedMesh = mesh;
        meshRenderer.sharedMaterial = material;
    }
}