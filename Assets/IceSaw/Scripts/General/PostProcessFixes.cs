using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PostProcessFixes : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths, bool didDomainReload)
    {
        GameObject gameObject = GameObject.Find("/Tricky Level Manager");
        if (gameObject != null)
        {
            gameObject.GetComponent<TrickyLevelManager>().FixScriptLinks();
        }

        gameObject = GameObject.Find("/OG Level Manager");
        if (gameObject != null)
        {
            gameObject.GetComponent<OGLevelManager>().FixScriptLinks();
        }
    }
}
