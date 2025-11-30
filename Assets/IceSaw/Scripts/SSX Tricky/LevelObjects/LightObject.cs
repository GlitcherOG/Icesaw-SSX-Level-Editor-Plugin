using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using static SSXMultiTool.JsonFiles.Tricky.LightJsonHandler;
using static TrickyBaseObject;

[ExecuteInEditMode]
public class LightObject : TrickyBaseObject
{
    public override ObjectType Type
    {
        get { return ObjectType.Light; }
    }

    public LightType lightType;
    public int SpriteRes;
    public float UnknownFloat1;
    public int UnknownInt1;
    public Vector3 Colour;
    public Vector3 LowestXYZ;
    public Vector3 HighestXYZ;
    public float UnknownFloat2;
    public int UnknownInt2;
    public float UnknownFloat3;
    public int UnknownInt3;

    public int Hash;

    public void LoadLight(LightJsonHandler.LightJson lightJson)
    {
        transform.name = lightJson.LightName;

        lightType = (LightType)lightJson.Type;
        SpriteRes = lightJson.SpriteRes;
        UnknownFloat1 = lightJson.UnknownFloat1;
        UnknownInt1 = lightJson.UnknownInt1;
        Colour = JsonUtil.ArrayToVector3(lightJson.Colour);

        LowestXYZ = JsonUtil.ArrayToVector3(lightJson.LowestXYZ);
        HighestXYZ = JsonUtil.ArrayToVector3(lightJson.HighestXYZ);
        UnknownFloat2 = lightJson.UnknownFloat2;
        UnknownInt2 = lightJson.UnknownInt2;
        UnknownFloat3 = lightJson.UnknownFloat3;
        UnknownInt3 = lightJson.UnknownInt3;
        Hash = lightJson.Hash;

        transform.localPosition = JsonUtil.ArrayToVector3(lightJson.Position);
        transform.localRotation = Quaternion.LookRotation(JsonUtil.ArrayToVector3(lightJson.Direction), Vector3.down);
    }

    public LightJsonHandler.LightJson GenerateLight()
    {
        var NewLight = new LightJsonHandler.LightJson();

        NewLight.LightName = transform.name;
        NewLight.Position = JsonUtil.Vector3ToArray(transform.localPosition);
        NewLight.Type = (int)lightType;
        NewLight.SpriteRes = SpriteRes;
        NewLight.UnknownFloat1 = UnknownFloat1;
        NewLight.UnknownInt1 = UnknownInt1;
        NewLight.Colour = JsonUtil.Vector3ToArray(Colour);
        NewLight.Direction = JsonUtil.Vector3ToArray(TrickyLevelManager.Instance.transform.InverseTransformPoint(transform.TransformVector(Vector3.forward * 100)).normalized);
        NewLight.LowestXYZ = JsonUtil.Vector3ToArray(LowestXYZ);
        NewLight.HighestXYZ = JsonUtil.Vector3ToArray(HighestXYZ);
        NewLight.UnknownFloat2 = UnknownFloat2;
        NewLight.UnknownInt2 = UnknownInt2;
        NewLight.UnknownFloat3 = UnknownFloat3;
        NewLight.UnknownInt3 = UnknownInt3;
        NewLight.Hash = Hash;

        return NewLight;
    }

    [MenuItem("GameObject/Ice Saw/Light", false, 15)]
    public static void CreateLight(MenuCommand menuCommand)
    {
        GameObject TempObject = new GameObject("Light");
        if (menuCommand.context != null)
        {
            var AddToObject = (GameObject)menuCommand.context;
            TempObject.transform.parent = AddToObject.transform;
        }
        TempObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        TempObject.transform.localScale = new Vector3(1, 1, 1);
        Selection.activeGameObject = TempObject;
        TempObject.AddComponent<LightObject>();
    }

    Vector3 ConvertLocalPoint(Vector3 point)
    {
        return transform.InverseTransformPoint(TrickyLevelManager.Instance.transform.TransformPoint(point));
    }

    Vector3 ConvertWorldPoint(Vector3 point)
    {
        return TrickyLevelManager.Instance.transform.InverseTransformPoint(transform.TransformPoint(point));
    }

    public enum LightType
    {
        Directional,
        U0,
        U1,
        Ambient,
    }
}
