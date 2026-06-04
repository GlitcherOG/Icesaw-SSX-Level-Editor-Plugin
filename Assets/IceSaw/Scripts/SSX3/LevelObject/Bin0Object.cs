using NUnit.Framework.Internal;
using SSXMultiTool.JsonFiles.SSX3;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class Bin0Object : MonoBehaviour
{
    public int TrackID;
    public int RID;

    public string TextureName;
    public int U1;
    public int U2;
    public int U3;
    public int U4;
    public int U5;
    public int U6;
    public int U7;
    public int U8;
    public int U9;
    public int U10;

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

    public void LoadBin0(Bin0JsonHandler.Bin0File bin0)
    {
        AddMissingComponents();

        TrackID = bin0.TrackID;
        RID = bin0.RID;

        TextureName = bin0.TextureName;
        U1 = bin0.U1;
        U2 = bin0.U2;
        U3 = bin0.U3;
        U4 = bin0.U4;
        U5 = bin0.U5;
        U6 = bin0.U6;
        U7 = bin0.U7;
        U8 = bin0.U8;
        U9 = bin0.U9;
        U10 = bin0.U10;

        transform.name = RID.ToString();

        GenerateMaterialSphere();
    }

    public void GenerateMaterialSphere()
    {
        meshRenderer.sharedMaterial.SetTexture("_MainTexture", GetTexture(TextureName));
        meshRenderer.sharedMaterial.SetFloat("_NoLightMode", 1);
    }

    public static Texture2D GetTexture(string TextureID)
    {
        Texture2D texture = null;
        try
        {
            //return SSX3WorldManager.Instance.texture2ds[TextureID].Texture;
            for (int i = 0; i < SSX3WorldManager.Instance.texture2ds.Count; i++)
            {
                if (SSX3WorldManager.Instance.texture2ds[i].Name.ToLower() == TextureID.ToLower())
                {
                    texture = SSX3WorldManager.Instance.texture2ds[i].Texture;
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
