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
        gameObject.name = json.MaterialName;

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
}
