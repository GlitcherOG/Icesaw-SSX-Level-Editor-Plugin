using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialObject : MonoBehaviour
{
    public string TexturePath;
    public int UnknownInt2;
    public int UnknownInt3;

    public float UnknownFloat1;
    public float UnknownFloat2;
    public float UnknownFloat3;
    public float UnknownFloat4;

    public int UnknownInt8;

    public float UnknownFloat5;
    public float UnknownFloat6;
    public float UnknownFloat7;
    public float UnknownFloat8;

    public int UnknownInt13;
    public int UnknownInt14;
    public int UnknownInt15;
    public int UnknownInt16;
    public int UnknownInt17;
    public int UnknownInt18;

    public List<string> TextureFlipbook;
    public int UnknownInt20;


    public void LoadMaterial(MaterialJsonHandler.MaterialsJson json)
    {
        if (json.MaterialName != "" && json.MaterialName != null)
        {
            gameObject.name = json.MaterialName;
        }

        TexturePath = json.TexturePath;
        UnknownInt2 = json.UnknownInt2;
        UnknownInt3 = json.UnknownInt3;

        UnknownFloat1 = json.UnknownFloat1;
        UnknownFloat2 = json.UnknownFloat2;
        UnknownFloat3 = json.UnknownFloat3;
        UnknownFloat4 = json.UnknownFloat4;

        UnknownInt8 = json.UnknownInt8;

        UnknownFloat5 = json.UnknownFloat5;
        UnknownFloat6 = json.UnknownFloat6;
        UnknownFloat7 = json.UnknownFloat7;
        UnknownFloat8 = json.UnknownFloat8;

        UnknownInt13 = json.UnknownInt13;
        UnknownInt14 = json.UnknownInt14;
        UnknownInt15 = json.UnknownInt15;
        UnknownInt16 = json.UnknownInt16;
        UnknownInt17 = json.UnknownInt17;
        UnknownInt18 = json.UnknownInt18;

        TextureFlipbook = json.TextureFlipbook;
        UnknownInt20 = json.UnknownInt20;

        GenerateMaterialSphere();
    }

    private void GenerateMaterialSphere()
    {
        
    }

    public MaterialJsonHandler.MaterialsJson GenerateMaterial()
    {
        var NewJson = new MaterialJsonHandler.MaterialsJson();

        NewJson.MaterialName = transform.name;

        NewJson.TexturePath = TexturePath;
        NewJson.UnknownInt2 = UnknownInt2;
        NewJson.UnknownInt3 = UnknownInt3;

        NewJson.UnknownFloat1 = UnknownFloat1;
        NewJson.UnknownFloat2 = UnknownFloat2;
        NewJson.UnknownFloat3 = UnknownFloat3;
        NewJson.UnknownFloat4 = UnknownFloat4;

        NewJson.UnknownInt8 = UnknownInt8;

        NewJson.UnknownFloat5 = UnknownFloat5;
        NewJson.UnknownFloat6 = UnknownFloat6;
        NewJson.UnknownFloat7 = UnknownFloat7;
        NewJson.UnknownFloat8 = UnknownFloat8;

        NewJson.UnknownInt13 = UnknownInt13;
        NewJson.UnknownInt14 = UnknownInt14;
        NewJson.UnknownInt15 = UnknownInt15;
        NewJson.UnknownInt16 = UnknownInt16;
        NewJson.UnknownInt17 = UnknownInt17;
        NewJson.UnknownInt18 = UnknownInt18;

        NewJson.TextureFlipbook = TextureFlipbook;
        NewJson.UnknownInt20 = UnknownInt20;

        return NewJson;
    }
}
