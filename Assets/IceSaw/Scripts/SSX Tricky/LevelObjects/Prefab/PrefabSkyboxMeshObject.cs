using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.Tricky;

public class PrefabSkyboxMeshObject : PrefabMeshBase
{
    public override ObjectType Type
    {
        get { return ObjectType.SkyboxPrefabMesh; }
    }
    [OnChangedCall("GenerateModel")]
    public TrickySkyboxMaterialObject TrickyMaterialObject;

    public void LoadPrefabMeshObject(PrefabJsonHandler.MeshHeader objectHeader)
    {
        AddMissingComponents();
        MeshPath = objectHeader.MeshPath;
        MaterialIndex = objectHeader.MaterialID;
    }

    public void PostLoad(TrickySkyboxMaterialObject[] MaterialObjects)
    {
        if (MaterialObjects.Length - 1 >= MaterialIndex && MaterialIndex != -1)
        {
            TrickyMaterialObject = MaterialObjects[MaterialIndex];
        }

        GenerateModel();
    }

    public PrefabJsonHandler.MeshHeader GeneratePrefabMesh()
    {
        PrefabJsonHandler.MeshHeader meshHeader = new PrefabJsonHandler.MeshHeader();
        meshHeader.MeshPath = MeshPath;

        if (TrickyMaterialObject != null)
        {
            meshHeader.MaterialID = TrickyLevelManager.Instance.dataManager.GetSkyboxMaterialID(TrickyMaterialObject);
        }
        else
        {
            meshHeader.MaterialID = -1;
        }

        return meshHeader;
    }
    [ContextMenu("Refresh Models")]
    public void GenerateModel()
    {
        mesh = TrickyLevelManager.Instance.GetSkyboxMesh(MeshPath);
        material = GenerateMaterial(TrickyMaterialObject);

        AddMissingComponents();

        meshFilter.sharedMesh = mesh;
        meshRenderer.material = material;
    }

    public static Material GenerateMaterial(TrickySkyboxMaterialObject trickyMaterialObject)
    {
        Material material = new Material(Shader.Find("NewModelShader"));
        string TextureID = "";
        if (trickyMaterialObject != null)
        {
            TextureID = trickyMaterialObject.TexturePath;
        }
        material.SetTexture("_MainTexture", GetTexture(TextureID));
        //material.SetFloat("_NoLightMode", 1);
        return material;
    }

    public static Texture2D GetTexture(string TextureID)
    {
        Texture2D texture = null;
        try
        {
            for (int i = 0; i < TrickyLevelManager.Instance.SkyboxTextures2d.Count; i++)
            {
                if (TrickyLevelManager.Instance.SkyboxTextures2d[i].Name.ToLower() == TextureID.ToLower())
                {
                    texture = TrickyLevelManager.Instance.SkyboxTextures2d[i].Texture;
                    return texture;
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
}
