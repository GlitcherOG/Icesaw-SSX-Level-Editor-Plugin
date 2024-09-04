using SSXMultiTool.JsonFiles.SSXOG;
using SSXMultiTool.Utilities;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OGInstanceObject : MonoBehaviour
{

    public Vector4 LightVector1;
    public Vector4 LightVector2;
    public Vector4 LightVector3;
    public Vector4 AmbentLightVector;

    public Vector4 LightColour1;
    public Vector4 LightColour2;
    public Vector4 LightColour3;
    public Vector4 AmbentLightColour;

    public int U2;
    public int U3;
    [OnChangedCall("LoadPrefabs")]
    public int PrefabID;

    public int U5; //16

    public bool Visable;
    public bool PlayerCollision;
    public bool PlayerBounce;

    public float PlayerBounceValue;

    public int CollsionMode; //16
    public string[] CollsionModelPaths;
    public int PhysicsIndex; //16
    public int U11; //16

    public float U12;
    public int U13;

    public int U14;
    public int U15;
    public int U16;
    public int U17;

    GameObject Prefab;
    GameObject Collision;

    public void LoadInstance(InstanceJsonHandler.InstanceJson instance)
    {
        transform.name = instance.Name;

        transform.localPosition = JsonUtil.ArrayToVector3(instance.Location);
        transform.localRotation = JsonUtil.ArrayToQuaternion(instance.Rotation);
        transform.localScale = JsonUtil.ArrayToVector3(instance.Scale);

        LightVector1 = JsonUtil.ArrayToVector4(instance.LightVector1);
        LightVector2 = JsonUtil.ArrayToVector4(instance.LightVector2);
        LightVector3 = JsonUtil.ArrayToVector4(instance.LightVector3);
        AmbentLightVector = JsonUtil.ArrayToVector4(instance.AmbentLightVector);

        LightColour1 = JsonUtil.ArrayToVector4(instance.LightColour1);
        LightColour2 = JsonUtil.ArrayToVector4(instance.LightColour2);
        LightColour3 = JsonUtil.ArrayToVector4(instance.LightColour3);
        AmbentLightColour = JsonUtil.ArrayToVector4(instance.AmbentLightColour);

        U2 = instance.U2;
        U3 = instance.U3;
        PrefabID = instance.PrefabID;

        U5 = instance.U5;

        Visable = instance.Visable;
        PlayerCollision = instance.PlayerCollision;
        PlayerBounce = instance.PlayerBounce;

        PlayerBounceValue = instance.PlayerBounceValue;

        CollsionMode = instance.CollsionMode;
        CollsionModelPaths = instance.CollsionModelPaths;
        PhysicsIndex = instance.PhysicsIndex;
        U11 = instance.U11;

        U12 = instance.U12;
        U13 = instance.U13;

        U14 = instance.U14;
        U15 = instance.U15;
        U16 = instance.U16;
        U17 = instance.U17;
        LoadPrefabs();
        LoadCollisionModels();
    }

    public void LoadPrefabs()
    {
        if (Prefab != null)
        {
            DestroyImmediate(Prefab);
        }

        if (PrefabID != -1)
        {
            Prefab = OGPrefabManager.Instance.GetPrefabObject(PrefabID).GeneratePrefab();
            Prefab.gameObject.name = "Prefab";
            Prefab.transform.parent = transform;
            Prefab.transform.localRotation = new Quaternion(0, 0, 0, 0);
            Prefab.transform.localPosition = new Vector3(0, 0, 0);
            Prefab.transform.localScale = new Vector3(1, 1, 1);
            //Prefab.AddComponent<SelectParent>();

            var TempPrefablist = Prefab.transform.childCount;
            for (int i = 0; i < TempPrefablist; i++)
            {
                var TempChildPrefab = Prefab.transform.GetChild(i);

                //TempChildPrefab.AddComponent<SelectParent>();

                var MeshRender = TempChildPrefab.GetComponent<MeshRenderer>();
                var TempLight = (AmbentLightColour) / 255f;
                MeshRender.sharedMaterial.SetColor("_AmbientColour", new Color(TempLight.x, TempLight.y, TempLight.z, TempLight.w));
                TempLight = (LightColour1) / 255f;
                MeshRender.sharedMaterial.SetColor("_VectorColour1", new Color(TempLight.x, TempLight.y, TempLight.z, TempLight.w));
                MeshRender.sharedMaterial.SetVector("_VectorDir1", transform.TransformVector(LightVector1) * 100);
                TempLight = (LightColour2) / 255f;
                MeshRender.sharedMaterial.SetColor("_VectorColour2", new Color(TempLight.x, TempLight.y, TempLight.z, TempLight.w));
                MeshRender.sharedMaterial.SetVector("_VectorDir2", transform.TransformVector(LightVector2) * 100);
                TempLight = (LightColour3) / 255f;
                MeshRender.sharedMaterial.SetColor("_VectorColour3", new Color(TempLight.x, TempLight.y, TempLight.z, TempLight.w));
                MeshRender.sharedMaterial.SetVector("_VectorDir3", transform.TransformVector(LightVector3) * 100);


            }
        }
        Prefab.SetActive(OGWorldManager.Instance.ShowInstanceModels);
    }

    Vector3 ConvertWorldPoint(Vector3 point, Transform objectTransform)
    {
        if (OGLevelManager.Instance != null)
        {
            return OGLevelManager.Instance.transform.InverseTransformPoint(objectTransform.TransformPoint(point));
        }

        return objectTransform.TransformPoint(point);
    }

    [ContextMenu("Refresh Collision Model")]
    public void LoadCollisionModels()
    {
        //Generate Collisions

        if (Collision != null)
        {
            DestroyImmediate(Collision);
        }

        Collision = new GameObject("CollisionModel");
        Collision.transform.parent = transform;
        Collision.transform.localRotation = new Quaternion(0, 0, 0, 0);
        Collision.transform.localPosition = new Vector3(0, 0, 0);
        Collision.transform.localScale = new Vector3(1, 1, 1);

        Collision.transform.hideFlags = HideFlags.HideInHierarchy;

        //if (CollsionMode == 1)
        //{
            if (CollsionModelPaths != null)
            {
                if (CollsionModelPaths.Length != 0)
                {
                    //AddSubObjects
                    for (int i = 0; i < CollsionModelPaths.Length; i++)
                    {
                        var TempObject = new GameObject(i.ToString());
                        TempObject.transform.parent = Collision.transform;
                        TempObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
                        TempObject.transform.localPosition = new Vector3(0, 0, 0);
                        TempObject.transform.localScale = new Vector3(1, 1, 1);

                        TempObject.AddComponent<MeshFilter>().sharedMesh = OGPrefabManager.Instance.GetColMesh(CollsionModelPaths[i]);

                        var TempMaterial = new Material(Shader.Find("Standard"));
                        TempMaterial.color = Color.red;

                        TempObject.AddComponent<MeshRenderer>().sharedMaterial = TempMaterial;
                        TempObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                        TempObject.GetComponent<MeshRenderer>().receiveShadows = false;
                    }
                }
            }
        //}
        //else if (CollsionMode==2)
        //{
        //    //Calculate BBox
        //    Vector3 HighestBBox = new Vector3();
        //    Vector3 LowestBBox = new Vector3();
        //    bool Hotfix = false;

        //    var TempMeshList = Prefab.GetComponentsInChildren<MeshFilter>();

        //    for (int i = 0; i < TempMeshList.Length; i++)
        //    {
        //        var VertexList = TempMeshList[i].sharedMesh.vertices;

        //        for (int a = 0; a < VertexList.Length; a++)
        //        {
        //            var TempVector = TempMeshList[i].transform.TransformPoint(VertexList[a]);
        //            TempVector = transform.InverseTransformPoint(TempVector);

        //            if(!Hotfix)
        //            {
        //                HighestBBox = TempVector;
        //                LowestBBox = TempVector;
        //                Hotfix = true;
        //            }

        //            HighestBBox = JsonUtil.Highest(HighestBBox, TempVector);
        //            LowestBBox = JsonUtil.Lowest(LowestBBox, TempVector);
        //        }
        //    }

        //    //GenerateMesh
        //    var TempObject = new GameObject("0");
        //    TempObject.transform.parent = Collision.transform;
        //    TempObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        //    TempObject.transform.localPosition = new Vector3(0, 0, 0);
        //    TempObject.transform.localScale = new Vector3(1, 1, 1);

        //    TempObject.AddComponent<MeshFilter>().sharedMesh = GenerateBBoxMesh(HighestBBox, LowestBBox);

        //    var TempMaterial = new Material(Shader.Find("Standard"));
        //    TempMaterial.color = Color.red;

        //    TempObject.AddComponent<MeshRenderer>().sharedMaterial = TempMaterial;
        //    TempObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        //    TempObject.GetComponent<MeshRenderer>().receiveShadows = false;
        //}

        Collision.SetActive(OGWorldManager.Instance.ShowCollisionModels);
    }

    public void ToggleLightingMode(bool Lightmap)
    {
        var TempPrefabs = Prefab.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < TempPrefabs.Length; i++)
        {
            if (Lightmap)
            {
                TempPrefabs[i].sharedMaterial.SetFloat("_LightMode", 1f);
            }
            else
            {
                TempPrefabs[i].sharedMaterial.SetFloat("_LightMode", 0f);
            }
        }
    }

    public List<ObjExporter.MassModelData> GenerateModel()
    {
        string[] TempTextures = OGPrefabManager.Instance.GetPrefabObject(PrefabID).GetTextureNames();
        MeshFilter[] ObjectList = Prefab.GetComponentsInChildren<MeshFilter>();
        List<ObjExporter.MassModelData> MainList = new List<ObjExporter.MassModelData>();
        for (int a = 0; a < ObjectList.Length; a++)
        {
            ObjExporter.MassModelData TempModel = new ObjExporter.MassModelData();
            TempModel.Name = gameObject.name + a;

            //Go through and update points so they are correct for rotation and then regenerate normals
            var OldMesh = ObjectList[a].sharedMesh;
            var Verts = OldMesh.vertices;
            for (int i = 0; i < Verts.Length; i++)
            {
                Verts[i] = ConvertWorldPoint(Verts[i], ObjectList[a].transform);
            }
            var TempMesh = new Mesh();
            TempMesh.vertices = Verts;
            TempMesh.uv = OldMesh.uv;
            TempMesh.normals = OldMesh.normals;
            TempMesh.triangles = OldMesh.triangles;

            TempMesh.Optimize();
            TempMesh.RecalculateNormals();

            TempModel.Model = TempMesh;
            TempModel.TextureName = TempTextures[a];
            MainList.Add(TempModel);
        }
        return MainList;
    }

    public void RefreshHiddenModels()
    {
        if (Prefab != null)
        {
            Prefab.SetActive(OGWorldManager.Instance.ShowInstanceModels);
        }
        if (Collision != null)
        {
            Collision.SetActive(OGWorldManager.Instance.ShowCollisionModels);
        }
    }
}
