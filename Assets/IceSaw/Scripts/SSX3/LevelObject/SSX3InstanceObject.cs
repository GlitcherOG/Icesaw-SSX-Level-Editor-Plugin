using SSXMultiTool.JsonFiles.SSX3;
using SSXMultiTool.Utilities;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteInEditMode]
[SelectionBase]
public class SSX3InstanceObject : MonoBehaviour
{
    public int TrackID;
    public int RID;

    public int U0;
    public int U1;
    public int U2;
    public int U3;

    public Vector4 V0;
    public Vector3 V1;
    public Vector3 V2;

    public int U4;

    public int ModelTrackID;
    public int ModelRID;

    public float U5;
    public int U6;
    public int U7;

    public int U8;
    public int U9;
    public int U10;
    public int U11;
    public int U12;

    public void LoadBin3(InstanceJsonHandler.Instance bin3)
    {
        transform.name = bin3.Name;

        transform.localEulerAngles = JsonUtil.ArrayToQuaternion(bin3.Rotation).eulerAngles;
        transform.localScale = JsonUtil.ArrayToVector3(bin3.Scale);
        transform.localPosition = JsonUtil.ArrayToVector3(bin3.Position);

        U0 = bin3.U0;
        U1 = bin3.U1;
        U2 = bin3.U2;
        U3 = bin3.U3;

        V0 = JsonUtil.ArrayToVector4(bin3.V0);
        V1 = JsonUtil.ArrayToVector3(bin3.V1);
        V2 = JsonUtil.ArrayToVector3(bin3.V2);

        TrackID = bin3.TrackID;
        RID = bin3.RID;
        U4 = bin3.U4;

        ModelTrackID = bin3.ModelTrackID;
        ModelRID = bin3.ModelRID;

        U5 = bin3.U5;
        U6 = bin3.U6;
        U7 = bin3.U7;

        U8 = bin3.U8;
        U9 = bin3.U9;
        U10 = bin3.U10;
        U11 = bin3.U11;
        U12 = bin3.U12;
    }
}