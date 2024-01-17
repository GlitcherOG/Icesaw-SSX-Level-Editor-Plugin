using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.SSXOG;
using SSXMultiTool.Utilities;
using UnityEditor;

public class OGSplineObject : MonoBehaviour
{
    public Vector3 vector3;
    public Vector3 vector31;

    public int U0;
    public int U1;
    public int U2;

    //16
    public int U3;
    public int U4;
    public int U5;
    public int U6;
    public int U7;
    public int U8;
    public int U9;
    public int U10;

    //[OnChangedCall("DrawCurve")]
    LineRenderer lineRenderer;

    //private int curveCount = 0;
    private int SEGMENT_COUNT = 10;

    [ContextMenu("Add Missing Components")]
    public void AddMissingComponents()
    {
        if (lineRenderer != null)
        {
            Destroy(lineRenderer);
        }

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.hideFlags = HideFlags.HideInInspector;
        lineRenderer.material = OGLevelManager.Instance.Spline;
        lineRenderer.textureMode = LineTextureMode.Tile;
        Undo.undoRedoPerformed += UndoAndRedoFix;
    }

    void UndoAndRedoFix()
    {
        //LocalPoint1 = ConvertLocalPoint(Point1);
        //LocalPoint2 = ConvertLocalPoint(Point2);
        //LocalPoint3 = ConvertLocalPoint(Point3);
        //LocalPoint4 = ConvertLocalPoint(Point4);
        //DrawCurve();
    }
    public void LoadSpline(SplinesJsonHandler.SplineJson spline)
    {
        AddMissingComponents();

        vector3 = JsonUtil.ArrayToVector3(spline.vector3);
        vector31 = JsonUtil.ArrayToVector3(spline.vector31);

        U0 = spline.U0;
        U1 = spline.U1;
        U2 = spline.U2;

        U3 = spline.U3;
        U4 = spline.U4;
        U5 = spline.U5;
        U6 = spline.U6;
        U7 = spline.U7;
        U8 = spline.U8;
        U9 = spline.U9;
        U10 = spline.U10;

        DrawCurve();
    }

    //public SplineJsonHandler.SplineJson GenerateSpline()
    //{
    //    SplineJsonHandler.SplineJson spline = new SplineJsonHandler.SplineJson();

    //    spline.SplineName = transform.name;

    //    spline.U0 = U0;
    //    spline.U1 = U1;
    //    spline.SplineStyle = SplineStyle;
    //    spline.Segments = new List<SplineJsonHandler.SegmentJson>();

    //    //var Segments = transform.GetComponentsInChildren<SplineSegmentObject>();

    //    //for (int i = 0; i < Segments.Length; i++)
    //    //{
    //    //    spline.Segments.Add(Segments[i].GenerateSplineSegment());
    //    //}

    //    return spline;
    //}

    //[MenuItem("GameObject/Ice Saw/Spline", false, 11)]
    //public static void CreateSpline(MenuCommand menuCommand)
    //{
    //    GameObject TempObject = new GameObject("Spline");
    //    if (menuCommand.context != null)
    //    {
    //        var AddToObject = (GameObject)menuCommand.context;
    //        TempObject.transform.parent = AddToObject.transform;
    //    }
    //    TempObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
    //    TempObject.transform.localScale = new Vector3(1, 1, 1);
    //    Selection.activeGameObject = TempObject;
    //    TempObject.AddComponent<TrickySplineObject>().AddMissingComponents();

    //}

    //Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    //{
    //    float u = 1 - t;
    //    float tt = t * t;
    //    float uu = u * u;
    //    float uuu = uu * u;
    //    float ttt = tt * t;

    //    Vector3 p = uuu * p0;
    //    p += 3 * uu * t * p1;
    //    p += 3 * u * tt * p2;
    //    p += ttt * p3;

    //    return p;
    //}

    [ContextMenu("DrawCurve")]
    public void DrawCurve()
    {
        List<Vector3> curves = new List<Vector3>();
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.positionCount = 2;

        curves.Add(vector3);
        curves.Add(vector31);
        //for (int i = 0; i < splineSegments.Count; i++)
        //{
        //    var TempSegment = splineSegments[i];

        //    TempSegment.LocalPoint1 = ConvertLocalPoint(TempSegment.Point1);
        //    TempSegment.LocalPoint2 = ConvertLocalPoint(TempSegment.Point2);
        //    TempSegment.LocalPoint3 = ConvertLocalPoint(TempSegment.Point3);
        //    TempSegment.LocalPoint4 = ConvertLocalPoint(TempSegment.Point4);

        //    lineRenderer.positionCount += SEGMENT_COUNT + 2;
        //    curves.Add(TempSegment.LocalPoint1);
        //    for (int a = 1; a <= SEGMENT_COUNT; a++)
        //    {
        //        float t = a / (float)SEGMENT_COUNT;
        //        Vector3 pixel = CalculateCubicBezierPoint(t, (TempSegment.LocalPoint1), (TempSegment.LocalPoint2), (TempSegment.LocalPoint3), (TempSegment.LocalPoint4));
        //        curves.Add(pixel);
        //    }
        //    curves.Add(TempSegment.LocalPoint4);

        //    splineSegments[i] = TempSegment;
        //}

        lineRenderer.SetPositions(curves.ToArray());
    }

    Vector3 ConvertLocalPoint(Vector3 point)
    {
        return transform.InverseTransformPoint(TrickyLevelManager.Instance.transform.TransformPoint(point));
    }

    Vector3 ConvertWorldPoint(Vector3 point)
    {
        return TrickyLevelManager.Instance.transform.InverseTransformPoint(transform.TransformPoint(point));
    }

    //[ContextMenu("Reset Transform")]
    //public void TransformReset()
    //{
    //    var Segments = transform.GetComponentsInChildren<SplineSegmentObject>();
    //    for (int i = 0; i < Segments.Length; i++)
    //    {
    //        Segments[i].Hold = true;
    //    }

    //    transform.localPosition = Vector3.zero;
    //    transform.localRotation = new Quaternion(0, 0, 0, 0);
    //    transform.localScale = new Vector3(1, 1, 1);
    //    transform.hasChanged = false;

    //    for (int i = 0; i < Segments.Length; i++)
    //    {
    //        Segments[i].DrawCurve();
    //        Segments[i].Hold = false;
    //    }
    //}
}
