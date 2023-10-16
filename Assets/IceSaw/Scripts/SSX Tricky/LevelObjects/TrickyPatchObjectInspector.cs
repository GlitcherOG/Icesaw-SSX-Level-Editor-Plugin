using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.ComponentModel;
using Unity.VisualScripting;

[CustomEditor(typeof(TrickyPatchObject))]
public class TrickyPatchObjectInspector : Editor
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

        VisualElement ButtonGroup = myInspector.Q("UVGroup");

        VisualElement UVLeftButton = ButtonGroup.Q("UVRotateLeft");
        var TempButton = UVLeftButton.Query<Button>();
        TempButton.First().RegisterCallback<ClickEvent>(UVRotateLeft);

        VisualElement UVRightButton = ButtonGroup.Q("UVRotateRight");
        TempButton = UVRightButton.Query<Button>();
        TempButton.First().RegisterCallback<ClickEvent>(UVRotateRight);

        VisualElement FlipPatchButton = myInspector.Q("FlipPatch");
        TempButton = FlipPatchButton.Query<Button>();
        TempButton.First().RegisterCallback<ClickEvent>(FlipPatch);

        VisualElement ResetTransformButton = myInspector.Q("ResetTransform");
        TempButton = ResetTransformButton.Query<Button>();
        TempButton.First().RegisterCallback<ClickEvent>(ResetTransform);

        VisualElement ForceRegenerateButton = myInspector.Q("ForceRegenerate");
        TempButton = ForceRegenerateButton.Query<Button>();
        TempButton.First().RegisterCallback<ClickEvent>(ForceRegenerate);

        VisualElement AddMissingButton = myInspector.Q("AddMissing");
        TempButton = AddMissingButton.Query<Button>();
        TempButton.First().RegisterCallback<ClickEvent>(AddMissing);

        // Return the finished inspector UI
        return myInspector;
    }

    private void UVRotateLeft(ClickEvent evt)
    {
        serializedObject.targetObject.GetComponent<TrickyPatchObject>().RotateUVLeft();
    }

    private void UVRotateRight(ClickEvent evt)
    {
        serializedObject.targetObject.GetComponent<TrickyPatchObject>().RotateUVRight();
    }

    private void FlipPatch(ClickEvent evt)
    {
        serializedObject.targetObject.GetComponent<TrickyPatchObject>().FlipPatch();
    }

    private void ResetTransform(ClickEvent evt)
    {
        serializedObject.targetObject.GetComponent<TrickyPatchObject>().TransformReset();
    }

    private void ForceRegenerate(ClickEvent evt)
    {
        serializedObject.targetObject.GetComponent<TrickyPatchObject>().ForceRegeneration();
    }

    private void AddMissing(ClickEvent evt)
    {
        serializedObject.targetObject.GetComponent<TrickyPatchObject>().AddMissingComponents();
    }
}
