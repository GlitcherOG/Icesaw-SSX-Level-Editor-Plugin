using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SSXMultiTool.JsonFiles.Tricky.AIPSOPJsonHandler;

public class PathBObject : MonoBehaviour
{
    public int Type;
    public int U0;
    public int U1;
    public float U2;

    [OnChangedCall("DrawLines")]
    public List<Vector3> PathPoints;
    public List<UnknownStruct> UnknownStructs;

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
        lineRenderer.material = LevelManager.Instance.RaceLine;
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
        }

        UnknownStructs = new List<UnknownStruct>();
        for (int i = 0; i < pathB.UnknownStructs.Count; i++)
        {
            var NewStruct = new UnknownStruct();

            NewStruct.U0 = pathB.UnknownStructs[i].U0;
            NewStruct.U1 = pathB.UnknownStructs[i].U1;
            NewStruct.U2 = pathB.UnknownStructs[i].U2;
            NewStruct.U3 = pathB.UnknownStructs[i].U3;

            UnknownStructs.Add(NewStruct);
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
        }

        pathB.UnknownStructs = new List<AIPSOPJsonHandler.UnknownStruct>();
        for (int i = 0; i < UnknownStructs.Count; i++)
        {
            var NewStruct = new AIPSOPJsonHandler.UnknownStruct();

            NewStruct.U0 = UnknownStructs[i].U0;
            NewStruct.U1 = UnknownStructs[i].U1;
            NewStruct.U2 = UnknownStructs[i].U2;
            NewStruct.U3 = UnknownStructs[i].U3;

            pathB.UnknownStructs.Add(NewStruct);
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

    [System.Serializable]
    public struct UnknownStruct
    {
        public int U0;
        public int U1;
        public float U2;
        public float U3;
    }

}
