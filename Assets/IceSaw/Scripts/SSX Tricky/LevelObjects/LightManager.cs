using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;

[ExecuteInEditMode]
public class LightObject : MonoBehaviour
{
    public int Type;
    public int SpriteRes;
    public float UnknownFloat1;
    public int UnknownInt1;
    public float[] Colour;
    public float[] Direction;
    public float[] Postion;
    public float[] LowestXYZ;
    public float[] HighestXYZ;
    public float UnknownFloat2;
    public int UnknownInt2;
    public float UnknownFloat3;
    public int UnknownInt3;

    public int Hash;

    public void LoadLight(LightJsonHandler.LightJson lightJson)
    {
        transform.name = lightJson.LightName;

        Type = lightJson.Type;
        SpriteRes = lightJson.SpriteRes;
        UnknownFloat1 = lightJson.UnknownFloat1;
        UnknownInt1 = lightJson.UnknownInt1;
        Colour = lightJson.Colour;
        Direction = lightJson.Direction;
        Postion = lightJson.Postion;
        LowestXYZ = lightJson.LowestXYZ;
        HighestXYZ = lightJson.HighestXYZ;
        UnknownFloat2 = lightJson.UnknownFloat2;
        UnknownInt2 = lightJson.UnknownInt2;
        UnknownFloat3 = lightJson.UnknownFloat3;
        UnknownInt3 = lightJson.UnknownInt3;
        Hash = lightJson.Hash;

        transform.localPosition = JsonUtil.ArrayToVector3(lightJson.Postion);
    }
}
