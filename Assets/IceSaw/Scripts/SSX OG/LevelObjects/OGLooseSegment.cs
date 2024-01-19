using SSXMultiTool.JsonFiles.SSXOG;
using SSXMultiTool.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OGLooseSegment : MonoBehaviour
{
    public Vector3 Point1;
    public Vector3 Point2;
    public Vector3 Point3;
    public Vector3 Point4;
    public float U0;
    public float U1;
    public float U2;
    public float U3;

    [HideInInspector]
    public Vector3 LocalPoint1;
    [HideInInspector]
    public Vector3 LocalPoint2;
    [HideInInspector]
    public Vector3 LocalPoint3;
    [HideInInspector]
    public Vector3 LocalPoint4;

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
        //Undo.undoRedoPerformed += UndoAndRedoFix;
    }

    public void LoadSpline(SplinesJsonHandler.SplineSegment spline)
    {
        AddMissingComponents();

        transform.localPosition = JsonUtil.ArrayToVector3(spline.Point1);

        Point1 = JsonUtil.ArrayToVector3(spline.Point1);
        Point2 = JsonUtil.ArrayToVector3(spline.Point2);
        Point3 = JsonUtil.ArrayToVector3(spline.Point3);
        Point4 = JsonUtil.ArrayToVector3(spline.Point4);

        U0 = spline.U0;
        U1 = spline.U1;
        U2 = spline.U2;
        U3 = spline.U3;

        DrawCurve();
    }

    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }

    [ContextMenu("DrawCurve")]
    public void DrawCurve()
    {
        List<Vector3> curves = new List<Vector3>();
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.positionCount = 0;



        LocalPoint1 = ConvertLocalPoint(Point1);
        LocalPoint2 = ConvertLocalPoint(Point2);
        LocalPoint3 = ConvertLocalPoint(Point3);
        LocalPoint4 = ConvertLocalPoint(Point4);

        lineRenderer.positionCount += SEGMENT_COUNT + 2;
        curves.Add(LocalPoint1);
        for (int a = 1; a <= SEGMENT_COUNT; a++)
        {
            float t = a / (float)SEGMENT_COUNT;
            Vector3 pixel = CalculateCubicBezierPoint(t, (LocalPoint1), (LocalPoint2), (LocalPoint3), (LocalPoint4));
            curves.Add(pixel);
        }
        curves.Add(LocalPoint4);


        lineRenderer.SetPositions(curves.ToArray());
    }

    Vector3 ConvertLocalPoint(Vector3 point)
    {
        return transform.InverseTransformPoint(OGLevelManager.Instance.transform.TransformPoint(point));
    }

    Vector3 ConvertWorldPoint(Vector3 point)
    {
        return OGLevelManager.Instance.transform.InverseTransformPoint(transform.TransformPoint(point));
    }
}
