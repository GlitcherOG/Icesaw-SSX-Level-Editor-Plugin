using SSXMultiTool.JsonFiles.SSXOG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OGWorldManager : MonoBehaviour
{
    public static OGWorldManager Instance;

    public GameObject PatchesHolder;
    public GameObject InstancesHolder;
    public GameObject SplinesHolder;
    public GameObject SegmentsHolder;
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

        SegmentsHolder = new GameObject("Loose Segments");
        SegmentsHolder.transform.parent = transform;
        SegmentsHolder.transform.localScale = Vector3.one;
        SegmentsHolder.transform.localEulerAngles = Vector3.zero;
        SegmentsHolder.transform.hideFlags = HideFlags.HideInInspector;

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
        LoadInstances(path + "\\Instances.json");
        LoadSplines(path + "\\Splines.json");
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
            GameObject NewPatch = new GameObject("Patch " + i);
            NewPatch.transform.parent = PatchesHolder.transform;
            NewPatch.transform.localPosition = Vector3.zero;
            NewPatch.transform.localScale = Vector3.one;
            NewPatch.transform.localEulerAngles = Vector3.zero;
            var TempObject = NewPatch.AddComponent<OGPatchObject>();
            TempObject.AddMissingComponents();
            TempObject.LoadPatch(patchesJsonHandler.Patches[i]);

        }
    }

    public void LoadInstances(string Path)
    {
        InstanceJsonHandler instanceJsonHandler = new InstanceJsonHandler();
        instanceJsonHandler = InstanceJsonHandler.Load(Path);

        for (int i = 0; i < instanceJsonHandler.Instances.Count; i++)
        {
            var TempGameObject = new GameObject("Instance " + i);
            TempGameObject.transform.parent = InstancesHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            var TempInstance = TempGameObject.AddComponent<OGInstanceObject>();
            TempInstance.LoadInstance(instanceJsonHandler.Instances[i]);
        }
    }

    public void LoadSplines(string Path)
    {
        SplinesJsonHandler splineJsonHandler = new SplinesJsonHandler();
        splineJsonHandler = SplinesJsonHandler.Load(Path);

        for (int i = 0; i < splineJsonHandler.Splines.Count; i++)
        {
            var TempGameObject = new GameObject("Spline " + i);
            TempGameObject.transform.parent = SplinesHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            var TempSpline = TempGameObject.AddComponent<OGSplineObject>();
            TempSpline.LoadSpline(splineJsonHandler.Splines[i]);
        }

        for (int i = 0; i < splineJsonHandler.SegmentsData.Count; i++)
        {
            var TempGameObject = new GameObject("Segment " + i);
            TempGameObject.transform.parent = SegmentsHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            var TempSpline = TempGameObject.AddComponent<OGLooseSegment>();
            TempSpline.LoadSpline(splineJsonHandler.SegmentsData[i]);
        }
    }

    public OGPatchObject[] GetPatchList()
    {
        return PatchesHolder.GetComponentsInChildren<OGPatchObject>(true);
    }

    public OGInstanceObject[] GetInstanceList()
    {
        return InstancesHolder.GetComponentsInChildren<OGInstanceObject>(true);
    }
}
