using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSXMultiTool.JsonFiles.SSXOG;

public class OGPathFileManager : MonoBehaviour
{
    public static OGPathFileManager Instance;

    public GameObject PathAHolder;
    public GameObject PathBHolder;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
    }

    public void GenerateEmptyObjects()
    {
        transform.hideFlags = HideFlags.HideInInspector;

        PathAHolder = new GameObject("Race Lines");
        PathAHolder.transform.parent = transform;
        PathAHolder.transform.localScale = Vector3.one;
        PathAHolder.transform.localEulerAngles = Vector3.zero;
        PathAHolder.transform.hideFlags = HideFlags.HideInInspector;

        PathBHolder = new GameObject("AI Path");
        PathBHolder.transform.parent = transform;
        PathBHolder.transform.localScale = Vector3.one;
        PathBHolder.transform.localEulerAngles = Vector3.zero;
        PathBHolder.transform.hideFlags = HideFlags.HideInInspector;
    }

    public void LoadData(string Path)
    {
        LoadAIP(Path + "\\AIP.json");
    }

    public void LoadAIP(string Path)
    {
        AIPJsonHandler aipJson = new AIPJsonHandler();
        aipJson = AIPJsonHandler.Load(Path);
        GeneratePathAs(aipJson.PathAs);
        GeneratePathBs(aipJson.PathBs);
    }

    public void GeneratePathAs(List<AIPJsonHandler.PathData> pathAs)
    {
        for (int i = 0; i < pathAs.Count; i++)
        {
            var TempGameObject = new GameObject("PathA " + i);
            TempGameObject.transform.parent = PathAHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            var TempInstance = TempGameObject.AddComponent<OGPathAObject>();
            TempInstance.LoadPathA(pathAs[i]);
        }
    }

    public void GeneratePathBs(List<AIPJsonHandler.PathData> pathBs)
    {
        for (int i = 0; i < pathBs.Count; i++)
        {
            var TempGameObject = new GameObject("PathB " + i);
            TempGameObject.transform.parent = PathBHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            var TempInstance = TempGameObject.AddComponent<OGPathBObject>();
            TempInstance.LoadPathB(pathBs[i]);
        }
    }
}
