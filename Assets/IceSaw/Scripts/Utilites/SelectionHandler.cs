using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Unity.VisualScripting;

[InitializeOnLoad]
public class SelectionHandler
{

    static SelectionHandler()
    {
        Selection.selectionChanged += OnSelectionChanged;
    }

    static void OnSelectionChanged()
    {
        //singleUpdater();
        multipleUpdater();
    }

    static Object[] SelectionList;
    static void multipleUpdater()
    {
        SelectionList = Selection.gameObjects;
        bool Update = false;
        for (int i = 0; i < SelectionList.Length; i++)
        {
            if (SelectionList[i].GetComponent<SelectParent>() != null)
            {
                CheckParent(i, SelectionList[i].GetComponent<Transform>().parent.gameObject);
                Update = true;
            }
        }

        if (Update)
        {
            EditorApplication.delayCall += () =>
            {
                Selection.objects = SelectionList;
            };
        }
    }

    static void CheckParent(int ChildID, GameObject Parent)
    {
        if(Parent.GetComponent<SelectParent>() != null)
        {
            CheckParent(ChildID, Parent.transform.parent.gameObject);
        }
        else
        {
            SelectionList[ChildID] = (Object)Parent;
        }

    }
}