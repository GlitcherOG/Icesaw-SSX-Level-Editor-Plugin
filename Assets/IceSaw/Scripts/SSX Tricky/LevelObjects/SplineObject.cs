using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;

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

        for (int i = 0; i < spline.Segments.Count; i++)
        {
            var TempGameobject = new GameObject("Segment " + i);
            TempGameobject.transform.parent = transform;
            TempGameobject.transform.localScale = Vector3.one;
            TempGameobject.transform.localEulerAngles = Vector3.zero;
            var TempObj = TempGameobject.AddComponent<SplineSegmentObject>();
            TempObj.LoadSplineSegment(spline.Segments[i]);

        }
    }

    //public SplineJsonHandler.SplineJson GenerateSpline()
    //{
    //    SplineJsonHandler.SplineJson spline = new SplineJsonHandler.SplineJson();

    //    spline.SplineName = SplineName;
    //    spline.Segments = new List<SplineJsonHandler.SegmentJson>();

    //    for (int i = 0; i < splineSegmentObjects.Count; i++)
    //    {
    //        spline.Segments.Add(splineSegmentObjects[i].GenerateSplineSegment());
    //    }

    //    return spline;
    //}

}
