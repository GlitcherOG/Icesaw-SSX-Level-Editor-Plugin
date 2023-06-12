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
