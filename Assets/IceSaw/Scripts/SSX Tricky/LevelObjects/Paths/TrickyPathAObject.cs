using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using UnityEditor;

public class TrickyPathAObject : MonoBehaviour
{
    public int Type;
    public int U1;
    public int U2;
    public int U3;
    public int U4;
    public int U5;
    public int Respawnable;

    [OnChangedCall("DrawLines")]
    public List<Vector3> PathPoints;
    //[HideInInspector]
    //public List<Vector3> WorldPathPoints;

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
        lineRenderer.useWorldSpace = true;
        lineRenderer.hideFlags = HideFlags.HideInInspector;
        lineRenderer.material = TrickyLevelManager.Instance.AIPath;
        lineRenderer.textureMode = LineTextureMode.Tile;
    }


    public void LoadPathA(AIPSOPJsonHandler.PathA pathA)
    {
        AddMissingComponents();

        Type = pathA.Type;
        U1 = pathA.U1;
        U2 = pathA.U2;
        U3 = pathA.U3;
        U4 = pathA.U4;
        U5 = pathA.U5;
        Respawnable = pathA.Respawnable;

        transform.localPosition = JsonUtil.ArrayToVector3(pathA.PathPos);

        PathPoints = new List<Vector3>();

        for (int i = 0; i < pathA.PathPoints.GetLength(0); i++)
        {
            PathPoints.Add(new Vector3(pathA.PathPoints[i,0], pathA.PathPoints[i, 1], pathA.PathPoints[i, 2]));
            if (i != 0)
            {
                PathPoints[i] += PathPoints[i - 1];
            }
        }

        PathEvents = new List<PathEvent>();
        for (int i = 0; i < pathA.PathEvents.Count; i++)
        {
            var NewStruct = new PathEvent();

            NewStruct.EventType = pathA.PathEvents[i].EventType;
            NewStruct.EventValue = pathA.PathEvents[i].EventValue;
            NewStruct.EventStart = pathA.PathEvents[i].EventStart;
            NewStruct.EventEnd = pathA.PathEvents[i].EventEnd;

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

        return new Vector3(0, 0, 0);
    }

    public AIPSOPJsonHandler.PathA GeneratePathA()
    {
        ResetTransformation();

        AIPSOPJsonHandler.PathA NewPathA = new AIPSOPJsonHandler.PathA();

        NewPathA.Type = Type;
        NewPathA.U1 = U1;
        NewPathA.U2 = U2;
        NewPathA.U3 = U3;
        NewPathA.U4 = U4;
        NewPathA.U5 = U5;
        NewPathA.Respawnable = Respawnable;

        NewPathA.PathPos = JsonUtil.Vector3ToArray(transform.localPosition);

        NewPathA.PathPoints = new float[PathPoints.Count, 3];

        for (int i = 0; i < PathPoints.Count; i++)
        {
            NewPathA.PathPoints[i, 0] = PathPoints[i].x;
            NewPathA.PathPoints[i, 1] = PathPoints[i].y;
            NewPathA.PathPoints[i, 2] = PathPoints[i].z;

            if (i != 0)
            {
                NewPathA.PathPoints[i, 0] -= PathPoints[i - 1].x;
                NewPathA.PathPoints[i, 1] -= PathPoints[i - 1].y;
                NewPathA.PathPoints[i, 2] -= PathPoints[i - 1].z;
            }
        }

        NewPathA.PathEvents = new List<AIPSOPJsonHandler.PathEvent>();

        for (int i = 0; i < PathEvents.Count; i++)
        {
            var NewStruct = new AIPSOPJsonHandler.PathEvent();

            NewStruct.EventType = PathEvents[i].EventType;
            NewStruct.EventValue = PathEvents[i].EventValue;
            NewStruct.EventStart = PathEvents[i].EventStart;
            NewStruct.EventEnd = PathEvents[i].EventEnd;

            NewPathA.PathEvents.Add(NewStruct);
        }

        return NewPathA;
    }

    [MenuItem("GameObject/Ice Saw/Path A", false, 201)]
    public static void CreatePathA(MenuCommand menuCommand)
    {
        GameObject TempObject = new GameObject("Path A");
        if (menuCommand.context != null)
        {
            var AddToObject = (GameObject)menuCommand.context;
            TempObject.transform.parent = AddToObject.transform;
        }
        TempObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        TempObject.transform.localScale = new Vector3(1, 1, 1);
        Selection.activeGameObject = TempObject;
        TempObject.AddComponent<TrickyPathAObject>().AddMissingComponents();

    }

    public void DrawLines()
    {
        lineRenderer.positionCount = PathPoints.Count+1;
        lineRenderer.SetPosition(0, transform.TransformPoint(new Vector3(0, 0, 0)));
        for (int i = 0; i < PathPoints.Count; i++)
        {
            lineRenderer.SetPosition(i+1, transform.TransformPoint(PathPoints[i]));
        }
    }
    [ContextMenu("Reset Tranformation")]
    public void ResetTransformation()
    {
        var Positions = PathPoints;

        for (int i = 0;i < Positions.Count;i++)
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

    Vector3 ConvertLocalPoint(Vector3 point)
    {
        return transform.InverseTransformPoint(TrickyLevelManager.Instance.transform.TransformPoint(point));
    }

    Vector3 ConvertWorldPoint(Vector3 point)
    {
        return TrickyLevelManager.Instance.transform.InverseTransformPoint(transform.TransformPoint(point));
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

[CustomEditor(typeof(TrickyPathAObject))]
public class TrickyPathAObjectEditor : Editor
{
    private Vector3[] positions;
    void OnSceneGUI()
    {
        TrickyPathAObject connectedObjects = target as TrickyPathAObject;
        if (!connectedObjects.Hold)
        {
            if (TrickyLevelManager.Instance.EditMode && TrickyLevelManager.Instance.PathEventMode)
            {
                Tools.current = Tool.None;
                positions = new Vector3[connectedObjects.PathEvents.Count * 2];
                for (int i = 0; i < connectedObjects.PathEvents.Count * 2; i++)
                {
                    positions[i*2] = Handles.PositionHandle(connectedObjects.transform.TransformPoint(connectedObjects.FindPathLocalPoint(connectedObjects.PathEvents[i].EventStart)), Quaternion.identity);
                    positions[i*2 + 1] = Handles.PositionHandle(connectedObjects.transform.TransformPoint(connectedObjects.FindPathLocalPoint(connectedObjects.PathEvents[i].EventEnd)), Quaternion.identity);
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
