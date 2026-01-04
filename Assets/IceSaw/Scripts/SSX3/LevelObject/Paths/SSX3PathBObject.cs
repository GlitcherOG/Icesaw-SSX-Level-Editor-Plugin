using SSXMultiTool.Utilities;
using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.SSX3;

public class SSX3PathBObject : MonoBehaviour
{
    [HideInInspector]
    public LineRenderer lineRenderer;

    public int Type;
    public int U0;
    public int U1;
    public float U2;

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
        lineRenderer.material = SSX3WorldManager.Instance.RaceLine;
        lineRenderer.textureMode = LineTextureMode.Tile;
    }

    public void LoadPathB(AIPJsonHandler.TrackPath pathB)
    {
        AddMissingComponents();

        Type = pathB.Type;
        U0 = pathB.U0;
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
