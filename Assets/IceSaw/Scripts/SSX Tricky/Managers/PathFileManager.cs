using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SSXMultiTool.JsonFiles.Tricky;

[ExecuteInEditMode]
public class PathFileManager : MonoBehaviour
{
    public static PathFileManager Instance;

    GameObject AIPHolder;
    GameObject SOPHolder;

    private void Awake()
    {
        Instance = this;
    }

    public void GenerateEmptyObjects()
    {
        transform.hideFlags = HideFlags.HideInInspector;

        AIPHolder = new GameObject("AIP");
        AIPHolder.transform.parent = transform;
        AIPHolder.transform.localScale = Vector3.one;
        AIPHolder.transform.localEulerAngles = Vector3.zero;
        AIPHolder.transform.hideFlags = HideFlags.HideInInspector;
        AIPHolder.AddComponent<PathManager>().GenerateEmptyObjects();

        SOPHolder = new GameObject("SOP");
        SOPHolder.transform.parent = transform;
        SOPHolder.transform.localScale = Vector3.one;
        SOPHolder.transform.localEulerAngles = Vector3.zero;
        SOPHolder.transform.hideFlags = HideFlags.HideInInspector;
        SOPHolder.AddComponent<PathManager>().GenerateEmptyObjects();
    }

    public void LoadData(string Path)
    {
        LoadAIP(Path + "\\AIP.json");
        LoadSOP(Path + "\\SOP.json");
    }

    public void LoadAIP(string Path)
    {
        AIPSOPJsonHandler aipJson = new AIPSOPJsonHandler();
        aipJson = AIPSOPJsonHandler.Load(Path);
        AIPHolder.GetComponent<PathManager>().LoadJson(aipJson);
    }

    public void LoadSOP(string Path)
    {
        AIPSOPJsonHandler sopJson = new AIPSOPJsonHandler();
        sopJson = AIPSOPJsonHandler.Load(Path);
        SOPHolder.GetComponent<PathManager>().LoadJson(sopJson);
    }
}