using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.Tricky;
using static SSXMultiTool.JsonFiles.Tricky.AIPSOPJsonHandler;
using SSXMultiTool.Utilities;

public class PathAObject : MonoBehaviour
{
    public int Type;
    public int U1;
    public int U2;
    public int U3;
    public int U4;
    public int U5;
    public int U6;

    [OnChangedCall("DrawLines")]
    public List<Vector3> PathPoints;
    public List<UnknownStruct> UnknownStructs;

    LineRenderer lineRenderer;

    public void LoadPathA(AIPSOPJsonHandler.PathA pathA)
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.hideFlags = HideFlags.HideInInspector;

        Type = pathA.Type;
        U1 = pathA.U1;
        U2 = pathA.U2;
        U3 = pathA.U3;
        U4 = pathA.U4;
        U5 = pathA.U5;
        U6 = pathA.U6;

        transform.localPosition = JsonUtil.ArrayToVector3(pathA.PathPos);

        PathPoints = new List<Vector3>();

        for (int i = 0; i < pathA.PathPoints.GetLength(0); i++)
        {
            PathPoints.Add(new Vector3(pathA.PathPoints[i,0], pathA.PathPoints[i, 1], pathA.PathPoints[i, 2]));
        }

        UnknownStructs = new List<UnknownStruct>();
        for (int i = 0; i < pathA.UnknownStructs.Count; i++)
        {
            var NewStruct = new UnknownStruct();

            NewStruct.U0 = pathA.UnknownStructs[i].U0;
            NewStruct.U1 = pathA.UnknownStructs[i].U1;
            NewStruct.U2 = pathA.UnknownStructs[i].U2;
            NewStruct.U3 = pathA.UnknownStructs[i].U3;

            UnknownStructs.Add(NewStruct);
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
        NewPathA.U6 = U6;

        NewPathA.PathPos = JsonUtil.Vector3ToArray(transform.localPosition);

        NewPathA.PathPoints = new float[PathPoints.Count, 3];

        for (int i = 0; i < PathPoints.Count; i++)
        {
            NewPathA.PathPoints[i, 0] = PathPoints[i].x;
            NewPathA.PathPoints[i, 1] = PathPoints[i].y;
            NewPathA.PathPoints[i, 2] = PathPoints[i].z;
        }

        NewPathA.UnknownStructs = new List<AIPSOPJsonHandler.UnknownStruct>();

        for (int i = 0; i < UnknownStructs.Count; i++)
        {
            var NewStruct = new AIPSOPJsonHandler.UnknownStruct();

            NewStruct.U0 = UnknownStructs[i].U0;
            NewStruct.U1 = UnknownStructs[i].U1;
            NewStruct.U2 = UnknownStructs[i].U2;
            NewStruct.U3 = UnknownStructs[i].U3;

            NewPathA.UnknownStructs.Add(NewStruct);
        }

        return NewPathA;
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
    public struct UnknownStruct
    {
        public int U0;
        public int U1;
        public float U2;
        public float U3;
    }
}
