using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.AI;

[ExecuteInEditMode]
public class TrickyWorldManager : MonoBehaviour
{
    public static TrickyWorldManager Instance;

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

    #region Load Data

    public void LoadData(string path)
    {
        //SetStatic();
        LoadPatches(path + "\\Patches.json");
        LoadInstance(path + "\\Instances.json");
        LoadSplines(path + "\\Splines.json");
        LoadLighting(path + "\\Lights.json");
        LoadParticleInstances(path + "\\ParticleInstances.json");
        LoadCameraInstances(path + "\\Cameras.json");
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
            var TempObject = NewPatch.AddComponent<TrickyPatchObject>();
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
            var TempInstance = TempGameObject.AddComponent<TrickyInstanceObject>();
            TempInstance.LoadInstance(instanceJsonHandler.Instances[i]);
        }
    }

    public void LoadSplines(string path)
    {
        SplineJsonHandler SplineJson = new SplineJsonHandler();
        SplineJson = SplineJsonHandler.Load(path);
        for (int i = 0; i < SplineJson.Splines.Count; i++)
        {
            var TempSplineData = SplineJson.Splines[i];
            GameObject TempSpline = new GameObject("Spline " + i);
            TempSpline.transform.parent = SplinesHolder.transform;
            TempSpline.transform.localScale = Vector3.one;
            TempSpline.transform.localEulerAngles = Vector3.zero;
            var TempObj = TempSpline.AddComponent<TrickySplineObject>();
            TempObj.LoadSpline(TempSplineData);
        }
    }

    public void LoadLighting(string path)
    {
        LightJsonHandler lightJson = new LightJsonHandler();
        lightJson = LightJsonHandler.Load(path);
        for (int i = 0; i < lightJson.Lights.Count; i++)
        {
            var TempGameObject = new GameObject("Light " + i);
            TempGameObject.transform.parent = LightingHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            var TempInstance = TempGameObject.AddComponent<LightObject>();
            TempInstance.LoadLight(lightJson.Lights[i]);
        }

    }

    public void LoadParticleInstances(string path)
    {
        ParticleInstanceJsonHandler instanceJsonHandler = new ParticleInstanceJsonHandler();
        instanceJsonHandler = ParticleInstanceJsonHandler.Load(path);

        for (int i = 0; i < instanceJsonHandler.Particles.Count; i++)
        {
            var TempGameObject = new GameObject("Particle Instance " + i);
            TempGameObject.transform.parent = ParticlesHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            var TempInstance = TempGameObject.AddComponent<PaticleInstanceObject>();
            TempInstance.LoadPaticleInstance(instanceJsonHandler.Particles[i]);
        }
    }

    public void LoadCameraInstances(string path)
    {
        CameraJSONHandler instanceJsonHandler = new CameraJSONHandler();
        instanceJsonHandler = CameraJSONHandler.Load(path);

        for (int i = 0; i < instanceJsonHandler.Cameras.Count; i++)
        {
            var TempGameObject = new GameObject("Camera " + i);
            TempGameObject.transform.parent = CameraHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            var TempInstance = TempGameObject.AddComponent<CameraObject>();
            TempInstance.LoadCamera(instanceJsonHandler.Cameras[i]);
        }
    }
    #endregion

    #region Save Data

    public void SaveData(string path)
    {
        SavePatches(path + "\\Patches.json");
        SaveInstances(path + "\\Instances.json");
        SaveSplines(path + "\\Splines.json");
        SaveLighting(path + "\\Lights.json");
        SaveParticleInstance(path + "\\ParticleInstances.json");
        SaveCameras(path + "\\Cameras.json");
    }

    public void SavePatches(string path)
    {
        PatchesJsonHandler patchesJsonHandler = new PatchesJsonHandler();
        patchesJsonHandler.Patches = new List<PatchesJsonHandler.PatchJson>();

        var PatchList = GetPatchList();

        for (int i = 0; i < PatchList.Length; i++)
        {
            patchesJsonHandler.Patches.Add(PatchList[i].GeneratePatch());
        }

        patchesJsonHandler.CreateJson(path);
    }

    public void SaveInstances(string path)
    {
        InstanceJsonHandler instanceJsonHandler = new InstanceJsonHandler();
        instanceJsonHandler.Instances = new List<InstanceJsonHandler.InstanceJson>();

        var InstanceList = GetInstanceList();

        for (int i = 0; i < InstanceList.Length; i++)
        {
            instanceJsonHandler.Instances.Add(InstanceList[i].GenerateInstance());
        }
        instanceJsonHandler.CreateJson(path);
    }

    public void SaveSplines(string path)
    {
        SplineJsonHandler splineJsonHandler = new SplineJsonHandler();
        splineJsonHandler.Splines = new List<SplineJsonHandler.SplineJson>();

        var SplineList = GetSplineList();

        for (int i = 0; i < SplineList.Length; i++)
        {
            splineJsonHandler.Splines.Add(SplineList[i].GenerateSpline());
        }
        splineJsonHandler.CreateJson(path);
    }

    public void SaveLighting(string path)
    {
        LightJsonHandler lightJsonHandler = new LightJsonHandler();
        lightJsonHandler.Lights = new List<LightJsonHandler.LightJson>();

        var LightList = GetLightList();

        for (int i = 0; i < LightList.Length; i++)
        {
            lightJsonHandler.Lights.Add(LightList[i].GenerateLight());
        }

        lightJsonHandler.CreateJson(path);
    }

    public void SaveCameras(string path)
    {
        CameraJSONHandler cameraJSONHandler = new CameraJSONHandler();
        cameraJSONHandler.Cameras = new List<CameraJSONHandler.CameraInstance>();

        var CameraList = GetCameraList();

        for (int i = 0; i < CameraList.Length; i++)
        {
            cameraJSONHandler.Cameras.Add(CameraList[i].GenerateCamera());
        }

        cameraJSONHandler.CreateJson(path);
    }

    public void SaveParticleInstance(string path)
    {
        ParticleInstanceJsonHandler particleInstanceJsonHandler = new ParticleInstanceJsonHandler();
        particleInstanceJsonHandler.Particles = new List<ParticleInstanceJsonHandler.ParticleJson>();

        var ParticleList = GetParticleInstanceList();

        for (int i = 0; i < ParticleList.Length; i++)
        {
            particleInstanceJsonHandler.Particles.Add(ParticleList[i].GenerateParticleInstance());
        }

        particleInstanceJsonHandler.CreateJson(path);
    }

    #endregion


    public TrickyPatchObject[] GetPatchList()
    {
        return PatchesHolder.GetComponentsInChildren<TrickyPatchObject>(true);
    }

    public TrickyInstanceObject[] GetInstanceList()
    {
        return InstancesHolder.GetComponentsInChildren<TrickyInstanceObject>(true);
    }

    public TrickySplineObject[] GetSplineList()
    {
        return SplinesHolder.GetComponentsInChildren<TrickySplineObject>(true);
    }

    public LightObject[] GetLightList()
    {
        return LightingHolder.GetComponentsInChildren<LightObject>(true);
    }

    public CameraObject[] GetCameraList()
    {
        return CameraHolder.GetComponentsInChildren<CameraObject>(true);
    }

    public PaticleInstanceObject[] GetParticleInstanceList()
    {
        return ParticlesHolder.GetComponentsInChildren<PaticleInstanceObject>(true);
    }
}
