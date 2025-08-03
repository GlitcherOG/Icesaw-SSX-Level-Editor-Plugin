using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static SSXMultiTool.JsonFiles.Tricky.AIPSOPJsonHandler;

public class TrickyPathBObject : MonoBehaviour
{
    public int Type;
    public int U0;
    public int U1;
    public float U2;

    [OnChangedCall("PathPointsUpdate")]
    public List<Vector3> PathPoints;
    [HideInInspector]
    public List<Vector3> VectorPoints;

    public List<PathEvent> PathEvents;

    [HideInInspector]
    public LineRenderer lineRenderer;

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
        lineRenderer.widthMultiplier = 0.45f;
        lineRenderer.material = TrickyLevelManager.Instance.RaceLine;
        lineRenderer.textureMode = LineTextureMode.Tile;
    }

    public void LoadPathB(AIPSOPJsonHandler.PathB pathB)
    {
        AddMissingComponents();
        Type = pathB.Type;
        U1 = pathB.U1;
        U2 = pathB.U2;

        transform.localPosition = JsonUtil.ArrayToVector3(pathB.PathPos);

        PathPoints = new List<Vector3>();
        VectorPoints = new List<Vector3>();
        for (int i = 0; i < pathB.PathPoints.GetLength(0); i++)
        {
            VectorPoints.Add(new Vector3(pathB.PathPoints[i, 0], pathB.PathPoints[i, 1], pathB.PathPoints[i, 2]));
            PathPoints.Add(VectorPoints[i]);
            if (i != 0)
            {
                PathPoints[i] += PathPoints[i - 1];
            }
        }

        PathEvents = new List<PathEvent>();
        for (int i = 0; i < pathB.PathEvents.Count; i++)
        {
            var NewStruct = new PathEvent();

            NewStruct.EventType = pathB.PathEvents[i].EventType;
            NewStruct.EventValue = pathB.PathEvents[i].EventValue;
            NewStruct.EventStart = pathB.PathEvents[i].EventStart;
            NewStruct.EventEnd = pathB.PathEvents[i].EventEnd;

            PathEvents.Add(NewStruct);
        }

        DrawLines();
    }

    public Vector3 FindPathLocalPoint(float FindDistance)
    {
        float OldDistance = 0f;
        float TestDistance = 0f;
        for (int i = 0; i < VectorPoints.Count; i++)
        {
            TestDistance += Vector2.Distance(VectorPoints[i], new Vector2(0, 0));
            if (TestDistance >= FindDistance)
            {
                //Get Size
                float Size = TestDistance - OldDistance;
                //Get Pos
                float position = TestDistance - FindDistance;
                //Get Percentage
                float Percentage = (position / Size);

                //Return Local Point
                return (Percentage * VectorPoints[i]) + PathPoints[i - 1];
            }
            OldDistance = TestDistance;
        }

        return new Vector3 (0, 0, 0);
    }

    public AIPSOPJsonHandler.PathB GeneratePathB()
    {
        ResetTransformation();

        AIPSOPJsonHandler.PathB pathB = new AIPSOPJsonHandler.PathB();

        pathB.Type = Type;
        pathB.U1 = U1;
        pathB.U2 = U2;

        pathB.PathPos = JsonUtil.Vector3ToArray(transform.localPosition);

        pathB.PathPoints = new float[PathPoints.Count, 3];

        for (int i = 0; i < PathPoints.Count; i++)
        {
            pathB.PathPoints[i, 0] = PathPoints[i].x;
            pathB.PathPoints[i, 1] = PathPoints[i].y;
            pathB.PathPoints[i, 2] = PathPoints[i].z;

            if (i != 0)
            {
                pathB.PathPoints[i, 0] -= PathPoints[i-1].x;
                pathB.PathPoints[i, 1] -= PathPoints[i-1].y;
                pathB.PathPoints[i, 2] -= PathPoints[i-1].z;
            }
        }

        pathB.PathEvents = new List<AIPSOPJsonHandler.PathEvent>();
        for (int i = 0; i < PathEvents.Count; i++)
        {
            var NewStruct = new AIPSOPJsonHandler.PathEvent();

            NewStruct.EventType = PathEvents[i].EventType;
            NewStruct.EventValue = PathEvents[i].EventValue;
            NewStruct.EventStart = PathEvents[i].EventStart;
            NewStruct.EventEnd = PathEvents[i].EventEnd;

            pathB.PathEvents.Add(NewStruct);
        }


        return pathB;
    }

    public void PathPointsUpdate()
    {
        GenerateVectors();
        DrawLines();
    }

    public void DrawLines()
    {
        lineRenderer.positionCount = PathPoints.Count+1;
        lineRenderer.SetPosition(0, new Vector3(0,0,0));
        for (int i = 0; i < PathPoints.Count; i++)
        {
            lineRenderer.SetPosition(i+1, PathPoints[i]);
        }
    }

    public void GenerateVectors()
    {
        VectorPoints = new List<Vector3>();
        for (int i = 0; i < PathPoints.Count;i++)
        {
            if(i==0)
            {
               VectorPoints.Add(PathPoints[i]);
            }
            else
            {
                VectorPoints.Add(PathPoints[i] - PathPoints[i-1]);
            }

        }
    }

    [MenuItem("GameObject/Ice Saw/Path B", false, 202)]
    public static void CreatePathB(MenuCommand menuCommand)
    {
        GameObject TempObject = new GameObject("Path B");
        if (menuCommand.context != null)
        {
            var AddToObject = (GameObject)menuCommand.context;
            TempObject.transform.parent = AddToObject.transform;
        }
        TempObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        TempObject.transform.localScale = new Vector3(1, 1, 1);
        Selection.activeGameObject = TempObject;
        TempObject.AddComponent<TrickyPathBObject>().AddMissingComponents();
    }

    public Vector3 ConvertLocalPoint(Vector3 point)
    {
        return transform.InverseTransformPoint(TrickyLevelManager.Instance.transform.TransformPoint(point));
    }

    public Vector3 ConvertWorldPoint(Vector3 point)
    {
        return TrickyLevelManager.Instance.transform.InverseTransformPoint(transform.TransformPoint(point));
    }

    [ContextMenu("Reset Tranformation")]
    public void ResetTransformation()
    {
        var Positions = PathPoints;

        for (int i = 0; i < Positions.Count; i++)
        {
            Positions[i] = transform.TransformPoint(Positions[i]);
        }

        transform.localRotation = new Quaternion(0, 0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);

        for (int i = 0; i < Positions.Count; i++)
        {
            PathPoints[i] = transform.InverseTransformPoint(Positions[i]);
        }
    }

    [System.Serializable]
    public struct PathEvent
    {
        public int EventType;
        public int EventValue;
        public float EventStart;
        public float EventEnd;
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
                DrawLines();
                PrevSelected = false;
                Tools.current = Tool.Move;
            }
        }

        if ((transform.localRotation != OldRotation || transform.localScale != OldScale || transform.localPosition != OldPos) && !Hold)
        {
            Hold = true;
            OldRotation = transform.localRotation;
            OldScale = transform.localScale;
            OldPos = transform.localPosition;
            OldPosSeg = transform.localPosition;

            DrawLines();
            Hold = false;
        }
        else if (PathPoints.Count > 0)
        {
            //if (splineSegments[0].Point1 != OldPosSeg)
            //{
            //    transform.localPosition = splineSegments[0].Point1;
            //    OldPos = transform.localPosition;
            //    OldPosSeg = transform.localPosition;
            //    DrawLines();
            //}
        }
        else if (Selection.activeObject == this.gameObject && TrickyLevelManager.Instance.EditMode)
        {
            DrawLines();
        }
    }
}

[CustomEditor(typeof(TrickyPathBObject))]
public class TrickyPathBObjectEditor : Editor
{
    private Vector3[] positions;
    void OnSceneGUI()
    {
        TrickyPathBObject connectedObjects = target as TrickyPathBObject;
        if (!connectedObjects.Hold)
        {
            if (TrickyLevelManager.Instance.EditMode && TrickyLevelManager.Instance.PathEventMode)
            {
                Tools.current = Tool.None;
                positions = new Vector3[connectedObjects.PathEvents.Count*2];
                for (int i = 0; i < connectedObjects.PathEvents.Count*2; i++)
                {
                    positions[i*2] = Handles.PositionHandle(connectedObjects.transform.TransformPoint(connectedObjects.FindPathLocalPoint(connectedObjects.PathEvents[i].EventStart)), Quaternion.identity);
                    positions[i*2+1] = Handles.PositionHandle(connectedObjects.transform.TransformPoint(connectedObjects.FindPathLocalPoint(connectedObjects.PathEvents[i].EventEnd)), Quaternion.identity);
                }
            }
            else if (TrickyLevelManager.Instance.EditMode && !TrickyLevelManager.Instance.PathEventMode)
            {
                Tools.current = Tool.None;
                connectedObjects.lineRenderer.enabled = false;

                // Draw your handles here No Curve Handle
                positions = new Vector3[connectedObjects.PathPoints.Count];
                for (var i = 0; i < connectedObjects.PathPoints.Count; i++)
                {
                    positions[i] = connectedObjects.transform.TransformPoint(connectedObjects.PathPoints[i]);
                }

                Handles.color = UnityEngine.Color.red;
                Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
                for (int i = 0; i < positions.Length - 1; i++)
                {
                    Handles.DrawLine(positions[i], positions[i + 1], 6f);
                }
                Handles.color = UnityEngine.Color.white;

                //Draw Gizmo
                for (var i = 0; i < connectedObjects.PathPoints.Count; i++)
                {
                    var TempSegment = connectedObjects.transform.TransformPoint(connectedObjects.PathPoints[i]);
                    Vector3 TestVector = Handles.PositionHandle(positions[i], Quaternion.identity);
                    if (TestVector != positions[i])
                    {
                        connectedObjects.PathPoints[i] = connectedObjects.transform.InverseTransformPoint(TestVector);
                    }
                }
            }
            else
            {
                connectedObjects.lineRenderer.enabled = true;
                connectedObjects.DrawLines();
            }
        }
    }
}
