using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using UnityEditor;

public class GrabNewtonSoft
{
    static AddRequest Request;

    [MenuItem("Ice Saw/Settings/Download Newtonsoft")]
    static void Add()
    {
        // Add a package to the project
        Request = Client.Add("com.unity.nuget.newtonsoft-json");
        EditorApplication.update += Progress;
    }

    static void Progress()
    {
        if (Request.IsCompleted)
        {
            if (Request.Status == StatusCode.Success)
                Debug.Log("Installed: " + Request.Result.packageId);
            else if (Request.Status >= StatusCode.Failure)
                Debug.Log(Request.Error.message);

            EditorApplication.update -= Progress;
        }
    }
}
