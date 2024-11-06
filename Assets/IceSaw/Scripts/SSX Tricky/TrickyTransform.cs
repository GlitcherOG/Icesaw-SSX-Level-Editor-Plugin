using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CanEditMultipleObjects]
[ExecuteInEditMode]
public class TrickyTransform : MonoBehaviour
{
    public static Matrix4x4 SSXMatrix;
    public static Matrix4x4 SSXRotationMatrix;
    public static Matrix4x4 SSXScaleMatrix;

    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;

    public void Awake()
    {
        //if(SSXMatrix == null)
        //{
        SSXMatrix = Matrix4x4.TRS(new Vector3(0,0,0), Quaternion.Euler(new Vector3(-90, 0, 0)), new Vector3(1, -1, 1) * SSXProjectWindow.Scale);
        SSXRotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(new Vector3(-90, 0, 0)));
        SSXScaleMatrix = Matrix4x4.Scale(new Vector3(1, -1, 1) * SSXProjectWindow.Scale);
        //}

        //this.transform.hideFlags = HideFlags.HideInInspector;

        Position = SSXMatrix.inverse.MultiplyPoint(transform.localPosition);
        var TempRot = Quaternion.Euler(new Vector3(-90, 0, 0));
        Rotation =  (Quaternion.Inverse(transform.localRotation) * TempRot).eulerAngles;

        Scale = SSXScaleMatrix.inverse.MultiplyPoint(transform.localScale);
    }

}
