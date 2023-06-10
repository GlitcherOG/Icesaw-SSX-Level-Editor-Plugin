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

    GameObject WorldManager;
    GameObject SkyboxManager;
    GameObject PrefabManager;
    public static string CurrentPath;

    public static float Scale = 0.01f;

    [MenuItem("Ice Saw/Project")]
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

        }
        if (GUILayout.Button("Reload Project", GUILayout.Width(WindowHalfSize), GUILayout.Height(40)))
        {

        }
        GUILayout.EndHorizontal();
    }

    public void LoadProject()
    {
        string path = EditorUtility.OpenFilePanel("Open SSX Tricky Prject", "", "SSX");

        if(path.Length!=0)
        {
            CurrentPath = Path.GetDirectoryName(path);
            LoadProjectData();
        }
    }
    public void ClearCurrentProject()
    {
        if(WorldManager!=null)
        {
            DestroyImmediate(WorldManager);
            DestroyImmediate(SkyboxManager);
            DestroyImmediate(PrefabManager);
        }
        else
        {
            WorldManager = GameObject.Find("/Tricky World Manager");
            SkyboxManager = GameObject.Find("/Tricky Skybox Manager");
            PrefabManager = GameObject.Find("/Tricky Prefab Manager");

            if(WorldManager != null)
            {
                DestroyImmediate(WorldManager);
                DestroyImmediate(SkyboxManager);
                DestroyImmediate(PrefabManager);
            }
        }


    }
    public void GenerateEmptyProject()
    {
        ClearCurrentProject();

        //Generate World Manager
        WorldManager = new GameObject("Tricky World Manager");
        var TempWorld = WorldManager.AddComponent<WorldManager>();
        TempWorld.runInEditMode = true;
        TempWorld.SetStatic();
        TempWorld.GenerateEmptyObjects();

        //Generate Skybox Manager
        SkyboxManager = new GameObject("Tricky Skybox Manager");

        //Generate Prefab Manager
        PrefabManager = new GameObject("Tricky Prefab Manager");
        var TempPrefab = PrefabManager.AddComponent<PrefabManager>();
        TempPrefab.runInEditMode = true;
        TempPrefab.SetStatic();
        TempPrefab.GenerateEmptyObjects();

    }
    public void LoadProjectData()
    {
        GenerateEmptyProject();
        WorldManager.GetComponent<WorldManager>().LoadData();
        PrefabManager.GetComponent<PrefabManager>().LoadData();
    }
}
