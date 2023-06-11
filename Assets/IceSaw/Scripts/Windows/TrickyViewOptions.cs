using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TrickyViewOptions
{
    [MenuItem("Ice Saw View/Show All")]
    public static void ShowAll()
    {
        //Grab Patches
        PatchObject[] patchObject = WorldManager.Instance.GetPatchList();
        for (int i = 0; i < patchObject.Length; i++)
        {
            patchObject[i].gameObject.SetActive(true);
        }

        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        //Grab Instances
        for (int i = 0; i < instanceObjects.Length; i++)
        {
            instanceObjects[i].gameObject.SetActive(true);
        }
    }

    [MenuItem("Ice Saw View/Race Only")]
    public static void RaceOnly()
    {
        //Grab Patches
        PatchObject[] patchObject = WorldManager.Instance.GetPatchList();
        for (int i = 0; i < patchObject.Length; i++)
        {
            if (patchObject[i].TrickOnlyPatch)
            {
                patchObject[i].gameObject.SetActive(false);
            }
            else
            {
                patchObject[i].gameObject.SetActive(true);
            }
        }

        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        //Grab Instances
        for (int i = 0; i < instanceObjects.Length; i++)
        {
            if (instanceObjects[i].LTGState==2)
            {
                instanceObjects[i].gameObject.SetActive(false);
            }
            else
            {
                instanceObjects[i].gameObject.SetActive(true);
            }
        }

        //Run Effect

    }

    [MenuItem("Ice Saw View/Showoff Only")]
    public static void ShowOffOnly()
    {
        //Grab Patches
        PatchObject[] patchObject = WorldManager.Instance.GetPatchList();
        for (int i = 0; i < patchObject.Length; i++)
        {
            patchObject[i].gameObject.SetActive(true);
        }

        InstanceObject[] instanceObjects = WorldManager.Instance.GetInstanceList();
        //Grab Instances
        for (int i = 0; i < instanceObjects.Length; i++)
        {
            if (instanceObjects[i].LTGState == 1)
            {
                instanceObjects[i].gameObject.SetActive(false);
            }
            else
            {
                instanceObjects[i].gameObject.SetActive(true);
            }
        }

        //Run Effect
    }
}
