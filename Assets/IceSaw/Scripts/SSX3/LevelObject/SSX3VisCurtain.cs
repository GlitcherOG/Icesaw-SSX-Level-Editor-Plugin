using SSXMultiTool.JsonFiles.SSX3;
using SSXMultiTool.Utilities;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteInEditMode]
[SelectionBase]
public class SSX3VisCurtain : MonoBehaviour
{
    public float U0;
    public float U1;
    public float U2;
    public float U3;

    public Vector4 Point4;
    public Vector4 Point3;
    public Vector4 Point2;
    public Vector4 ControlPoint;

    public float U4;
    public float U5;
    public float U6;
    public float U7;

    public LineRenderer lineRenderer;

    public void LoadVisCurtain(VisCurtainJsonHandler.VisCurtain bin11)
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = SSX3LevelManager.Instance.Spline;
        lineRenderer.useWorldSpace = false;

        //transform.localEulerAngles = JsonUtil.ArrayToQuaternion(bin3.Rotation).eulerAngles;
        //transform.localScale = JsonUtil.ArrayToVector3(bin3.Scale);
        //transform.localPosition = JsonUtil.ArrayToVector3(bin3.Position);

        U0 = bin11.U0;
        U1 = bin11.U1;
        U2 = bin11.U2;
        U3 = bin11.U3;

        Point4 = JsonUtil.ArrayToVector4(bin11.Point4);
        Point3 = JsonUtil.ArrayToVector4(bin11.Point3);
        Point2 = JsonUtil.ArrayToVector4(bin11.Point2);
        ControlPoint = JsonUtil.ArrayToVector4(bin11.ControlPoint);

        U4 = bin11.U4;
        U5 = bin11.U5;
        U6 = bin11.U6;
        U7 = bin11.U7;

        lineRenderer.positionCount = 5;

        List<Vector3> curves = new List<Vector3>();
        curves.Add(JsonUtil.Vector4ToVector3(Point4));
        curves.Add(JsonUtil.Vector4ToVector3(Point3));
        curves.Add(JsonUtil.Vector4ToVector3(Point2));
        curves.Add(JsonUtil.Vector4ToVector3(ControlPoint));
        curves.Add(JsonUtil.Vector4ToVector3(Point4));

        lineRenderer.SetPositions(curves.ToArray());
    }
}