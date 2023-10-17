using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PrefabMeshObject : MonoBehaviour
{
    [OnChangedCall("GenerateModel")]
    public string MeshPath;
    public int MeshID;
    [HideInInspector]
    public Mesh mesh;
    [OnChangedCall("GenerateModel")]
    public int MaterialID;
    [HideInInspector]
    public Material material;

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;

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

    public void LoadPrefabMeshObject(PrefabJsonHandler.MeshHeader objectHeader)
    {
        AddMissingComponents();
        MeshPath = objectHeader.MeshPath;
        MeshID = objectHeader.MeshID;
        MaterialID = objectHeader.MaterialID;
        GenerateModel();
    }

    public PrefabJsonHandler.MeshHeader GeneratePrefabMesh()
    {
        PrefabJsonHandler.MeshHeader meshHeader = new PrefabJsonHandler.MeshHeader();
        meshHeader.MeshPath = MeshPath;
        meshHeader.MeshID = MeshID;
        meshHeader.MaterialID = MaterialID;

        return meshHeader;
    }
    [ContextMenu("Refresh Models")]
    public void GenerateModel()
    {
        if (!transform.parent.parent.GetComponent<PrefabObject>().SkyboxModel)
        {
            mesh = TrickyPrefabManager.Instance.GetMesh(MeshPath);
        }
        else
        {
            mesh = SkyboxManager.Instance.GetMesh(MeshPath);
        }
        material = GenerateMaterial(MaterialID, transform.parent.parent.GetComponent<PrefabObject>().SkyboxModel);

        AddMissingComponents();

        meshFilter.sharedMesh = mesh;
        meshRenderer.material = material;
    }

    public static Material GenerateMaterial(int MaterialID, bool Skybox)
    {
        Material material = new Material(Shader.Find("ModelShader"));
        string TextureID = "";
        if (MaterialID != -1)
        {
            if (!Skybox)
            {
                TextureID = TrickyPrefabManager.Instance.GetMaterialObject(MaterialID).TexturePath;
            }
            else
            {
                TextureID = SkyboxManager.Instance.GetMaterialObject(MaterialID).TexturePath;
            }
        }
        material.SetTexture("_MainTexture", GetTexture(TextureID, Skybox));
        material.SetFloat("_OutlineWidth", 0);
        material.SetFloat("_OpacityMaskOutline", 0f);
        material.SetColor("_OutlineColor", new Color32(255, 255, 255, 0));
        material.SetFloat("_NoLightMode", 1);
        return material;
    }

    public static Texture2D GetTexture(string TextureID, bool Skybox)
    {
        Texture2D texture = null;
        try
        {
            if (!Skybox)
            {
                for (int i = 0; i < TrickyLevelManager.Instance.texture2Ds.Count; i++)
                {
                    if (TrickyLevelManager.Instance.texture2Ds[i].Name.ToLower() == TextureID.ToLower())
                    {
                        texture = TrickyLevelManager.Instance.texture2Ds[i].Texture;
                        return texture;
                    }
                }
            }
            else
            {
                for (int i = 0; i < SkyboxManager.Instance.SkyboxTextures2d.Count; i++)
                {
                    if (SkyboxManager.Instance.SkyboxTextures2d[i].Name.ToLower() == TextureID.ToLower())
                    {
                        texture = SkyboxManager.Instance.SkyboxTextures2d[i].Texture;
                        return texture;
                    }
                }
            }
            texture = TrickyLevelManager.Instance.Error;
        }
        catch
        {
            texture = TrickyLevelManager.Instance.Error;
        }
        return texture;
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
