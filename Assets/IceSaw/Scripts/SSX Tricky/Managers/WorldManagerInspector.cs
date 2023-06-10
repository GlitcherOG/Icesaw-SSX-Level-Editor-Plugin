using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.ComponentModel;

[CustomEditor(typeof(WorldManager))]
public class WorldManagerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //Component.hideFlags = HideFlags.HideInInspector;
    }


    //public override VisualElement CreateInspectorGUI()
    //{
    //    //// Create a new VisualElement to be the root of our inspector UI
    //    //VisualElement myInspector = new VisualElement();

    //    //// Add a simple label
    //    //myInspector.Add(new Label("This is a custom inspector"));

    //    //// Return the finished inspector UI
    //    //return myInspector;
    //}
}
