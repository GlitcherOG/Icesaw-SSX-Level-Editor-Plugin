using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static SSXMultiTool.JsonFiles.Tricky.InstanceJsonHandler;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;

    GameObject PatchesHolder;
    GameObject InstancesHolder;
    GameObject SplinesHolder;
    GameObject ParticlesHolder;
    GameObject LightingHolder;
    public void SetStatic()
    {
        if (Instance == null)
        Instance = this;
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
        SplinesHolder.transform.hideFlags = HideFlags.HideInInspector;

        ParticlesHolder = new GameObject("Particles");
        ParticlesHolder.transform.parent = transform;
        ParticlesHolder.transform.hideFlags = HideFlags.HideInInspector;

        LightingHolder = new GameObject("Lighting");
        LightingHolder.transform.parent = transform;
        LightingHolder.transform.hideFlags = HideFlags.HideInInspector;

    }

    public void LoadData(string Path)
    {
        SetStatic();
        LoadPatches(Path + "\\Patches.json");
        LoadInstance(Path + "\\Instances.json");
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
            var TempObject = NewPatch.AddComponent<PatchObject>();
            TempObject.AddMissingComponents();
            TempObject.LoadPatch(patchesJsonHandler.Patches[i]);

        }
    }

    public void LoadInstance(string Path)
    {
        InstanceJsonHandler instanceJsonHandler = new InstanceJsonHandler();
        instanceJsonHandler = InstanceJsonHandler.Load(Path);

        for (int i = 0; i < instanceJsonHandler.Instances.Count; i++)
        {
            var TempGameObject = new GameObject("Instance " + i);
            TempGameObject.transform.parent = InstancesHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            var TempInstance = TempGameObject.AddComponent<InstanceObject>();
            TempInstance.LoadInstance(instanceJsonHandler.Instances[i]);
        }
    }

}
