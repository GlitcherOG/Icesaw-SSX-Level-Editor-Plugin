using SSXMultiTool.JsonFiles.SSX3;
using SSXMultiTool.Utilities;
using System.Collections.Generic;
using UnityEngine;

public class SSX3PathAObject : MonoBehaviour
{
    [HideInInspector]
    public LineRenderer lineRenderer;

    public int U0;
    public int U1;
    public int U2;
    public int U3;
    public int U4;
    public int U5;
    public int U6;

    public List<Vector3> PathPoints;
    [HideInInspector]
    public List<Vector3> VectorPoints;

    public List<PathEvent> PathEvents;


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
        lineRenderer.material = SSX3WorldManager.Instance.AIPath;
        lineRenderer.textureMode = LineTextureMode.Tile;
    }

    public void LoadPathA(AIPJsonHandler.AIPath pathA)
    {
        AddMissingComponents();

        U0=pathA.U0;
        U1=pathA.U1;
        U2=pathA.U2;
        U3=pathA.U3;
        U4=pathA.U4;
        U5=pathA.U5;
        U6=pathA.U6;

        transform.localPosition = JsonUtil.ArrayToVector3(pathA.PathPos);

        PathPoints = new List<Vector3>();
        VectorPoints = new List<Vector3>();
        for (int i = 0; i < pathA.PathPoints.GetLength(0); i++)
        {
            VectorPoints.Add(new Vector3(pathA.PathPoints[i, 0], pathA.PathPoints[i, 1], pathA.PathPoints[i, 2]));
            PathPoints.Add(VectorPoints[i]);
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

    public void DrawLines()
    {
        lineRenderer.positionCount = PathPoints.Count + 1;
        lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
        for (int i = 0; i < PathPoints.Count; i++)
        {
            lineRenderer.SetPosition(i + 1, PathPoints[i]);
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
}
