using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CameraTools : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Event e = Event.current;
        if (e != null)
        {
            switch (e.type)
            {
                case EventType.KeyDown:
                    {
                        if (Event.current.keyCode == (KeyCode.F))
                        {
                            transform.position = SceneView.lastActiveSceneView.camera.transform.position;
                            transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
                        }
                        break;
                    }
            }
        }
    }
    [ContextMenu("Match Scene View")]
    void MatchSceneView()
    {
        transform.position = SceneView.lastActiveSceneView.camera.transform.position;
        transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
    }

    [ContextMenu("Take ScreenShot")]
    void TakeScreenshot()
    {
        string folderPath = "Assets/Screenshots/"; // the path of your project folder

        if (!System.IO.Directory.Exists(folderPath)) // if this path does not exist yet
            System.IO.Directory.CreateDirectory(folderPath);  // it will get created

        var screenshotName =
                                "Screenshot_" +
                                System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + // puts the current time right into the screenshot name
                                ".png"; // put youre favorite data format here
        ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(folderPath, screenshotName), 2); // takes the sceenshot, the "2" is for the scaled resolution, you can put this to 600 but it will take really long to scale the image up
        Debug.Log(folderPath + screenshotName);
    }

    [ContextMenu("Take High Res ScreenShot")]
    void TakeHighScreenshot()
    {
        string folderPath = "Assets/Screenshots/"; // the path of your project folder

        if (!System.IO.Directory.Exists(folderPath)) // if this path does not exist yet
            System.IO.Directory.CreateDirectory(folderPath);  // it will get created

        var screenshotName =
                                "Screenshot_" +
                                System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + // puts the current time right into the screenshot name
                                ".png"; // put youre favorite data format here
        ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(folderPath, screenshotName), 4); // takes the sceenshot, the "2" is for the scaled resolution, you can put this to 600 but it will take really long to scale the image up
        Debug.Log(folderPath + screenshotName);
    }
}
