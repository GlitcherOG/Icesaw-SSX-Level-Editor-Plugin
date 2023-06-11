using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using Unity.VisualScripting;

public class SplineSegmentObject : MonoBehaviour
{
    [Space(10)]
    public Vector3 Point1;
    public Vector3 Point2;
    public Vector3 Point3;
    public Vector3 Point4;
    [Space(10)]
    public float U0;
    public float U1;
    public float U2;
    public float U3;
    
    public LineRenderer lineRenderer;

    //private int curveCount = 0;
    private int SEGMENT_COUNT = 50;

    public void LoadSplineSegment(SplineJsonHandler.SegmentJson segments)
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;

        Point1 = JsonUtil.ArrayToVector3(segments.Point1);
        Point2 = JsonUtil.ArrayToVector3(segments.Point2);
        Point3 = JsonUtil.ArrayToVector3(segments.Point3);
        Point4 = JsonUtil.ArrayToVector3(segments.Point4);

        U0 = segments.U0;
        U1 = segments.U1;
        U2 = segments.U2;
        U3 = segments.U3;


        SetDataLineRender();
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
        lineRenderer.SetPosition(0, (Point1 - Point1));
        lineRenderer.SetPosition(1, (Point2 - Point1));
        lineRenderer.SetPosition(2, (Point3 - Point1));
        lineRenderer.SetPosition(3, (Point4 - Point1));
    }


    void DrawCurve()
    {
        for (int i = 1; i <= SEGMENT_COUNT; i++)
        {
            float t = i / (float)SEGMENT_COUNT;
            Vector3 pixel = CalculateCubicBezierPoint(t, (Point1 - Point1), (Point2 - Point1), (Point3 - Point1), (Point4 - Point1));
            lineRenderer.positionCount = (i);
            lineRenderer.SetPosition((i - 1), pixel);
        }
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
}
