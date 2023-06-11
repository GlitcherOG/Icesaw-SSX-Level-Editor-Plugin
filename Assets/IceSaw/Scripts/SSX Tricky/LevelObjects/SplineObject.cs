using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;

public class SplineObject : MonoBehaviour
{

    public void LoadSpline(SplineJsonHandler.SplineJson spline)
    {
        transform.name= spline.SplineName;

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
