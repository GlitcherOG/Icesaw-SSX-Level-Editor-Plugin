using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using Unity.VisualScripting;
using UnityEditor;
using System.Drawing;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class PatchObject : MonoBehaviour
{
    NURBS.Surface surface;

    public Vector4 LightMapPoint;
    //[Space(10)]
    [SerializeField]
    [OnChangedCall("LoadUVMap")]
    public Vector2 UVPoint1;
    [SerializeField]
    [OnChangedCall("LoadUVMap")]
    public Vector2 UVPoint2;
    [SerializeField]
    [OnChangedCall("LoadUVMap")]
    public Vector2 UVPoint3;
    [SerializeField]
    [OnChangedCall("LoadUVMap")]
    public Vector2 UVPoint4;

    [Space(10)]
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawControlPoint;
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR1C2;
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR1C3;
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR1C4;
    [Space(5)]
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR2C1;
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR2C2;
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR2C3;
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR2C4;
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    [Space(5)]
    public Vector3 RawR3C1;
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR3C2;
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR3C3;
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR3C4;
    [Space(5)]
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR4C1;
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR4C2;
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR4C3;
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR4C4;

    [Space(10)]
    public int PatchStyle;
    public bool TrickOnlyPatch;
    [SerializeField]
    [OnChangedCall("UpdateTexture")]
    public string TextureAssigment;
    [SerializeField]
    [OnChangedCall("UpdateTexture")]
    public int LightmapID;

    MeshRenderer meshRenderer;

    public void AddMissingComponents()
    {
        this.AddComponent<MeshFilter>();
        meshRenderer = this.AddComponent<MeshRenderer>();
        this.AddComponent<MeshCollider>();
        //Set Material
        var TempMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets\\IceSaw\\Material\\MainPatchMaterial.mat", typeof(Material));
        Material mat = new Material(TempMaterial);
        meshRenderer.material = mat;
    }
    public void LoadPatch(PatchesJsonHandler.PatchJson import)
    {
        Undo.undoRedoPerformed += UndoAndRedoFix;
        transform.name = import.PatchName;
        LightMapPoint = JsonUtil.ArrayToVector4(import.LightMapPoint);

        UVPoint1 = new Vector2(import.UVPoints[0, 0], import.UVPoints[0, 1]);
        UVPoint2 = new Vector2(import.UVPoints[1, 0], import.UVPoints[1, 1]);
        UVPoint3 = new Vector2(import.UVPoints[2, 0], import.UVPoints[2, 1]);
        UVPoint4 = new Vector2(import.UVPoints[3, 0], import.UVPoints[3, 1]);

        RawR4C4 = new Vector3(import.Points[15,0], import.Points[15, 1], import.Points[15, 2]);
        RawR4C3 = new Vector3(import.Points[14, 0], import.Points[14, 1], import.Points[14, 2]);
        RawR4C2 = new Vector3(import.Points[13, 0], import.Points[13, 1], import.Points[13, 2]);
        RawR4C1 = new Vector3(import.Points[12, 0], import.Points[12, 1], import.Points[12, 2]);
        RawR3C4 = new Vector3(import.Points[11, 0], import.Points[11, 1], import.Points[11, 2]);
        RawR3C3 = new Vector3(import.Points[10, 0], import.Points[10, 1], import.Points[10, 2]);
        RawR3C2 = new Vector3(import.Points[9, 0], import.Points[9, 1], import.Points[9, 2]);
        RawR3C1 = new Vector3(import.Points[8, 0], import.Points[8, 1], import.Points[8, 2]);
        RawR2C4 = new Vector3(import.Points[7, 0], import.Points[7, 1], import.Points[7, 2]);
        RawR2C3 = new Vector3(import.Points[6, 0], import.Points[6, 1], import.Points[6, 2]);
        RawR2C2 = new Vector3(import.Points[5, 0], import.Points[5, 1], import.Points[5, 2]);
        RawR2C1 = new Vector3(import.Points[4, 0], import.Points[4, 1], import.Points[4, 2]);
        RawR1C4 = new Vector3(import.Points[3, 0], import.Points[3, 1], import.Points[3, 2]);
        RawR1C3 = new Vector3(import.Points[2, 0], import.Points[2, 1], import.Points[2, 2]);
        RawR1C2 = new Vector3(import.Points[1, 0], import.Points[1, 1], import.Points[1, 2]);
        RawControlPoint = new Vector3(import.Points[0, 0], import.Points[0, 1], import.Points[0, 2]);

        PatchStyle = import.PatchStyle;
        TrickOnlyPatch = import.TrickOnlyPatch;
        TextureAssigment = import.TexturePath;
        LightmapID = import.LightmapID;
        //transform.localPosition = RawControlPoint;


        LoadNURBSpatch();
        LoadUVMap();
        LoadLightmap();
        UpdateTexture();
    }

    public PatchesJsonHandler.PatchJson GeneratePatch()
    {
        PatchesJsonHandler.PatchJson patch = new PatchesJsonHandler.PatchJson();
        patch.PatchName = transform.name;
        patch.LightMapPoint = JsonUtil.Vector4ToArray(LightMapPoint);

        patch.UVPoints = new float[4,2];

        patch.UVPoints[0, 0] = UVPoint1.x;
        patch.UVPoints[0, 1] = UVPoint1.y;
        patch.UVPoints[1, 0] = UVPoint2.x;
        patch.UVPoints[1, 1] = UVPoint2.y;
        patch.UVPoints[2, 0] = UVPoint3.x;
        patch.UVPoints[2, 1] = UVPoint3.y;
        patch.UVPoints[3, 0] = UVPoint4.x;
        patch.UVPoints[3, 1] = UVPoint4.y;

        patch.Points = new float[16, 3];

        patch.Points[0, 0] = RawControlPoint.x;
        patch.Points[0, 1] = RawControlPoint.y;
        patch.Points[0, 2] = RawControlPoint.z;

        patch.Points[1, 0] = RawR1C2.x;
        patch.Points[1, 1] = RawR1C2.y;
        patch.Points[1, 2] = RawR1C2.z;

        patch.Points[2, 0] = RawR1C3.x;
        patch.Points[2, 1] = RawR1C3.y;
        patch.Points[2, 2] = RawR1C3.z;

        patch.Points[3, 0] = RawR1C4.x;
        patch.Points[3, 1] = RawR1C4.y;
        patch.Points[3, 2] = RawR1C4.z;

        patch.Points[4, 0] = RawR2C1.x;
        patch.Points[4, 1] = RawR2C1.y;
        patch.Points[4, 2] = RawR2C1.z;

        patch.Points[5, 0] = RawR2C2.x;
        patch.Points[5, 1] = RawR2C2.y;
        patch.Points[5, 2] = RawR2C2.z;

        patch.Points[6, 0] = RawR2C3.x;
        patch.Points[6, 1] = RawR2C3.y;
        patch.Points[6, 2] = RawR2C3.z;

        patch.Points[7, 0] = RawR2C4.x;
        patch.Points[7, 1] = RawR2C4.y;
        patch.Points[7, 2] = RawR2C4.z;

        patch.Points[8, 0] = RawR3C1.x;
        patch.Points[8, 1] = RawR3C1.y;
        patch.Points[8, 2] = RawR3C1.z;

        patch.Points[9, 0] = RawR3C2.x;
        patch.Points[9, 1] = RawR3C2.y;
        patch.Points[9, 2] = RawR3C2.z;

        patch.Points[10, 0] = RawR3C3.x;
        patch.Points[10, 1] = RawR3C3.y;
        patch.Points[10, 2] = RawR3C3.z;

        patch.Points[11, 0] = RawR3C4.x;
        patch.Points[11, 1] = RawR3C4.y;
        patch.Points[11, 2] = RawR3C4.z;

        patch.Points[12, 0] = RawR4C1.x;
        patch.Points[12, 1] = RawR4C1.y;
        patch.Points[12, 2] = RawR4C1.z;

        patch.Points[13, 0] = RawR4C2.x;
        patch.Points[13, 1] = RawR4C2.y;
        patch.Points[13, 2] = RawR4C2.z;

        patch.Points[14, 0] = RawR4C3.x;
        patch.Points[14, 1] = RawR4C3.y;
        patch.Points[15, 2] = RawR4C3.z;

        patch.Points[15, 0] = RawR4C4.x;
        patch.Points[15, 1] = RawR4C4.y;
        patch.Points[15, 2] = RawR4C4.z;

        patch.PatchStyle = PatchStyle;
        patch.TrickOnlyPatch = TrickOnlyPatch;
        patch.TexturePath = TextureAssigment;
        patch.LightmapID = LightmapID;

        return patch;
    }

    public void LoadNURBSpatch()
    {
        Vector3[,] vertices = new Vector3[4, 4];

        //Vertices
        vertices[0, 0] = RawControlPoint;
        vertices[0, 1] = RawR1C2;
        vertices[0, 2] = RawR1C3;
        vertices[0, 3] = RawR1C4;

        vertices[1, 0] = RawR2C1;
        vertices[1, 1] = RawR2C2;
        vertices[1, 2] = RawR2C3;
        vertices[1, 3] = RawR2C4;

        vertices[2, 0] = RawR3C1;
        vertices[2, 1] = RawR3C2;
        vertices[2, 2] = RawR3C3;
        vertices[2, 3] = RawR3C4;

        vertices[3, 0] = RawR4C1;
        vertices[3, 1] = RawR4C2;
        vertices[3, 2] = RawR4C3;
        vertices[3, 3] = RawR4C4;

        //Control points
        NURBS.ControlPoint[,] cps = new NURBS.ControlPoint[4, 4];
        int c = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Vector3 pos = vertices[i, j];
                cps[i, j] = new NURBS.ControlPoint(pos.x, pos.y, pos.z, 1);
                c++;
            }
        }

        int degreeU = 3;
        int degreeV = 3;

        int resolutionU = 7; //7;
        int resolutionV = 7; //7; ()

        surface = new NURBS.Surface(cps, degreeU, degreeV);

        //Update degree
        surface.DegreeU(degreeU);
        surface.DegreeV(degreeV);

        //Update control points
        surface.controlPoints = cps;

        //Build mesh (reusing Mesh to save GC allocation)
        var mesh=surface.BuildMesh(resolutionU, resolutionV);

        //Set material
        GetComponent<MeshFilter>().mesh= mesh;
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        GetComponent<MeshCollider>().enabled = true;

        LoadUVMap();
        LoadLightmap();
    }

    public void LoadUVMap()
    {
        //Build UV Points

        NURBS.ControlPoint[,] cps = new NURBS.ControlPoint[2, 2];

        List<Vector2> vector2s = new List<Vector2>();
        vector2s.Add(UVPoint1);
        vector2s.Add(UVPoint2);
        vector2s.Add(UVPoint3);
        vector2s.Add(UVPoint4);

        vector2s = PointCorrection(vector2s);

        cps[0, 0] = new NURBS.ControlPoint(vector2s[0].x, vector2s[0].y, 0, 1);
        cps[1, 0] = new NURBS.ControlPoint(vector2s[1].x, vector2s[1].y, 0, 1);
        cps[0, 1] = new NURBS.ControlPoint(vector2s[2].x, vector2s[2].y, 0, 1);
        cps[1, 1] = new NURBS.ControlPoint(vector2s[3].x, vector2s[3].y, 0, 1);

        surface = new NURBS.Surface(cps, 1, 1);

        int degreeU = 3;
        int degreeV = 3;

        int resolutionU = 7; //7;
        int resolutionV = 7; //7; ()

        Vector3[] UV = surface.ReturnVertices(resolutionU, resolutionV);

        Vector2[] UV2 = new Vector2[UV.Length];

        for (int i = 0; i < UV.Length; i++)
        {
            UV2[i] = new Vector2(UV[i].x, UV[i].y);
        }
        GetComponent<MeshFilter>().sharedMesh.uv = UV2;
    }

    public void LoadLightmap()
    {
        //Build Lightmap Points
        NURBS.ControlPoint[,] cps = new NURBS.ControlPoint[2, 2];

        cps[0, 0] = new NURBS.ControlPoint(0.1f, 0.1f, 0, 1);
        cps[0, 1] = new NURBS.ControlPoint(0.1f, 0.9f, 0, 1);
        cps[1, 0] = new NURBS.ControlPoint(0.9f, 0.1f, 0, 1);
        cps[1, 1] = new NURBS.ControlPoint(0.9f, 0.9f, 0, 1);

        surface = new NURBS.Surface(cps, 1, 1);

        int degreeU = 3;
        int degreeV = 3;

        int resolutionU = 7; //7;
        int resolutionV = 7; //7; ()

        Vector3[] UV = surface.ReturnVertices(resolutionU, resolutionV);

        Vector2[] UV2 = new Vector2[UV.Length];

        for (int i = 0; i < UV.Length; i++)
        {
            UV2[i] = new Vector2(UV[i].x, UV[i].y);
        }

        GetComponent<MeshFilter>().sharedMesh.uv2 = UV2;
    }
    public List<Vector2> PointCorrection(List<Vector2> NewList)
    {
        for (int i = 0; i < NewList.Count; i++)
        {
            var TempPoint = NewList[i];
            TempPoint.y = -TempPoint.y;
            NewList[i] = TempPoint;
        }

        return NewList;
    }

    public void UpdateTexture()
    {
        try
        {
            bool Found = false;
            for (int i = 0; i < LevelManager.Instance.texture2Ds.Count; i++)
            {
                if (LevelManager.Instance.texture2Ds[i].name.ToLower() == TextureAssigment.ToLower())
                {
                    Found = true;
                    meshRenderer.sharedMaterial.SetTexture("_MainTexture", LevelManager.Instance.texture2Ds[i]);
                    //Renderer.material.SetTexture("_Lightmap", TrickyMapInterface.Instance.GrabLightmapTexture(LightMapPoint, LightmapID));
                    return;
                }
            }

            if (!Found)
            {
                meshRenderer.sharedMaterial.SetTexture("_MainTexture", LevelManager.Instance.Error);
            }
            else
            {

            }
        }
        catch
        {
            meshRenderer.sharedMaterial.SetTexture("_MainTexture", LevelManager.Instance.Error);
        }
    }

    void UndoAndRedoFix()
    {
        LoadNURBSpatch();
    }

    public void OnDrawGizmosSelected()
    {
        if (transform.hasChanged)
        {

        }
    }

    public void RecalculateNurbpoints()
    {
        //take current points 


    }

}
