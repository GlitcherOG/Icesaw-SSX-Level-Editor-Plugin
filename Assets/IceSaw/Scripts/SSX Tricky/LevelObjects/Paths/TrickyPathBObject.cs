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

    public float TestDistance;

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
        bool find = false;
        Type = pathB.Type;
        U1 = pathB.U1;
        U2 = pathB.U2;

        transform.localPosition = JsonUtil.ArrayToVector3(pathB.PathPos);

        PathPoints = new List<Vector3>();
        //List<Vector3> PathPointsOld = new List<Vector3>();
        for (int i = 0; i < pathB.PathPoints.GetLength(0); i++)
        {
            PathPoints.Add(new Vector3(pathB.PathPoints[i, 0], pathB.PathPoints[i, 1], pathB.PathPoints[i, 2]));
            //PathPointsOld.Add(new Vector3(pathB.PathPoints[i, 0], pathB.PathPoints[i, 1], pathB.PathPoints[i, 2]));
            TestDistance += Vector2.Distance(PathPoints[i], new Vector2(0,0));

            if (TestDistance > U2 && !find)
            {
                find = true;
                GameObject tempGameObject = new GameObject("Test " + i + " " + gameObject.name);
                tempGameObject.transform.parent = gameObject.transform.parent;
                tempGameObject.transform.localScale = gameObject.transform.localScale;

                Vector3 vector3 = Vector3.Normalize(PathPoints[i]);

                vector3 = vector3 * (U2 - TestDistance);
                tempGameObject.transform.localPosition = vector3+ PathPoints[i - 1]+gameObject.transform.localPosition;
            }

            if (i!=0)
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

            NewStruct.EventType = PathEvents[i].EventType;
            NewStruct.EventValue = PathEvents[i].EventValue;
            NewStruct.EventStart = PathEvents[i].EventStart;
            NewStruct.EventEnd = PathEvents[i].EventEnd;

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

    public void CalcualteEventPoints()
    {
        List<float> Distances = new List<float>();
        Vector3 PrevPoint = Vector3.zero;
        for (int i = 0;i < PathPoints.Count;i++)
        {
            Distances.Add(Vector3.Distance(PrevPoint, PathPoints[i]));
            PrevPoint = PathPoints[i];
        }

        int AlongPoint = 0;
        for (int i = 0; i < Distances.Count; i++)
        {
            AlongPoint = i;


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
        public int EventType;
        public int EventValue;
        public float EventStart;
        public float EventEnd;
    }

}
