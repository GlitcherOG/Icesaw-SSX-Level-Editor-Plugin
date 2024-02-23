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

            NewStruct.U0 = pathA.PathEvents[i].U0;
            NewStruct.U1 = pathA.PathEvents[i].U1;
            NewStruct.U2 = pathA.PathEvents[i].U2;
            NewStruct.U3 = pathA.PathEvents[i].U3;

            PathEvents.Add(NewStruct);
        }
        DrawLines();
    }

    public AIPSOPJsonHandler.PathA GeneratePathA()
    {
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

            NewStruct.U0 = PathEvents[i].U0;
            NewStruct.U1 = PathEvents[i].U1;
            NewStruct.U2 = PathEvents[i].U2;
            NewStruct.U3 = PathEvents[i].U3;

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
        lineRenderer.positionCount = PathPoints.Count;
        for (int i = 0; i < PathPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, PathPoints[i]);
        }
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
