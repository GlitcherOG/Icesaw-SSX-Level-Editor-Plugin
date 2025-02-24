using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ObjInstanceImport : MonoBehaviour
{

    void LoadOBJGeneratePrefabandInstance()
    {
        string path = EditorUtility.OpenFilePanel("Import OBJ Model", "", "obj");
        //Load OBJ Plus MLT

        string FileName = Path.GetFileName(path);

        File.Copy(path, TrickyLevelManager.Instance.LoadPath + "\\Models\\" + FileName);
        //TrickyLevelManager.Instance.ReloadModels();

        //Generate Material

        //Generate Prefab

        //Generate Instance
    }
}
