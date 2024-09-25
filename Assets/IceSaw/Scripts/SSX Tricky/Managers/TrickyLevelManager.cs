using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class TrickyLevelManager : MonoBehaviour
{
    public static TrickyLevelManager Instance;
    [HideInInspector]
    public string LoadPath;
    [HideInInspector]
    public bool EditMode;
    [HideInInspector]
    public bool PathEventMode;

    //[OnChangedCall("ForceTextureUpdate")]
    public List<TextureData> texture2ds = new List<TextureData>();
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

    // Start is called before the first frame update
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            FixScriptLinks();
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
        PrefabManagerHolder = new GameObject("Tricky Prefab Manager");
        var TempPrefab = PrefabManagerHolder.AddComponent<TrickyPrefabManager>();
        TempPrefab.runInEditMode = true;
        TempPrefab.GenerateEmptyObjects();
        TempPrefab.transform.parent = this.transform;
        TempPrefab.transform.transform.localScale = new Vector3(1, 1, 1);
        TempPrefab.transform.localEulerAngles = new Vector3(0, 0, 0);
        TempPrefab.transform.localPosition = new Vector3(0, 0, 100000);

        //Generate World Manager
        WorldManagerHolder = new GameObject("Tricky World Manager");
        WorldManagerHolder.transform.parent = this.transform;
        WorldManagerHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        WorldManagerHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
        WorldManagerHolder.transform.localPosition = new Vector3(0, 0, 0);
        var TempWorld = WorldManagerHolder.AddComponent<TrickyWorldManager>();
        TempWorld.runInEditMode = true;
        TempWorld.GenerateEmptyObjects();

        //Generate Skybox Manager
        SkyboxManagerHolder = new GameObject("Tricky Skybox Manager");
        SkyboxManagerHolder.transform.parent = this.transform;
        SkyboxManagerHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        SkyboxManagerHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
        SkyboxManagerHolder.transform.localPosition = new Vector3(0, 0, 100000*2);
        var TempSkybox = SkyboxManagerHolder.AddComponent<SkyboxManager>();
        TempSkybox.runInEditMode = true;
        TempSkybox.GenerateEmptyObjects();

        //Generate Logic Manager
        LogicManager = new GameObject("Tricky Logic Manager");
        LogicManager.transform.parent = this.transform;
        LogicManager.transform.transform.localScale = new Vector3(1, 1, 1);
        LogicManager.transform.localEulerAngles = new Vector3(0, 0, 0);
        var TempLogic = LogicManager.AddComponent<TrickyLogicManager>();
        TempLogic.runInEditMode = true;
        TempLogic.GenerateEmptyObjects();

        //Generate Path File Manager
        PathFileManager = new GameObject("Tricky Path Manager");
        PathFileManager.transform.parent = this.transform;
        PathFileManager.transform.transform.localScale = new Vector3(1, 1, 1);
        PathFileManager.transform.localEulerAngles = new Vector3(0, 0, 0);
        var TempPathFile = PathFileManager.AddComponent<TrickyPathFileManager>();
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

        PrefabManagerHolder.GetComponent<TrickyPrefabManager>().LoadData(Path);
        WorldManagerHolder.GetComponent<TrickyWorldManager>().LoadData(Path);
        LogicManager.GetComponent<TrickyLogicManager>().LoadData(Path);
        SkyboxManagerHolder.GetComponent<SkyboxManager>().LoadData(Path);
        PathFileManager.GetComponent<TrickyPathFileManager>().LoadData(Path);

        PostLoad();
    }

    public void PostLoad()
    {
        PrefabManagerHolder.GetComponent<TrickyPrefabManager>().PostLoad();
        WorldManagerHolder.GetComponent<TrickyWorldManager>().PostLoad();
        LogicManager.GetComponent<TrickyLogicManager>().PostLoad();
        SkyboxManagerHolder.GetComponent<SkyboxManager>().PostLoad();
        //PathFileManager.GetComponent<TrickyPathFileManager>().LoadData(Path);
    }

    public void SaveData(string Path)
    {
        PrefabManagerHolder.GetComponent<TrickyPrefabManager>().SaveData(Path);
        WorldManagerHolder.GetComponent<TrickyWorldManager>().SaveData(Path);
        LogicManager.GetComponent<TrickyLogicManager>().SaveData(Path);
        SkyboxManagerHolder.GetComponent<SkyboxManager>().SaveData(Path);
        PathFileManager.GetComponent<TrickyPathFileManager>().SaveData(Path);

        SaveTextures();
        SaveLightmap();
    }

    public void LoadTextures()
    {
        string TextureLoadPath = LoadPath + "\\Textures";

        string[] Files = Directory.GetFiles(TextureLoadPath, "*.png", SearchOption.AllDirectories);
        texture2ds = new List<TextureData>();
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
                texture2ds.Add(NewTexture);
            }
        }
    }

    public void SaveTextures()
    {
        for (int i = 0; i < texture2ds.Count; i++)
        {
            var TempTexture = texture2ds[i];
            MemoryStream stream = new MemoryStream();
            stream.Write(TempTexture.Texture.EncodeToPNG());

            string FileName = TempTexture.Name;

            if(!FileName.ToLower().EndsWith(".png"))
            {
                FileName = FileName + ".png";
            }

            if(File.Exists(FileName))
            {
                File.Delete(FileName);
            }

            var file = File.Create(LoadPath + "\\Textures\\" + FileName);
            stream.Position = 0;
            stream.CopyTo(file);
            file.Close();
            stream.Dispose();
        }
    }

    public void SaveLightmap()
    {
        for (int i = 0; i < lightmaps.Count; i++)
        {
            var TempTexture = lightmaps[i];

            Texture2D correctedTexture = new Texture2D(TempTexture.width, TempTexture.height);
            for (int x = 0; x < TempTexture.width; x++)
            {
                for (int y = 0; y < TempTexture.height; y++)
                {
                    correctedTexture.SetPixel(x, y, TempTexture.GetPixel(x, TempTexture.height - 1 - y));
                }
            }
            correctedTexture.Apply();

            MemoryStream stream = new MemoryStream();
            stream.Write(correctedTexture.EncodeToPNG());

            string FileName = TempTexture.name;

            if (!FileName.ToLower().EndsWith(".png"))
            {
                FileName = FileName + ".png";
            }

            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }

            var file = File.Create(LoadPath + "\\Lightmaps\\" + FileName);
            stream.Position = 0;
            stream.CopyTo(file);
            file.Close();
            stream.Dispose();
        }
    }

    public void ReloadTextures()
    {
        string TextureLoadPath = LoadPath + "\\Textures";

        string[] Files = Directory.GetFiles(TextureLoadPath, "*.png", SearchOption.AllDirectories);
        for (int i = 0; i < Files.Length; i++)
        {
            var FileName = Files[i].TrimStart(TextureLoadPath.ToCharArray());

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
            }

            bool TestIfExists = false;
            for (int a = 0; a < texture2ds.Count; a++)
            {
                if (texture2ds[i].Name==FileName)
                {
                    TestIfExists = true;
                    var Temp = texture2ds[i];
                    Temp.Texture = NewImage;
                    texture2ds[i] = Temp;
                }    
            }

            if(!TestIfExists)
            {
                var NewTexture = new TextureData();
                NewTexture.Name = NewImage.name;
                NewTexture.Texture = NewImage;
                texture2ds.Add(NewTexture);
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

    [ContextMenu("Reload Textures")]
    public void RefreshTextures()
    {
        ReloadTextures();
        ForceTextureUpdate();
    }

    [ContextMenu("Force Texture Update")]
    public void ForceTextureUpdate()
    {
        //Reload Patches
        var TempPatches = TrickyWorldManager.Instance.GetPatchList();

        for (int i = 0; i < TempPatches.Length; i++)
        {
            TempPatches[i].UpdateTexture();
        }

        //Reload Materials
        var TempMaterials = TrickyPrefabManager.Instance.GetMaterialList();

        for (int i = 0; i < TempMaterials.Length; i++)
        {
            TempMaterials[i].GenerateMaterialSphere();
        }

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

    [ContextMenu("Reload Lightmap")]
    public void RefreshLightmap()
    {
        ReloadLightmaps();

        var TempList = TrickyWorldManager.Instance.GetPatchList();

        for (int i = 0; i < TempList.Length; i++)
        {
            TempList[i].UpdateTexture();
        }
    }

    [ContextMenu("Fix Script Links")]
    public void FixScriptLinks()
    {
        Awake();

        if (gameObject.GetComponentInChildren<TrickyLogicManager>() != null)
        {
            LogicManager = gameObject.GetComponentInChildren<TrickyLogicManager>().gameObject;
            LogicManager.GetComponent<TrickyLogicManager>().Awake();
        }

        if (gameObject.GetComponentInChildren<TrickyPrefabManager>() != null)
        {
            PrefabManagerHolder = gameObject.GetComponentInChildren<TrickyPrefabManager>().gameObject;
            PrefabManagerHolder.GetComponent<TrickyPrefabManager>().Awake();
        }

        if (gameObject.GetComponentInChildren<TrickyWorldManager>())
        {
            WorldManagerHolder = gameObject.GetComponentInChildren<TrickyWorldManager>().gameObject;
            WorldManagerHolder.GetComponent<TrickyWorldManager>().Awake();
        }

        if (gameObject.GetComponentInChildren<SkyboxManager>() != null)
        {
            SkyboxManagerHolder = gameObject.GetComponentInChildren<SkyboxManager>().gameObject;
            SkyboxManagerHolder.GetComponent<SkyboxManager>().Awake();
        }

        if (gameObject.GetComponentInChildren<TrickyPathFileManager>() != null)
        {
            PathFileManager = gameObject.GetComponentInChildren<TrickyPathFileManager>().gameObject;
            PathFileManager.GetComponent<TrickyPathFileManager>().Awake();
        }
    }

    public Material CreateLineMaterial(string Path)
    {
        Material material = new Material(Shader.Find("Unlit/Texture"));

        material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(Path, typeof(Texture2D));

        return material;
    }

    [System.Serializable]
    public struct TextureData
    {
        public string Name;
        public Texture2D Texture;
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }
    public Rect windowRect = new Rect(20, 20, 120, 70);
    private void OnSceneGUI(SceneView sceneView)
    {
        Handles.BeginGUI();
        windowRect = GUI.Window(0, windowRect, DoMyWindow, "IceSaw Tools");
        Handles.EndGUI();
    }

    void DoMyWindow(int windowID)
    {
        // Make a very long rect that is 20 pixels tall.
        // This will make the window be resizable by the top
        // title bar - no matter how wide it gets.
        GUI.DragWindow(new Rect(0, 0, 10000, 20));
        if (EditMode)
        {
            if (GUILayout.Button("Edit Mode"))
                EditMode = false;
            //if (!PathEventMode)
            //{
            //    if (GUILayout.Button("Path Mode"))
            //        PathEventMode = true;
            //}
            //else
            //{
            //    if (GUILayout.Button("Event Mode"))
            //        PathEventMode = false;
            //}
        }
        else
        {
            if (GUILayout.Button("Object Mode"))
                EditMode = true;
        }
    }

}


[CustomEditor(typeof(TrickyLevelManager))]
public class TrickyLevelManagerInspector : Editor
{
    //public override void OnInspectorGUI()
    //{
    //    DrawDefaultInspector();

    //    //Component.hideFlags = HideFlags.HideInInspector;

    //    if(EditorGUILayout.LinkButton("Refresh Textures"))
    //    {
    //        var Temp = (typeof(TrickyLevelManager))serializedObject.targetObject;
    //    }
    //}

    public VisualTreeAsset m_InspectorXML;

    public override VisualElement CreateInspectorGUI()
    {
        m_InspectorXML = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets\\IceSaw\\Scripts\\SSX Tricky\\Managers\\Inspectors\\LevelManager.uxml");

        // Create a new VisualElement to be the root of our inspector UI
        VisualElement myInspector = new VisualElement();
        m_InspectorXML.CloneTree(myInspector);

        VisualElement inspectorGroup = myInspector.Q("Default_Inspector");
        InspectorElement.FillDefaultInspector(inspectorGroup, serializedObject, this);

        VisualElement ButtonGroup = myInspector.Q("ButtonGroup1");

        VisualElement ReloadTextureButton = ButtonGroup.Q("_ReloadTextures");
        var TempTextureButton = ReloadTextureButton.Query<Button>();
        TempTextureButton.First().RegisterCallback<ClickEvent>(ReloadTextures);

        VisualElement ReloadLightButton = ButtonGroup.Q("_RefreshLightmap");
        var TempLightButton = ReloadLightButton.Query<Button>();
        TempLightButton.First().RegisterCallback<ClickEvent>(ReloadLightmaps);

        // Return the finished inspector UI
        return myInspector;
    }

    private void ReloadTextures(ClickEvent evt)
    {
        serializedObject.targetObject.GetComponent<TrickyLevelManager>().RefreshTextures();
    }

    private void ReloadLightmaps(ClickEvent evt)
    {
        serializedObject.targetObject.GetComponent<TrickyLevelManager>().RefreshLightmap();
    }
}

