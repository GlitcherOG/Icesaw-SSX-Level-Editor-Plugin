using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.ComponentModel;
using Unity.VisualScripting;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerInspector : Editor
{
    //public override void OnInspectorGUI()
    //{
    //    DrawDefaultInspector();

    //    //Component.hideFlags = HideFlags.HideInInspector;

    //    if(EditorGUILayout.LinkButton("Refresh Textures"))
    //    {
    //        var Temp = (typeof(LevelManager))serializedObject.targetObject;
    //    }
    //}

    public VisualTreeAsset m_InspectorXML;

    public override VisualElement CreateInspectorGUI()
    {
        // Create a new VisualElement to be the root of our inspector UI
        VisualElement myInspector = new VisualElement();
        m_InspectorXML.CloneTree(myInspector);

        VisualElement inspectorGroup = myInspector.Q("Default_Inspector");
        InspectorElement.FillDefaultInspector(inspectorGroup, serializedObject, this);

        VisualElement ReloadTextureButton = myInspector.Q("_ReloadTextures");
        var TempTextureButton = ReloadTextureButton.Query<Button>();
        TempTextureButton.First().RegisterCallback<ClickEvent>(ReloadTextures);

        // Return the finished inspector UI
        return myInspector;
    }

    private void ReloadTextures(ClickEvent evt)
    {
        Debug.Log("Testing");
        serializedObject.targetObject.GetComponent<LevelManager>().RefreshTextures();
    }
}
