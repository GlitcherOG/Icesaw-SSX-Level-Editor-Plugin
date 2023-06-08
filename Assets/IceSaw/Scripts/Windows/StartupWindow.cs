using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Timeline.Actions;

[MenuEntry("Ice Saw", 1000)]
public class StartupWindow
{


    [MenuItem("Ice Saw/Startup",false, -1000)]
    public static void BringUpStartUp()
    {
        Debug.Log("Testing");

        TrickyProjectWindow.CreateInstance("TrickyProjectWindow");

    }


}
