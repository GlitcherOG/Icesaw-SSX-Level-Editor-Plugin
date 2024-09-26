using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IceSawPatchesViewWindow : EditorWindow
{
    Color PickedColour;
    int Type;

    [MenuItem("Ice Saw View/Patch Highlighter")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        IceSawPatchesViewWindow window = (IceSawPatchesViewWindow)EditorWindow.GetWindow(typeof(IceSawPatchesViewWindow));
        window.Show();
    }
    void OnGUI()
    {
        titleContent.text = "Patch Highlighter";

        Type = EditorGUILayout.IntField("Patch Style",Type);
        PickedColour = EditorGUILayout.ColorField("Highlight Colour",PickedColour);
        if (OGWorldManager.Instance != null)
        {
            if (GUILayout.Button("Highlight"))
            {
                OGPatchObject[] patchObjects = OGWorldManager.Instance.GetPatchList();
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
                OGPatchObject[] patchObjects = OGWorldManager.Instance.GetPatchList();
                for (int i = 0; i < patchObjects.Length; i++)
                {
                    patchObjects[i].UpdateHighlight(Color.white);
                }
            }
        }
        else
        if (TrickyWorldManager.Instance != null)
        {
            if (GUILayout.Button("Highlight"))
            {
                TrickyPatchObject[] patchObjects = TrickyWorldManager.Instance.GetPatchList();
                for (int i = 0; i < patchObjects.Length; i++)
                {
                    if (patchObjects[i].SurfaceType == (TrickyPatchObject.PatchSurfaceType)Type)
                    {
                        patchObjects[i].UpdateHighlight(PickedColour);
                    }
                }
            }
            if (GUILayout.Button("Reset"))
            {
                TrickyPatchObject[] patchObjects = TrickyWorldManager.Instance.GetPatchList();
                for (int i = 0; i < patchObjects.Length; i++)
                {
                    patchObjects[i].UpdateHighlight(Color.white);
                }
            }
        }

    }
}
