using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ContextMenuItems
{
    [MenuItem("GameObject/Ice Saw/Get Index ID", false, 100)]
    static void GetChildID(MenuCommand menuCommand)
    {
        var Selected = menuCommand.context.GetComponent<Transform>();
        Debug.Log(Selected.GetSiblingIndex() + " " + Selected.name);
    }
}
