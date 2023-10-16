using SSXMultiTool.JsonFiles.SSXOG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OGWorldManager : MonoBehaviour
{
    public static OGWorldManager Instance;

    public GameObject PatchesHolder;
    public GameObject InstancesHolder;
    public GameObject SplinesHolder;
    public GameObject ParticlesHolder;
    public GameObject LightingHolder;
    public GameObject CameraHolder;

    [HideInInspector]
    public bool ShowInstanceModels = true;
    [HideInInspector]
    public bool ShowCollisionModels = true;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
    }

    public void GenerateEmptyObjects()
    {
        transform.hideFlags = HideFlags.HideInInspector;

        PatchesHolder = new GameObject("Patches");
        PatchesHolder.transform.parent = transform;
        PatchesHolder.transform.localScale = Vector3.one;
        PatchesHolder.transform.localEulerAngles = Vector3.zero;
        PatchesHolder.transform.hideFlags = HideFlags.HideInInspector;

        InstancesHolder = new GameObject("Instances");
        InstancesHolder.transform.parent = transform;
        InstancesHolder.transform.localScale = Vector3.one;
        InstancesHolder.transform.localEulerAngles = Vector3.zero;
        InstancesHolder.transform.hideFlags = HideFlags.HideInInspector;

        SplinesHolder = new GameObject("Splines");
        SplinesHolder.transform.parent = transform;
        SplinesHolder.transform.localScale = Vector3.one;
        SplinesHolder.transform.localEulerAngles = Vector3.zero;
        SplinesHolder.transform.hideFlags = HideFlags.HideInInspector;

        ParticlesHolder = new GameObject("Particles");
        ParticlesHolder.transform.parent = transform;
        ParticlesHolder.transform.localScale = Vector3.one;
        ParticlesHolder.transform.localEulerAngles = Vector3.zero;
        ParticlesHolder.transform.hideFlags = HideFlags.HideInInspector;

        LightingHolder = new GameObject("Lighting");
        LightingHolder.transform.parent = transform;
        LightingHolder.transform.localScale = Vector3.one;
        LightingHolder.transform.localEulerAngles = Vector3.zero;
        LightingHolder.transform.hideFlags = HideFlags.HideInInspector;

        CameraHolder = new GameObject("Cameras");
        CameraHolder.transform.parent = transform;
        CameraHolder.transform.localPosition = Vector3.zero;
        CameraHolder.transform.localScale = Vector3.one;
        CameraHolder.transform.localEulerAngles = Vector3.zero;
        CameraHolder.transform.hideFlags = HideFlags.HideInInspector;
    }

    public void LoadData(string path)
    {
        //SetStatic();
        LoadPatches(path + "\\Patches.json");
        //LoadInstance(path + "\\Instances.json");
        //LoadSplines(path + "\\Splines.json");
        //LoadLighting(path + "\\Lights.json");
        //LoadParticleInstances(path + "\\ParticleInstances.json");
        //LoadCameraInstances(path + "\\Cameras.json");
    }

    public void LoadPatches(string JsonPath)
    {
        PatchesJsonHandler patchesJsonHandler = new PatchesJsonHandler();
        patchesJsonHandler = PatchesJsonHandler.Load(JsonPath);

        for (int i = 0; i < patchesJsonHandler.Patches.Count; i++)
        {
            GameObject NewPatch = new GameObject();
            NewPatch.transform.parent = PatchesHolder.transform;
            NewPatch.transform.localPosition = Vector3.zero;
            NewPatch.transform.localScale = Vector3.one;
            NewPatch.transform.localEulerAngles = Vector3.zero;
            var TempObject = NewPatch.AddComponent<OGPatchObject>();
            TempObject.AddMissingComponents();
            TempObject.LoadPatch(patchesJsonHandler.Patches[i]);

        }
    }

}
