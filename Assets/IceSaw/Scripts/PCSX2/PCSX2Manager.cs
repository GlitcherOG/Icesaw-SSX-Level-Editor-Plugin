using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PCSX2Manager : MonoBehaviour
{
    public bool active = false;
    public static PCSX2Manager Instance;
    public ProcessMemory processMemory = new ProcessMemory();
    // Start is called before the first frame update
    void Awake()
    {
        if (processMemory.InitaliseProcess("pcsx2") && Instance == null)
        {
            active = true;
            Instance = this;
            Debug.Log("Pcsx2 Detected");
        }
        else
        {
            Debug.Log("Pcsx2 Not Detected");
        }
    }

    [ContextMenu("Attempt Hook")]
    void ForceHook()
    {
        if (processMemory.InitaliseProcess("pcsx2"))
        {
            active = true;
            Instance = this;
            Debug.Log("Pcsx2 Detected");
        }
        else
        {
            active = false;
            Debug.Log("Pcsx2 Not Detected");
        }
    }
}
