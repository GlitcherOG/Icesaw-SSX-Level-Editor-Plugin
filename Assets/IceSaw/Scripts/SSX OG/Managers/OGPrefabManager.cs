using SSXMultiTool.JsonFiles.SSXOG;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static SSXMultiTool.JsonFiles.SSXOG.PrefabJsonHandler;

[ExecuteInEditMode]
public class OGPrefabManager : MonoBehaviour
{
    public static OGPrefabManager Instance;
    public GameObject PrefabsHolder;
    public GameObject MaterialHolder;
    public GameObject ParticlePrefabHolder;

    public List<MeshData> MeshCache = new List<MeshData>();
    public List<MeshData> CollisionMeshCahce = new List<MeshData>();

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

        PrefabsHolder = new GameObject("Prefabs");
        PrefabsHolder.transform.parent = transform;
        PrefabsHolder.transform.localPosition = new Vector3(0, 0, 20000);
        PrefabsHolder.transform.localEulerAngles = Vector3.zero;
        PrefabsHolder.transform.localScale = Vector3.one;
        PrefabsHolder.transform.hideFlags = HideFlags.HideInInspector;

        MaterialHolder = new GameObject("Materials");
        MaterialHolder.transform.parent = transform;
        MaterialHolder.transform.localPosition = Vector3.zero;
        MaterialHolder.transform.localEulerAngles = Vector3.zero;
        MaterialHolder.transform.localScale = Vector3.one;
        MaterialHolder.transform.hideFlags = HideFlags.HideInInspector;

        ParticlePrefabHolder = new GameObject("Particle Prefabs");
        ParticlePrefabHolder.transform.parent = transform;
        ParticlePrefabHolder.transform.localPosition = new Vector3(0,0,30000);
        ParticlePrefabHolder.transform.localEulerAngles = Vector3.zero;
        ParticlePrefabHolder.transform.localScale = Vector3.one;
        ParticlePrefabHolder.transform.hideFlags = HideFlags.HideInInspector;
    }

    #region Load Data
    public void LoadData(string Path)
    {
        LoadMeshCache(Path + "\\Models");
        //LoadCollisionMeshCache(Path + "\\Collision");
        LoadMaterials(Path + "\\Materials.json");
        LoadPrefabs(Path + "\\Prefabs.json");
        //LoadParticlePrefabs(Path + "\\ParticlePrefabs.json");
    }

    public void LoadMeshCache(string path)
    {
        MeshCache = new List<MeshData>();

        string[] Files = Directory.GetFiles(path, "*.obj", SearchOption.AllDirectories);
        for (int i = 0; i < Files.Length; i++)
        {
            MeshData TempMesh = new MeshData();
            TempMesh.mesh = ObjImporter.ObjLoad(Files[i]);
            TempMesh.Name = Files[i].Substring(path.Length + 1);
            MeshCache.Add(TempMesh);
        }
    }

    public void LoadCollisionMeshCache(string path)
    {
        CollisionMeshCahce = new List<MeshData>();

        string[] Files = Directory.GetFiles(path, "*.obj", SearchOption.AllDirectories);
        for (int i = 0; i < Files.Length; i++)
        {
            MeshData TempMesh = new MeshData();
            TempMesh.mesh = ObjImporter.ObjLoad(Files[i]);
            TempMesh.Name = Files[i].Substring(path.Length + 1);
            CollisionMeshCahce.Add(TempMesh);
        }
    }

    public void LoadMaterials(string Path)
    {
        float XPosition = 0;
        float ZPosition = 0;
        int X = 0;

        MaterialsJsonHandler materialJsonHandler = new MaterialsJsonHandler();
        materialJsonHandler = MaterialsJsonHandler.Load(Path);

        int WH = (int)Mathf.Sqrt(materialJsonHandler.Materials.Count);

        for (int i = 0; i < materialJsonHandler.Materials.Count; i++)
        {
            GameObject gameObject = new GameObject("Materials " + i);
            //gameObject.transform.hideFlags = HideFlags.HideInInspector;
            gameObject.transform.parent = MaterialHolder.transform;
            gameObject.transform.localPosition = new Vector3(XPosition, -ZPosition, 0);
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            gameObject.transform.localScale = new Vector3(10, 10, 10);
            OGMaterialObject materialObject = gameObject.AddComponent<OGMaterialObject>();
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

    public void LoadPrefabs(string Path)
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
            OGPrefabObject mObject = gameObject.AddComponent<OGPrefabObject>();
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

    //public void LoadParticlePrefabs(string Path)
    //{
    //    float XPosition = 0;
    //    float ZPosition = 0;
    //    int X = 0;

    //    ParticleModelJsonHandler PrefabJson = ParticleModelJsonHandler.Load(Path);
    //    int WH = (int)Mathf.Sqrt(PrefabJson.ParticlePrefabs.Count);
    //    for (int i = 0; i < PrefabJson.ParticlePrefabs.Count; i++)
    //    {
    //        var TempModelJson = PrefabJson.ParticlePrefabs[i];
    //        GameObject gameObject = new GameObject("Particle Prefab " + i);
    //        //gameObject.transform.hideFlags = HideFlags.HideInInspector;
    //        gameObject.transform.parent = ParticlePrefabHolder.transform;
    //        gameObject.transform.localPosition = new Vector3(XPosition, -ZPosition, 0);
    //        gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
    //        gameObject.transform.localScale = new Vector3(1, 1, 1);
    //        ParticlePrefabObject mObject = gameObject.AddComponent<ParticlePrefabObject>();
    //        mObject.LoadParticle(TempModelJson);

    //        if (X != WH)
    //        {
    //            XPosition += 10000;
    //            X++;
    //        }
    //        else
    //        {
    //            XPosition = 0;
    //            X = 0;
    //            ZPosition += 10000;
    //        }
    //    }
    //}
    #endregion

    #region Save Data
    public void SaveData(string Path)
    {
        //SaveMeshCache(Path + "\\Models");
        SaveMaterials(Path + "\\Materials.json");
        //SaveParticlePrefabs(Path + "\\ParticlePrefabs.json");
        //SavePrefabs(Path + "\\Prefabs.json");
    }

    public void SaveMeshCache(string Path)
    {
        for (int i = 0; i < MeshCache.Count; i++)
        {
            string FileName = MeshCache[i].Name;

            if(!FileName.EndsWith(".obj"))
            {
                FileName += ".obj";
            }

            ObjExporter.ObjSave(Path + "\\" + FileName, MeshCache[i].mesh);
        }
    }

    public void SaveMaterials(string path)
    {
        MaterialsJsonHandler materialJsonHandler = new MaterialsJsonHandler();
        materialJsonHandler.Materials = new List<MaterialsJsonHandler.MaterialJson>();

        var TempMaterial = GetMaterialList();

        for (int i = 0; i < TempMaterial.Length; i++)
        {
            materialJsonHandler.Materials.Add(TempMaterial[i].GenerateMaterial());
        }
        materialJsonHandler.CreateJson(path);
    }

    //public void SaveParticlePrefabs(string path)
    //{
    //    ParticleModelJsonHandler particleModelJsonHandler = new ParticleModelJsonHandler();
    //    particleModelJsonHandler.ParticlePrefabs = new List<ParticleModelJsonHandler.ParticleModelJson>();

    //    var TempList = GetParticlePrefabsList();
    //    for (int i = 0; i < TempList.Length; i++)
    //    {
    //        particleModelJsonHandler.ParticlePrefabs.Add(TempList[i].GenerateParticle());
    //    }

    //    particleModelJsonHandler.CreateJson(path);
    //}

    //public void SavePrefabs(string path)
    //{
    //    PrefabJsonHandler prefabJsonHandler = new PrefabJsonHandler();
    //    prefabJsonHandler.Prefabs = new List<PrefabJson>();

    //    var TempList = GetPrefabList();

    //    for (int i = 0; i < TempList.Length; i++)
    //    {
    //        prefabJsonHandler.Prefabs.Add(TempList[i].GeneratePrefabs());
    //    }

    //    prefabJsonHandler.CreateJson(path);
    //}

    #endregion

    public OGMaterialObject[] GetMaterialList()
    {
        return MaterialHolder.transform.GetComponentsInChildren<OGMaterialObject>(true);
    }

    //public ParticlePrefabObject[] GetParticlePrefabsList()
    //{
    //    return ParticlePrefabHolder.transform.GetComponentsInChildren<ParticlePrefabObject>(true);
    //}

    //public PrefabObject[] GetPrefabList()
    //{
    //    return PrefabsHolder.transform.GetComponentsInChildren<PrefabObject>(true);
    //}

    public TrickyMaterialObject GetMaterialObject(int A)
    {
        TrickyMaterialObject[] TempObject = MaterialHolder.transform.GetComponentsInChildren<TrickyMaterialObject>(true); 

        return TempObject[A];
    }

    public TrickyPrefabObject GetPrefabObject(int A)
    {
        TrickyPrefabObject[] TempObject = PrefabsHolder.transform.GetComponentsInChildren<TrickyPrefabObject>(true);
        return TempObject[A];
    }

    public Mesh GetMesh(string MeshPath)
    {
        Mesh mesh = null;

        for (int i = 0; i < MeshCache.Count; i++)
        {
            if(MeshCache[i].Name==MeshPath)
            {
                mesh = MeshCache[i].mesh;
            }
        }

        if(mesh==null)
        {
            mesh = (Mesh)AssetDatabase.LoadAssetAtPath("Assets\\IceSaw\\Mesh\\tinker.obj", typeof(Mesh));
        }

        return mesh;

    }

    public Mesh GetColMesh(string MeshPath)
    {
        Mesh mesh = null;

        for (int i = 0; i < CollisionMeshCahce.Count; i++)
        {
            if (CollisionMeshCahce[i].Name == MeshPath)
            {
                mesh = CollisionMeshCahce[i].mesh;
            }
        }

        if (mesh == null)
        {
            mesh = (Mesh)AssetDatabase.LoadAssetAtPath("Assets\\IceSaw\\Mesh\\tinker.obj", typeof(Mesh));
        }

        return mesh;

    }
    [ContextMenu("Reload Collision")]
    public void ReloadCollision()
    {
        LoadCollisionMeshCache(TrickyLevelManager.Instance.LoadPath + "\\Collision");

        //Reload Instances
        var TempInstanceList = TrickyWorldManager.Instance.GetInstanceList();

        for (int i = 0; i < TempInstanceList.Length; i++)
        {
            TempInstanceList[i].LoadCollisionModels();
        }
    }

    [ContextMenu("Reload Models")]
    public void ReloadModels()
    {
        LoadMeshCache(TrickyLevelManager.Instance.LoadPath + "\\Models");

        //Reload Prefabs
        var TempPrefabs = TrickyPrefabManager.Instance.GetPrefabList();

        for (int i = 0; i < TempPrefabs.Length; i++)
        {
            TempPrefabs[i].ForceReloadMeshMat();
        }

        //Reload Instances
        var TempInstanceList = TrickyWorldManager.Instance.GetInstanceList();

        for (int i = 0; i < TempInstanceList.Length; i++)
        {
            TempInstanceList[i].LoadPrefabs();
        }
    }

    [ContextMenu("Regen Prefab Square")]
    public void PrefabSquare()
    {
        float XPosition = 0;
        float ZPosition = 0;
        int X = 0;

        var PrefabJson = TrickyPrefabManager.Instance.GetPrefabList();
        int WH = (int)Mathf.Sqrt(PrefabJson.Length);
        for (int i = 0; i < PrefabJson.Length; i++)
        {
            var TempModelJson = PrefabJson[i].gameObject;
            TempModelJson.transform.localPosition = new Vector3(XPosition, -ZPosition, 0);
            TempModelJson.transform.localEulerAngles = new Vector3(0, 0, 0);
            TempModelJson.transform.localScale = new Vector3(1, 1, 1);

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

    [ContextMenu("Regen Material Square")]
    public void MatSquare()
    {
        float XPosition = 0;
        float ZPosition = 0;
        int X = 0;

        var PrefabJson = TrickyPrefabManager.Instance.GetMaterialList();
        int WH = (int)Mathf.Sqrt(PrefabJson.Length);
        for (int i = 0; i < PrefabJson.Length; i++)
        {
            var TempModelJson = PrefabJson[i].gameObject;
            TempModelJson.transform.localPosition = new Vector3(XPosition, -ZPosition, 0);
            TempModelJson.transform.localEulerAngles = new Vector3(0, 0, 0);
            TempModelJson.transform.localScale = new Vector3(1, 1, 1);

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

    [System.Serializable]
    public struct MeshData
    {
        public string Name;
        public Mesh mesh;
    }
}
