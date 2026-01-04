using SSXMultiTool.JsonFiles.SSX3;
using SSXMultiTool.JsonFiles.Tricky;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static PathManager;

public class SSX3PathManager : MonoBehaviour
{
    [HideInInspector]
    GameObject PathAHolder;
    [HideInInspector]
    GameObject PathBHolder;
    [HideInInspector]
    GameObject UStruct0Holder;
    [HideInInspector]
    GameObject UStruct1Holder;
    public void GenerateEmptyObjects()
    {
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

        UStruct0Holder = new GameObject("UStruct0");
        UStruct0Holder.transform.parent = transform;
        UStruct0Holder.transform.localScale = Vector3.one;
        UStruct0Holder.transform.localEulerAngles = Vector3.zero;
        UStruct0Holder.transform.hideFlags = HideFlags.HideInInspector;

        UStruct1Holder = new GameObject("UStruct1");
        UStruct1Holder.transform.parent = transform;
        UStruct1Holder.transform.localScale = Vector3.one;
        UStruct1Holder.transform.localEulerAngles = Vector3.zero;
        UStruct1Holder.transform.hideFlags = HideFlags.HideInInspector;
    }

    public void LoadJson(string path)
    {
        AIPJsonHandler aipJsonHandler = new AIPJsonHandler();
        aipJsonHandler = AIPJsonHandler.Load(path);

        GenerateEmptyObjects();

        GeneratePathAs(aipJsonHandler.aiPaths);
        GeneratePathBs(aipJsonHandler.trackPaths);
    }

    public void GeneratePathAs(List<AIPJsonHandler.AIPath> pathAs)
    {
        for (int i = 0; i < pathAs.Count; i++)
        {
            var TempGameObject = new GameObject("PathA " + i);
            TempGameObject.transform.parent = PathAHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            var TempInstance = TempGameObject.AddComponent<SSX3PathAObject>();
            TempInstance.LoadPathA(pathAs[i]);
        }
    }

    public void GeneratePathBs(List<AIPJsonHandler.TrackPath> pathBs)
    {
        for (int i = 0; i < pathBs.Count; i++)
        {
            var TempGameObject = new GameObject("PathB " + i);
            TempGameObject.transform.parent = PathBHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            var TempInstance = TempGameObject.AddComponent<SSX3PathBObject>();
            TempInstance.LoadPathB(pathBs[i]);
        }
    }
}
