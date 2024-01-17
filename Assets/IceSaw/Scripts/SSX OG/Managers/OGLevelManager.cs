using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class OGLevelManager : MonoBehaviour
{
    public static OGLevelManager Instance;
    public string LoadPath;

    public List<TrickyLevelManager.TextureData> texture2Ds = new List<TrickyLevelManager.TextureData>();
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
        var TempPrefab = PrefabManagerHolder.AddComponent<OGPrefabManager>();
        TempPrefab.runInEditMode = true;
        TempPrefab.GenerateEmptyObjects();

        //Generate World Manager
        WorldManagerHolder = new GameObject("OG World Manager");
        WorldManagerHolder.transform.parent = this.transform;
        WorldManagerHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        WorldManagerHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
        WorldManagerHolder.transform.localPosition = new Vector3(0, 0, 0);
        var TempWorld = WorldManagerHolder.AddComponent<OGWorldManager>();
        TempWorld.runInEditMode = true;
        TempWorld.GenerateEmptyObjects();

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
        var TempPathFile = PathFileManager.AddComponent<OGPathFileManager>();
        TempPathFile.runInEditMode = true;
        TempPathFile.GenerateEmptyObjects();

        Error = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets\\IceSaw\\Textures\\Error.png", typeof(Texture2D));
        Spline = CreateLineMaterial("Assets\\IceSaw\\Textures\\Spline.png");
        AIPath = CreateLineMaterial("Assets\\IceSaw\\Textures\\AIPath.png");
        RaceLine = CreateLineMaterial("Assets\\IceSaw\\Textures\\RacePath.png");
    }

    public void LoadData(string Path)
    {
        CreateEmptyObjects();
        LoadPath = SSXProjectWindow.CurrentPath;

        LoadTextures();
        ReloadLightmaps();

        PrefabManagerHolder.GetComponent<OGPrefabManager>().LoadData(Path);
        WorldManagerHolder.GetComponent<OGWorldManager>().LoadData(Path);
        //LogicManager.GetComponent<LogicManager>().LoadData(Path);
        //SkyboxManagerHolder.GetComponent<SkyboxManager>().LoadData(Path);
        PathFileManager.GetComponent<OGPathFileManager>().LoadData(Path);
    }

    public void LoadTextures()
    {
        string TextureLoadPath = LoadPath + "\\Textures";

        string[] Files = Directory.GetFiles(TextureLoadPath, "*.png", SearchOption.AllDirectories);
        texture2Ds = new List<TrickyLevelManager.TextureData>();
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
                var NewTexture = new TrickyLevelManager.TextureData();
                NewTexture.Name = NewImage.name;
                NewTexture.Texture = NewImage;
                texture2Ds.Add(NewTexture);
            }
        }
    }

    public void ReloadLightmaps()
    {
        string[] Files = Directory.GetFiles(LoadPath + "\\Lightmaps");
        lightmaps = new List<Texture2D>();
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
                    NewImage.filterMode = FilterMode.Point;
                }
                Texture2D correctedTexture = new Texture2D(NewImage.width, NewImage.height);
                for (int x = 0; x < NewImage.width; x++)
                {
                    for (int y = 0; y < NewImage.height; y++)
                    {
                        correctedTexture.SetPixel(x, y, NewImage.GetPixel(x, NewImage.height - 1 - y));
                    }
                }
                correctedTexture.name = Files[i].Substring((LoadPath + "\\Lightmaps").Length + 1);
                correctedTexture.Apply();
                lightmaps.Add(correctedTexture);
            }
        }
    }

    public Texture2D GrabLightmapTexture(Vector4 lightmapPoint, int ID)
    {
        int XCord = (int)(lightmapPoint.x * lightmaps[ID].width);
        int YCord = (int)(lightmapPoint.y * lightmaps[ID].height);
        int Width = (int)(lightmapPoint.z * lightmaps[ID].width);
        int Height = (int)(lightmapPoint.w * lightmaps[ID].height);
        Texture2D LightmapGrab = new Texture2D(Width, Height);
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var Colour = lightmaps[ID].GetPixel(XCord + x, YCord + y);
                LightmapGrab.SetPixel(x, y, Colour);
            }
        }
        LightmapGrab.Apply();

        Texture2D TenByTen = new Texture2D(Width + 2, Height + 2);

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var Colour = LightmapGrab.GetPixel(x, y);
                TenByTen.SetPixel(x + 1, y + 1, Colour);
            }
        }

        for (int i = 0; i < Width; i++)
        {
            var Colour = LightmapGrab.GetPixel(0, i);
            TenByTen.SetPixel(0, i + 1, Colour);

            Colour = LightmapGrab.GetPixel(i, Height - 1);
            TenByTen.SetPixel(i + 1, Height + 1, Colour);
        }

        for (int i = 0; i < Height; i++)
        {
            var Colour = LightmapGrab.GetPixel(i, 0);
            TenByTen.SetPixel(i + 1, 0, Colour);

            Colour = LightmapGrab.GetPixel(Width - 1, i);
            TenByTen.SetPixel(Width + 1, i + 1, Colour);
        }

        //Probably better to replace with averages of what the corners should look like
        var Colour1 = LightmapGrab.GetPixel(0, 0);
        TenByTen.SetPixel(0, 0, Colour1);

        Colour1 = LightmapGrab.GetPixel(Width - 1, 0);
        TenByTen.SetPixel(Width + 1, 0, Colour1);

        Colour1 = LightmapGrab.GetPixel(0, Height - 1);
        TenByTen.SetPixel(0, Height + 1, Colour1);

        Colour1 = LightmapGrab.GetPixel(Width - 1, Height - 1);
        TenByTen.SetPixel(Width + 1, Height + 1, Colour1);

        TenByTen.Apply();
        //TenByTen.filterMode = FilterMode.Bilinear;
        return TenByTen;
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

        if (gameObject.GetComponentInChildren<OGPathFileManager>() != null)
        {
            PathFileManager = gameObject.GetComponentInChildren<OGPathFileManager>().gameObject;
            PathFileManager.GetComponent<OGPathFileManager>().Awake();
        }
    }

    public Material CreateLineMaterial(string Path)
    {
        Material material = new Material(Shader.Find("Unlit/Texture"));

        material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(Path, typeof(Texture2D));

        return material;
    }
}
