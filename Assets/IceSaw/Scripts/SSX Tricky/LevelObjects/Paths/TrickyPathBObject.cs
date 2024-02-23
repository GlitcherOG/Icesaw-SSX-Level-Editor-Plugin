using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static SSXMultiTool.JsonFiles.Tricky.AIPSOPJsonHandler;

public class TrickyPathBObject : MonoBehaviour
{
    public int Type;
    public int U0;
    public int U1;
    public float U2;

    [OnChangedCall("DrawLines")]
    public List<Vector3> PathPoints;
    public List<PathEvent> PathEvents;

    LineRenderer lineRenderer;

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

        for (int i = 0; i < pathB.PathPoints.GetLength(0); i++)
        {
            PathPoints.Add(new Vector3(pathB.PathPoints[i, 0], pathB.PathPoints[i, 1], pathB.PathPoints[i, 2]));
            if(i!=0)
            {
                PathPoints[i] += PathPoints[i - 1];
            }
        }

        PathEvents = new List<PathEvent>();
        for (int i = 0; i < pathB.PathEvents.Count; i++)
        {
            var NewStruct = new PathEvent();

            NewStruct.U0 = pathB.PathEvents[i].U0;
            NewStruct.U1 = pathB.PathEvents[i].U1;
            NewStruct.U2 = pathB.PathEvents[i].U2;
            NewStruct.U3 = pathB.PathEvents[i].U3;

            PathEvents.Add(NewStruct);
        }

        DrawLines();
    }

    public AIPSOPJsonHandler.PathB GeneratePathB()
    {
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

            NewStruct.U0 = PathEvents[i].U0;
            NewStruct.U1 = PathEvents[i].U1;
            NewStruct.U2 = PathEvents[i].U2;
            NewStruct.U3 = PathEvents[i].U3;

            pathB.PathEvents.Add(NewStruct);
        }


        return pathB;
    }

    public void DrawLines()
    {
        lineRenderer.positionCount = PathPoints.Count;
        for (int i = 0; i < PathPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, PathPoints[i]);
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

    [System.Serializable]
    public struct PathEvent
    {
        public int U0;
        public int U1;
        public float U2;
        public float U3;
    }

}
