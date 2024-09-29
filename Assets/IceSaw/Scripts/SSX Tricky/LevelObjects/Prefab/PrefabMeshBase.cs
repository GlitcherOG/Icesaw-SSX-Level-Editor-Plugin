using SSXMultiTool.JsonFiles.Tricky; 
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PrefabMeshBase : TrickyBaseObject
{
    [OnChangedCall("GenerateModel")]
    public string MeshPath;
    [HideInInspector]
    public Mesh mesh;

    [HideInInspector]
    public int MaterialIndex;
    [HideInInspector]
    public Material material;

    [HideInInspector]
    public MeshFilter meshFilter;
    [HideInInspector]
    public MeshRenderer meshRenderer;

    [ContextMenu("Add Missing Components")]
    public void AddMissingComponents()
    {
        if(meshFilter!=null)
        {
            DestroyImmediate(meshFilter);
        }
        if(meshRenderer!=null)
        {
            DestroyImmediate(meshRenderer);
        }

        meshFilter = transform.AddComponent<MeshFilter>();
        meshRenderer = transform.AddComponent<MeshRenderer>();

        meshFilter.hideFlags = HideFlags.HideInInspector;
        meshRenderer.hideFlags = HideFlags.HideInInspector;
    }

    [MenuItem("GameObject/Ice Saw/Prefab Mesh Object", false, 103)]
    public static void CreatePrefabMeshObject(MenuCommand menuCommand)
    {
        GameObject TempObject = new GameObject("Prefab Mesh Object");
        if (menuCommand.context != null)
        {
            var AddToObject = (GameObject)menuCommand.context;
            TempObject.transform.parent = AddToObject.transform;
        }
        TempObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        TempObject.transform.localScale = new Vector3(1, 1, 1);
        Selection.activeGameObject = TempObject;
        TempObject.AddComponent<PrefabMeshObject>().AddMissingComponents();

    }
}
