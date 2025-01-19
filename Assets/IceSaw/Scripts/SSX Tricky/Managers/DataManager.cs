using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SSXMultiTool.JsonFiles.Tricky.SSFJsonHandler;

public class DataManager
{
    #region HeaderObjects
    public GameObject PrefabManagerHolder;
    public GameObject WorldManagerHolder;
    public GameObject SkyboxManagerHolder;
    public GameObject LogicManager;
    public GameObject PathFileManager;

    public GameObject PrefabsHolder;
    public GameObject MaterialHolder;
    public GameObject ParticlePrefabHolder;

    public GameObject PatchesHolder;
    public GameObject InstancesHolder;
    public GameObject SplinesHolder;
    public GameObject ParticlesHolder;
    public GameObject LightingHolder;
    public GameObject CameraHolder;

    public GameObject SkyboxMaterialHolder;
    public GameObject SkyboxPrefabsHolder;

    public GameObject EffectSlotHolder;
    public GameObject EffectHolder;
    public GameObject PhysicsHolder;
    public GameObject FunctionHolder;

    public GameObject AIPHolder;
    public GameObject SOPHolder;
    #endregion


    #region Prefab Manager
    public List<TrickyPrefabObject> trickyPrefabObjects;
    public List<TrickyPrefabSubObject> trickyPrefabSubObjects;
    public List<PrefabMeshObject> prefabMeshObjects;
 
    public List<TrickyMaterialObject> trickyMaterialObjects;
    public List<ParticlePrefabObject> particlePrefabObjects;
    #endregion

    #region Word Objects
    public List<TrickyPatchObject> trickyPatchObjects;
    public List <TrickyInstanceObject> trickyInstances;
    public List<TrickySplineObject> trickySplineObjects;
    public List<PaticleInstanceObject> paticleInstanceObjects;
    public List<LightObject> lightObjects;
    public List<TrickyCameraObject> trickyCameraObjects;
    #endregion

    #region Skybox
    public List<TrickySkyboxMaterialObject> trickySkyboxMaterialObjects;
    public List<TrickySkyboxPrefabObject> trickySkyboxPrefabObjects;
    public List<TrickyPrefabSkyboxSubObject> trickyPrefabSkyboxSubObjects;
    public List<PrefabSkyboxMeshObject> prefabSkyboxMeshObjects;
    #endregion

    #region Logic
    public List<EffectSlotObject> effectSlotObjects;
    public List<TrickyEffectHeader> trickyEffectHeaders;
    public List<PhysicsObject> trickyPhysicsObjects;
    public List<TrickyFunctionHeader> trickyFunctionHeaders;
    #endregion

    #region Paths
    public PathManager trickyGeneralPaths;
    public PathManager trickyShowoffPaths;
    #endregion

    List<TrickyBaseObject> trickyBaseObjects = new List<TrickyBaseObject>();

    public void RefreshObjectList()
    {
        trickyPrefabObjects = new List<TrickyPrefabObject>();
        trickyPrefabSubObjects= new List<TrickyPrefabSubObject>();
        prefabMeshObjects = new List<PrefabMeshObject>();

        trickyMaterialObjects = new List<TrickyMaterialObject>();
        particlePrefabObjects = new List<ParticlePrefabObject>();

        trickyPatchObjects = new List<TrickyPatchObject>();
        trickyInstances = new List<TrickyInstanceObject>();
        trickySplineObjects = new List<TrickySplineObject>();
        paticleInstanceObjects = new List<PaticleInstanceObject>();
        lightObjects = new List<LightObject>();
        trickyCameraObjects = new List<TrickyCameraObject>();

        trickySkyboxMaterialObjects = new List<TrickySkyboxMaterialObject>();
        trickySkyboxPrefabObjects = new List<TrickySkyboxPrefabObject>();

        effectSlotObjects = new List<EffectSlotObject>();
        trickyEffectHeaders = new List<TrickyEffectHeader>();
        trickyPhysicsObjects = new List<PhysicsObject>();
        trickyFunctionHeaders = new List<TrickyFunctionHeader>();

        trickyGeneralPaths = null;
        trickyShowoffPaths = null;

        trickyBaseObjects = new List<TrickyBaseObject>();

        var Scene = SceneManager.GetActiveScene();
        var ObjectList = Scene.GetRootGameObjects();
        //CAN PROBABLY SWAP OUT WITH GET COMPONENT IN CHILDREN
        for (int i = 0; i < ObjectList.Length; i++)
        {
            if (ObjectList[i].GetComponent<TrickyBaseObject>() != null)
            {
                trickyBaseObjects.Add(ObjectList[i].GetComponent<TrickyBaseObject>());
            }
            if(ObjectList[i].transform.childCount != 0)
            {
                GetChildTrickyBaseChild(ObjectList[i]);
            }
            FixRefreshedHolders(ObjectList[i]);
        }

        for (int i = 0; i < trickyBaseObjects.Count; i++)
        {
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Prefab)
            {
                trickyPrefabObjects.Add((TrickyPrefabObject)trickyBaseObjects[i]);
            }
            //if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.PrefabSub)
            //{
            //    trickyPrefabSubObjects.Add((TrickyPrefabSubObject)trickyBaseObjects[i]);
            //}
            //if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.PrefabMesh)
            //{
            //    prefabMeshObjects.Add((PrefabMeshObject)trickyBaseObjects[i]);
            //}
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Material)
            {
                trickyMaterialObjects.Add((TrickyMaterialObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.ParticlePrefab)
            {
                particlePrefabObjects.Add((ParticlePrefabObject)trickyBaseObjects[i]);
            }


            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Patch)
            {
                trickyPatchObjects.Add((TrickyPatchObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Instance)
            {
                trickyInstances.Add((TrickyInstanceObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Spline)
            {
                trickySplineObjects.Add((TrickySplineObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Particle)
            {
                paticleInstanceObjects.Add((PaticleInstanceObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Light)
            {
                lightObjects.Add((LightObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Camera)
            {
                trickyCameraObjects.Add((TrickyCameraObject)trickyBaseObjects[i]);
            }


            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.SkyboxMaterial)
            {
                trickySkyboxMaterialObjects.Add((TrickySkyboxMaterialObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.SkyboxPrefab)
            {
                trickySkyboxPrefabObjects.Add((TrickySkyboxPrefabObject)trickyBaseObjects[i]);
            }
            //if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.SkyboxPrefabSub)
            //{
            //    trickyPrefabSkyboxSubObjects.Add((TrickyPrefabSkyboxSubObject)trickyBaseObjects[i]);
            //}
            //if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.SkyboxPrefabMesh)
            //{
            //    prefabSkyboxMeshObjects.Add((PrefabSkyboxMeshObject)trickyBaseObjects[i]);
            //}

            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.EffectSlot)
            {
                effectSlotObjects.Add((EffectSlotObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Effect)
            {
                trickyEffectHeaders.Add((TrickyEffectHeader)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Physics)
            {
                trickyPhysicsObjects.Add((PhysicsObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Function)
            {
                trickyFunctionHeaders.Add((TrickyFunctionHeader)trickyBaseObjects[i]);
            }

            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.PathManager)
            {
                if(((PathManager)trickyBaseObjects[i]).pathManagerType == PathManager.PathManagerType.General)
                {
                    trickyGeneralPaths = (PathManager)trickyBaseObjects[i];
                }

                if (((PathManager)trickyBaseObjects[i]).pathManagerType == PathManager.PathManagerType.Showoff)
                {
                    trickyShowoffPaths = (PathManager)trickyBaseObjects[i];
                }
            }
        }
    }

    public void FixRefreshedHolders(GameObject gameObject)
    {
        if(gameObject.transform.name == "Tricky Prefab Manager" && PrefabManagerHolder == null)
        {
            PrefabManagerHolder = gameObject;
        }
        if (gameObject.transform.name == "Tricky World Manager" && WorldManagerHolder == null)
        {
            WorldManagerHolder = gameObject;
        }
        if (gameObject.transform.name == "Tricky Skybox Manager" && SkyboxManagerHolder == null)
        {
            SkyboxManagerHolder = gameObject;
        }
        if (gameObject.transform.name == "Tricky Logic Manager" && LogicManager == null)
        {
            LogicManager = gameObject;
        }
        if (gameObject.transform.name == "Tricky Path Manager" && PathFileManager == null)
        {
            PathFileManager = gameObject;
        }


        //public GameObject PrefabsHolder;
        //public GameObject MaterialHolder;
        //public GameObject ParticlePrefabHolder;

        //public GameObject PatchesHolder;
        //public GameObject InstancesHolder;
        //public GameObject SplinesHolder;
        //public GameObject ParticlesHolder;
        //public GameObject LightingHolder;
        //public GameObject CameraHolder;

        //public GameObject SkyboxMaterialHolder;
        //public GameObject SkyboxPrefabsHolder;

        //public GameObject EffectSlotHolder;
        //public GameObject EffectHolder;
        //public GameObject PhysicsHolder;
        //public GameObject FunctionHolder;

        //public GameObject AIPHolder;
        //public GameObject SOPHolder;
    }

    public void GetChildTrickyBaseChild(GameObject gameObject)
    {
        //CAN PROBABLY SWAP OUT WITH GET COMPONENT IN CHILDREN
        var ChildCount = gameObject.transform.childCount;

        for (int i = 0; i < ChildCount; i++)
        {
            var ChildObject = gameObject.transform.GetChild(i);

            if (ChildObject.GetComponent<TrickyBaseObject>() != null)
            {
                trickyBaseObjects.Add(ChildObject.GetComponent<TrickyBaseObject>());
            }
            if (ChildObject.transform.childCount != 0)
            {
                GetChildTrickyBaseChild(ChildObject.gameObject);
            }

            FixRefreshedHolders(ChildObject.gameObject);
        }
    }

    #region Load Data
    public void LoadObjects(GameObject gameObject, string Path)
    {
        gameObject.transform.hideFlags = HideFlags.HideInInspector;

        //Generate Prefab Manager
        PrefabManagerHolder = new GameObject("Tricky Prefab Manager");
        PrefabManagerHolder.transform.parent = gameObject.transform;
        PrefabManagerHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        PrefabManagerHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
        PrefabManagerHolder.transform.localPosition = new Vector3(0, 0, 100000);
        //var TempPrefab = PrefabManagerHolder.AddComponent<TrickyPrefabManager>();
        //TempPrefab.runInEditMode = true;
        //TempPrefab.GenerateEmptyObjects();

        //Generate World Manager
        WorldManagerHolder = new GameObject("Tricky World Manager");
        WorldManagerHolder.transform.parent = gameObject.transform;
        WorldManagerHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        WorldManagerHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
        WorldManagerHolder.transform.localPosition = new Vector3(0, 0, 0);
        //var TempWorld = WorldManagerHolder.AddComponent<TrickyWorldManager>();
        //TempWorld.runInEditMode = true;
        //TempWorld.GenerateEmptyObjects();

        //Generate Skybox Manager
        SkyboxManagerHolder = new GameObject("Tricky Skybox Manager");
        SkyboxManagerHolder.transform.parent = gameObject.transform;
        SkyboxManagerHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        SkyboxManagerHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
        SkyboxManagerHolder.transform.localPosition = new Vector3(0, 0, 100000 * 2);
        //var TempSkybox = SkyboxManagerHolder.AddComponent<SkyboxManager>();
        //TempSkybox.runInEditMode = true;
        //TempSkybox.GenerateEmptyObjects();

        //Generate Logic Manager
        LogicManager = new GameObject("Tricky Logic Manager");
        LogicManager.transform.parent = gameObject.transform;
        LogicManager.transform.transform.localScale = new Vector3(1, 1, 1);
        LogicManager.transform.localEulerAngles = new Vector3(0, 0, 0);
        //var TempLogic = LogicManager.AddComponent<TrickyLogicManager>();
        //TempLogic.runInEditMode = true;
        //TempLogic.GenerateEmptyObjects();

        //Generate Path File Manager
        PathFileManager = new GameObject("Tricky Path Manager");
        PathFileManager.transform.parent = gameObject.transform;
        PathFileManager.transform.transform.localScale = new Vector3(1, 1, 1);
        PathFileManager.transform.localEulerAngles = new Vector3(0, 0, 0);
        //var TempPathFile = PathFileManager.AddComponent<TrickyPathFileManager>();
        //TempPathFile.runInEditMode = true;
        //TempPathFile.GenerateEmptyObjects();

        PrefabsHolder = new GameObject("Prefabs");
        PrefabsHolder.transform.parent = PrefabManagerHolder.transform;
        PrefabsHolder.transform.localPosition = new Vector3(0, 0, 20000);
        PrefabsHolder.transform.localEulerAngles = Vector3.zero;
        PrefabsHolder.transform.localScale = Vector3.one;

        MaterialHolder = new GameObject("Materials");
        MaterialHolder.transform.parent = PrefabManagerHolder.transform;
        MaterialHolder.transform.localPosition = Vector3.zero;
        MaterialHolder.transform.localEulerAngles = Vector3.zero;
        MaterialHolder.transform.localScale = Vector3.one;

        ParticlePrefabHolder = new GameObject("Particle Prefabs");
        ParticlePrefabHolder.transform.parent = PrefabManagerHolder.transform;
        ParticlePrefabHolder.transform.localPosition = new Vector3(0, 0, 30000);
        ParticlePrefabHolder.transform.localEulerAngles = Vector3.zero;
        ParticlePrefabHolder.transform.localScale = Vector3.one;

        PatchesHolder = new GameObject("Patches");
        PatchesHolder.transform.parent = WorldManagerHolder.transform;
        PatchesHolder.transform.localScale = Vector3.one;
        PatchesHolder.transform.localEulerAngles = Vector3.zero;

        InstancesHolder = new GameObject("Instances");
        InstancesHolder.transform.parent = WorldManagerHolder.transform;
        InstancesHolder.transform.localScale = Vector3.one;
        InstancesHolder.transform.localEulerAngles = Vector3.zero;

        SplinesHolder = new GameObject("Splines");
        SplinesHolder.transform.parent = WorldManagerHolder.transform;
        SplinesHolder.transform.localScale = Vector3.one;
        SplinesHolder.transform.localEulerAngles = Vector3.zero;

        ParticlesHolder = new GameObject("Particles");
        ParticlesHolder.transform.parent = WorldManagerHolder.transform;
        ParticlesHolder.transform.localScale = Vector3.one;
        ParticlesHolder.transform.localEulerAngles = Vector3.zero;

        LightingHolder = new GameObject("Lighting");
        LightingHolder.transform.parent = WorldManagerHolder.transform;
        LightingHolder.transform.localScale = Vector3.one;
        LightingHolder.transform.localEulerAngles = Vector3.zero;

        CameraHolder = new GameObject("Cameras");
        CameraHolder.transform.parent = WorldManagerHolder.transform;
        CameraHolder.transform.localPosition = Vector3.zero;
        CameraHolder.transform.localScale = Vector3.one;
        CameraHolder.transform.localEulerAngles = Vector3.zero;

        SkyboxMaterialHolder = new GameObject("Materials");
        SkyboxMaterialHolder.transform.parent = SkyboxManagerHolder.transform;
        SkyboxMaterialHolder.transform.localScale = Vector3.one;
        SkyboxMaterialHolder.transform.localEulerAngles = Vector3.zero;
        SkyboxMaterialHolder.transform.localPosition = Vector3.zero;

        SkyboxPrefabsHolder = new GameObject("Prefabs");
        SkyboxPrefabsHolder.transform.parent = SkyboxManagerHolder.transform;
        SkyboxPrefabsHolder.transform.localScale = Vector3.one;
        SkyboxPrefabsHolder.transform.localEulerAngles = Vector3.zero;
        SkyboxPrefabsHolder.transform.localPosition = new Vector3(0, 0, 10000);

        EffectSlotHolder = new GameObject("Effect Slots");
        EffectSlotHolder.transform.parent = LogicManager.transform;
        EffectSlotHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        EffectSlotHolder.transform.localEulerAngles = new Vector3(0, 0, 0);

        PhysicsHolder = new GameObject("Physics");
        PhysicsHolder.transform.parent = LogicManager.transform;
        PhysicsHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        PhysicsHolder.transform.localEulerAngles = new Vector3(0, 0, 0);

        EffectHolder = new GameObject("Effects");
        EffectHolder.transform.parent = LogicManager.transform;
        EffectHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        EffectHolder.transform.localEulerAngles = new Vector3(0, 0, 0);

        FunctionHolder = new GameObject("Functions");
        FunctionHolder.transform.parent = LogicManager.transform;
        FunctionHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        FunctionHolder.transform.localEulerAngles = new Vector3(0, 0, 0);

        AIPHolder = new GameObject("General");
        AIPHolder.transform.parent = PathFileManager.transform;
        AIPHolder.transform.localScale = Vector3.one;
        AIPHolder.transform.localEulerAngles = Vector3.zero;
        AIPHolder.AddComponent<PathManager>().GenerateEmptyObjects(PathManager.PathManagerType.General);

        SOPHolder = new GameObject("Showoff");
        SOPHolder.transform.parent = PathFileManager.transform;
        SOPHolder.transform.localScale = Vector3.one;
        SOPHolder.transform.localEulerAngles = Vector3.zero;
        SOPHolder.AddComponent<PathManager>().GenerateEmptyObjects(PathManager.PathManagerType.Showoff);

        LoadMaterials(Path + "\\Materials.json", MaterialHolder);
        LoadPrefabs(Path + "\\Prefabs.json", PrefabsHolder);
        LoadParticlePrefabs(Path + "\\ParticlePrefabs.json", ParticlePrefabHolder);
        LoadPatches(Path + "\\Patches.json", PatchesHolder);
        LoadInstance(Path + "\\Instances.json", InstancesHolder);
        LoadSplines(Path + "\\Splines.json", SplinesHolder);
        LoadLighting(Path + "\\Lights.json", LightingHolder);
        LoadParticleInstances(Path + "\\ParticleInstances.json", ParticlesHolder);
        LoadCameraInstances(Path + "\\Cameras.json", CameraHolder);
        SSFJsonHandler ssfJsonHandler = new SSFJsonHandler();
        ssfJsonHandler = SSFJsonHandler.Load(Path + "\\SSFLogic.json");
        LoadEffectSlots(ssfJsonHandler.EffectSlots, EffectSlotHolder);
        LoadPhysics(ssfJsonHandler.PhysicsHeaders, PhysicsHolder);
        LoadEffects(ssfJsonHandler.EffectHeaders, EffectHolder);
        LoadFunctions(ssfJsonHandler.Functions, FunctionHolder);
        if (File.Exists(Path + "\\Skybox\\Materials.json"))
        {
            LoadSkyboxMaterials(Path + "\\Skybox\\Materials.json", SkyboxMaterialHolder);
            LoadSkyboxPrefabs(Path + "\\Skybox\\Prefabs.json", SkyboxPrefabsHolder);
        }
        LoadAIP(Path + "\\AIP.json", AIPHolder);
        LoadSOP(Path + "\\SOP.json", SOPHolder);
    }

    public void LoadMaterials(string Path, GameObject MaterialHolder)
    {
        float XPosition = 0;
        float ZPosition = 0;
        int X = 0;

        MaterialJsonHandler materialJsonHandler = new MaterialJsonHandler();
        materialJsonHandler = MaterialJsonHandler.Load(Path);

        int WH = (int)Mathf.Sqrt(materialJsonHandler.Materials.Count);

        for (int i = 0; i < materialJsonHandler.Materials.Count; i++)
        {
            GameObject gameObject = new GameObject("Materials " + i);
            //gameObject.transform.hideFlags = HideFlags.HideInInspector;
            gameObject.transform.parent = MaterialHolder.transform;
            gameObject.transform.localPosition = new Vector3(XPosition, -ZPosition, 0);
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            gameObject.transform.localScale = new Vector3(10, 10, 10);
            TrickyMaterialObject materialObject = gameObject.AddComponent<TrickyMaterialObject>();
            materialObject.LoadMaterial(materialJsonHandler.Materials[i]);

            if (X != WH)
            {
                XPosition += 10000;
                X++;
            }
            else
            {
                XPosition = 0;
                X = 0;
                ZPosition += 10000;
            }
        }
    }

    public void LoadPrefabs(string Path, GameObject PrefabsHolder)
    {
        float XPosition = 0;
        float ZPosition = 0;
        int X = 0;

        PrefabJsonHandler PrefabJson = PrefabJsonHandler.Load(Path);
        int WH = (int)Mathf.Sqrt(PrefabJson.Prefabs.Count);
        for (int i = 0; i < PrefabJson.Prefabs.Count; i++)
        {
            var TempModelJson = PrefabJson.Prefabs[i];
            GameObject gameObject = new GameObject("Prefab " + i);
            //gameObject.transform.hideFlags = HideFlags.HideInInspector;
            gameObject.transform.parent = PrefabsHolder.transform;
            gameObject.transform.localPosition = new Vector3(XPosition, -ZPosition, 0);
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            TrickyPrefabObject mObject = gameObject.AddComponent<TrickyPrefabObject>();
            mObject.LoadPrefab(TempModelJson);

            if (X != WH)
            {
                XPosition += 10000;
                X++;
            }
            else
            {
                XPosition = 0;
                X = 0;
                ZPosition += 10000;
            }
        }
    }

    public void LoadParticlePrefabs(string Path, GameObject ParticlePrefabHolder)
    {
        float XPosition = 0;
        float ZPosition = 0;
        int X = 0;

        ParticleModelJsonHandler PrefabJson = ParticleModelJsonHandler.Load(Path);
        int WH = (int)Mathf.Sqrt(PrefabJson.ParticlePrefabs.Count);
        for (int i = 0; i < PrefabJson.ParticlePrefabs.Count; i++)
        {
            var TempModelJson = PrefabJson.ParticlePrefabs[i];
            GameObject gameObject = new GameObject("Particle Prefab " + i);
            //gameObject.transform.hideFlags = HideFlags.HideInInspector;
            gameObject.transform.parent = ParticlePrefabHolder.transform;
            gameObject.transform.localPosition = new Vector3(XPosition, -ZPosition, 0);
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            ParticlePrefabObject mObject = gameObject.AddComponent<ParticlePrefabObject>();
            mObject.LoadParticle(TempModelJson);

            if (X != WH)
            {
                XPosition += 10000;
                X++;
            }
            else
            {
                XPosition = 0;
                X = 0;
                ZPosition += 10000;
            }
        }
    }

    public void LoadPatches(string JsonPath, GameObject PatchesHolder)
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

    public void LoadInstance(string Path, GameObject InstancesHolder)
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

    public void LoadSplines(string path, GameObject SplinesHolder)
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

    public void LoadLighting(string path, GameObject LightingHolder)
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

    public void LoadParticleInstances(string path, GameObject ParticlesHolder)
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

    public void LoadCameraInstances(string path, GameObject CameraHolder)
    {
        CameraJSONHandler instanceJsonHandler = new CameraJSONHandler();
        instanceJsonHandler = CameraJSONHandler.Load(path);

        for (int i = 0; i < instanceJsonHandler.Cameras.Count; i++)
        {
            var TempGameObject = new GameObject("Camera " + i);
            TempGameObject.transform.parent = CameraHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            var TempInstance = TempGameObject.AddComponent<TrickyCameraObject>();
            TempInstance.LoadCamera(instanceJsonHandler.Cameras[i]);
        }
    }

    public void LoadEffectSlots(List<SSFJsonHandler.EffectSlotJson> effectSlotJson, GameObject EffectSlotHolder)
    {
        for (int i = 0; i < effectSlotJson.Count; i++)
        {
            var TempGameObject = new GameObject("Effect Slot " + i);
            TempGameObject.transform.parent = EffectSlotHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            TempGameObject.transform.hideFlags = HideFlags.HideInInspector;

            var TempInstance = TempGameObject.AddComponent<EffectSlotObject>();
            TempInstance.LoadEffectSlot(effectSlotJson[i]);
        }
    }

    public void LoadPhysics(List<SSFJsonHandler.PhysicsHeader> physicsHeaders, GameObject PhysicsHolder)
    {
        for (int i = 0; i < physicsHeaders.Count; i++)
        {
            var TempGameObject = new GameObject("Physics " + i);
            TempGameObject.transform.parent = PhysicsHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            TempGameObject.transform.hideFlags = HideFlags.HideInInspector;
            var TempInstance = TempGameObject.AddComponent<PhysicsObject>();
            TempInstance.LoadPhysics(physicsHeaders[i]);
        }
    }

    public void LoadEffects(List<SSFJsonHandler.EffectHeaderStruct> effects, GameObject EffectHolder)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            var TempGameObject = new GameObject(effects[i].EffectName);
            TempGameObject.transform.parent = EffectHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            TempGameObject.transform.hideFlags = HideFlags.HideInInspector;
            TempGameObject.AddComponent<TrickyEffectHeader>().LoadEffectList(effects[i]);
        }
    }

    public void LoadFunctions(List<SSFJsonHandler.Function> effects, GameObject FunctionHolder)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            var TempGameObject = new GameObject(effects[i].FunctionName);
            TempGameObject.transform.parent = FunctionHolder.transform;
            TempGameObject.transform.localScale = Vector3.one;
            TempGameObject.transform.localEulerAngles = Vector3.zero;
            TempGameObject.transform.hideFlags = HideFlags.HideInInspector;
            TempGameObject.AddComponent<TrickyFunctionHeader>().LoadFunction(effects[i]);
        }
    }

    public void LoadSkyboxMaterials(string Path, GameObject MaterialHolder)
    {
        float XPosition = 0;
        float ZPosition = 0;
        int X = 0;
        MaterialJsonHandler materialJsonHandler = new MaterialJsonHandler();
        materialJsonHandler = MaterialJsonHandler.Load(Path);

        int WH = (int)Mathf.Sqrt(materialJsonHandler.Materials.Count);

        for (int i = 0; i < materialJsonHandler.Materials.Count; i++)
        {
            GameObject gameObject = new GameObject("Materials " + i);
            //gameObject.transform.hideFlags = HideFlags.HideInInspector;
            gameObject.transform.parent = MaterialHolder.transform;
            gameObject.transform.localPosition = new Vector3(XPosition, -ZPosition, 0);
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            gameObject.transform.localScale = new Vector3(10, 10, 10);
            TrickySkyboxMaterialObject materialObject = gameObject.AddComponent<TrickySkyboxMaterialObject>();
            materialObject.LoadMaterial(materialJsonHandler.Materials[i], true);


            if (X != WH)
            {
                XPosition += 10000;
                X++;
            }
            else
            {
                XPosition = 0;
                X = 0;
                ZPosition += 10000;
            }
        }
    }

    public void LoadSkyboxPrefabs(string Path, GameObject PrefabsHolder)
    {
        float XPosition = 0;
        float ZPosition = 0;
        int X = 0;

        PrefabJsonHandler PrefabJson = PrefabJsonHandler.Load(Path);
        int WH = (int)Mathf.Sqrt(PrefabJson.Prefabs.Count);
        for (int i = 0; i < PrefabJson.Prefabs.Count; i++)
        {
            var TempModelJson = PrefabJson.Prefabs[i];
            GameObject gameObject = new GameObject("Skybox Prefab " + i);
            //gameObject.transform.hideFlags = HideFlags.HideInInspector;
            gameObject.transform.parent = PrefabsHolder.transform;
            gameObject.transform.localPosition = new Vector3(XPosition, -ZPosition, 0);
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            TrickySkyboxPrefabObject mObject = gameObject.AddComponent<TrickySkyboxPrefabObject>();
            mObject.LoadPrefab(TempModelJson, true);

            if (X != WH)
            {
                XPosition += 10000;
                X++;
            }
            else
            {
                XPosition = 0;
                X = 0;
                ZPosition += 10000;
            }
        }
    }

    public void LoadAIP(string Path, GameObject AIPObject)
    {
        AIPSOPJsonHandler aipJson = new AIPSOPJsonHandler();
        aipJson = AIPSOPJsonHandler.Load(Path);
        AIPObject.GetComponent<PathManager>().LoadJson(aipJson);
    }

    public void LoadSOP(string Path, GameObject SOPObject)
    {
        AIPSOPJsonHandler sopJson = new AIPSOPJsonHandler();
        sopJson = AIPSOPJsonHandler.Load(Path);
        SOPObject.GetComponent<PathManager>().LoadJson(sopJson);
    }
    #endregion

    #region Save Data
    public void SaveData(string Path)
    {
        SaveMaterials(Path + "\\Materials.json");
        SaveParticlePrefabs(Path + "\\ParticlePrefabs.json");
        SavePrefabs(Path + "\\Prefabs.json");
        SavePatches(Path + "\\Patches.json");
        SaveInstances(Path + "\\Instances.json");
        SaveSplines(Path + "\\Splines.json");
        SaveLighting(Path + "\\Lights.json");
        SaveParticleInstance(Path + "\\ParticleInstances.json");
        SaveCameras(Path + "\\Cameras.json");
        SaveSSFData(Path);
        SaveSkyboxMaterials(Path + "\\Skybox\\Materials.json");
        SaveSkyboxPrefabs(Path + "\\Skybox\\Prefabs.json");
        SaveAIP(Path + "\\AIP.json");
        SaveSOP(Path + "\\SOP.json");
    }

    public void SaveSSFData(string path)
    {
        SSFJsonHandler ssfJsonHandler = new SSFJsonHandler();
        ssfJsonHandler.EffectSlots = SaveEffectSlots();
        ssfJsonHandler.PhysicsHeaders = SavePhysicsHeader();
        ssfJsonHandler.EffectHeaders = GetEffectHeadersList();
        ssfJsonHandler.Functions = GetFunctionList();
        ssfJsonHandler.CreateJson(path + "\\SSFLogic.json");
    }

    public void SaveMaterials(string path)
    {
        MaterialJsonHandler materialJsonHandler = new MaterialJsonHandler();
        materialJsonHandler.Materials = new List<MaterialJsonHandler.MaterialsJson>();

        for (int i = 0; i < trickyMaterialObjects.Count; i++)
        {
            materialJsonHandler.Materials.Add(trickyMaterialObjects[i].GenerateMaterial());
        }
        materialJsonHandler.CreateJson(path);
    }

    public void SaveParticlePrefabs(string path)
    {
        ParticleModelJsonHandler particleModelJsonHandler = new ParticleModelJsonHandler();
        particleModelJsonHandler.ParticlePrefabs = new List<ParticleModelJsonHandler.ParticleModelJson>();

        for (int i = 0; i < particlePrefabObjects.Count; i++)
        {
            particleModelJsonHandler.ParticlePrefabs.Add(particlePrefabObjects[i].GenerateParticle());
        }

        particleModelJsonHandler.CreateJson(path);
    }

    public void SavePrefabs(string path)
    {
        PrefabJsonHandler prefabJsonHandler = new PrefabJsonHandler();
        prefabJsonHandler.Prefabs = new List<PrefabJsonHandler.PrefabJson>();

        for (int i = 0; i < trickyPrefabObjects.Count; i++)
        {
            prefabJsonHandler.Prefabs.Add(trickyPrefabObjects[i].GeneratePrefabs());
        }

        prefabJsonHandler.CreateJson(path);
    }

    public void SavePatches(string path)
    {
        PatchesJsonHandler patchesJsonHandler = new PatchesJsonHandler();
        patchesJsonHandler.Patches = new List<PatchesJsonHandler.PatchJson>();

        for (int i = 0; i < trickyPatchObjects.Count; i++)
        {
            patchesJsonHandler.Patches.Add(trickyPatchObjects[i].GeneratePatch());
        }

        patchesJsonHandler.CreateJson(path);
    }

    public void SaveInstances(string path)
    {
        InstanceJsonHandler instanceJsonHandler = new InstanceJsonHandler();
        instanceJsonHandler.Instances = new List<InstanceJsonHandler.InstanceJson>();

        for (int i = 0; i < trickyInstances.Count; i++)
        {
            instanceJsonHandler.Instances.Add(trickyInstances[i].GenerateInstance());
        }
        instanceJsonHandler.CreateJson(path);
    }

    public void SaveSplines(string path)
    {
        SplineJsonHandler splineJsonHandler = new SplineJsonHandler();
        splineJsonHandler.Splines = new List<SplineJsonHandler.SplineJson>();

        for (int i = 0; i < trickySplineObjects.Count; i++)
        {
            splineJsonHandler.Splines.Add(trickySplineObjects[i].GenerateSpline());
        }
        splineJsonHandler.CreateJson(path);
    }

    public void SaveLighting(string path)
    {
        LightJsonHandler lightJsonHandler = new LightJsonHandler();
        lightJsonHandler.Lights = new List<LightJsonHandler.LightJson>();

        for (int i = 0; i < lightObjects.Count; i++)
        {
            lightJsonHandler.Lights.Add(lightObjects[i].GenerateLight());
        }

        lightJsonHandler.CreateJson(path);
    }

    public void SaveCameras(string path)
    {
        CameraJSONHandler cameraJSONHandler = new CameraJSONHandler();
        cameraJSONHandler.Cameras = new List<CameraJSONHandler.CameraInstance>();

        for (int i = 0; i < trickyCameraObjects.Count; i++)
        {
            cameraJSONHandler.Cameras.Add(trickyCameraObjects[i].GenerateCamera());
        }

        cameraJSONHandler.CreateJson(path);
    }

    public void SaveParticleInstance(string path)
    {
        ParticleInstanceJsonHandler particleInstanceJsonHandler = new ParticleInstanceJsonHandler();
        particleInstanceJsonHandler.Particles = new List<ParticleInstanceJsonHandler.ParticleJson>();

        for (int i = 0; i < paticleInstanceObjects.Count; i++)
        {
            particleInstanceJsonHandler.Particles.Add(paticleInstanceObjects[i].GenerateParticleInstance());
        }

        particleInstanceJsonHandler.CreateJson(path);
    }

    public List<SSFJsonHandler.EffectSlotJson> SaveEffectSlots()
    {
        var NewEffectSlotList = new List<SSFJsonHandler.EffectSlotJson>();

        for (int i = 0; i < effectSlotObjects.Count; i++)
        {
            NewEffectSlotList.Add(effectSlotObjects[i].SaveEffectSlot());
        }
        return NewEffectSlotList;
    }

    public List<SSFJsonHandler.PhysicsHeader> SavePhysicsHeader()
    {
        var NewPhysicsHeaders = new List<SSFJsonHandler.PhysicsHeader>();

        for (int i = 0; i < trickyPhysicsObjects.Count; i++)
        {
            NewPhysicsHeaders.Add(trickyPhysicsObjects[i].GeneratePhysics());
        }

        return NewPhysicsHeaders;
    }

    public List<SSFJsonHandler.EffectHeaderStruct> GetEffectHeadersList()
    {
        List<SSFJsonHandler.EffectHeaderStruct> HeaderList = new List<SSFJsonHandler.EffectHeaderStruct>();

        for (int i = 0; i < trickyEffectHeaders.Count; i++)
        {
            HeaderList.Add(trickyEffectHeaders[i].GenerateEffectHeader());
        }

        return HeaderList;
    }

    public List<SSFJsonHandler.Function> GetFunctionList()
    {
        List<SSFJsonHandler.Function> HeaderList = new List<SSFJsonHandler.Function>();

        for (int i = 0; i < trickyFunctionHeaders.Count; i++)
        {
            HeaderList.Add(trickyFunctionHeaders[i].GenerateFunction());
        }

        return HeaderList;
    }


    public void SaveSkyboxMaterials(string path)
    {
        MaterialJsonHandler materialJsonHandler = new MaterialJsonHandler();
        materialJsonHandler.Materials = new List<MaterialJsonHandler.MaterialsJson>();

        for (int i = 0; i < trickySkyboxMaterialObjects.Count; i++)
        {
            materialJsonHandler.Materials.Add(trickySkyboxMaterialObjects[i].GenerateMaterial());
        }
        materialJsonHandler.CreateJson(path);
    }

    public void SaveSkyboxPrefabs(string path)
    {
        PrefabJsonHandler prefabJsonHandler = new PrefabJsonHandler();
        prefabJsonHandler.Prefabs = new List<PrefabJsonHandler.PrefabJson>();

        for (int i = 0; i < trickySkyboxPrefabObjects.Count; i++)
        {
            prefabJsonHandler.Prefabs.Add(trickySkyboxPrefabObjects[i].GeneratePrefabs(true));
        }

        prefabJsonHandler.CreateJson(path);
    }

    public void SaveAIP(string Path)
    {
        trickyGeneralPaths.SaveJson(Path);
    }

    public void SaveSOP(string Path)
    {
        trickyShowoffPaths.SaveJson(Path);
    }
    #endregion

    public int GetInstanceID(TrickyInstanceObject trickyInstanceObject)
    {
        int ID = -1;

        if(trickyInstances.Contains(trickyInstanceObject))
        {
            ID = trickyInstances.IndexOf(trickyInstanceObject);
        }

        return ID;
    }

    public int GetSplineID(TrickySplineObject trickySplineObject)
    {
        int ID = -1;

        if (trickySplineObjects.Contains(trickySplineObject))
        {
            ID = trickySplineObjects.IndexOf(trickySplineObject);
        }

        return ID;
    }

    public int GetPrefabID(TrickyPrefabObject trickyPrefabObject)
    {
        int ID = -1;

        if (trickyPrefabObjects.Contains(trickyPrefabObject))
        {
            ID = trickyPrefabObjects.IndexOf(trickyPrefabObject);
        }

        return ID;
    }

    public int GetEffectSlotID(EffectSlotObject effectSlotObject)
    {
        int ID = -1;

        if (effectSlotObjects.Contains(effectSlotObject))
        {
            ID = effectSlotObjects.IndexOf(effectSlotObject);
        }

        return ID;
    }

    public int GetPhysicstID(PhysicsObject physicsObject)
    {
        int ID = -1;

        if (trickyPhysicsObjects.Contains(physicsObject))
        {
            ID = trickyPhysicsObjects.IndexOf(physicsObject);
        }

        return ID;
    }

    public int GetFunctionID(TrickyFunctionHeader trickyFunctionHeader)
    {
        int ID = -1;

        if (trickyFunctionHeaders.Contains(trickyFunctionHeader))
        {
            ID = trickyFunctionHeaders.IndexOf(trickyFunctionHeader);
        }

        return ID;
    }

    public int GetEffectID(TrickyEffectHeader trickyEffectHeader)
    {
        int ID = -1;

        if (trickyEffectHeaders.Contains(trickyEffectHeader))
        {
            ID = trickyEffectHeaders.IndexOf(trickyEffectHeader);
        }

        return ID;
    }

    public int GetSkyboxMaterialID(TrickySkyboxMaterialObject trickySkyboxMaterialObject)
    {
        int ID = -1;

        if (trickySkyboxMaterialObjects.Contains(trickySkyboxMaterialObject))
        {
            ID = trickySkyboxMaterialObjects.IndexOf(trickySkyboxMaterialObject);
        }

        return ID;
    }

    public int GetEffectHeaderID(TrickyEffectHeader trickyEffectHeader)
    {
        int ID = -1;

        if (trickyEffectHeaders.Contains(trickyEffectHeader))
        {
            ID = trickyEffectHeaders.IndexOf(trickyEffectHeader);
        }

        return ID;
    }
}
