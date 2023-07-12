using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using static SSXMultiTool.JsonFiles.Tricky.PrefabJsonHandler;

public class SkyboxManager : MonoBehaviour
{
    public static SkyboxManager Instance;

    GameObject MaterialHolder;
    GameObject PrefabsHolder;
    GameObject SkyboxCamera;

    public List<Texture2D> SkyboxTextures2d = new List<Texture2D>();
    public List<Mesh> SkyboxMeshCache = new List<Mesh>();

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void GenerateEmptyObjects()
    {
        MaterialHolder = new GameObject("Materials");
        MaterialHolder.transform.parent = transform;
        MaterialHolder.transform.localScale = Vector3.one;
        MaterialHolder.transform.localEulerAngles = Vector3.zero;
        MaterialHolder.transform.localPosition = Vector3.zero;
        MaterialHolder.transform.hideFlags = HideFlags.HideInInspector;

        PrefabsHolder = new GameObject("Prefabs");
        PrefabsHolder.transform.parent = transform;
        PrefabsHolder.transform.localScale = Vector3.one;
        PrefabsHolder.transform.localEulerAngles = Vector3.zero;
        PrefabsHolder.transform.localPosition = new Vector3(0, 0, 10000);
        PrefabsHolder.transform.hideFlags = HideFlags.HideInInspector;


        SkyboxCamera = new GameObject("Skybox Camera");
        SkyboxCamera.transform.parent = transform;
        SkyboxCamera.transform.localScale = Vector3.one;
        SkyboxCamera.transform.eulerAngles = Vector3.zero;
        SkyboxCamera.transform.localPosition = new Vector3(0, 0, 10000);
        SkyboxCamera.transform.hideFlags = HideFlags.HideInInspector;

        var TempCamera = SkyboxCamera.AddComponent<Camera>();
        TempCamera.depth = -1000;
        TempCamera.clearFlags = CameraClearFlags.SolidColor;

        SkyboxCamera.AddComponent<FollowMainCamera>();

    }

    public void LoadData(string Path)
    {
        ReloadTextures(Path + "\\Skybox");
        LoadSkyMeshCache(Path + "\\Skybox\\Models");
        if (File.Exists(Path + "\\Skybox\\Materials.json"))
        {
            LoadMaterials(Path + "\\Skybox\\Materials.json");
            LoadPrefabs(Path + "\\Skybox\\Prefabs.json");
        }
    }

    public void LoadSkyMeshCache(string path)
    {
        SkyboxMeshCache = new List<Mesh>();

        string[] Files = Directory.GetFiles(path, "*.obj", SearchOption.AllDirectories);
        for (int i = 0; i < Files.Length; i++)
        {
            Mesh TempMesh = ObjImporter.ObjLoad(Files[i]);
            TempMesh.name = Files[i].TrimStart(path.ToCharArray());
            SkyboxMeshCache.Add(TempMesh);
        }
    }

    public void ReloadTextures(string path)
    {
        string TextureLoadPath = path + "\\Textures";

        string[] Files = Directory.GetFiles(TextureLoadPath, "*.png", SearchOption.AllDirectories);
        SkyboxTextures2d = new List<Texture2D>();
        for (int i = 0; i < Files.Length; i++)
        {
            Texture2D NewImage = new Texture2D(1, 1);
            if (Files[i].ToLower().Contains(".png"))
            {
                using (Stream stream = File.Open(Files[i], FileMode.Open))
                {
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                    NewImage.LoadImage(bytes);
                    NewImage.name = Files[i].TrimStart(TextureLoadPath.ToCharArray());
                    //NewImage.wrapMode = TextureWrapMode.MirrorOnce;
                }
                SkyboxTextures2d.Add(NewImage);
            }
        }
    }

    public void LoadMaterials(string Path)
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
            gameObject.transform.hideFlags = HideFlags.HideInInspector;
            gameObject.transform.parent = MaterialHolder.transform;
            gameObject.transform.localPosition = new Vector3(XPosition, -ZPosition, 0);
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            gameObject.transform.localScale = new Vector3(10, 10, 10);
            MaterialObject materialObject = gameObject.AddComponent<MaterialObject>();
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
            GameObject gameObject = new GameObject("Skybox Prefab " + i);
            gameObject.transform.hideFlags = HideFlags.HideInInspector;
            gameObject.transform.parent = PrefabsHolder.transform;
            gameObject.transform.localPosition = new Vector3(XPosition, -ZPosition, 0);
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            PrefabObject mObject = gameObject.AddComponent<PrefabObject>();
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

    public void SaveData(string path)
    {
        SaveSkyboxMaterials(path + "\\Skybox\\Materials.json");
        SavePrefabs(path + "\\Skybox\\Prefabs.json");
    }

    public void SaveSkyboxMaterials(string path)
    {
        MaterialJsonHandler materialJsonHandler = new MaterialJsonHandler();
        materialJsonHandler.Materials = new List<MaterialJsonHandler.MaterialsJson>();

        var TempMaterial = GetMaterialList();

        for (int i = 0; i < TempMaterial.Length; i++)
        {
            materialJsonHandler.Materials.Add(TempMaterial[i].GenerateMaterial());
        }
        materialJsonHandler.CreateJson(path);
    }

    public void SavePrefabs(string path)
    {
        PrefabJsonHandler prefabJsonHandler = new PrefabJsonHandler();
        prefabJsonHandler.Prefabs = new List<PrefabJson>();

        var TempList = GetPrefabsList();

        for (int i = 0; i < TempList.Length; i++)
        {
            prefabJsonHandler.Prefabs.Add(TempList[i].GeneratePrefabs(true));
        }

        prefabJsonHandler.CreateJson(path);
    }

    public PrefabObject[] GetPrefabsList()
    {
        return PrefabsHolder.transform.GetComponentsInChildren<PrefabObject>(true);
    }

    public MaterialObject[] GetMaterialList()
    {
        return MaterialHolder.transform.GetComponentsInChildren<MaterialObject>(true);
    }

    public MaterialObject GetMaterialObject(int A)
    {
        MaterialObject[] TempObject = MaterialHolder.transform.GetComponentsInChildren<MaterialObject>(true);

        return TempObject[A];
    }

    public Mesh GetMesh(string MeshPath)
    {
        Mesh mesh = null;

        for (int i = 0; i < SkyboxMeshCache.Count; i++)
        {
            if (SkyboxMeshCache[i].name == MeshPath)
            {
                mesh = SkyboxMeshCache[i];
            }
        }

        if (mesh == null)
        {
            //Set Error Mesh
        }

        return mesh;

    }
}
