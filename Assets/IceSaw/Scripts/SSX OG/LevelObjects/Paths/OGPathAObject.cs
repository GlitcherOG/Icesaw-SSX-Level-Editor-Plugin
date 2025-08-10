using SSXMultiTool.JsonFiles.SSXOG;
using SSXMultiTool.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OGPathAObject : MonoBehaviour
{
    public float U0;

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
        lineRenderer.material = OGLevelManager.Instance.RaceLine;
        lineRenderer.textureMode = LineTextureMode.Tile;
    }


    public void LoadPathA(AIPJsonHandler.PathData pathA)
    {
        AddMissingComponents();

        U0 = pathA.U0;

        transform.localPosition = JsonUtil.ArrayToVector3(pathA.PathPos);

        PathPoints = new List<Vector3>();

        for (int i = 0; i < pathA.PathPoints.GetLength(0); i++)
        {
            PathPoints.Add(new Vector3(pathA.PathPoints[i, 0], pathA.PathPoints[i, 1], pathA.PathPoints[i, 2]));
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

    //public AIPSOPJsonHandler.PathA GeneratePathA()
    //{
    //    AIPSOPJsonHandler.PathA NewPathA = new AIPSOPJsonHandler.PathA();

    //    NewPathA.Type = Type;
    //    NewPathA.U1 = U1;
    //    NewPathA.U2 = U2;
    //    NewPathA.U3 = U3;
    //    NewPathA.U4 = U4;
    //    NewPathA.U5 = U5;
    //    NewPathA.Respawnable = Respawnable;

    //    NewPathA.PathPos = JsonUtil.Vector3ToArray(transform.localPosition);

    //    NewPathA.PathPoints = new float[PathPoints.Count, 3];

    //    for (int i = 0; i < PathPoints.Count; i++)
    //    {
    //        NewPathA.PathPoints[i, 0] = PathPoints[i].x;
    //        NewPathA.PathPoints[i, 1] = PathPoints[i].y;
    //        NewPathA.PathPoints[i, 2] = PathPoints[i].z;
    //    }

    //    NewPathA.PathEvents = new List<AIPSOPJsonHandler.PathEvent>();

    //    for (int i = 0; i < PathEvents.Count; i++)
    //    {
    //        var NewStruct = new AIPSOPJsonHandler.PathEvent();

    //        NewStruct.U0 = PathEvents[i].U0;
    //        NewStruct.U1 = PathEvents[i].U1;
    //        NewStruct.U2 = PathEvents[i].U2;
    //        NewStruct.U3 = PathEvents[i].U3;

    //        NewPathA.PathEvents.Add(NewStruct);
    //    }

    //    return NewPathA;
    //}

    //[MenuItem("GameObject/Ice Saw/Path A", false, 201)]
    //public static void CreatePathA(MenuCommand menuCommand)
    //{
    //    GameObject TempObject = new GameObject("Path A");
    //    if (menuCommand.context != null)
    //    {
    //        var AddToObject = (GameObject)menuCommand.context;
    //        TempObject.transform.parent = AddToObject.transform;
    //    }
    //    TempObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
    //    TempObject.transform.localScale = new Vector3(1, 1, 1);
    //    Selection.activeGameObject = TempObject;
    //    TempObject.AddComponent<TrickyPathAObject>().AddMissingComponents();

    //}

    public void DrawLines()
    {
        lineRenderer.positionCount = PathPoints.Count;
        for (int i = 0; i < PathPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, PathPoints[i]);
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
