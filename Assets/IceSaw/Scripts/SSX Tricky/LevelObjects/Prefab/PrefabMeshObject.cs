using SSXMultiTool.JsonFiles.Tricky; 
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PrefabMeshObject : MonoBehaviour
{
    [OnChangedCall("GenerateModel")]
    public string MeshPath;
    [HideInInspector]
    public Mesh mesh;

    [OnChangedCall("GenerateModel")]
    public TrickyMaterialObject TrickyMaterialObject;

    int MaterialIndex;
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

    public PrefabJsonHandler.MeshHeader GeneratePrefabMesh()
    {
        PrefabJsonHandler.MeshHeader meshHeader = new PrefabJsonHandler.MeshHeader();
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
        if (!transform.parent.parent.GetComponent<TrickyPrefabObject>().SkyboxModel)
        {
            mesh = TrickyPrefabManager.Instance.GetMesh(MeshPath);
        }
        else
        {
            mesh = SkyboxManager.Instance.GetMesh(MeshPath);
        }
        material = GenerateMaterial(TrickyMaterialObject, transform.parent.parent.GetComponent<TrickyPrefabObject>().SkyboxModel);

        AddMissingComponents();

        meshFilter.sharedMesh = mesh;
        meshRenderer.material = material;
    }

    public static Material GenerateMaterial(TrickyMaterialObject trickyMaterialObject, bool Skybox)
    {
        Material material = new Material(Shader.Find("NewModelShader"));
        string TextureID = "";
        if(trickyMaterialObject!=null)
        {
            TextureID = trickyMaterialObject.TexturePath;
        }
        material.SetTexture("_MainTexture", GetTexture(TextureID, Skybox));
        //material.SetFloat("_NoLightMode", 1);
        return material;
    }

    public static Texture2D GetTexture(string TextureID, bool Skybox)
    {
        Texture2D texture = null;
        try
        {
            if (!Skybox)
            {
                for (int i = 0; i < TrickyLevelManager.Instance.texture2ds.Count; i++)
                {
                    if (TrickyLevelManager.Instance.texture2ds[i].Name.ToLower() == TextureID.ToLower())
                    {
                        texture = TrickyLevelManager.Instance.texture2ds[i].Texture;
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
