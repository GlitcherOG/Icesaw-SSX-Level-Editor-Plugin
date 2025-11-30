using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.Tricky;

public class PrefabMeshObject : PrefabMeshBase
{
    public override ObjectType Type
    {
        get { return ObjectType.PrefabMesh; }
    }

    [OnChangedCall("GenerateModel")]
    public TrickyMaterialObject TrickyMaterialObject;
    public void LoadPrefabMeshObject(ModelJsonHandler.MeshHeader objectHeader)
    {
        AddMissingComponents();
        MeshPath = objectHeader.MeshPath;
        MaterialIndex = objectHeader.MaterialID;
    }

    public void PostLoad(TrickyMaterialObject[] MaterialObjects)
    {
        if (MaterialObjects.Length - 1 >= MaterialIndex && MaterialIndex != -1)
        {
            TrickyMaterialObject = MaterialObjects[MaterialIndex];
        }

        GenerateModel();
    }

    public ModelJsonHandler.MeshHeader GeneratePrefabMesh()
    {
        ModelJsonHandler.MeshHeader meshHeader = new ModelJsonHandler.MeshHeader();
        meshHeader.MeshPath = MeshPath;

        if (TrickyMaterialObject != null)
        {
            meshHeader.MaterialID = TrickyMaterialObject.transform.GetSiblingIndex();
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
        mesh = TrickyLevelManager.Instance.GetMesh(MeshPath);

        material = GenerateMaterial(TrickyMaterialObject);

        AddMissingComponents();

        meshFilter.sharedMesh = mesh;
        meshRenderer.material = material;
    }

    public static Material GenerateMaterial(TrickyMaterialObject trickyMaterialObject)
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
                for (int i = 0; i < TrickyLevelManager.Instance.texture2ds.Count; i++)
                {
                    if (TrickyLevelManager.Instance.texture2ds[i].Name.ToLower() == TextureID.ToLower())
                    {
                        texture = TrickyLevelManager.Instance.texture2ds[i].Texture;
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

    public PrefabMeshObject[] GetPrefabMesh()
    {
        return GetComponentsInChildren<PrefabMeshObject>();
    }
}
