using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FollowMainCamera : MonoBehaviour
{
    private void Update()
    {
        if(Camera.main!=null)
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}
