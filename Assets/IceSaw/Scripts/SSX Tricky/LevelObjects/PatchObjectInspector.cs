using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.ComponentModel;
using Unity.VisualScripting;

[CustomEditor(typeof(PatchObject))]
public class PatchObjectInspector : Editor
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

        //VisualElement inspectorGroup = myInspector.Q("Default_Inspector");
        //InspectorElement.FillDefaultInspector(inspectorGroup, serializedObject, this);

        //Make Main Points in fold out
        //Make UV Points FoldOut
        //Add Rest as Normal

        VisualElement ButtonGroup = myInspector.Q("ButtonGroup1");

        VisualElement UVLeftButton = ButtonGroup.Q("Rotate UV Left");
        var TempButton = UVLeftButton.Query<Button>();
        TempTextureButton.First().RegisterCallback<ClickEvent>(ReloadTextures);

        VisualElement UVRightButton = ButtonGroup.Q("Rotate UV Right");
        TempButton = UVRightButton.Query<Button>();
        TempButton.First().RegisterCallback<ClickEvent>(ReloadLightmaps);

        VisualElement FlipPatchButton = ButtonGroup.Q("Flip Patch");
        TempButton = FlipPatchButton.Query<Button>();
        TempButton.First().RegisterCallback<ClickEvent>(ReloadLightmaps);

        //VisualElement StitchPatchButton = ButtonGroup.Q("Stich Patch Edges");
        //TempButton = StitchPatchButton.Query<Button>();
        //TempButton.First().RegisterCallback<ClickEvent>(ReloadLightmaps);

        VisualElement RegenPatchButton = ButtonGroup.Q("Regenerate Patch");
        TempButton = RegenPatchButton.Query<Button>();
        TempButton.First().RegisterCallback<ClickEvent>(ReloadLightmaps);

        VisualElement AddMissingButton = ButtonGroup.Q("Add Missing Components");
        TempButton = AddMissingButton.Query<Button>();
        TempButton.First().RegisterCallback<ClickEvent>(ReloadLightmaps);

        // Return the finished inspector UI
        return myInspector;
    }

    private void ReloadTextures(ClickEvent evt)
    {
        serializedObject.targetObject.GetComponent<LevelManager>().RefreshTextures();
    }

    private void ReloadLightmaps(ClickEvent evt)
    {
        serializedObject.targetObject.GetComponent<LevelManager>().RefreshLightmap();
    }
}
