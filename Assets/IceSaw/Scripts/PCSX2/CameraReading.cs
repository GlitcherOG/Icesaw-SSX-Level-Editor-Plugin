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
                transform.localRotation = ToQuaternion(new Vector3(Location[3], Location[4], Location[5]));
            }
        }
    }

    public static Quaternion ToQuaternion(Vector3 v)
    {
        float cy = (float)Math.Cos(v.z * 0.5);
        float sy = (float)Math.Sin(v.z * 0.5);
        float cp = (float)Math.Cos(v.y * 0.5);
        float sp = (float)Math.Sin(v.y * 0.5);
        float cr = (float)Math.Cos(v.x * 0.5);
        float sr = (float)Math.Sin(v.x * 0.5);

        return new Quaternion
        {
            w = (cr * cp * cy + sr * sp * sy),
            x = (sr * cp * cy - cr * sp * sy),
            y = (cr * sp * cy + sr * cp * sy),
            z = (cr * cp * sy - sr * sp * cy)
        };

    }
}
