using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CanEditMultipleObjects]
[ExecuteInEditMode]
public class TrickyTransform : MonoBehaviour
{
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;

    public void Awake()
    {
        this.transform.hideFlags = HideFlags.HideInInspector;

        Position = this.transform.localPosition;
        Rotation = this.transform.localEulerAngles;
        Scale = this.transform.localScale;
    }
}
