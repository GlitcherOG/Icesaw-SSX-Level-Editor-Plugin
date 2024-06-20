using SSXMultiTool.JsonFiles.Tricky;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class SSXProjectWindow : EditorWindow
{
    public static SSXProjectWindow Instance;

    public static string CurrentPath;

    public static float Scale = 0.01f;

    //[MenuItem("Ice Saw/Project",false,-1000)]
    //static void Init()
    //{
    //    // Get existing open window or if none, make a new one:
    //    TrickyProjectWindow window = (TrickyProjectWindow)EditorWindow.GetWindow(typeof(TrickyProjectWindow));
    //    window.Show();
    //}
    //void OnGUI()
    //{
    //    int WindowHalfSize = (int)(position.width/2)-4;

    //    GUILayout.BeginHorizontal();
    //    if (GUILayout.Button("New Project", GUILayout.Width(WindowHalfSize), GUILayout.Height(40)))
    //    {
    //        GenerateTrickyEmptyProject();
    //    }
    //    if (GUILayout.Button("Load Project", GUILayout.Width(WindowHalfSize), GUILayout.Height(40)))
    //    {
    //        LoadProject();
    //    }
    //    GUILayout.EndHorizontal();

    //    GUILayout.BeginHorizontal();
    //    if (GUILayout.Button("Save Project", GUILayout.Width(WindowHalfSize), GUILayout.Height(40)))
    //    {
    //        SaveProject();
    //    }
    //    if (GUILayout.Button("Reload Project", GUILayout.Width(WindowHalfSize), GUILayout.Height(40)))
    //    {

    //    }
    //    GUILayout.EndHorizontal();
    //}
    [MenuItem("Ice Saw/Load Project", false, -1000)]
    public static void LoadProject()
    {
        string path = EditorUtility.OpenFilePanel("Open SSX Project", "", "SSX");

        GameCheckerJson trickyConfig = GameCheckerJson.Load(path);
        if (trickyConfig.Game == 1 && trickyConfig.Version == 1)
        {
            CurrentPath = Path.GetDirectoryName(path);
            LoadOGProjectData();
        }
        else if (trickyConfig.Game == 2 && trickyConfig.Version == 2)
        {
            CurrentPath = Path.GetDirectoryName(path);
            LoadTrickyProjectData();
        }
        else
        {
            Debug.LogError("Unknown Game and Version");
        }
    }
    [MenuItem("Ice Saw/Save Project", false, -1000)]
    public static void SaveProject()
    {
        string path = EditorUtility.SaveFilePanel("Open SSX Project", "", "Config" , "SSX");

        //CHECK LOADED VERSION
        if (path.Length != 0)
        {
            CurrentPath = Path.GetDirectoryName(path);
            SaveTrickyProjectData();
        }
    }
    public static void ClearCurrentProject()
    {
        if(TrickyLevelManager.Instance!=null)
        {
            DestroyImmediate(TrickyLevelManager.Instance.gameObject);
        }
        if (OGLevelManager.Instance != null)
        {
            DestroyImmediate(OGLevelManager.Instance.gameObject);
        }
    }
    public static void LoadTrickyProjectData()
    {
        GenerateTrickyEmptyProject();

        TrickyLevelManager.Instance.LoadData(CurrentPath);
    }
    public static void GenerateTrickyEmptyProject()
    {
        ClearCurrentProject();
        var LevelManagerObject = new GameObject("Tricky Level Manager");
        LevelManagerObject.transform.transform.localScale = new Vector3(1, -1, 1) * SSXProjectWindow.Scale;
        LevelManagerObject.transform.eulerAngles = new Vector3(-90, 0, 0);
        LevelManagerObject.AddComponent<TrickyLevelManager>();
    }

    public static void LoadOGProjectData()
    {
        GenerateOGEmptyProject();

        OGLevelManager.Instance.LoadData(CurrentPath);
    }
    public static void GenerateOGEmptyProject()
    {
        ClearCurrentProject();
        var LevelManagerObject = new GameObject("OG Level Manager");
        LevelManagerObject.transform.transform.localScale = new Vector3(1, -1, 1) * SSXProjectWindow.Scale;
        LevelManagerObject.transform.eulerAngles = new Vector3(-90, 0, 0);
        LevelManagerObject.AddComponent<OGLevelManager>();
    }
    public static void SaveTrickyProjectData()
    {
        TrickyLevelManager.Instance.SaveData(CurrentPath);
    }
}
