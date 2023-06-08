using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class TrickyProjectWindow : EditorWindow
{
    GameObject WorldManager;
    GameObject SkyboxManager;
    GameObject PrefabManager;

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

        }
        if (GUILayout.Button("Load Project", GUILayout.Width(WindowHalfSize), GUILayout.Height(40)))
        {

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
        string Path = EditorUtility.OpenFilePanel("Open SSX Tricky Prject", "", "SSX");

        if(Path.Length!=0)
        {

        }
    }

    public void ClearCurrentProject()
    {

    }


    public void GenerateEmptyProject()
    {
        //Generate World Manager

        //Generate Skybox Manager

        //Generate Prefab Manager
    }
}
