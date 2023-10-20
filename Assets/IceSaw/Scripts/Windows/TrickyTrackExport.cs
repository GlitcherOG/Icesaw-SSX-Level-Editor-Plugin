using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TrickyTrackExport
{
    [MenuItem("Ice Saw/Export Track")]
    public static void ExportTrackData()
    {
        if (TrickyLevelManager.Instance != null && TrickyPrefabManager.Instance != null && TrickyWorldManager.Instance)
        {
            //Get Save path
            //Sandard method of having them save an obj file and stripping out the obj file to get a path
            string SavePath = EditorUtility.SaveFilePanel("Open SSX Tricky Model", "", "OBJ Model", "obj");

            //Generate MMD List
            List<ObjExporter.MassModelData> MMD = new List<ObjExporter.MassModelData>();

            var TempPatchList = TrickyWorldManager.Instance.GetPatchList();
            for (int i = 0; i < TempPatchList.Length; i++)
            {
                MMD.Add(TempPatchList[i].GenerateModel());
            }

            var TempInstanceList = TrickyWorldManager.Instance.GetInstanceList();
            for (int i = 0; i < TempInstanceList.Length; i++)
            {
                MMD.AddRange(TempInstanceList[i].GenerateModel());
            }

            //Save Objects 
            ObjExporter.SaveModelList(SavePath, MMD, TrickyLevelManager.Instance.texture2Ds);
        }
        else if (OGLevelManager.Instance != null && OGPrefabManager.Instance != null && OGWorldManager.Instance)
        {
            //Get Save path
            //Sandard method of having them save an obj file and stripping out the obj file to get a path
            string SavePath = EditorUtility.SaveFilePanel("Open SSX Tricky Model", "", "OBJ Model", "obj");

            //Generate MMD List
            List<ObjExporter.MassModelData> MMD = new List<ObjExporter.MassModelData>();

            var TempPatchList = OGWorldManager.Instance.GetPatchList();
            for (int i = 0; i < TempPatchList.Length; i++)
            {
                MMD.Add(TempPatchList[i].GenerateModel());
            }

            var TempInstanceList = OGWorldManager.Instance.GetInstanceList();
            for (int i = 0; i < TempInstanceList.Length; i++)
            {
                MMD.AddRange(TempInstanceList[i].GenerateModel());
            }

            //Save Objects 
            ObjExporter.SaveModelList(SavePath, MMD, OGLevelManager.Instance.texture2Ds);
        }
        else
        {
            Debug.Log("Ice Saw - Unable to Export Track. Level Manager or Prefab Manager Not detected");
        }
    }
}
