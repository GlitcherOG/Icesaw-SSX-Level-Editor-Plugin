using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using Unity.VisualScripting;
using UnityEditor;
using Newtonsoft.Json.Bson;

[ExecuteInEditMode]
public class PatchObject : MonoBehaviour
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
    MeshFilter meshFilter;
    [SerializeField]
    public Vector3 LocalR1C2;
    public Vector3 LocalR1C3;
    public Vector3 LocalR1C4;
    public Vector3 LocalR2C1;
    public Vector3 LocalR2C2;
    public Vector3 LocalR2C3;
    public Vector3 LocalR2C4;
    public Vector3 LocalR3C1;
    public Vector3 LocalR3C2;
    public Vector3 LocalR3C3;
    public Vector3 LocalR3C4;
    public Vector3 LocalR4C1;
    public Vector3 LocalR4C2;
    public Vector3 LocalR4C3;
    public Vector3 LocalR4C4;


    [ContextMenu("Add Missing Components")]
    public void AddMissingComponents()
    {
        if(meshFilter!=null)
        {
            Destroy(meshFilter);
            Destroy(meshRenderer);
        }

        meshFilter = this.AddComponent<MeshFilter>();
        meshRenderer = this.AddComponent<MeshRenderer>();

        meshFilter.hideFlags = HideFlags.HideInInspector;
        meshRenderer.hideFlags = HideFlags.HideInInspector;
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

        PatchStyle = import.PatchStyle;
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

        patch.PatchStyle = PatchStyle;
        patch.TrickOnlyPatch = TrickOnlyPatch;
        patch.TexturePath = TextureAssigment;
        patch.LightmapID = LightmapID;

        return patch;
    }

    Vector3 ConvertLocalPoint(Vector3 point)
    {
        return transform.InverseTransformPoint(LevelManager.Instance.transform.TransformPoint(point));
    }

    Vector3 ConvertWorldPoint(Vector3 point)
    {
        return LevelManager.Instance.transform.InverseTransformPoint(transform.TransformPoint(point));
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
        meshFilter.mesh = mesh;

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
            for (int i = 0; i < LevelManager.Instance.texture2Ds.Count; i++)
            {
                if (LevelManager.Instance.texture2Ds[i].name.ToLower() == TextureAssigment.ToLower())
                {
                    Found = true;
                    meshRenderer.sharedMaterial.SetTexture("_MainTexture", LevelManager.Instance.texture2Ds[i]);

                    if (LevelManager.Instance.lightmaps.Count - 1 >= LightmapID)
                    {
                        meshRenderer.sharedMaterial.SetTexture("_Lightmap", LevelManager.Instance.GrabLightmapTexture(LightMapPoint, LightmapID));
                    }
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

    public void UpdateHighlight(UnityEngine.Color color)
    {
        meshRenderer.sharedMaterial.SetColor("_Highlight", color);
    }

    void UndoAndRedoFix()
    {
        FixNURBPoints();
        LoadNURBSpatch();
    }

    public void Start()
    {
        
    }

    public void OnDrawGizmos()
    {
        if (transform.hasChanged && !Hold)
        {
            FixNURBPoints();
            LoadNURBSpatch();
            transform.hasChanged = false;
        }
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

        UVPoint1 = Temp4;
        UVPoint2 = Temp1;
        UVPoint3 = Temp2;
        UVPoint4 = Temp3;
        LoadUVMap();
    }

    [ContextMenu("RotateUV Right")]
    public void RotateUVRight()
    {
        Vector2 Temp1 = UVPoint1;
        Vector2 Temp2 = UVPoint2;
        Vector2 Temp3 = UVPoint3;
        Vector2 Temp4 = UVPoint4;

        UVPoint1 = Temp2;
        UVPoint2 = Temp3;
        UVPoint3 = Temp4;
        UVPoint4 = Temp1;
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

        RawControlPoint = Temp16;
        RawR1C2 = Temp15;
        RawR1C3 = Temp14;
        RawR1C4 = Temp13;
        RawR2C1 = Temp12;
        RawR2C2 = Temp11;
        RawR2C3 = Temp10;
        RawR2C4 = Temp9;
        RawR3C1 = Temp8;
        RawR3C2 = Temp7;
        RawR3C3 = Temp6;
        RawR3C4 = Temp5;
        RawR4C1 = Temp4;
        RawR4C2 = Temp3;
        RawR4C3 = Temp2;
        RawR4C4 = Temp1;

        LoadNURBSpatch();
    }

    bool Hold = false;
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
            meshRenderer.sharedMaterial.SetFloat("_LightMapStrength", 1);
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

    [MenuItem("GameObject/Ice Saw/Patch", false, 12)]
    public static void CreatePatch(MenuCommand menuCommand)
    {


    }
}
