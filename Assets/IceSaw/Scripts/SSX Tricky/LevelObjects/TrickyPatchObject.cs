using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using UnityEditor;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class TrickyPatchObject : MonoBehaviour
{
    NURBS.Surface surface;
    public Vector4 LightMapPoint;
    //[Space(10)]
    [SerializeField]
    [OnChangedCall("LoadUVMap")]
    public Vector2 UVPoint1 = new Vector2(1, 0);
    [SerializeField]
    [OnChangedCall("LoadUVMap")]
    public Vector2 UVPoint2 = new Vector2(1, -1);
    [SerializeField]
    [OnChangedCall("LoadUVMap")]
    public Vector2 UVPoint3 = new Vector2(0, 0);
    [SerializeField]
    [OnChangedCall("LoadUVMap")]
    public Vector2 UVPoint4 = new Vector2(0, -1);

    [Space(10)]
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawControlPoint = new Vector3(0,0,0);
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR1C2 = new Vector3(250,0,0);
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR1C3 = new Vector3(500,0,0);
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR1C4 = new Vector3(750,0,0);
    [Space(5)]
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR2C1 = new Vector3(0,250,0);
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR2C2 = new Vector3(250, 250, 0);
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR2C3 = new Vector3(500, 250, 0);
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR2C4 = new Vector3(750, 250, 0);
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    [Space(5)]
    public Vector3 RawR3C1 = new Vector3(0, 500, 0);
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR3C2 = new Vector3(250, 500, 0);
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR3C3 = new Vector3(500, 500, 0);
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR3C4 = new Vector3(750, 500, 0);
    [Space(5)]
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR4C1 = new Vector3(0, 750, 0);
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR4C2 = new Vector3(250, 750, 0);
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR4C3 = new Vector3(500, 750, 0);
    [SerializeField]
    [OnChangedCall("LoadNURBSpatch")]
    public Vector3 RawR4C4 = new Vector3(750, 750, 0);

    [Space(10)]
    public PatchType PatchStyle;
    public bool TrickOnlyPatch;
    [SerializeField]
    [OnChangedCall("UpdateTexture")]
    public string TextureAssigment;
    [SerializeField]
    [OnChangedCall("UpdateTexture")]
    public int LightmapID;

    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    [SerializeField]
    [HideInInspector] public Vector3 LocalR1C2 = new Vector3(250, 0, 0);
    [HideInInspector] public Vector3 LocalR1C3 = new Vector3(500, 0, 0);
    [HideInInspector] public Vector3 LocalR1C4 = new Vector3(750, 0, 0);
    [HideInInspector] public Vector3 LocalR2C1 = new Vector3(0, -250, 0);
    [HideInInspector] public Vector3 LocalR2C2 = new Vector3(250, -250, 0);
    [HideInInspector] public Vector3 LocalR2C3 = new Vector3(500, -250, 0);
    [HideInInspector] public Vector3 LocalR2C4 = new Vector3(750, -250, 0);
    [HideInInspector] public Vector3 LocalR3C1 = new Vector3(0, -500, 0);
    [HideInInspector] public Vector3 LocalR3C2 = new Vector3(250, -500, 0);
    [HideInInspector] public Vector3 LocalR3C3 = new Vector3(500, -500, 0);
    [HideInInspector] public Vector3 LocalR3C4 = new Vector3(750, -500, 0);
    [HideInInspector] public Vector3 LocalR4C1 = new Vector3(0, -750, 0);
    [HideInInspector] public Vector3 LocalR4C2 = new Vector3(250, -750, 0);
    [HideInInspector] public Vector3 LocalR4C3 = new Vector3(500, -750, 0);
    [HideInInspector] public Vector3 LocalR4C4  = new Vector3(750, -750, 0);

    public void Awake()
    {
        Undo.undoRedoPerformed += UndoAndRedoFix;
    }

    [ContextMenu("Add Missing Components")]
    public void AddMissingComponents()
    {
        if(meshFilter!=null)
        {
            DestroyImmediate(meshFilter);
            DestroyImmediate(meshRenderer);
        }

        meshFilter = this.AddComponent<MeshFilter>();
        meshRenderer = this.AddComponent<MeshRenderer>();

        meshFilter.hideFlags = HideFlags.HideInInspector;
        meshRenderer.hideFlags = HideFlags.HideInInspector;
        //Set Material
        var TempMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets\\IceSaw\\Material\\MainPatchMaterial.mat", typeof(Material));
        Material mat = new Material(TempMaterial);
        meshRenderer.material = mat;
        //Undo.undoRedoPerformed += UndoAndRedoFix;
        UpdateTexture();
    }
    public void LoadPatch(PatchesJsonHandler.PatchJson import)
    {
        transform.name = import.PatchName;
        LightMapPoint = JsonUtil.ArrayToVector4(import.LightMapPoint);

        UVPoint1 = new Vector2(import.UVPoints[0, 0], import.UVPoints[0, 1]);
        UVPoint2 = new Vector2(import.UVPoints[1, 0], import.UVPoints[1, 1]);
        UVPoint3 = new Vector2(import.UVPoints[2, 0], import.UVPoints[2, 1]);
        UVPoint4 = new Vector2(import.UVPoints[3, 0], import.UVPoints[3, 1]);

        RawR4C4 = new Vector3(import.Points[15, 0], import.Points[15, 1], import.Points[15, 2]);
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

        PatchStyle = (PatchType)import.PatchStyle;
        TrickOnlyPatch = import.TrickOnlyPatch;
        TextureAssigment = import.TexturePath;
        LightmapID = import.LightmapID;
        transform.localPosition = RawControlPoint;

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

        patch.UVPoints = new float[4, 2];

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
        patch.Points[14, 2] = RawR4C3.z;

        patch.Points[15, 0] = RawR4C4.x;
        patch.Points[15, 1] = RawR4C4.y;
        patch.Points[15, 2] = RawR4C4.z;

        patch.PatchStyle = (int)PatchStyle;
        patch.TrickOnlyPatch = TrickOnlyPatch;
        patch.TexturePath = TextureAssigment;
        patch.LightmapID = LightmapID;

        return patch;
    }

    Vector3 ConvertLocalPoint(Vector3 point)
    {
        if (TrickyLevelManager.Instance != null)
        {
            return transform.InverseTransformPoint(TrickyLevelManager.Instance.transform.TransformPoint(point));
        }

        return transform.InverseTransformPoint(point);
    }

    Vector3 ConvertWorldPoint(Vector3 point)
    {
        if(TrickyLevelManager.Instance!=null)
        {
            return TrickyLevelManager.Instance.transform.InverseTransformPoint(transform.TransformPoint(point));
        }

        return transform.TransformPoint(point);
    }

    public void LoadNURBSpatch()
    {
        Vector3[,] vertices = new Vector3[4, 4];

        LocalR1C2 = ConvertLocalPoint(RawR1C2);
        LocalR1C3 = ConvertLocalPoint(RawR1C3);
        LocalR1C4 = ConvertLocalPoint(RawR1C4);

        LocalR2C1 = ConvertLocalPoint(RawR2C1);
        LocalR2C2 = ConvertLocalPoint(RawR2C2);
        LocalR2C3 = ConvertLocalPoint(RawR2C3);
        LocalR2C4 = ConvertLocalPoint(RawR2C4);

        LocalR3C1 = ConvertLocalPoint(RawR3C1);
        LocalR3C2 = ConvertLocalPoint(RawR3C2);
        LocalR3C3 = ConvertLocalPoint(RawR3C3);
        LocalR3C4 = ConvertLocalPoint(RawR3C4);

        LocalR4C1 = ConvertLocalPoint(RawR4C1);
        LocalR4C2 = ConvertLocalPoint(RawR4C2);
        LocalR4C3 = ConvertLocalPoint(RawR4C3);
        LocalR4C4 = ConvertLocalPoint(RawR4C4);

        //Vertices
        vertices[0, 0] = ConvertLocalPoint(RawControlPoint);
        vertices[0, 1] = ConvertLocalPoint(RawR1C2);
        vertices[0, 2] = ConvertLocalPoint(RawR1C3);
        vertices[0, 3] = ConvertLocalPoint(RawR1C4);

        vertices[1, 0] = ConvertLocalPoint(RawR2C1);
        vertices[1, 1] = ConvertLocalPoint(RawR2C2);
        vertices[1, 2] = ConvertLocalPoint(RawR2C3);
        vertices[1, 3] = ConvertLocalPoint(RawR2C4);

        vertices[2, 0] = ConvertLocalPoint(RawR3C1);
        vertices[2, 1] = ConvertLocalPoint(RawR3C2);
        vertices[2, 2] = ConvertLocalPoint(RawR3C3);
        vertices[2, 3] = ConvertLocalPoint(RawR3C4);

        vertices[3, 0] = ConvertLocalPoint(RawR4C1);
        vertices[3, 1] = ConvertLocalPoint(RawR4C2);
        vertices[3, 2] = ConvertLocalPoint(RawR4C3);
        vertices[3, 3] = ConvertLocalPoint(RawR4C4);

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
        var mesh = surface.BuildMesh(resolutionU, resolutionV);

        //Set material
        if (meshFilter != null)
        {
            meshFilter.mesh = mesh;
        }
        else
        {
            meshFilter = GetComponent<MeshFilter>();
        }

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

        int resolutionU = 7; //7;
        int resolutionV = 7; //7; ()

        Vector3[] UV = surface.ReturnVertices(resolutionU, resolutionV);

        Vector2[] UV2 = new Vector2[UV.Length];

        for (int i = 0; i < UV.Length; i++)
        {
            UV2[i] = new Vector2(UV[i].x, UV[i].y);
        }
        meshFilter.sharedMesh.uv = UV2;
    }

    public void LoadLightmap()
    {
        //Build Lightmap Points
        NURBS.ControlPoint[,] cps = new NURBS.ControlPoint[2, 2];

        cps[0, 0] = new NURBS.ControlPoint(0.1f, 0.1f, 0, 1);
        cps[1, 0] = new NURBS.ControlPoint(0.1f, 0.9f, 0, 1);
        cps[0, 1] = new NURBS.ControlPoint(0.9f, 0.1f, 0, 1);
        cps[1, 1] = new NURBS.ControlPoint(0.9f, 0.9f, 0, 1);

        surface = new NURBS.Surface(cps, 1, 1);

        int resolutionU = 7; //7;
        int resolutionV = 7; //7; ()

        Vector3[] UV = surface.ReturnVertices(resolutionU, resolutionV);

        Vector2[] UV2 = new Vector2[UV.Length];

        for (int i = 0; i < UV.Length; i++)
        {
            UV2[i] = new Vector2(UV[i].x, UV[i].y);
        }

        meshFilter.sharedMesh.uv2 = UV2;
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
            for (int i = 0; i < TrickyLevelManager.Instance.texture2Ds.Count; i++)
            {
                if (TrickyLevelManager.Instance.texture2Ds[i].Name.ToLower() == TextureAssigment.ToLower())
                {
                    Found = true;
                    meshRenderer.sharedMaterial.SetTexture("_MainTexture", TrickyLevelManager.Instance.texture2Ds[i].Texture);

                    if (TrickyLevelManager.Instance.lightmaps.Count - 1 >= LightmapID)
                    {
                        meshRenderer.sharedMaterial.SetTexture("_Lightmap", TrickyLevelManager.Instance.GrabLightmapTexture(LightMapPoint, LightmapID));
                    }
                    return;
                }
            }

            if (!Found)
            {
                meshRenderer.sharedMaterial.SetTexture("_MainTexture", TrickyLevelManager.Instance.Error);
            }
            else
            {

            }
        }
        catch
        {
            meshRenderer.sharedMaterial.SetTexture("_MainTexture", TrickyLevelManager.Instance.Error);
        }
    }

    public void UpdateHighlight(UnityEngine.Color color)
    {
        meshRenderer.sharedMaterial.SetColor("_Highlight", color);
    }

    void UndoAndRedoFix()
    {
        FixNURBPoints();
        LoadNURBSpatch();
    }

    public void FixNURBPoints()
    {
        RawControlPoint = ConvertWorldPoint(new Vector3(0,0,0));
        RawR1C2 = ConvertWorldPoint(LocalR1C2);
        RawR1C3 = ConvertWorldPoint(LocalR1C3);
        RawR1C4 = ConvertWorldPoint(LocalR1C4);

        RawR2C1 = ConvertWorldPoint(LocalR2C1);
        RawR2C2 = ConvertWorldPoint(LocalR2C2);
        RawR2C3 = ConvertWorldPoint(LocalR2C3);
        RawR2C4 = ConvertWorldPoint(LocalR2C4);

        RawR3C1 = ConvertWorldPoint(LocalR3C1);
        RawR3C2 = ConvertWorldPoint(LocalR3C2);
        RawR3C3 = ConvertWorldPoint(LocalR3C3);
        RawR3C4 = ConvertWorldPoint(LocalR3C4);

        RawR4C1 = ConvertWorldPoint(LocalR4C1);
        RawR4C2 = ConvertWorldPoint(LocalR4C2);
        RawR4C3 = ConvertWorldPoint(LocalR4C3);
        RawR4C4 = ConvertWorldPoint(LocalR4C4);
    }

    [ContextMenu("RotateUV Left")]
    public void RotateUVLeft()
    {
        Vector2 Temp1 = UVPoint1;
        Vector2 Temp2 = UVPoint2;
        Vector2 Temp3 = UVPoint3;
        Vector2 Temp4 = UVPoint4;

        UVPoint1 = new Vector2(Temp4.y, Temp1.x);
        UVPoint2 = new Vector2(Temp1.y, Temp2.x);
        UVPoint3 = new Vector2(Temp2.y, Temp3.x);
        UVPoint4 = new Vector2(Temp3.y, Temp4.x);
        LoadUVMap();
    }

    [ContextMenu("RotateUV Right")]
    public void RotateUVRight()
    {
        Vector2 Temp1 = UVPoint1;
        Vector2 Temp2 = UVPoint2;
        Vector2 Temp3 = UVPoint3;
        Vector2 Temp4 = UVPoint4;

        UVPoint1 = new Vector2(Temp1.y,Temp2.x);
        UVPoint2 = new Vector2(Temp2.y, Temp3.x);
        UVPoint3 = new Vector2(Temp3.y, Temp4.x);
        UVPoint4 = new Vector2(Temp4.y, Temp1.x);
        LoadUVMap();
    }

    [ContextMenu("Flip Patch")]
    public void FlipPatch()
    {
        var Temp1 = RawControlPoint;
        var Temp2 = RawR1C2;
        var Temp3 = RawR1C3;
        var Temp4 = RawR1C4;
        var Temp5 = RawR2C1;
        var Temp6 = RawR2C2;
        var Temp7 = RawR2C3;
        var Temp8 = RawR2C4;
        var Temp9 = RawR3C1;
        var Temp10 = RawR3C2;
        var Temp11 = RawR3C3;
        var Temp12 = RawR3C4;
        var Temp13 = RawR4C1;
        var Temp14 = RawR4C2;
        var Temp15 = RawR4C3;
        var Temp16 = RawR4C4;

        RawControlPoint = Temp4;
        RawR1C2 = Temp3;
        RawR1C3 = Temp2;
        RawR1C4 = Temp1;
        RawR2C1 = Temp8;
        RawR2C2 = Temp7;
        RawR2C3 = Temp6;
        RawR2C4 = Temp5;
        RawR3C1 = Temp12;
        RawR3C2 = Temp11;
        RawR3C3 = Temp10;
        RawR3C4 = Temp9;
        RawR4C1 = Temp16;
        RawR4C2 = Temp15;
        RawR4C3 = Temp14;
        RawR4C4 = Temp13;

        LoadNURBSpatch();
    }
    [HideInInspector]
    public bool Hold = false;
    [ContextMenu("Reset Transform")]
    public void TransformReset()
    {
        Hold = true;
        transform.localRotation = new Quaternion(0,0,0,0);
        transform.localScale = new Vector3(1,1,1);
        LoadNURBSpatch();
        transform.hasChanged = false;
        Hold = false;
    }

    public void ToggleLightingMode(bool Lightmap)
    {
        if (Lightmap)
        {
            meshRenderer.sharedMaterial.SetFloat("_LightMapStrength", 1f);
        }
        else
        {
            meshRenderer.sharedMaterial.SetFloat("_LightMapStrength", 0);
        }
    }

    [ContextMenu("Force Regen")]
    public void ForceRegeneration()
    {
        LoadNURBSpatch();
    }

    [MenuItem("GameObject/Ice Saw/Patch", false, 10)]
    public static void CreatePatch(MenuCommand menuCommand)
    {
        GameObject TempObject = new GameObject("Patch");
        if (menuCommand.context != null)
        {
            var AddToObject = (GameObject)menuCommand.context;
            TempObject.transform.parent = AddToObject.transform;
        }
        TempObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        TempObject.transform.localScale = new Vector3(1,1,1);
        Selection.activeGameObject = TempObject;
        TempObject.AddComponent<TrickyPatchObject>().AddMissingComponents();

    }

    public ObjExporter.MassModelData GenerateModel()
    {
        ObjExporter.MassModelData TempModel = new ObjExporter.MassModelData();
        TempModel.Name = gameObject.name;

        //Go through and update points so they are correct for rotation and then regenerate normals
        var OldMesh = meshFilter.sharedMesh;
        var TempMesh = new Mesh();

        var Verts = OldMesh.vertices;
        for (int i = 0; i < Verts.Length; i++)
        {
            Verts[i] = transform.TransformPoint(Verts[i]);
        }
        TempMesh.vertices = Verts;
        TempMesh.uv = OldMesh.uv;
        TempMesh.normals = OldMesh.normals;
        TempMesh.triangles = OldMesh.triangles;
        TempMesh.Optimize();
        TempMesh.RecalculateNormals();
        
        TempModel.Model = TempMesh;
        TempModel.TextureName = TextureAssigment;
        
        return TempModel;
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }
    bool SetOnce = false;
    bool PrevSelected = false;
    private void OnSceneGUI(SceneView sceneView)
    {
        if (transform.hasChanged && !Hold)
        {
            FixNURBPoints();
            LoadNURBSpatch();
            transform.hasChanged = false;
        }
        if (Selection.activeObject == this.gameObject)
        {
            PrevSelected = true;
            if (!TrickyLevelManager.Instance.EditMode && SetOnce)
            {
                SetOnce = false;
                Tools.current = Tool.Move;
            }
            else if (TrickyLevelManager.Instance.EditMode)
            {
                SetOnce = true;
            }
        }
        if (Selection.activeObject == this.gameObject && !Hold)
        {
            LoadNURBSpatch();
        }
        if(PrevSelected && Selection.activeObject != this.gameObject && SetOnce)
        {
            Tools.current = Tool.Move;
            PrevSelected = false;
            SetOnce = false;
        }
    }

    public enum PatchType
    {
        Reset,
        StandardSnow,
        StandardOffTrack,
        PoweredSnow,
        SlowPoweredSnow,
        IceStandard,
        BounceUnskiable,
        IceWaterNoTrail,
        GlidyPoweredSnow,
        Rock,
        Wall,
        IceNoTrail,
        SmallParticleWake,
        OffTrackMetal,
        MetalGliding,
        Standard1,
        StandardSand,
        NoCollision,
        ShowOffRampMetal
    }
}

[CustomEditor(typeof(TrickyPatchObject))]
public class TrickyPatchObjectEditor : Editor
{
    //public override void OnInspectorGUI()
    //{
    //    DrawDefaultInspector();

    //    //Component.hideFlags = HideFlags.HideInInspector;

    //    if(EditorGUILayout.LinkButton("Refresh Textures"))
    //    {
    //        var Temp = (typeof(LevelManager))serializedObject.targetObject;
    //    }
    //}

    public VisualTreeAsset m_InspectorXML;

    public override VisualElement CreateInspectorGUI()
    {
        m_InspectorXML = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets\\IceSaw\\Scripts\\SSX Tricky\\LevelObjects\\Inspectors\\PatchObjects.uxml");

        // Create a new VisualElement to be the root of our inspector UI
        VisualElement myInspector = new VisualElement();
        m_InspectorXML.CloneTree(myInspector);

        VisualElement inspectorGroup = myInspector.Q("Default_Inspector");

        MonoBehaviour monoBev = (MonoBehaviour)target;
        TrickyPatchObject PatchObject = monoBev.GetComponent<TrickyPatchObject>();

        TextElement Details = new TextElement();
        Details.style.fontSize = 16;
        Details.text = "Patch ID " + PatchObject.transform.GetSiblingIndex();

        inspectorGroup.Add(Details);

        InspectorElement.FillDefaultInspector(inspectorGroup, serializedObject, this);

        VisualElement ButtonGroup = myInspector.Q("UVGroup");

        VisualElement UVLeftButton = ButtonGroup.Q("UVRotateLeft");
        var TempButton = UVLeftButton.Query<Button>();
        TempButton.First().RegisterCallback<ClickEvent>(UVRotateLeft);

        VisualElement UVRightButton = ButtonGroup.Q("UVRotateRight");
        TempButton = UVRightButton.Query<Button>();
        TempButton.First().RegisterCallback<ClickEvent>(UVRotateRight);

        VisualElement FlipPatchButton = myInspector.Q("FlipPatch");
        TempButton = FlipPatchButton.Query<Button>();
        TempButton.First().RegisterCallback<ClickEvent>(FlipPatch);

        VisualElement ResetTransformButton = myInspector.Q("ResetTransform");
        TempButton = ResetTransformButton.Query<Button>();
        TempButton.First().RegisterCallback<ClickEvent>(ResetTransform);

        VisualElement ForceRegenerateButton = myInspector.Q("ForceRegenerate");
        TempButton = ForceRegenerateButton.Query<Button>();
        TempButton.First().RegisterCallback<ClickEvent>(ForceRegenerate);

        VisualElement AddMissingButton = myInspector.Q("AddMissing");
        TempButton = AddMissingButton.Query<Button>();
        TempButton.First().RegisterCallback<ClickEvent>(AddMissing);

        // Return the finished inspector UI
        return myInspector;
    }

    private void UVRotateLeft(ClickEvent evt)
    {
        serializedObject.targetObject.GetComponent<TrickyPatchObject>().RotateUVLeft();
    }

    private void UVRotateRight(ClickEvent evt)
    {
        serializedObject.targetObject.GetComponent<TrickyPatchObject>().RotateUVRight();
    }

    private void FlipPatch(ClickEvent evt)
    {
        serializedObject.targetObject.GetComponent<TrickyPatchObject>().FlipPatch();
    }

    private void ResetTransform(ClickEvent evt)
    {
        serializedObject.targetObject.GetComponent<TrickyPatchObject>().TransformReset();
    }

    private void ForceRegenerate(ClickEvent evt)
    {
        serializedObject.targetObject.GetComponent<TrickyPatchObject>().ForceRegeneration();
    }

    private void AddMissing(ClickEvent evt)
    {
        serializedObject.targetObject.GetComponent<TrickyPatchObject>().AddMissingComponents();
    }

    private Vector3[,] positions;
    void OnSceneGUI()
    {
        TrickyPatchObject connectedObjects = target as TrickyPatchObject;
        if (!connectedObjects.Hold)
        {
            if (TrickyLevelManager.Instance.EditMode)
            {
                Tools.current = Tool.None;
                positions = new Vector3[4,4];
                //Collect All Points
                positions[0,0] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.RawControlPoint);
                positions[0, 1] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.RawR1C2);
                positions[0, 2] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.RawR1C3);
                positions[0, 3] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.RawR1C4);

                positions[1, 0] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.RawR2C1);
                positions[1, 1] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.RawR2C2);
                positions[1, 2] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.RawR2C3);
                positions[1, 3] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.RawR2C4);

                positions[2, 0] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.RawR3C1);
                positions[2, 1] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.RawR3C2);
                positions[2, 2] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.RawR3C3);
                positions[2, 3] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.RawR3C4);

                positions[3, 0] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.RawR4C1);
                positions[3, 1] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.RawR4C2);
                positions[3, 2] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.RawR4C3);
                positions[3, 3] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.RawR4C4);

                Handles.color = UnityEngine.Color.blue;
                //Draw Lines
                for (int i = 0; i < 4; i++)
                {
                    Handles.DrawLine(positions[i, 0], positions[i, 1], 6f);
                    Handles.DrawLine(positions[i, 1], positions[i, 2], 6f);
                    Handles.DrawLine(positions[i, 2], positions[i, 3], 6f);
                }

                for (int i = 0; i < 4; i++)
                {
                    Handles.DrawLine(positions[0, i], positions[1, i], 6f);
                    Handles.DrawLine(positions[1, i], positions[2, i], 6f);
                    Handles.DrawLine(positions[2, i], positions[3, i], 6f);
                }
                Handles.color = UnityEngine.Color.white;
                //Draw Movement Points

                for (int i = 0; i < 4; i++)
                {
                    for (int a = 0; a < 4; a++)
                    {
                        Vector3 TestVector = Handles.PositionHandle(positions[i,a], Quaternion.identity);
                        if(TestVector != positions[i,a])
                        {
                            if(i==0&&a==0)
                            {
                                connectedObjects.RawControlPoint = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                            }
                            if (i == 0 && a == 1)
                            {
                                connectedObjects.RawR1C2 = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                            }
                            if (i == 0 && a == 2)
                            {
                                connectedObjects.RawR1C3 = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                            }
                            if (i == 0 && a == 3)
                            {
                                connectedObjects.RawR1C4 = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                            }
                            if (i == 1 && a == 0)
                            {
                                connectedObjects.RawR2C1 = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                            }
                            if (i == 1 && a == 1)
                            {
                                connectedObjects.RawR2C2 = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                            }
                            if (i == 1 && a == 2)
                            {
                                connectedObjects.RawR2C3 = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                            }
                            if (i == 1 && a == 3)
                            {
                                connectedObjects.RawR2C4 = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                            }
                            if (i == 2 && a == 0)
                            {
                                connectedObjects.RawR3C1 = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                            }
                            if (i == 2 && a == 1)
                            {
                                connectedObjects.RawR3C2 = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                            }
                            if (i == 2 && a == 2)
                            {
                                connectedObjects.RawR3C3 = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                            }
                            if (i == 2 && a == 3)
                            {
                                connectedObjects.RawR3C4 = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                            }
                            if (i == 3 && a == 0)
                            {
                                connectedObjects.RawR4C1 = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                            }
                            if (i == 3 && a == 1)
                            {
                                connectedObjects.RawR4C2 = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                            }
                            if (i == 3 && a == 2)
                            {
                                connectedObjects.RawR4C3 = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                            }
                            if (i == 3 && a == 3)
                            {
                                connectedObjects.RawR4C4 = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                            }
                        }
                    }
                }
            }
        }
    }
}