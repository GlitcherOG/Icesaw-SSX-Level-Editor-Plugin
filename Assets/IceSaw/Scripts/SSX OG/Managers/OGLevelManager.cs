using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OGLevelManager : MonoBehaviour
{
    public static OGLevelManager Instance;
    public string LoadPath;

    public List<TextureData> texture2Ds = new List<TextureData>();
    public List<Texture2D> lightmaps = new List<Texture2D>();

    public bool LightmapMode;

    [HideInInspector]
    public Texture2D Error;
    [HideInInspector]
    public Material Spline;
    [HideInInspector]
    public Material AIPath;
    [HideInInspector]
    public Material RaceLine;

    GameObject WorldManagerHolder;
    GameObject SkyboxManagerHolder;
    GameObject PrefabManagerHolder;
    GameObject LogicManager;
    GameObject PathFileManager;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            FixScriptLinks();
        }
        else if (Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
    }

    public void CreateEmptyObjects()
    {
        transform.hideFlags = HideFlags.HideInInspector;

        //Generate Prefab Manager
        PrefabManagerHolder = new GameObject("OG Prefab Manager");
        PrefabManagerHolder.transform.parent = this.transform;
        PrefabManagerHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        PrefabManagerHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
        PrefabManagerHolder.transform.localPosition = new Vector3(0, 0, 100000);
        //var TempPrefab = PrefabManagerHolder.AddComponent<PrefabManager>();
        //TempPrefab.runInEditMode = true;
        //TempPrefab.GenerateEmptyObjects();

        //Generate World Manager
        WorldManagerHolder = new GameObject("OG World Manager");
        WorldManagerHolder.transform.parent = this.transform;
        WorldManagerHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        WorldManagerHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
        WorldManagerHolder.transform.localPosition = new Vector3(0, 0, 0);
        //var TempWorld = WorldManagerHolder.AddComponent<WorldManager>();
        //TempWorld.runInEditMode = true;
        //TempWorld.GenerateEmptyObjects();

        //Generate Skybox Manager
        SkyboxManagerHolder = new GameObject("OG Skybox Manager");
        SkyboxManagerHolder.transform.parent = this.transform;
        SkyboxManagerHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        SkyboxManagerHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
        SkyboxManagerHolder.transform.localPosition = new Vector3(0, 0, 100000 * 2);
        //var TempSkybox = SkyboxManagerHolder.AddComponent<SkyboxManager>();
        //TempSkybox.runInEditMode = true;
        //TempSkybox.GenerateEmptyObjects();

        //Generate Logic Manager
        LogicManager = new GameObject("OG Logic Manager");
        LogicManager.transform.parent = this.transform;
        LogicManager.transform.transform.localScale = new Vector3(1, 1, 1);
        LogicManager.transform.localEulerAngles = new Vector3(0, 0, 0);
        //var TempLogic = LogicManager.AddComponent<LogicManager>();
        //TempLogic.runInEditMode = true;
        //TempLogic.GenerateEmptyObjects();

        //Generate Path File Manager
        PathFileManager = new GameObject("OG Path Manager");
        PathFileManager.transform.parent = this.transform;
        PathFileManager.transform.transform.localScale = new Vector3(1, 1, 1);
        PathFileManager.transform.localEulerAngles = new Vector3(0, 0, 0);
        //var TempPathFile = PathFileManager.AddComponent<PathFileManager>();
        //TempPathFile.runInEditMode = true;
        //TempPathFile.GenerateEmptyObjects();

        //Error = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets\\IceSaw\\Textures\\Error.png", typeof(Texture2D));
        //Spline = CreateLineMaterial("Assets\\IceSaw\\Textures\\Spline.png");
        //AIPath = CreateLineMaterial("Assets\\IceSaw\\Textures\\AIPath.png");
        //RaceLine = CreateLineMaterial("Assets\\IceSaw\\Textures\\RacePath.png");
    }

    public void LoadData(string Path)
    {
        CreateEmptyObjects();
        LoadPath = SSXProjectWindow.CurrentPath;

        LoadTextures();
        //ReloadLightmaps();

        //PrefabManagerHolder.GetComponent<PrefabManager>().LoadData(Path);
        //WorldManagerHolder.GetComponent<WorldManager>().LoadData(Path);
        //LogicManager.GetComponent<LogicManager>().LoadData(Path);
        //SkyboxManagerHolder.GetComponent<SkyboxManager>().LoadData(Path);
        //PathFileManager.GetComponent<PathFileManager>().LoadData(Path);
    }

    public void LoadTextures()
    {
        string TextureLoadPath = LoadPath + "\\Textures";

        string[] Files = Directory.GetFiles(TextureLoadPath, "*.png", SearchOption.AllDirectories);
        texture2Ds = new List<TextureData>();
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
                    NewImage.name = Files[i].Substring(TextureLoadPath.Length + 1);
                    //NewImage.wrapMode = TextureWrapMode.MirrorOnce;
                }
                var NewTexture = new TextureData();
                NewTexture.Name = NewImage.name;
                NewTexture.Texture = NewImage;
                texture2Ds.Add(NewTexture);
            }
        }
    }

    [ContextMenu("Fix Script Links")]
    public void FixScriptLinks()
    {
        Awake();

        //if (gameObject.GetComponentInChildren<LogicManager>() != null)
        //{
        //    LogicManager = gameObject.GetComponentInChildren<LogicManager>().gameObject;
        //    LogicManager.GetComponent<LogicManager>().Awake();
        //}

        //if (gameObject.GetComponentInChildren<PrefabManager>() != null)
        //{
        //    PrefabManagerHolder = gameObject.GetComponentInChildren<PrefabManager>().gameObject;
        //    PrefabManagerHolder.GetComponent<PrefabManager>().Awake();
        //}

        //if (gameObject.GetComponentInChildren<WorldManager>())
        //{
        //    WorldManagerHolder = gameObject.GetComponentInChildren<WorldManager>().gameObject;
        //    WorldManagerHolder.GetComponent<WorldManager>().Awake();
        //}

        //if (gameObject.GetComponentInChildren<SkyboxManager>() != null)
        //{
        //    SkyboxManagerHolder = gameObject.GetComponentInChildren<SkyboxManager>().gameObject;
        //    SkyboxManagerHolder.GetComponent<SkyboxManager>().Awake();
        //}

        //if (gameObject.GetComponentInChildren<PathFileManager>() != null)
        //{
        //    PathFileManager = gameObject.GetComponentInChildren<PathFileManager>().gameObject;
        //    PathFileManager.GetComponent<PathFileManager>().Awake();
        //}
    }

    [System.Serializable]
    public struct TextureData
    {
        public string Name;
        public Texture2D Texture;
    }
}
