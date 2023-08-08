using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class TrickyProjectWindow : EditorWindow
{
    public static TrickyProjectWindow Instance;

    public static string CurrentPath;

    public static float Scale = 0.01f;

    //[MenuItem("Ice Saw/Project",false,-1000)]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        TrickyProjectWindow window = (TrickyProjectWindow)EditorWindow.GetWindow(typeof(TrickyProjectWindow));
        window.Show();
    }
    void OnGUI()
    {
        int WindowHalfSize = (int)(position.width/2)-4;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("New Project", GUILayout.Width(WindowHalfSize), GUILayout.Height(40)))
        {
            GenerateEmptyProject();
        }
        if (GUILayout.Button("Load Project", GUILayout.Width(WindowHalfSize), GUILayout.Height(40)))
        {
            LoadProject();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save Project", GUILayout.Width(WindowHalfSize), GUILayout.Height(40)))
        {
            SaveProject();
        }
        if (GUILayout.Button("Reload Project", GUILayout.Width(WindowHalfSize), GUILayout.Height(40)))
        {

        }
        GUILayout.EndHorizontal();
    }
    [MenuItem("Ice Saw/Load Project", false, -1000)]
    public static void LoadProject()
    {
        string path = EditorUtility.OpenFilePanel("Open SSX Tricky Project", "", "SSX");

        if(path.Length!=0)
        {
            CurrentPath = Path.GetDirectoryName(path);
            LoadProjectData();
        }
    }
    [MenuItem("Ice Saw/Save Project", false, -1000)]
    public static void SaveProject()
    {
        string path = EditorUtility.SaveFilePanel("Open SSX Tricky Project", "", "Config" , "SSX");

        if (path.Length != 0)
        {
            CurrentPath = Path.GetDirectoryName(path);
            SaveProjectData();
        }
    }
    public static void ClearCurrentProject()
    {
        if(LevelManager.Instance!=null)
        {
            DestroyImmediate(LevelManager.Instance.gameObject);
        }
    }
    public static void GenerateEmptyProject()
    {
        ClearCurrentProject();
        var LevelManagerObject = new GameObject("Tricky Level Manager");
        LevelManagerObject.transform.transform.localScale = new Vector3(1,-1,1) * TrickyProjectWindow.Scale;
        LevelManagerObject.transform.eulerAngles = new Vector3(-90, 0, 0);
        LevelManagerObject.AddComponent<LevelManager>();
    }
    public static void LoadProjectData()
    {
        GenerateEmptyProject();

        LevelManager.Instance.LoadData(CurrentPath);
    }

    public static void SaveProjectData()
    {
        LevelManager.Instance.SaveData(CurrentPath);
    }
}
