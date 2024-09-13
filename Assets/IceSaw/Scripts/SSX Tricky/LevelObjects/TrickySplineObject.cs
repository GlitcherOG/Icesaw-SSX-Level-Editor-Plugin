using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using UnityEditor;
using System.Drawing;
using static TrickySplineObject;

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

    Vector3 OldPos;
    Vector3 OldPosSeg;
    Quaternion OldRotation;
    Vector3 OldScale;

    [HideInInspector]
    public bool Hold = false;

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
        //Undo.undoRedoPerformed += UndoAndRedoFix;
    }

    public void LoadSpline(SplineJsonHandler.SplineJson spline)
    {
        AddMissingComponents();

        transform.name = spline.SplineName;
        U0 = spline.U0;
        U1 = spline.U1;
        SplineStyle = spline.SplineStyle;

        transform.localPosition = JsonUtil.Array2DToVector3(spline.Segments[0].Points, 0);

        OldPos = transform.localPosition;
        OldPosSeg = transform.localPosition;
        OldRotation = transform.localRotation;
        OldScale = transform.localScale;

        for (int i = 0; i < spline.Segments.Count; i++)
        {
            SplineSegment splineSegment = new SplineSegment();

            splineSegment.Point1 = JsonUtil.Array2DToVector3(spline.Segments[i].Points, 0);
            splineSegment.Point2 = JsonUtil.Array2DToVector3(spline.Segments[i].Points, 1);
            splineSegment.Point3 = JsonUtil.Array2DToVector3(spline.Segments[i].Points, 2);
            splineSegment.Point4 = JsonUtil.Array2DToVector3(spline.Segments[i].Points, 3);

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

        for (int i = 0; i < splineSegments.Count; i++)
        {
            SplineJsonHandler.SegmentJson Segment = new SplineJsonHandler.SegmentJson();

            Segment.Points = new float[4, 3];

            Segment.Points[0, 0] = splineSegments[i].Point1.x;
            Segment.Points[0, 1] = splineSegments[i].Point1.y;
            Segment.Points[0, 2] = splineSegments[i].Point1.y;

            Segment.Points[1, 0] = splineSegments[i].Point2.x;
            Segment.Points[1, 1] = splineSegments[i].Point2.y;
            Segment.Points[1, 2] = splineSegments[i].Point2.y;

            Segment.Points[2, 0] = spline.splineSegments[i].Point3.x;
            Segment.Points[2, 1] = splineSegments[i].Point3.y;
            Segment.Points[2, 2] = splineSegments[i].Point3.y;

            Segment.Points[3, 0] = splineSegments[i].Point4.x;
            Segment.Points[3, 1] = splineSegments[i].Point4.y;
            Segment.Points[3, 2] = splineSegments[i].Point4.y;

            Segment.U0 = splineSegments[i].U0;
            Segment.U1 = splineSegments[i].U1;
            Segment.U2 = splineSegments[i].U2;
            Segment.U3 = splineSegments[i].U3;

            spline.Segments.Add(Segment);
        }

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

    [ContextMenu("Refresh Curve")]
    public void DrawCurve(bool Generated = false)
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

            if (!Generated)
            {
                TempSegment.LocalPoint1 = ConvertLocalPoint(TempSegment.Point1);
                TempSegment.LocalPoint2 = ConvertLocalPoint(TempSegment.Point2);
                TempSegment.LocalPoint3 = ConvertLocalPoint(TempSegment.Point3);
                TempSegment.LocalPoint4 = ConvertLocalPoint(TempSegment.Point4);
            }

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

    [ContextMenu("Reset Transform")]
    public void TransformReset()
    {
        Hold = true;
        transform.localRotation = new Quaternion(0, 0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
        OldPos = transform.localPosition;
        OldPosSeg = transform.localPosition;
        OldRotation = transform.localRotation;
        OldScale = transform.localScale;
        DrawCurve();
        Hold = false;
    }

    public void SubdivideSegments()
    {
        //public List<SplineSegment> splineSegments = new List<SplineSegment>();
        //Collect All Segments
        var TempSegmentList = splineSegments;
        //Create a new segment list
        splineSegments = new List<SplineSegment>();

        //For each segment split in half calculating mid points along curve
        for (int i = 0; i < TempSegmentList.Count; i++)
        {
            //Maths might be wrong
            var NewSegment = new SplineSegment();
            NewSegment.Point1 = TempSegmentList[i].Point1;

            NewSegment.Point2 = CalculateCubicBezierPoint((1f/3f) / 2, TempSegmentList[i].Point1, TempSegmentList[i].Point2, TempSegmentList[i].Point3, TempSegmentList[i].Point4);

            NewSegment.Point3 = CalculateCubicBezierPoint((2f / 3f) / 2, TempSegmentList[i].Point1, TempSegmentList[i].Point2, TempSegmentList[i].Point3, TempSegmentList[i].Point4);

            NewSegment.Point4 = CalculateCubicBezierPoint(1f / 2, TempSegmentList[i].Point1, TempSegmentList[i].Point2, TempSegmentList[i].Point3, TempSegmentList[i].Point4);

            splineSegments.Add(NewSegment);

            NewSegment = new SplineSegment();

            NewSegment.Point1 = CalculateCubicBezierPoint(1f / 2, TempSegmentList[i].Point1, TempSegmentList[i].Point2, TempSegmentList[i].Point3, TempSegmentList[i].Point4);

            NewSegment.Point2 = CalculateCubicBezierPoint(((1f / 3f)+1f) / 2, TempSegmentList[i].Point1, TempSegmentList[i].Point2, TempSegmentList[i].Point3, TempSegmentList[i].Point4);

            NewSegment.Point3 = CalculateCubicBezierPoint(((2f / 3f) + 1f) / 2, TempSegmentList[i].Point1, TempSegmentList[i].Point2, TempSegmentList[i].Point3, TempSegmentList[i].Point4);

            NewSegment.Point4 = TempSegmentList[i].Point4;

            splineSegments.Add(NewSegment);
        }


        //Add to list
    }

    public void CollapseSegments()
    {
        int Remainder = 0;
        if(TempSegmentList.Count/2 != 0)
        {
            Remainder = 1;
        }

        var TempSegmentList = splineSegments;

        splineSegments = new List<SplineSegment>();

        for (int i = 0; i < (TempSegmentList.Count - Remainder)/2; i++)
        {
            var NewSegment = new SplineSegment();

            NewSegment.Point1 = TempSegmentList[i*2].Point1;

            NewSegment.Point2 = CalculateCubicBezierPoint(2f / 3f, TempSegmentList[i*2].Point1, TempSegmentList[i*2].Point2, TempSegmentList[i*2].Point3, TempSegmentList[i*2].Point4);

            NewSegment.Point3 = CalculateCubicBezierPoint(1f / 3f, TempSegmentList[i * 2 + 1].Point1, TempSegmentList[i * 2 + 1].Point2, TempSegmentList[i * 2 + 1].Point3, TempSegmentList[i * 2 + 1].Point4);

            NewSegment.Point4 = TempSegmentList[i*2+1].Point4;

            splineSegments.Add(NewSegment);
        }

        if(Remainder ==1)
        {
            splineSegments.Add(TempSegmentList[TempSegmentList.Count - 1]);
        }
    }
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
    bool SetOnce = false;
    bool PrevSelected = false;
    private void OnSceneGUI(SceneView sceneView)
    {
        if (Selection.activeObject == this.gameObject)
        {
            PrevSelected = true;
            if (!TrickyLevelManager.Instance.EditMode && !SetOnce)
            {
                lineRenderer.enabled = false;
                SetOnce = true;
                Tools.current = Tool.Move;
            }
            else if (TrickyLevelManager.Instance.EditMode)
            {
                SetOnce = false;
            }
        }
        else
        {
            lineRenderer.enabled = true;
            SetOnce = false;
            if (PrevSelected)
            {
                DrawCurve();
                PrevSelected = false;
                Tools.current = Tool.Move;
            }
        }

        if ((transform.localRotation != OldRotation || transform.localScale != OldScale || transform.localPosition != OldPos) &&!Hold)
        {
            Hold = true;
            OldRotation = transform.localRotation;
            OldScale = transform.localScale;
            OldPos = transform.localPosition;
            OldPosSeg = transform.localPosition;

            var TempSegment = splineSegments[0];
            TempSegment.LocalPoint1 = new Vector3 (0.0f, 0.0f, 0.0f);
            splineSegments[0] = TempSegment;
            for (int i = 0; i < splineSegments.Count; i++)
            {
                TempSegment = splineSegments[i];

                if (i != 0)
                {
                    TempSegment.Point1 = ConvertWorldPoint(TempSegment.LocalPoint1);
                }
                else
                {
                    TempSegment.LocalPoint1 = new Vector3(0.0f, 0.0f, 0.0f);
                    TempSegment.Point1 = OldPos;
                }
                TempSegment.Point2 = ConvertWorldPoint(TempSegment.LocalPoint2);
                TempSegment.Point3 = ConvertWorldPoint(TempSegment.LocalPoint3);
                TempSegment.Point4 = ConvertWorldPoint(TempSegment.LocalPoint4);

                splineSegments[i] = TempSegment;
            }
            DrawCurve(true);
            Hold = false;
        }
        else if(splineSegments.Count>0)
        {
            if (splineSegments[0].Point1 != OldPosSeg)
            {
                transform.localPosition = splineSegments[0].Point1;
                OldPos = transform.localPosition;
                OldPosSeg = transform.localPosition;
                DrawCurve();
            }
        }
        else if (Selection.activeObject == this.gameObject && TrickyLevelManager.Instance.EditMode)
        {
            DrawCurve();
        }
    }
}

[CustomEditor(typeof(TrickySplineObject))]
public class TrickySplineObjectEditor : Editor
{
    public VisualTreeAsset m_InspectorXML;

    public override VisualElement CreateInspectorGUI()
    {
        m_InspectorXML = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets\\IceSaw\\Scripts\\SSX Tricky\\LevelObjects\\Inspectors\\SplineObjects.uxml");

        // Create a new VisualElement to be the root of our inspector UI
        VisualElement myInspector = new VisualElement();
        m_InspectorXML.CloneTree(myInspector);

        VisualElement inspectorGroup = myInspector.Q("Default_Inspector");

        MonoBehaviour monoBev = (MonoBehaviour)target;
        TrickyInstanceObject PatchObject = monoBev.GetComponent<TrickySplineObject>();

        //TextElement Details = new TextElement();
        //Details.style.fontSize = 16;
        //Details.text = "Instance ID " + PatchObject.transform.GetSiblingIndex();

        //inspectorGroup.Add(Details);

        //VisualElement RefreshModelButton = myInspector.Q("RefreshModel");
        //var TempButton = RefreshModelButton.Query<Button>();
        //TempButton.First().RegisterCallback<ClickEvent>(LoadPrefabs);

        //VisualElement RefreshCollisionModelButton = myInspector.Q("RefreshCollisionModel");
        //TempButton = RefreshCollisionModelButton.Query<Button>();
        //TempButton.First().RegisterCallback<ClickEvent>(LoadCollisionModels);

        //VisualElement GotoEffectSlotButton = myInspector.Q("GotoEffectSlot");
        //TempButton = GotoEffectSlotButton.Query<Button>();
        //TempButton.First().RegisterCallback<ClickEvent>(FlipPatch);

        //VisualElement GotoPhysicsButton = myInspector.Q("GotoPhysics");
        //TempButton = GotoPhysicsButton.Query<Button>();
        //TempButton.First().RegisterCallback<ClickEvent>(GotoPhysicsEffect);

        //VisualElement GotoModelButton = myInspector.Q("GotoModel");
        //TempButton = GotoModelButton.Query<Button>();
        //TempButton.First().RegisterCallback<ClickEvent>(GotoModel);

        InspectorElement.FillDefaultInspector(inspectorGroup, serializedObject, this);

        // Return the finished inspector UI
        return myInspector;
    }


    private Vector3[] positions;
    void OnSceneGUI()
    {
        TrickySplineObject connectedObjects = target as TrickySplineObject;
        if (!connectedObjects.Hold)
        {
            if (TrickyLevelManager.Instance.EditMode)
            {
                Tools.current = Tool.None;
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
                positions = new Vector3[4 * connectedObjects.splineSegments.Count];
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
                    Vector3 TestVector;
                    if (i == 0)
                    {
                        TestVector = Handles.PositionHandle(positions[0 + i * 4], Quaternion.identity);
                        if (TestVector != positions[0 + i * 4])
                        {
                            TempSegment.Point1 = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                        }
                    }

                    TestVector = Handles.PositionHandle(positions[1 + i * 4], Quaternion.identity);
                    if (TestVector != positions[1 + i * 4])
                    {
                        TempSegment.Point2 = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                    }

                    TestVector = Handles.PositionHandle(positions[2 + i * 4], Quaternion.identity);
                    if (TestVector != positions[2 + i * 4])
                    {
                        TempSegment.Point3 = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                    }

                    TestVector = Handles.PositionHandle(positions[3 + i * 4], Quaternion.identity);
                    if (TestVector != positions[3 + i * 4])
                    {
                        TempSegment.Point4 = TrickyLevelManager.Instance.transform.InverseTransformPoint(TestVector);
                        if (i != connectedObjects.splineSegments.Count - 1)
                        {
                            var TempSegment1 = connectedObjects.splineSegments[i + 1];
                            TempSegment1.Point1 = TempSegment.Point4;
                            connectedObjects.splineSegments[i + 1] = TempSegment1;
                        }
                    }
                    connectedObjects.splineSegments[i] = TempSegment;
                }
            }
            else
            {
                connectedObjects.lineRenderer.enabled = true;
                //connectedObjects.DrawCurve();
            }
        }
    }
}
