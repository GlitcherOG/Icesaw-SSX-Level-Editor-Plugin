using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : TrickyBaseObject
{
    public override ObjectType Type
    {
        get { return ObjectType.PathManager; }
    }

    public PathManagerType pathManagerType;

    [HideInInspector]
    GameObject PathAHolder;
    [HideInInspector]
    GameObject PathBHolder;

    public List<int> StartPos = new List<int>();


    public void GenerateEmptyObjects(PathManagerType type)
    {
        pathManagerType = type;

        PathAHolder = new GameObject("AI Path");
        PathAHolder.transform.parent = transform;
        PathAHolder.transform.localScale = Vector3.one;
        PathAHolder.transform.localEulerAngles = Vector3.zero;
        PathAHolder.transform.hideFlags = HideFlags.HideInInspector;

        PathBHolder = new GameObject("Race Lines");
        PathBHolder.transform.parent = transform;
        PathBHolder.transform.localScale = Vector3.one;
        PathBHolder.transform.localEulerAngles = Vector3.zero;
        PathBHolder.transform.hideFlags = HideFlags.HideInInspector;
    }

    public void LoadJson(AIPSOPJsonHandler aipsopJsonHandler)
    {
        StartPos = aipsopJsonHandler.StartPosList;
        GeneratePathAs(aipsopJsonHandler.AIPaths);
        GeneratePathBs(aipsopJsonHandler.RaceLines);
    }

    public void GeneratePathAs(List<AIPSOPJsonHandler.PathA> pathAs)
    {
        for (int i = 0; i < pathAs.Count; i++)
        {
            var TempGameObject = new GameObject("PathA " + i);
            TempGameObject.transform.parent = PathAHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            var TempInstance = TempGameObject.AddComponent<TrickyPathAObject>();
            TempInstance.LoadPathA(pathAs[i]);
        }
    }

    public void GeneratePathBs(List<AIPSOPJsonHandler.PathB> pathBs)
    {
        for (int i = 0; i < pathBs.Count; i++)
        {
            var TempGameObject = new GameObject("PathB " + i);
            TempGameObject.transform.parent = PathBHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            var TempInstance = TempGameObject.AddComponent<TrickyPathBObject>();
            TempInstance.LoadPathB(pathBs[i]);
        }
    }

    public void SaveJson(string Path)
    {
        AIPSOPJsonHandler aipsopJsonHandler = new AIPSOPJsonHandler();

        aipsopJsonHandler.StartPosList = StartPos;
        aipsopJsonHandler.RaceLines = new List<AIPSOPJsonHandler.PathB>();
        aipsopJsonHandler.AIPaths = new List<AIPSOPJsonHandler.PathA>();

        var TempPathAList = transform.GetComponentsInChildren<TrickyPathAObject>();

        for (int i = 0; i < TempPathAList.Length; i++)
        {
            aipsopJsonHandler.AIPaths.Add(TempPathAList[i].GeneratePathA());
        }

        var TempPathBList = transform.GetComponentsInChildren<TrickyPathBObject>();

        for (int i = 0; i < TempPathBList.Length; i++)
        {
            aipsopJsonHandler.RaceLines.Add(TempPathBList[i].GeneratePathB());
        }

        aipsopJsonHandler.CreateJson(Path);
    }



    public enum PathManagerType
    {
        General,
        Showoff
    }
}
