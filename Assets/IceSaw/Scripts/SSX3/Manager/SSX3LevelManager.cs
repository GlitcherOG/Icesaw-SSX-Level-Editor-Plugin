using SSXMultiTool.JsonFiles.SSX3;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class SSX3LevelManager : MonoBehaviour
{
    public List<MeshData> MeshCache = new List<MeshData>();
    public void LoadLevel(string LoadPath)
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

        var Bin3Holder = new GameObject("Bin3");
        Bin3Holder.transform.parent = transform;
        Bin3Holder.transform.localScale = Vector3.one;
        Bin3Holder.transform.localEulerAngles = Vector3.zero;

        LoadBin3(Directory.GetFiles(LoadPath, "Bin3.json", SearchOption.AllDirectories)[0], Bin3Holder);

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

    public void LoadBin3(string JsonPath, GameObject gameObject)
    {
        Bin3JsonHandler bin3JsonHandler = new Bin3JsonHandler();
        bin3JsonHandler = Bin3JsonHandler.Load(JsonPath);

        for (int i = 0; i < bin3JsonHandler.bin3Files.Count; i++)
        {
            GameObject NewPatch = new GameObject();
            NewPatch.transform.parent = gameObject.transform;
            NewPatch.transform.localPosition = Vector3.zero;
            NewPatch.transform.localScale = Vector3.one;
            NewPatch.transform.localEulerAngles = Vector3.zero;
            var TempObject = NewPatch.AddComponent<SSX3InstanceObject>();
            TempObject.LoadBin3(bin3JsonHandler.bin3Files[i]);
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
            var TempObject = NewPatch.AddComponent<SSX3PrefabObject>();
            TempObject.LoadPrefab(MDRJson.mainModelHeaders[i]);

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
}
