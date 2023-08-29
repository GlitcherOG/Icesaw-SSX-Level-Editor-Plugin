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
        string SavePath = "";

        //Generate MMD List
        List<MassModelData> MMD = new List<MassModelData>();
        var TempPrefabList = WorldManager.Instance.GetPatchList();
        
        for (int i = 0; i < TempPrefabList.Length; i++)
        {
            MMD.add(TempPrefabList[i].GenerateModel());
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
