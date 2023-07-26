using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraReading : MonoBehaviour
{
    public string StringCameraAddress = "0x207FF5AC";
    public long CameraAddress { get { return Convert.ToInt32(StringCameraAddress, 16); } }

    // Update is called once per frame
    void Update()
    {
        if (PCSX2Manager.Instance != null)
        {
            if (PCSX2Manager.Instance.active)
            {
                float[] Location = PCSX2Manager.Instance.processMemory.ReadFloats(CameraAddress, 6);
                transform.localPosition = new Vector3(Location[0], Location[1], Location[2]);
                transform.localEulerAngles = new Vector3(Location[3], Location[4], Location[5]);
            }
        }
    }
}
