using SSXMultiTool.JsonFiles.SSX3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityGLTF.Loader;

[ExecuteInEditMode]
public class SSX3WorldManager : MonoBehaviour
{
    public static SSX3WorldManager Instance;
    [HideInInspector]
    public string LoadPath;
    [HideInInspector]
    public bool EditMode;
    [HideInInspector]
    public bool PathEventMode;
    [HideInInspector]
    public bool ShowInstanceModels = true;
    [HideInInspector]
    public bool ShowCollisionModels = true;
    [HideInInspector]
    public DataManager dataManager;

    public List<TrickyLevelManager.TextureData> texture2ds = new List<TrickyLevelManager.TextureData>();
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

    public GameObject WorldManagerHolder;
    public List<GameObject> Levels = new List<GameObject>();

    // Start is called before the first frame update
    public void Awake()
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

    public void LoadData(string path)
    {
        LoadPath = path;

        LoadTextures();
        ReloadLightmaps();

        Error = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets\\IceSaw\\Textures\\Error.png", typeof(Texture2D));
        Spline = CreateLineMaterial("Assets\\IceSaw\\Textures\\Spline.png");
        AIPath = CreateLineMaterial("Assets\\IceSaw\\Textures\\AIPath.png");
        RaceLine = CreateLineMaterial("Assets\\IceSaw\\Textures\\RacePath.png");

        WorldManagerHolder = new GameObject("Levels");
        WorldManagerHolder.transform.parent = gameObject.transform;
        WorldManagerHolder.transform.transform.localScale = new Vector3(1, 1, 1);
        WorldManagerHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
        WorldManagerHolder.transform.localPosition = new Vector3(0, 0, 0);

        string[] Paths = Directory.GetDirectories(LoadPath + "\\Levels\\", "*", SearchOption.TopDirectoryOnly);

        for (int i = 0; i < 3; i++)
        {
            var TempLevel = new GameObject(Path.GetFileName(Paths[i]));
            TempLevel.transform.parent = WorldManagerHolder.transform;
            TempLevel.transform.localScale = Vector3.one;
            TempLevel.transform.localEulerAngles = Vector3.zero;

            var Level = TempLevel.AddComponent<SSX3LevelManager>();
            Level.LoadLevel(Paths[i]);
        }
    }

    public void LoadTextures()
    {
        string TextureLoadPath = LoadPath + "\\Textures";

        string[] Files = Directory.GetFiles(TextureLoadPath, "*.png", SearchOption.AllDirectories);
        texture2ds = new List<TrickyLevelManager.TextureData>();
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
                var NewTexture = new TrickyLevelManager.TextureData();
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
        dataManager.RefreshObjectList();

        //Reload Patches
        var TempPatches = dataManager.trickyPatchObjects.ToArray();

        for (int i = 0; i < TempPatches.Length; i++)
        {
            TempPatches[i].UpdateTexture();
        }

        //Reload Materials
        var TempMaterials = dataManager.trickyMaterialObjects.ToArray();

        for (int i = 0; i < TempMaterials.Length; i++)
        {
            TempMaterials[i].GenerateMaterialSphere();
        }

        //Reload Prefabs
        var TempPrefabs = dataManager.trickyPrefabObjects.ToArray();

        for (int i = 0; i < TempPrefabs.Length; i++)
        {
            TempPrefabs[i].ForceReloadMeshMat();
        }

        //Reload Instances
        var TempInstanceList = dataManager.trickyInstances.ToArray();

        for (int i = 0; i < TempInstanceList.Length; i++)
        {
            TempInstanceList[i].LoadPrefabs();
        }

        //Reload Skybox Materials
        var TempSkyboxMaterials = dataManager.trickySkyboxMaterialObjects.ToArray();

        for (int i = 0; i < TempSkyboxMaterials.Length; i++)
        {
            TempSkyboxMaterials[i].GenerateMaterialSphere();
        }

        //Reload Prefabs
        var TempSkyboxPrefabs = dataManager.trickySkyboxPrefabObjects.ToArray();

        for (int i = 0; i < TempSkyboxPrefabs.Length; i++)
        {
            TempSkyboxPrefabs[i].ForceReloadMeshMat();
        }
    }

    [ContextMenu("Reload Lightmap")]
    public void RefreshLightmap()
    {
        ReloadLightmaps();

        dataManager.RefreshObjectList();

        var TempList = dataManager.trickyPatchObjects.ToArray();

        for (int i = 0; i < TempList.Length; i++)
        {
            TempList[i].UpdateTexture();
        }
    }

    public SSX3PatchObject[] GetPatchList()
    {
        return WorldManagerHolder.GetComponentsInChildren<SSX3PatchObject>(true);
    }
    public Material CreateLineMaterial(string Path)
    {
        Material material = new Material(Shader.Find("Unlit/Texture"));

        material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(Path, typeof(Texture2D));

        return material;
    }

    [ContextMenu("Fix Script Links")]
    public void FixScriptLinks()
    {
        Awake();
        Error = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets\\IceSaw\\Textures\\Error.png", typeof(Texture2D));
        Spline = CreateLineMaterial("Assets\\IceSaw\\Textures\\Spline.png");
        AIPath = CreateLineMaterial("Assets\\IceSaw\\Textures\\AIPath.png");
        RaceLine = CreateLineMaterial("Assets\\IceSaw\\Textures\\RacePath.png");

        dataManager = new DataManager();
        dataManager.RefreshObjectList();
    }

    //[System.Serializable]
    //public struct TextureData
    //{
    //    public string Name;
    //    public Texture2D Texture;
    //}
}
