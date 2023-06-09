using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;
    public string Test;
    GameObject PatchesHolder;
    GameObject InstancesHolder;
    GameObject SplinesHolder;
    GameObject ParticlesHolder;
    GameObject LightingHolder;

    public void SetStatic()
    {
        Instance = this;
    }

    public void GenerateEmptyObjects()
    {
        PatchesHolder = new GameObject("Patches");
        PatchesHolder.transform.parent = transform;

        InstancesHolder = new GameObject("Instances");
        InstancesHolder.transform.parent = transform;

        SplinesHolder = new GameObject("Splines");
        SplinesHolder.transform.parent = transform;

        ParticlesHolder = new GameObject("Particles");
        ParticlesHolder.transform.parent = transform;

        LightingHolder = new GameObject("Lighting");
        LightingHolder.transform.parent = transform;
    }

    public void LoadData()
    {
        string LoadPath = TrickyProjectWindow.CurrentPath;
        LoadPatches(LoadPath + "\\Patches.json");
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
            NewPatch.transform.localEulerAngles = Vector3.zero;
            var TempObject = NewPatch.AddComponent<PatchObject>();
            TempObject.AddMissingComponents();
            TempObject.LoadPatch(patchesJsonHandler.Patches[i]);

        }


    }

}
