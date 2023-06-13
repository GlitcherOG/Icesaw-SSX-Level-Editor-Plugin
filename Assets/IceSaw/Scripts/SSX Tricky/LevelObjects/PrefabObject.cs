using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using static SSXMultiTool.JsonFiles.Tricky.PrefabJsonHandler;

[System.Serializable]
[SelectionBase]
public class PrefabObject : MonoBehaviour
{
    public int Unknown3;
    public float AnimTime;
    public List<ObjectHeader> PrefabObjects;

    public GameObject GeneratePrefab()
    {
        GameObject MainObject = new GameObject(transform.name);
        for (int i = 0; i < PrefabObjects.Count; i++)
        {
            var TempPrefab = PrefabObjects[i];
            for (int a = 0; a < TempPrefab.MeshData.Count; a++)
            {
                GameObject ChildMesh = new GameObject(i + ", " + a);

                ChildMesh.transform.parent = MainObject.transform;
                ChildMesh.transform.localPosition = TempPrefab.Position;
                ChildMesh.transform.localScale = TempPrefab.Scale;
                ChildMesh.transform.localRotation = TempPrefab.Rotation;

                var TempMeshFilter = ChildMesh.AddComponent<MeshFilter>();
                var TempRenderer = ChildMesh.AddComponent<MeshRenderer>();

                TempMeshFilter.mesh = TempPrefab.MeshData[a].mesh;
                TempRenderer.material = TempPrefab.MeshData[a].material;
            }
        }

        return MainObject;
    }

    public void GenerateSubModels()
    {
        for (int i = 0; i < PrefabObjects.Count; i++)
        {
            var TempPrefab = PrefabObjects[i];
            for (int a = 0; a < TempPrefab.MeshData.Count; a++)
            {
                GameObject ChildMesh = new GameObject(i + ", " + a);

                ChildMesh.transform.parent = this.transform;
                ChildMesh.transform.localPosition = TempPrefab.Position;
                ChildMesh.transform.localScale = TempPrefab.Scale;
                ChildMesh.transform.localRotation = TempPrefab.Rotation;

                var TempMeshFilter = ChildMesh.AddComponent<MeshFilter>();
                var TempRenderer = ChildMesh.AddComponent<MeshRenderer>();

                TempMeshFilter.mesh = TempPrefab.MeshData[a].mesh;
                TempRenderer.material = TempPrefab.MeshData[a].material;
            }
        }
    }

    public void LoadPrefab(PrefabJsonHandler.PrefabJson prefabJson, bool Skybox = false)
    {
        if (!Skybox)
        {
            transform.name = prefabJson.PrefabName;
        }
        Unknown3 = prefabJson.Unknown3;
        AnimTime = prefabJson.AnimTime;
        PrefabObjects = new List<ObjectHeader>();
        for (int i = 0; i < prefabJson.PrefabObjects.Count; i++)
        {
            var NewPrefabObject = new ObjectHeader();
            NewPrefabObject.ParentID = prefabJson.PrefabObjects[i].ParentID;
            NewPrefabObject.Flags = prefabJson.PrefabObjects[i].Flags;
            NewPrefabObject.IncludeMatrix = prefabJson.PrefabObjects[i].IncludeMatrix;
            if (prefabJson.PrefabObjects[i].IncludeMatrix)
            {
                NewPrefabObject.Position = JsonUtil.ArrayToVector3(prefabJson.PrefabObjects[i].Position);
                NewPrefabObject.Rotation = JsonUtil.ArrayToQuaternion(prefabJson.PrefabObjects[i].Rotation);
                NewPrefabObject.Scale = JsonUtil.ArrayToVector3(prefabJson.PrefabObjects[i].Scale);
            }
            else
            {
                NewPrefabObject.Scale = Vector3.one;
            }

            //OBJECT ANIMATION PUT HERE
            NewPrefabObject.IncludeAnimation = prefabJson.PrefabObjects[i].IncludeAnimation;
            NewPrefabObject.Animation = new ObjectAnimation();
            if(NewPrefabObject.IncludeAnimation)
            {
                NewPrefabObject.Animation.U1 = prefabJson.PrefabObjects[i].Animation.Value.U1;
                NewPrefabObject.Animation.U2 = prefabJson.PrefabObjects[i].Animation.Value.U2;
                NewPrefabObject.Animation.U3 = prefabJson.PrefabObjects[i].Animation.Value.U3;
                NewPrefabObject.Animation.U4 = prefabJson.PrefabObjects[i].Animation.Value.U4;
                NewPrefabObject.Animation.U5 = prefabJson.PrefabObjects[i].Animation.Value.U5;
                NewPrefabObject.Animation.U6 = prefabJson.PrefabObjects[i].Animation.Value.U6;
                NewPrefabObject.Animation.AnimationAction = prefabJson.PrefabObjects[i].Animation.Value.AnimationAction;

                NewPrefabObject.Animation.AnimationEntries = new List<AnimationEntry>();

                for (int a = 0; a < prefabJson.PrefabObjects[i].Animation.Value.AnimationEntries.Count; a++)
                {
                    var TempEntry = new AnimationEntry();
                    TempEntry.AnimationMaths = new List<AnimationMath>();
                    
                    for (int b = 0; b < prefabJson.PrefabObjects[i].Animation.Value.AnimationEntries[a].AnimationMaths.Count; b++)
                    {
                        var TempMaths = new AnimationMath();

                        TempMaths.Value1 = prefabJson.PrefabObjects[i].Animation.Value.AnimationEntries[a].AnimationMaths[b].Value1;
                        TempMaths.Value2 = prefabJson.PrefabObjects[i].Animation.Value.AnimationEntries[a].AnimationMaths[b].Value2;
                        TempMaths.Value3 = prefabJson.PrefabObjects[i].Animation.Value.AnimationEntries[a].AnimationMaths[b].Value3;
                        TempMaths.Value4 = prefabJson.PrefabObjects[i].Animation.Value.AnimationEntries[a].AnimationMaths[b].Value4;
                        TempMaths.Value5 = prefabJson.PrefabObjects[i].Animation.Value.AnimationEntries[a].AnimationMaths[b].Value5;
                        TempMaths.Value6 = prefabJson.PrefabObjects[i].Animation.Value.AnimationEntries[a].AnimationMaths[b].Value6;

                        TempEntry.AnimationMaths.Add(TempMaths);
                    }
                    NewPrefabObject.Animation.AnimationEntries.Add(TempEntry);
                }
            }

            NewPrefabObject.MeshData = new List<MeshHeader>();
            for (int a = 0; a < prefabJson.PrefabObjects[i].MeshData.Count; a++)
            {
                var TempMesh = prefabJson.PrefabObjects[i].MeshData[a];
                var TempNewMeshData = new MeshHeader();
                TempNewMeshData.MeshPath = TempMesh.MeshPath;
                TempNewMeshData.MeshID = TempMesh.MeshID;
                if(!Skybox)
                {
                    TempNewMeshData.mesh = PrefabManager.Instance.GetMesh(TempMesh.MeshPath);
                }
                else
                {
                    TempNewMeshData.mesh = SkyboxManager.Instance.GetMesh(TempMesh.MeshPath);
                }
                TempNewMeshData.MaterialID = TempMesh.MaterialID;

                TempNewMeshData.material = GenerateMaterial(TempMesh.MaterialID, Skybox);

                NewPrefabObject.MeshData.Add(TempNewMeshData);
            }

            PrefabObjects.Add(NewPrefabObject);
        }

        GenerateSubModels();
    }

    public void LoadModelsAndMesh(bool Skybox)
    {
        for (int i = 0; i < PrefabObjects.Count; i++)
        {
            var NewPrefabObject = PrefabObjects[i];
            for (int a = 0; a < NewPrefabObject.MeshData.Count; a++)
            {
                var TempMesh = NewPrefabObject.MeshData[a];
                if (!Skybox)
                {
                    TempMesh.mesh = PrefabManager.Instance.GetMesh(TempMesh.MeshPath);
                }
                else
                {
                    TempMesh.mesh = SkyboxManager.Instance.GetMesh(TempMesh.MeshPath);
                }
                TempMesh.material = GenerateMaterial(TempMesh.MaterialID, false);
                NewPrefabObject.MeshData[a] = TempMesh;
            }
            PrefabObjects[i] = NewPrefabObject;
        }
    }


    public static Material GenerateMaterial(int MaterialID, bool Skybox)
    {
        Material material = new Material(Shader.Find("ModelShader"));
        string TextureID = "";
        if (MaterialID != -1)
        {
            if (!Skybox)
            {
                TextureID = PrefabManager.Instance.GetMaterialObject(MaterialID).TexturePath;
            }
            else
            {
                TextureID = SkyboxManager.Instance.GetMaterialObject(MaterialID).TexturePath;
            }
        }
        material.SetTexture("_MainTexture", GetTexture(TextureID, Skybox));
        material.SetFloat("_OutlineWidth", 0);
        material.SetFloat("_OpacityMaskOutline", 0f);
        material.SetColor("_OutlineColor", new Color32(255, 255, 255, 0));
        material.SetFloat("_NoLightMode", 1);
        return material;
    }

    public static Texture2D GetTexture(string TextureID, bool Skybox)
    {
        Texture2D texture = null;
        try
        {
            if (!Skybox)
            {
                for (int i = 0; i < LevelManager.Instance.texture2Ds.Count; i++)
                {
                    if (LevelManager.Instance.texture2Ds[i].name.ToLower() == TextureID.ToLower())
                    {
                        texture = LevelManager.Instance.texture2Ds[i];
                        return texture;
                    }
                }
            }
            else
            {
                for (int i = 0; i < SkyboxManager.Instance.SkyboxTextures2d.Count; i++)
                {
                    if (SkyboxManager.Instance.SkyboxTextures2d[i].name.ToLower() == TextureID.ToLower())
                    {
                        texture = SkyboxManager.Instance.SkyboxTextures2d[i];
                        return texture;
                    }
                }
            }
            texture = LevelManager.Instance.Error;
        }
        catch
        {
            texture = LevelManager.Instance.Error;
        }
        return texture;
    }

    [Serializable]
    public struct ObjectHeader
    {
        public int ParentID;
        public int Flags;

        public ObjectAnimation Animation;
        public List<MeshHeader> MeshData;

        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;

        public bool IncludeAnimation;
        public bool IncludeMatrix;
    }
    [Serializable]
    public struct MeshHeader
    {
        public string MeshPath;
        public int MeshID;
        public Mesh mesh;
        public int MaterialID;
        public Material material;
    }
    [Serializable]
    public struct ObjectAnimation
    {
        public float U1;
        public float U2;
        public float U3;
        public float U4;
        public float U5;
        public float U6;

        public int AnimationAction;
        public List<AnimationEntry> AnimationEntries;
    }
    [Serializable]
    public struct AnimationEntry
    {
        public List<AnimationMath> AnimationMaths;
    }
    [Serializable]
    public struct AnimationMath
    {
        public float Value1;
        public float Value2;
        public float Value3;
        public float Value4;
        public float Value5;
        public float Value6;
    }
}
