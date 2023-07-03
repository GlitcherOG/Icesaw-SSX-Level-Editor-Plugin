using SSXMultiTool.JsonFiles.Tricky;
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

    public float[] PathPos;

    public float[,] PathPoints;
    public List<UnknownStruct> UnknownStructs;

    public void LoadPathB(AIPSOPJsonHandler.PathB pathB)
    {
        Type = pathB.Type;
        U1 = pathB.U1;
        U2 = pathB.U2;

        PathPos = pathB.PathPos;

        PathPoints = pathB.PathPoints;

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
    }

    public AIPSOPJsonHandler.PathB GeneratePathB()
    {
        AIPSOPJsonHandler.PathB pathB = new AIPSOPJsonHandler.PathB();

        pathB.Type = Type;
        pathB.U1 = U1;
        pathB.U2 = U2;

        pathB.PathPos = PathPos;
        pathB.PathPoints = PathPoints;

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

    [System.Serializable]
    public struct UnknownStruct
    {
        public int U0;
        public int U1;
        public float U2;
        public float U3;
    }

}
