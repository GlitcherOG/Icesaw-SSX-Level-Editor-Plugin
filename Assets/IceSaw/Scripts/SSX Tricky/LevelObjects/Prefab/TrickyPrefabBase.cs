using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

[System.Serializable]
[SelectionBase]
public class TrickyPrefabBase : MonoBehaviour
{
    public int Unknown3;
    public float AnimTime;

    public void ForceReloadMeshMat()
    {
        var TempHeader = GetComponentsInChildren<TrickyPrefabSubObject>();

        for (int i = 0; i < TempHeader.Length; i++)
        {
            TempHeader[i].ForceRegenMeshMat();
        }
    }

    public string[] GetTextureNames()
    {
        List<string> TextureNames = new List<string>();
        var TempList = GetComponentsInChildren<TrickyPrefabSubObject>();

        for (int i = 0; i < TempList.Length; i++)
        {
            var TempModel = TempList[i].GetComponentsInChildren<PrefabMeshObject>();
            for (int a = 0; a < TempModel.Length; a++)
            {
                TextureNames.Add(TempModel[a].TrickyMaterialObject.TexturePath);
            }
        }
        return TextureNames.ToArray();
    }
}
