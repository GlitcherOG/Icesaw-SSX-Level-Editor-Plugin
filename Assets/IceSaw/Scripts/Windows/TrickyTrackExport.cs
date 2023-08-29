using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TrickyTrackExport
{
    [MenuItem("Ice Saw/Export Track")]
    public static void ExportTrackData()
    {  
      if(LevelManager.Instance != null && PrefabManager.Instance != null && WorldManager.Instance)
      {
        //Get Save path
        //Sandard method of having them save an obj file and stripping out the obj file to get a path
        string SavePath = EditorUtility.SaveFilePanel("Open SSX Tricky Model", "", "obj");;
        SavePath = Path.GetDirectoryName(SavePath);

        //Generate MMD List
        List<MassModelData> MMD = new List<MassModelData>();

        var TempPatchList = WorldManager.Instance.GetPatchList();
        for (int i = 0; i < TempPatchList.Length; i++)
        {
            MMD.add(TempPatchList[i].GenerateModel());
        }

        var TempInstanceList = WorldManager.Instance.GetInstanceList();
        for (int i = 0; i < TempInstanceList.Length; i++)
        {
            MMD.AddRange(TempInstanceList[i].GenerateModel());
        }
        
        //Save Objects 
        ObjExporter.SaveModelList(SavePath, MMD, LevelManager.Instance.texture2Ds);
      }
      else
      {
          Debug.log("Ice Saw - Unable to Export Track. Level Manager or Prefab Manager Not detected")
      }
    }
}
