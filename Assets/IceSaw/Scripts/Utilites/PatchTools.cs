using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PatchTools : MonoBehaviour
{
    [MenuItem("Ice Saw Tools/Patch/Merge Edges", false, 0)]
    static void MergeEdge(MenuCommand menuCommand)
    {
        if (Selection.gameObjects.Length < 1)
        {
            var Patches = new List<TrickyPatchObject>();

            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                if (Selection.gameObjects[i].GetComponent<TrickyPatchObject>()!=null)
                {
                    Patches.Add(Selection.gameObjects[i].GetComponent<TrickyPatchObject>());
                }
            }

            Vector3[] vector3s = new Vector3[Patches.Count*4];
            bool[] EdgeLinked = new bool[Patches.Count*4];

            //11-14
            //14-44
            //44-41
            //41-11

            for (int i = 0; i < Patches.Count; i++)
            {
                vector3s[i*4] = Vector3.Lerp(Patches[i].RawControlPoint, Patches[i].RawR1C4, 0.5f);
                vector3s[i*4+1] = Vector3.Lerp(Patches[i].RawR1C4, Patches[i].RawR4C4, 0.5f);
                vector3s[i*4+2] = Vector3.Lerp(Patches[i].RawR4C4, Patches[i].RawR4C1, 0.5f);
                vector3s[i*4+3] = Vector3.Lerp(Patches[i].RawR4C1, Patches[i].RawControlPoint, 0.5f);
            }

            for (int i = 0; i < vector3s.Length; i++)
            {
                var TempVector = vector3s[i];

                if (EdgeLinked[i] == false)
                {
                    for (int a = 0; a < vector3s.Length; a++)
                    {
                        if (Vector3.Distance(TempVector, vector3s[a]) <= 1000f && EdgeLinked[a] == false)
                        {
                            //Find out what edges
                            //Find Closet points and merge them
                            //going along the line



                            EdgeLinked[i] = true;
                            EdgeLinked[a] = true;

                            break;
                        }
                    }
                }
            }
        }
        else
        {
            UnityEngine.Debug.Log("No Enough Patches Selected");
        }

    }
}
