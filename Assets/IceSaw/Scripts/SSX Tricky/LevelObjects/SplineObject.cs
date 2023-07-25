using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using UnityEditor;

public class SplineObject : MonoBehaviour
{
    public int U0;
    public int U1;
    public int SplineStyle;

    public void LoadSpline(SplineJsonHandler.SplineJson spline)
    {
        transform.name= spline.SplineName;
        U0 = spline.U0;
        U1 = spline.U1;
        SplineStyle = spline.SplineStyle;

        transform.localPosition = JsonUtil.ArrayToVector3(spline.Segments[0].Point1);

        for (int i = 0; i < spline.Segments.Count; i++)
        {
            var TempGameobject = new GameObject("Segment " + i);
            TempGameobject.transform.parent = transform;
            TempGameobject.transform.localPosition = Vector3.zero;
            TempGameobject.transform.localScale = Vector3.one;
            TempGameobject.transform.localEulerAngles = Vector3.zero;
            var TempObj = TempGameobject.AddComponent<SplineSegmentObject>();
            TempObj.LoadSplineSegment(spline.Segments[i]);

        }
    }

    public SplineJsonHandler.SplineJson GenerateSpline()
    {
        SplineJsonHandler.SplineJson spline = new SplineJsonHandler.SplineJson();

        spline.SplineName = transform.name;

        spline.U0 = U0;
        spline.U1 = U1;
        spline.SplineStyle = SplineStyle;
        spline.Segments = new List<SplineJsonHandler.SegmentJson>();

        var Segments = transform.GetComponentsInChildren<SplineSegmentObject>();

        for (int i = 0; i < Segments.Length; i++)
        {
            spline.Segments.Add(Segments[i].GenerateSplineSegment());
        }

        return spline;
    }

    [MenuItem("GameObject/Ice Saw/Spline", false, 11)]
    public static void CreateSpline(MenuCommand menuCommand)
    {
        GameObject TempObject = new GameObject("Spline");
        if (menuCommand.context != null)
        {
            var AddToObject = (GameObject)menuCommand.context;
            TempObject.transform.parent = AddToObject.transform;
        }
        TempObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        TempObject.transform.localScale = new Vector3(1, 1, 1);
        Selection.activeGameObject = TempObject;
        TempObject.AddComponent<SplineObject>();

    }

}
