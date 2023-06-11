using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TrickyPatchesViewWindow : EditorWindow
{
    Color PickedColour;
    int Type;

    [MenuItem("Ice Saw View/Patch Highlighter")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        TrickyPatchesViewWindow window = (TrickyPatchesViewWindow)EditorWindow.GetWindow(typeof(TrickyPatchesViewWindow));
        window.Show();
    }
    void OnGUI()
    {
        titleContent.text = "Patch Highlighter";

        Type = EditorGUILayout.IntField("Patch Style",Type);
        PickedColour = EditorGUILayout.ColorField("Highlight Colour",PickedColour);
        if(GUILayout.Button("Highlight"))
        {
            PatchObject[] patchObjects = WorldManager.Instance.GetPatchList();
            for (int i = 0; i < patchObjects.Length; i++)
            {
                if (patchObjects[i].PatchStyle == Type)
                {
                    patchObjects[i].UpdateHighlight(PickedColour);
                }
            }
        }
        if (GUILayout.Button("Reset"))
        {
            PatchObject[] patchObjects = WorldManager.Instance.GetPatchList();
            for (int i = 0; i < patchObjects.Length; i++)
            {
                patchObjects[i].UpdateHighlight(Color.white);
            }
        }

    }
}
