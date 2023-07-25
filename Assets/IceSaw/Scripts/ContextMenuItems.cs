using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ContextMenuItems
{
    [MenuItem("GameObject/Ice Saw/Get Index ID", false, 1000)]
    static void GetChildID(MenuCommand menuCommand)
    {
        if (menuCommand.context != null)
        {
            var Selected = menuCommand.context.GetComponent<Transform>();
            Debug.Log("(" +Selected.GetSiblingIndex() + ")" + Selected.name);
        }
        else
        {
            Debug.Log("No Object Selected");
        }
    }
}
