using NURBS;
using SSXMultiTool.JsonFiles.SSX3;
using SSXMultiTool.Utilities;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteInEditMode]
[SelectionBase]
public class SSX3Spline : MonoBehaviour
{
    public int U0;
    public int U1;
    public float U2;
    public int U3;
    public int U4;

    public List<Segment> Segments = new List<Segment>();

    public LineRenderer lineRenderer;

    public void LoadBin3(SplineJsonHandler.SplineJson spline)
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = SSX3WorldManager.Instance.Spline;
        lineRenderer.useWorldSpace = false;

        transform.name = spline.Name;

        //transform.localEulerAngles = JsonUtil.ArrayToQuaternion(bin3.Rotation).eulerAngles;
        //transform.localScale = JsonUtil.ArrayToVector3(bin3.Scale);
        //transform.localPosition = JsonUtil.ArrayToVector3(bin3.Position);

        U0 = spline.U0;
        U1 = spline.U1;
        U2 = spline.U2;
        U3 = spline.U3;

        for (int i = 0; i < spline.Segments.Count; i++)
        {
            Segment segment = new Segment();

            segment.Point1 = JsonUtil.Array2DToVector3(spline.Segments[i].Points, 0);
            segment.Point2 = JsonUtil.Array2DToVector3(spline.Segments[i].Points, 1);
            segment.Point3 = JsonUtil.Array2DToVector3(spline.Segments[i].Points, 2);
            segment.Point4 = JsonUtil.Array2DToVector3(spline.Segments[i].Points, 3);

            Segments.Add(segment);
        }

        lineRenderer.positionCount = 4* Segments.Count;

        List<Vector3> curves = new List<Vector3>();

        for (int i = 0; i < Segments.Count; i++)
        {
            curves.Add(Segments[i].Point1);
            curves.Add(Segments[i].Point2);
            curves.Add(Segments[i].Point3);
            curves.Add(Segments[i].Point4);
        }

        lineRenderer.SetPositions(curves.ToArray());
    }

    public struct Segment
    {
        public Vector3 Point1;
        public Vector3 Point2;
        public Vector3 Point3;
        public Vector3 Point4;
        public float E0;
        public float E1;
        public float E2;
        public float E3;
    }
}