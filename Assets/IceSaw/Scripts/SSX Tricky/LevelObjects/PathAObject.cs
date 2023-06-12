using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.Tricky;

public class PathAObject : MonoBehaviour
{
    public int Type;
    public int U1;
    public int U2;
    public int U3;
    public int U4;
    public int U5;
    public int U6;

    public float[] PathPos;

    public float[,] PathPoints;
    public List<UnknownStruct> UnknownStructs;

    public void LoadPathA(AIPSOPJsonHandler.PathA pathA)
    {
        Type = pathA.Type;
        U1 = pathA.U1;
        U2 = pathA.U2;
        U3 = pathA.U3;
        U4 = pathA.U4;
        U5 = pathA.U5;
        U6 = pathA.U6;

        PathPos = pathA.PathPos;

        PathPoints = pathA.PathPoints;

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
