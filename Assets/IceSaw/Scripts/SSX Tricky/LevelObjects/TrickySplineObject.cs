using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using UnityEditor;
using System.Drawing;
using UnityEditor.PackageManager.UI;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class TrickySplineObject : MonoBehaviour
{
    public int U0;
    public int U1;
    public int SplineStyle;

    //[OnChangedCall("DrawCurve")]
    public List<SplineSegment> splineSegments = new List<SplineSegment>();
    [HideInInspector]
    public LineRenderer lineRenderer;

    //private int curveCount = 0;
    [HideInInspector]
    public int SEGMENT_COUNT = 10;

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
        lineRenderer.material = TrickyLevelManager.Instance.Spline;
        lineRenderer.textureMode = LineTextureMode.Tile;
        Undo.undoRedoPerformed += UndoAndRedoFix;
    }

    void UndoAndRedoFix()
    {
        //LocalPoint1 = ConvertLocalPoint(Point1);
        //LocalPoint2 = ConvertLocalPoint(Point2);
        //LocalPoint3 = ConvertLocalPoint(Point3);
        //LocalPoint4 = ConvertLocalPoint(Point4);
        DrawCurve();
    }
    public void LoadSpline(SplineJsonHandler.SplineJson spline)
    {
        AddMissingComponents();

        transform.name = spline.SplineName;
        U0 = spline.U0;
        U1 = spline.U1;
        SplineStyle = spline.SplineStyle;

        transform.localPosition = JsonUtil.Array2DToVector3(spline.Segments[0].Points, 0);

        for (int i = 0; i < spline.Segments.Count; i++)
        {
            SplineSegment splineSegment = new SplineSegment();

            splineSegment.Point1 = JsonUtil.Array2DToVector3(spline.Segments[i].Points, 0);
            splineSegment.Point2 = JsonUtil.Array2DToVector3(spline.Segments[i].Points, 1);
            splineSegment.Point3 = JsonUtil.Array2DToVector3(spline.Segments[i].Points, 2);
            splineSegment.Point4 = JsonUtil.Array2DToVector3(spline.Segments[i].Points, 3);

            splineSegment.HiddenPoint1 = splineSegment.Point1;
            splineSegment.HiddenPoint2 = splineSegment.Point2;
            splineSegment.HiddenPoint3 = splineSegment.Point3;
            splineSegment.HiddenPoint4 = splineSegment.Point4;

            splineSegment.U0 = spline.Segments[i].U0;
            splineSegment.U1 = spline.Segments[i].U1;
            splineSegment.U2 = spline.Segments[i].U2;
            splineSegment.U3 = spline.Segments[i].U3;

            splineSegments.Add(splineSegment);
        }

        for (int i = 0; i < splineSegments.Count - 1; i++)
        {
            SplineSegment splineSegment = splineSegments[i];
            splineSegment.Point4 = splineSegments[i + 1].Point1;
            splineSegment.HiddenPoint4 = splineSegments[i + 1].Point1;
            splineSegments[i] = splineSegment;
        }

        DrawCurve();
    }

    public SplineJsonHandler.SplineJson GenerateSpline()
    {
        SplineJsonHandler.SplineJson spline = new SplineJsonHandler.SplineJson();

        spline.SplineName = transform.name;

        spline.U0 = U0;
        spline.U1 = U1;
        spline.SplineStyle = SplineStyle;
        spline.Segments = new List<SplineJsonHandler.SegmentJson>();

        //var Segments = transform.GetComponentsInChildren<SplineSegmentObject>();

        //for (int i = 0; i < Segments.Length; i++)
        //{
        //    spline.Segments.Add(Segments[i].GenerateSplineSegment());
        //}

        return spline;
    }

    [MenuItem("GameObject/Ice Saw/Spline", false, 11)]
    public static void CreateSpline(MenuCommand menuCommand)
    {
        GameObject TempObject = new GameObject("Spline");
        if (menuCommand.context != null)
        {
            var AddToObject = (GameObject)menuCommand.context;
            TempObject.transform.parent = AddToObject.transform;
        }
        TempObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        TempObject.transform.localScale = new Vector3(1, 1, 1);
        Selection.activeGameObject = TempObject;
        TempObject.AddComponent<TrickySplineObject>().AddMissingComponents();

    }

    public Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
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
        for (int i = 0; i < splineSegments.Count; i++)
        {
            var TempSegment = splineSegments[i];

            TempSegment.LocalPoint1 = ConvertLocalPoint(TempSegment.Point1);
            TempSegment.LocalPoint2 = ConvertLocalPoint(TempSegment.Point2);
            TempSegment.LocalPoint3 = ConvertLocalPoint(TempSegment.Point3);
            TempSegment.LocalPoint4 = ConvertLocalPoint(TempSegment.Point4);

            lineRenderer.positionCount += SEGMENT_COUNT + 2;
            curves.Add(TempSegment.LocalPoint1);
            for (int a = 1; a <= SEGMENT_COUNT; a++)
            {
                float t = a / (float)SEGMENT_COUNT;
                Vector3 pixel = CalculateCubicBezierPoint(t, (TempSegment.LocalPoint1), (TempSegment.LocalPoint2), (TempSegment.LocalPoint3), (TempSegment.LocalPoint4));
                curves.Add(pixel);
            }
            curves.Add(TempSegment.LocalPoint4);

            splineSegments[i] = TempSegment;
        }

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

    [System.Serializable]
    public struct SplineSegment
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
        public Vector3 HiddenPoint1;
        [HideInInspector]
        public Vector3 HiddenPoint2;
        [HideInInspector]
        public Vector3 HiddenPoint3;
        [HideInInspector]
        public Vector3 HiddenPoint4;

        [HideInInspector]
        public Vector3 LocalPoint1;
        [HideInInspector]
        public Vector3 LocalPoint2;
        [HideInInspector]
        public Vector3 LocalPoint3;
        [HideInInspector]
        public Vector3 LocalPoint4;
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if(Selection.activeObject!=this.gameObject)
        {
            lineRenderer.enabled = true;
        }
    }
}

[CustomEditor(typeof(TrickySplineObject))]
public class TrickySplineObjectEditor : Editor
{
    private Vector3[] positions;
    void OnSceneGUI()
    {
        TrickySplineObject connectedObjects = target as TrickySplineObject;

        connectedObjects.lineRenderer.enabled = false;

        //Draw Curve Handle
        List<Vector3> curves = new List<Vector3>();
        for (int i = 0; i < connectedObjects.splineSegments.Count; i++)
        {
            Vector3 LocalPoint1 = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.splineSegments[i].Point1);
            Vector3 LocalPoint2 = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.splineSegments[i].Point2);
            Vector3 LocalPoint3 = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.splineSegments[i].Point3);
            Vector3 LocalPoint4 = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.splineSegments[i].Point4);

            curves.Add(LocalPoint1);
            for (int a = 1; a <= connectedObjects.SEGMENT_COUNT; a++)
            {
                float t = a / (float)connectedObjects.SEGMENT_COUNT;
                Vector3 pixel = connectedObjects.CalculateCubicBezierPoint(t, (LocalPoint1), (LocalPoint2), (LocalPoint3), (LocalPoint4));
                curves.Add(pixel);
            }
            curves.Add(LocalPoint4);
        }
        Handles.color = UnityEngine.Color.yellow;
        Handles.zTest = UnityEngine.Rendering.CompareFunction.Less;
        for (int i = 0; i < curves.Count - 1; i++)
        {
            Handles.DrawLine(curves[i], curves[i + 1], 6f);
        }
        Handles.color = UnityEngine.Color.white;

        // Draw your handles here No Curve Handle
        Vector3[] positions = new Vector3[4 * connectedObjects.splineSegments.Count];
        for (var i = 0; i < connectedObjects.splineSegments.Count; i++)
        {
            positions[0 + i * 4] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.splineSegments[i].Point1);
            positions[1 + i * 4] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.splineSegments[i].Point2);
            positions[2 + i * 4] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.splineSegments[i].Point3);
            positions[3 + i * 4] = TrickyLevelManager.Instance.transform.TransformPoint(connectedObjects.splineSegments[i].Point4);

        }
        Handles.color = UnityEngine.Color.red;
        Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
        for (int i = 0; i < positions.Length - 1; i++)
        {
            Handles.DrawLine(positions[i], positions[i + 1], 6f);
        }
        Handles.color = UnityEngine.Color.white;

        //Draw Gizmo
        for (var i = 0; i < connectedObjects.splineSegments.Count; i++)
        {
            var TempSegment = connectedObjects.splineSegments[i];
            if (i==0)
            {
                TempSegment.Point1 = TrickyLevelManager.Instance.transform.InverseTransformPoint(Handles.PositionHandle(positions[0 + i * 4], Quaternion.identity));
            }
            TempSegment.Point2 = TrickyLevelManager.Instance.transform.InverseTransformPoint(Handles.PositionHandle(positions[1 + i * 4], Quaternion.identity));
            TempSegment.Point3 = TrickyLevelManager.Instance.transform.InverseTransformPoint(Handles.PositionHandle(positions[2 + i * 4], Quaternion.identity));
            TempSegment.Point4 = TrickyLevelManager.Instance.transform.InverseTransformPoint(Handles.PositionHandle(positions[3 + i * 4], Quaternion.identity));
            if(i!= connectedObjects.splineSegments.Count-1)
            {
                var TempSegment1 = connectedObjects.splineSegments[i+1];
                TempSegment1.Point1 = TempSegment.Point4;
                connectedObjects.splineSegments[i+1] = TempSegment1;
            }
            connectedObjects.splineSegments[i] = TempSegment;
        }
    }
}
