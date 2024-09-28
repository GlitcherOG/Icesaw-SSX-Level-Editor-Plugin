using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PrefabSystem : EditorWindow
{
    [MenuItem("Ice Saw WIP/Prefab Export")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        PrefabSystem window = (PrefabSystem)EditorWindow.GetWindow(typeof(PrefabSystem));
        window.Show();
    }
    Vector2 scrollPos = new Vector2(0, 0);
    bool IncludeInstances;
    bool IncludePrefabs;
    bool IncludeEffectSlots;
    bool IncludeEffects;
    bool IncludeTextures;
    bool IncludeModels;
    bool Recursion;

    bool Patches;
    bool Instances;

    public List<TrickyBaseObject> trickyBaseObjects = new List<TrickyBaseObject>();

    void OnGUI()
    {
        titleContent.text = "Prefab Export";

        EditorGUILayout.LabelField("Selected Objects");
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos,false,true, GUILayout.Height(200));

        int PatchCount = ObjectCount(TrickyBaseObject.ObjectType.Patch);
        if (PatchCount != 0)
        {
            Patches = EditorGUILayout.Foldout(Patches, "Patches (" + PatchCount + ")");

            if (Patches)
            {
                PaintObject(TrickyBaseObject.ObjectType.Patch);
            }
        }

        int InstanceCount = ObjectCount(TrickyBaseObject.ObjectType.Instance);
        if (InstanceCount != 0)
        {
            Instances = EditorGUILayout.Foldout(Instances, "Instances (" + InstanceCount + ")");

            if (Instances)
            {
                PaintObject(TrickyBaseObject.ObjectType.Instance);
            }
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.Separator();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Selection"))
        {
            AddSelection();
        }

        if (GUILayout.Button("Remove Selection"))
        {
            RemoveSelection();
        }
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Clear Selection"))
        {
            trickyBaseObjects = new List<TrickyBaseObject>();
            Repaint();
        }

        IncludeInstances = EditorGUILayout.Toggle("Attached Instances", IncludeInstances);
        IncludePrefabs = EditorGUILayout.Toggle("Attached Prefabs", IncludePrefabs);
        IncludeEffectSlots = EditorGUILayout.Toggle("Attached Effects Slots", IncludeEffectSlots);
        IncludeEffects = EditorGUILayout.Toggle("Attached Effects", IncludeEffects);
        Recursion = EditorGUILayout.Toggle("Recursion", Recursion);
    }

    void PaintObject(TrickyBaseObject.ObjectType objectType)
    {
        for (int i = 0; i < trickyBaseObjects.Count; i++)
        {
            if (objectType == trickyBaseObjects[i].Type)
            {
                GUILayout.BeginHorizontal();
                //EditorGUILayout.SelectableLabel(trickyBaseObjects[i].name);

                if (GUILayout.Button(trickyBaseObjects[i].name, GetBtnStyle()))
                {
                    Selection.activeGameObject = trickyBaseObjects[i].gameObject;
                }

                if (GUILayout.Button("-", GUILayout.Width(30)))
                {
                    trickyBaseObjects.Remove(trickyBaseObjects[i]);
                    Repaint();
                }

                GUILayout.EndHorizontal();
            }

        }
    }

    int ObjectCount(TrickyBaseObject.ObjectType objectType)
    {
        int count = 0;
        for (int i = 0; i < trickyBaseObjects.Count; i++)
        {
            if (objectType == trickyBaseObjects[i].Type)
            {
                count++;
            }
        }

        return count;
    }

    GUIStyle GetBtnStyle()
    {
        var s = new GUIStyle();

        if(EditorPrefs.GetInt("UserSkin")==0)
        {
            s.normal.textColor = Color.white;
        }
        else
        {
            s.normal.textColor = Color.black;
        }
        var b = s.border;
        b.left = 0;
        b.top = 0;
        b.right = 0;
        b.bottom = 0;
        return s;
    }

    void AddSelection()
    {
        var Selected = Selection.gameObjects;
        for (int i = 0; i < Selected.Length; i++)
        {
            if (Selected[i].GetComponent<TrickyBaseObject>() != null)
            {
                trickyBaseObjects.Add(Selected[i].GetComponent<TrickyBaseObject>());
            }
        }

        Repaint();
    }

    void RemoveSelection()
    {
        var Selected = Selection.gameObjects;
        for (int i = 0; i < Selected.Length; i++)
        {
            if (Selected[i].GetComponent<TrickyBaseObject>() != null)
            {
                if(trickyBaseObjects.Contains(Selected[i].GetComponent<TrickyBaseObject>()))
                {
                    trickyBaseObjects.Remove(Selected[i].GetComponent<TrickyBaseObject>());
                }
            }
        }

        Repaint();
    }
}
