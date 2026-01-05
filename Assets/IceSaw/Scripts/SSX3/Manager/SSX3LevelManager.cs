using SSXMultiTool.JsonFiles.SSX3;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static GLTFast.Schema.AnimationChannelBase;

[ExecuteInEditMode]
public class SSX3LevelManager : MonoBehaviour
{
    public string LoadPath;
    public bool Loaded;

    public List<MeshData> MeshCache = new List<MeshData>();
    
    public void AddLevelString(string loadPath)
    {
        LoadPath = loadPath;
    }

    public void LoadLevel()
    {
        var PatchesHolder = new GameObject("Patches");
        PatchesHolder.transform.parent = transform;
        PatchesHolder.transform.localScale = Vector3.one;
        PatchesHolder.transform.localEulerAngles = Vector3.zero;

        LoadPatches(Directory.GetFiles(LoadPath, "Patches.json", SearchOption.AllDirectories)[0], PatchesHolder);

        LoadMeshCache(LoadPath + "\\Models");

        var ModelsHolder = new GameObject("Models");
        ModelsHolder.transform.parent = transform;
        ModelsHolder.transform.localScale = Vector3.one;
        ModelsHolder.transform.localEulerAngles = Vector3.zero;

        LoadModels(Directory.GetFiles(LoadPath, "Prefabs.json", SearchOption.AllDirectories)[0], ModelsHolder);

        var Bin3Holder = new GameObject("Instances");
        Bin3Holder.transform.parent = transform;
        Bin3Holder.transform.localScale = Vector3.one;
        Bin3Holder.transform.localEulerAngles = Vector3.zero;

        LoadInstance(Directory.GetFiles(LoadPath, "Instances.json", SearchOption.AllDirectories)[0], Bin3Holder);

        var Bin11Holder = new GameObject("Vis Curtain");
        Bin11Holder.transform.parent = transform;
        Bin11Holder.transform.localScale = Vector3.one;
        Bin11Holder.transform.localEulerAngles = Vector3.zero;

        LoadBin11(Directory.GetFiles(LoadPath, "VisCurtain.json", SearchOption.AllDirectories)[0], Bin11Holder);

        var SplineHolder = new GameObject("Splines");
        SplineHolder.transform.parent = transform;
        SplineHolder.transform.localScale = Vector3.one;
        SplineHolder.transform.localEulerAngles = Vector3.zero;

        LoadSpline(Directory.GetFiles(LoadPath, "Splines.json", SearchOption.AllDirectories)[0], SplineHolder);

        var AIP0 = new GameObject("AIP");
        AIP0.transform.parent = transform;
        AIP0.transform.localScale = Vector3.one;
        AIP0.transform.localEulerAngles = Vector3.zero;
        AIP0.AddComponent<SSX3PathManager>().LoadJson(Directory.GetFiles(LoadPath, "AIP.json", SearchOption.AllDirectories)[0]);

        var AIP1 = new GameObject("Peak Race AIP");
        AIP1.transform.parent = transform;
        AIP1.transform.localScale = Vector3.one;
        AIP1.transform.localEulerAngles = Vector3.zero;
        AIP1.AddComponent<SSX3PathManager>().LoadJson(Directory.GetFiles(LoadPath, "PeakRaceAIP.json", SearchOption.AllDirectories)[0]);

        var AIP2 = new GameObject("Peak ShowOff AIP");
        AIP2.transform.parent = transform;
        AIP2.transform.localScale = Vector3.one;
        AIP2.transform.localEulerAngles = Vector3.zero;
        AIP2.AddComponent<SSX3PathManager>().LoadJson(Directory.GetFiles(LoadPath, "PeakShowOffAIP.json", SearchOption.AllDirectories)[0]);
    }

    public void UnloadLevel()
    {
        MeshCache.Clear();
        MeshCache = new List<MeshData>();

        var ChildrenCount = transform.childCount;
        for (int i = 0; i < ChildrenCount; i++)
        {
            var ChildTransform = transform.GetChild(0);
            DestroyImmediate(ChildTransform.gameObject);
        }
    }

    public void LoadPatches(string JsonPath, GameObject gameObject)
    {
        PatchesJsonHandler patchesJsonHandler = new PatchesJsonHandler();
        patchesJsonHandler = PatchesJsonHandler.Load(JsonPath);

        for (int i = 0; i < patchesJsonHandler.Patches.Count; i++)
        {
            GameObject NewPatch = new GameObject();
            NewPatch.transform.parent = gameObject.transform;
            NewPatch.transform.localPosition = Vector3.zero;
            NewPatch.transform.localScale = Vector3.one;
            NewPatch.transform.localEulerAngles = Vector3.zero;
            var TempObject = NewPatch.AddComponent<SSX3PatchObject>();
            TempObject.AddMissingComponents();
            TempObject.LoadPatch(patchesJsonHandler.Patches[i]);
        }
    }

    public void LoadInstance(string JsonPath, GameObject gameObject)
    {
        InstanceJsonHandler bin3JsonHandler = new InstanceJsonHandler();
        bin3JsonHandler = InstanceJsonHandler.Load(JsonPath);

        for (int i = 0; i < bin3JsonHandler.Instances.Count; i++)
        {
            GameObject NewPatch = new GameObject();
            NewPatch.transform.parent = gameObject.transform;
            NewPatch.transform.localPosition = Vector3.zero;
            NewPatch.transform.localScale = Vector3.one;
            NewPatch.transform.localEulerAngles = Vector3.zero;
            var TempObject = NewPatch.AddComponent<SSX3InstanceObject>();
            TempObject.LoadBin3(bin3JsonHandler.Instances[i]);
        }
    }

    public void LoadBin11(string JsonPath, GameObject gameObject)
    {
        VisCurtainJsonHandler visCurtainJsonHandler = new VisCurtainJsonHandler();
        visCurtainJsonHandler = VisCurtainJsonHandler.Load(JsonPath);

        for (int i = 0; i < visCurtainJsonHandler.VisCurtains.Count; i++)
        {
            GameObject NewPatch = new GameObject();
            NewPatch.name = i.ToString();
            NewPatch.transform.parent = gameObject.transform;
            NewPatch.transform.localPosition = Vector3.zero;
            NewPatch.transform.localScale = Vector3.one;
            NewPatch.transform.localEulerAngles = Vector3.zero;
            var TempObject = NewPatch.AddComponent<SSX3VisCurtain>();
            TempObject.LoadVisCurtain(visCurtainJsonHandler.VisCurtains[i]);
        }
    }

    public void LoadSpline(string JsonPath, GameObject gameObject)
    {
        SplineJsonHandler splineJsonHandler = new SplineJsonHandler();
        splineJsonHandler = SplineJsonHandler.Load(JsonPath);

        for (int i = 0; i < splineJsonHandler.Splines.Count; i++)
        {
            GameObject NewPatch = new GameObject();
            NewPatch.transform.parent = gameObject.transform;
            NewPatch.transform.localPosition = Vector3.zero;
            NewPatch.transform.localScale = Vector3.one;
            NewPatch.transform.localEulerAngles = Vector3.zero;
            var TempObject = NewPatch.AddComponent<SSX3Spline>();
            TempObject.LoadBin3(splineJsonHandler.Splines[i]);
        }
    }

    public void LoadModels(string JsonPath, GameObject gameObject)
    {
        MDRJsonHandler MDRJson = new MDRJsonHandler();
        MDRJson = MDRJsonHandler.Load(JsonPath);

        for (int i = 0; i < MDRJson.mainModelHeaders.Count; i++)
        {
            GameObject NewPatch = new GameObject();
            NewPatch.transform.parent = gameObject.transform;
            NewPatch.transform.localPosition = Vector3.zero;
            NewPatch.transform.localScale = Vector3.one;
            NewPatch.transform.localEulerAngles = Vector3.zero;
            var TempObject = NewPatch.AddComponent<SSX3ModelObject>();
            TempObject.LoadModel(MDRJson.mainModelHeaders[i]);

        }
    }

    public Mesh GetMesh(string MeshPath)
    {
        Mesh mesh = null;

        for (int i = 0; i < MeshCache.Count; i++)
        {
            if (MeshCache[i].Name == MeshPath)
            {
                mesh = MeshCache[i].mesh;
                break;
            }
        }

        if (mesh == null)
        {
            mesh = (Mesh)AssetDatabase.LoadAssetAtPath("Assets\\IceSaw\\Mesh\\tinker.obj", typeof(Mesh));
        }

        return mesh;

    }

    public void LoadMeshCache(string path)
    {
        MeshCache = new List<SSX3LevelManager.MeshData>();

        string[] Files = Directory.GetFiles(path, "*.obj", SearchOption.AllDirectories);
        for (int i = 0; i < Files.Length; i++)
        {
            SSX3LevelManager.MeshData TempMesh = new SSX3LevelManager.MeshData();
            TempMesh.mesh = ObjImporter.ObjLoad(Files[i]);
            TempMesh.Name = Files[i].Substring(path.Length + 1);
            MeshCache.Add(TempMesh);
        }
    }

    [System.Serializable]
    public struct MeshData
    {
        public string Name;
        public Mesh mesh;
    }

    public static SSX3LevelManager GetLevelManager(GameObject child)
    {
        if(child.transform.parent!=null)
        {
            return ParentLevelManagerCheck(child.transform.parent);
        }

        return null;
    }

    private static SSX3LevelManager ParentLevelManagerCheck(Transform parent)
    {
        var levelManager = parent.GetComponent<SSX3LevelManager>();

        if(levelManager!=null)
        {
            return levelManager; 
        }

        if(parent.parent!=null)
        {
            return ParentLevelManagerCheck(parent.parent);
        }

        return null;
    }
}

[CustomEditor(typeof(SSX3LevelManager)), CanEditMultipleObjects] // Links this editor to MyScript
public class MyScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector properties
        DrawDefaultInspector();

        // Get a reference to the target script
        var myScript = targets;

        // Add a button
        if (GUILayout.Button("Load Level"))
        {
            for (global::System.Int32 i = 0; i < myScript.Length; i++)
            {
                ((SSX3LevelManager)myScript[i]).LoadLevel();
            }
        }

        if (GUILayout.Button("Save Level"))
        {

        }

        if (GUILayout.Button("Unload Level"))
        {
            for (global::System.Int32 i = 0; i < myScript.Length; i++)
            {
                ((SSX3LevelManager)myScript[i]).UnloadLevel();
            }
        }
    }
}
