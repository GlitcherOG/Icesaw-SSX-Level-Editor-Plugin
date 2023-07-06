using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public string LoadPath;
    public Texture2D Error;
    public List<Texture2D> texture2Ds = new List<Texture2D>();

    GameObject WorldManager;
    GameObject SkyboxManager;
    GameObject PrefabManager;
    GameObject LogicManager;
    GameObject PathFileManager;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
    }

    public void CreateEmptyObjects()
    {
        transform.hideFlags = HideFlags.HideInInspector;

        //Generate Prefab Manager
        PrefabManager = new GameObject("Tricky Prefab Manager");
        var TempPrefab = PrefabManager.AddComponent<PrefabManager>();
        TempPrefab.runInEditMode = true;
        TempPrefab.GenerateEmptyObjects();
        TempPrefab.transform.parent = this.transform;
        TempPrefab.transform.transform.localScale = new Vector3(1, 1, 1);
        TempPrefab.transform.localEulerAngles = new Vector3(0, 0, 0);
        TempPrefab.transform.localPosition = new Vector3(0, 0, 50000);

        //Generate World Manager
        WorldManager = new GameObject("Tricky World Manager");
        WorldManager.transform.parent = this.transform;
        WorldManager.transform.transform.localScale = new Vector3(1, 1, 1);
        WorldManager.transform.localEulerAngles = new Vector3(0, 0, 0);
        WorldManager.transform.localPosition = new Vector3(0, 0, 0);
        var TempWorld = WorldManager.AddComponent<WorldManager>();
        TempWorld.runInEditMode = true;
        TempWorld.GenerateEmptyObjects();

        //Generate Skybox Manager
        SkyboxManager = new GameObject("Tricky Skybox Manager");
        SkyboxManager.transform.parent = this.transform;
        SkyboxManager.transform.transform.localScale = new Vector3(1, 1, 1);
        SkyboxManager.transform.localEulerAngles = new Vector3(0, 0, 0);
        SkyboxManager.transform.localPosition = new Vector3(0, 0, 50000*2);
        var TempSkybox = SkyboxManager.AddComponent<SkyboxManager>();
        TempSkybox.runInEditMode = true;
        TempSkybox.GenerateEmptyObjects();

        //Generate Logic Manager
        LogicManager = new GameObject("Tricky Logic Manager");
        LogicManager.transform.parent = this.transform;
        LogicManager.transform.transform.localScale = new Vector3(1, 1, 1);
        LogicManager.transform.localEulerAngles = new Vector3(0, 0, 0);
        var TempLogic = LogicManager.AddComponent<LogicManager>();
        TempLogic.runInEditMode = true;
        TempLogic.GenerateEmptyObjects();

        //Generate Path File Manager
        PathFileManager = new GameObject("Tricky Path Manager");
        PathFileManager.transform.parent = this.transform;
        PathFileManager.transform.transform.localScale = new Vector3(1, 1, 1);
        PathFileManager.transform.localEulerAngles = new Vector3(0, 0, 0);
        var TempPathFile = PathFileManager.AddComponent<PathFileManager>();
        TempPathFile.runInEditMode = true;
        TempPathFile.GenerateEmptyObjects();
    }

    public void LoadData(string Path)
    {
        CreateEmptyObjects();
        LoadPath = TrickyProjectWindow.CurrentPath;
        Error = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets\\IceSaw\\Textures\\Error.png", typeof(Texture2D));
        ReloadTextures();

        PrefabManager.GetComponent<PrefabManager>().LoadData(Path);
        WorldManager.GetComponent<WorldManager>().LoadData(Path);
        LogicManager.GetComponent<LogicManager>().LoadData(Path);
        SkyboxManager.GetComponent<SkyboxManager>().LoadData(Path);
        PathFileManager.GetComponent<PathFileManager>().LoadData(Path);
    }

    public void SaveData(string Path)
    {
        PrefabManager.GetComponent<PrefabManager>().SaveData(Path);
        WorldManager.GetComponent<WorldManager>().SaveData(Path);
        LogicManager.GetComponent<LogicManager>().SaveData(Path);
        SkyboxManager.GetComponent<SkyboxManager>().SaveData(Path);
        PathFileManager.GetComponent<PathFileManager>().SaveData(Path);
    }

    public void ReloadTextures()
    {
        string TextureLoadPath = LoadPath + "\\Textures";

        string[] Files = Directory.GetFiles(TextureLoadPath, "*.png", SearchOption.AllDirectories);
        texture2Ds = new List<Texture2D>();
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
                texture2Ds.Add(NewImage);
            }
        }
    }

    [ContextMenu("Fix Script Links")]
    public void FixScriptLinks()
    {
        Awake();

        LogicManager = gameObject.GetComponentInChildren<LogicManager>().gameObject;
        LogicManager.GetComponent<LogicManager>().Awake();

        PrefabManager = gameObject.GetComponentInChildren<PrefabManager>().gameObject;
        PrefabManager.GetComponent<PrefabManager>().Awake();

        WorldManager = gameObject.GetComponentInChildren<WorldManager>().gameObject;
        WorldManager.GetComponent<WorldManager>().Awake();
    }
}
