using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
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
            mesh = PrefabManager.Instance.GetMesh(MeshPath);
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
                TextureID = PrefabManager.Instance.GetMaterialObject(MaterialID).TexturePath;
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
                for (int i = 0; i < LevelManager.Instance.texture2Ds.Count; i++)
                {
                    if (LevelManager.Instance.texture2Ds[i].name.ToLower() == TextureID.ToLower())
                    {
                        texture = LevelManager.Instance.texture2Ds[i];
                        return texture;
                    }
                }
            }
            else
            {
                for (int i = 0; i < SkyboxManager.Instance.SkyboxTextures2d.Count; i++)
                {
                    if (SkyboxManager.Instance.SkyboxTextures2d[i].name.ToLower() == TextureID.ToLower())
                    {
                        texture = SkyboxManager.Instance.SkyboxTextures2d[i];
                        return texture;
                    }
                }
            }
            texture = LevelManager.Instance.Error;
        }
        catch
        {
            texture = LevelManager.Instance.Error;
        }
        return texture;
    }
}
