using SSXMultiTool.JsonFiles.SSXOG;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class OGMaterialObject : MonoBehaviour
{
    public bool SkyboxMaterial;

    public int U0;
    [OnChangedCall("GenerateMaterialSphere")]
    public string TexturePath;
    public int U2;
    public int U3;

    MeshRenderer meshRenderer;
    MeshFilter meshFilter;

    [ContextMenu("Add Missing Components")]
    public void AddMissingComponents()
    {
        if (meshFilter != null)
        {
            Destroy(meshFilter);
            Destroy(meshRenderer);
        }

        meshFilter = this.AddComponent<MeshFilter>();
        meshRenderer = this.AddComponent<MeshRenderer>();

        meshFilter.sharedMesh = (Mesh)AssetDatabase.LoadAssetAtPath("Assets\\IceSaw\\Mesh\\Sphere.obj", typeof(Mesh));

        meshFilter.hideFlags = HideFlags.HideInInspector;
        meshRenderer.hideFlags = HideFlags.HideInInspector;
        //Set Material
        var TempMaterial = new Material(Shader.Find("ModelShader"));
        Material mat = new Material(TempMaterial);
        meshRenderer.material = mat;
    }

    public void LoadMaterial(MaterialsJsonHandler.MaterialJson json, bool skybox = false)
    {
        AddMissingComponents();

        if (json.MaterialName != "" && json.MaterialName != null)
        {
            gameObject.name = json.MaterialName;
        }
        U0 = json.U0;
        TexturePath = json.TexturePath;
        U2 = json.U2;
        U3 = json.U3;

        SkyboxMaterial = skybox;

        GenerateMaterialSphere();
    }

    public void GenerateMaterialSphere()
    {
        meshRenderer.sharedMaterial.SetTexture("_MainTexture", GetTexture(TexturePath, SkyboxMaterial));
        meshRenderer.sharedMaterial.SetFloat("_OutlineWidth", 0);
        meshRenderer.sharedMaterial.SetFloat("_OpacityMaskOutline", 0f);
        meshRenderer.sharedMaterial.SetColor("_OutlineColor", new Color32(255, 255, 255, 0));
        meshRenderer.sharedMaterial.SetFloat("_NoLightMode", 1);
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
                //for (int i = 0; i < SkyboxManager.Instance.SkyboxTextures2d.Count; i++)
                //{
                //    if (SkyboxManager.Instance.SkyboxTextures2d[i].Name.ToLower() == TextureID.ToLower())
                //    {
                //        texture = SkyboxManager.Instance.SkyboxTextures2d[i].Texture;
                //        return texture;
                //    }
                //}
            }
            texture = OGLevelManager.Instance.Error;
        }
        catch
        {
            texture = OGLevelManager.Instance.Error;
        }
        return texture;
    }

    public MaterialsJsonHandler.MaterialJson GenerateMaterial()
    {
        var NewJson = new MaterialsJsonHandler.MaterialJson();

        NewJson.MaterialName = transform.name;
        NewJson.U0 = U0;
        NewJson.TexturePath = TexturePath;
        NewJson.U2 = U2;
        NewJson.U3 = U3;

        return NewJson;
    }

    //[MenuItem("GameObject/Ice Saw/OG/Material", false, 104)]
    public static void CreateMaterialObject(MenuCommand menuCommand)
    {
        GameObject TempObject = new GameObject("Material");
        if (menuCommand.context != null)
        {
            var AddToObject = (GameObject)menuCommand.context;
            TempObject.transform.parent = AddToObject.transform;
        }
        TempObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        TempObject.transform.localScale = new Vector3(1, 1, 1);
        Selection.activeGameObject = TempObject;
        TempObject.AddComponent<OGMaterialObject>().AddMissingComponents();
    }
}
