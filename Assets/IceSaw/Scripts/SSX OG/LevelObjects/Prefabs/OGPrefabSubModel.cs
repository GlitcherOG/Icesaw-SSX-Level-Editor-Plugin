using SSXMultiTool.JsonFiles.SSXOG;
using SSXMultiTool.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OGPrefabSubModel : MonoBehaviour
{
    public string MeshPath;

    public int U10;

    public int U12;
    public int MaterialID;
    public int U14;

    public int U16;

    public MatrixData matrixData;
    public bool IncludeMatrixData;

    Mesh mesh;
    Material material;

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;

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

    public void LoadSubModel(PrefabJsonHandler.ObjectHeader objectHeader)
    {
        MeshPath = objectHeader.MeshPath;

        U10 = objectHeader.U10;
        MaterialID = objectHeader.MaterialID;
        U12 = objectHeader.U12;
        U14 = objectHeader.U14;

        U16 = objectHeader.U16;

        if(objectHeader.matrixData!=null)
        {
            IncludeMatrixData = true;
            transform.localPosition = JsonUtil.ArrayToVector3(objectHeader.matrixData.Value.Location);
            transform.localRotation = JsonUtil.ArrayToQuaternion(objectHeader.matrixData.Value.Rotation);
            transform.localScale = JsonUtil.ArrayToVector3(objectHeader.matrixData.Value.Scale);
        }
        GenerateModel();
    }

    [ContextMenu("Refresh Models")]
    public void GenerateModel()
    {
        if (!transform.parent.GetComponent<OGPrefabObject>().SkyboxModel)
        {
            mesh = OGPrefabManager.Instance.GetMesh(MeshPath);
        }
        else
        {
            mesh = SkyboxManager.Instance.GetMesh(MeshPath);
        }
        material = GenerateMaterial(MaterialID, transform.parent.GetComponent<OGPrefabObject>().SkyboxModel);

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
                TextureID = OGPrefabManager.Instance.GetMaterialObject(MaterialID).TexturePath;
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
                for (int i = 0; i < OGLevelManager.Instance.texture2Ds.Count; i++)
                {
                    if (OGLevelManager.Instance.texture2Ds[i].Name.ToLower() == TextureID.ToLower())
                    {
                        texture = OGLevelManager.Instance.texture2Ds[i].Texture;
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
            texture = OGLevelManager.Instance.Error;
        }
        catch
        {
            texture = OGLevelManager.Instance.Error;
        }
        return texture;
    }

    public struct MatrixData
    {
        public float[] Location;
        public float[] Rotation;
        public float[] Scale;

        //16
        public int U0;
        public int U2;
        public int U3;
        public int U4;
        public int U5;
        public int U6;
        public int U7;

        public List<UStruct0> uStruct0s;
    }
    [Serializable]
    public struct UStruct0
    {
        public List<UStruct1> uStruct1s;
    }
    [Serializable]
    public struct UStruct1
    {
        public float[] vector30; //vector 3
        public float[] vector31; //vector 3
        public int U0;
        public int U1;
    }
}
