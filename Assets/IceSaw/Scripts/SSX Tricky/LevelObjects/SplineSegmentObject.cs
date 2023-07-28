using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using Unity.VisualScripting;
using UnityEditor;

[ExecuteInEditMode]
public class SplineSegmentObject : MonoBehaviour
{
    [Space(10)]
    [OnChangedCall("DrawCurve")]
    public Vector3 Point1;
    [OnChangedCall("DrawCurve")]
    public Vector3 Point2;
    [OnChangedCall("DrawCurve")]
    public Vector3 Point3;
    [OnChangedCall("DrawCurve")]
    public Vector3 Point4;
    [Space(10)]
    public float U0;
    public float U1;
    public float U2;
    public float U3;
    
    LineRenderer lineRenderer;

    //private int curveCount = 0;
    private int SEGMENT_COUNT = 10;

    Vector3 LocalPoint1;
    Vector3 LocalPoint2;
    Vector3 LocalPoint3;
    Vector3 LocalPoint4;

    [ContextMenu("Add Missing Components")]
    public void AddMissingComponents()
    {
        if(lineRenderer!=null)
        {
            Destroy(lineRenderer);
        }

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.hideFlags = HideFlags.HideInInspector;
        lineRenderer.material = LevelManager.Instance.Spline;
        lineRenderer.textureMode = LineTextureMode.Tile;
        Undo.undoRedoPerformed += UndoAndRedoFix;
    }

    public void LoadSplineSegment(SplineJsonHandler.SegmentJson segments)
    {
        AddMissingComponents();

        Point1 = JsonUtil.ArrayToVector3(segments.Point1);
        Point2 = JsonUtil.ArrayToVector3(segments.Point2);
        Point3 = JsonUtil.ArrayToVector3(segments.Point3);
        Point4 = JsonUtil.ArrayToVector3(segments.Point4);

        U0 = segments.U0;
        U1 = segments.U1;
        U2 = segments.U2;
        U3 = segments.U3;

        //SetDataLineRender();
        DrawCurve();
    }

    Vector3 ConvertLocalPoint(Vector3 point)
    {
        return transform.InverseTransformPoint(LevelManager.Instance.transform.TransformPoint(point));
    }

    Vector3 ConvertWorldPoint(Vector3 point)
    {
        return LevelManager.Instance.transform.InverseTransformPoint(transform.TransformPoint(point));
    }

    public SplineJsonHandler.SegmentJson GenerateSplineSegment()
    {
        SplineJsonHandler.SegmentJson segments = new SplineJsonHandler.SegmentJson();

        segments.Point1 = JsonUtil.Vector3ToArray(Point1);
        segments.Point2 = JsonUtil.Vector3ToArray(Point2);
        segments.Point3 = JsonUtil.Vector3ToArray(Point3);
        segments.Point4 = JsonUtil.Vector3ToArray(Point4);

        segments.U0 = U0;
        segments.U1 = U1;
        segments.U2 = U2;
        segments.U3 = U3;

        return segments;
    }

    public void SetDataLineRender()
    {
        transform.localPosition = Point1;
        lineRenderer.positionCount = 4;
        lineRenderer.SetPosition(0, LocalPoint1);
        lineRenderer.SetPosition(1, LocalPoint2);
        lineRenderer.SetPosition(2, LocalPoint3);
        lineRenderer.SetPosition(3, LocalPoint4);
    }


    public void DrawCurve()
    {
        LocalPoint1 = ConvertLocalPoint(Point1);
        LocalPoint2 = ConvertLocalPoint(Point2);
        LocalPoint3 = ConvertLocalPoint(Point3);
        LocalPoint4 = ConvertLocalPoint(Point4);

        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.positionCount = SEGMENT_COUNT + 2;
        lineRenderer.SetPosition(0, LocalPoint1);
        for (int i = 1; i <= SEGMENT_COUNT; i++)
        {
            float t = i / (float)SEGMENT_COUNT;
            Vector3 pixel = CalculateCubicBezierPoint(t, (LocalPoint1), (LocalPoint2), (LocalPoint3), (LocalPoint4));
            lineRenderer.SetPosition(i, pixel);
        }
        lineRenderer.SetPosition(SEGMENT_COUNT + 1, LocalPoint4);
    }

    void UndoAndRedoFix()
    {
        LocalPoint1 = ConvertLocalPoint(Point1);
        LocalPoint2 = ConvertLocalPoint(Point2);
        LocalPoint3 = ConvertLocalPoint(Point3);
        LocalPoint4 = ConvertLocalPoint(Point4);
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

    private void Update()
    {
        if (transform.hasChanged && !Hold)
        {
            Point1 = ConvertWorldPoint(LocalPoint1);
            Point2 = ConvertWorldPoint(LocalPoint2);
            Point3 = ConvertWorldPoint(LocalPoint3);
            Point4 = ConvertWorldPoint(LocalPoint4);

            DrawCurve();

            transform.hasChanged = false;
        }
    }

    bool Hold = false;
    [ContextMenu("Reset Transform")]
    public void TransformReset()
    {
        Hold = true;
        transform.localPosition = Vector3.zero;
        transform.localRotation = new Quaternion(0, 0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
        DrawCurve();
        transform.hasChanged = false;
        Hold = false;
    }

    [MenuItem("GameObject/Ice Saw/Spline Segment", false, 12)]
    public static void CreateSplineSegment(MenuCommand menuCommand)
    {
        GameObject TempObject = new GameObject("Spline Segment");
        if (menuCommand.context != null)
        {
            var AddToObject = (GameObject)menuCommand.context;
            TempObject.transform.parent = AddToObject.transform;
        }
        TempObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        TempObject.transform.localScale = new Vector3(1, 1, 1);
        Selection.activeGameObject = TempObject;
        TempObject.AddComponent<SplineSegmentObject>().AddMissingComponents();

    }
}
