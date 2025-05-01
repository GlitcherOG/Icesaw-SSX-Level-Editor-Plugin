using SSXMultiTool.JsonFiles.SSX3;
using SSXMultiTool.Utilities;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteInEditMode]
[SelectionBase]
public class SSX3InstanceObject : MonoBehaviour
{
    public int U0;
    public int U1;
    public int U2;
    public int U3;

    public Vector4 V0;
    public Vector3 V1;
    public Vector3 V2;

    public int TrackID;
    public int RID;
    public int U4;

    public int UTrackID;
    public int URID;

    public float U5;
    public int U6;
    public int U7;

    public int U8;
    public int U9;
    public int U10;
    public int U11;
    public int U12;

    public void LoadBin3(Bin3JsonHandler.Bin3File bin3)
    {
        transform.name = bin3.RID.ToString();

        transform.localEulerAngles = JsonUtil.ArrayToQuaternion(bin3.Rotation).eulerAngles;
        transform.localScale = JsonUtil.ArrayToVector3(bin3.Scale);
        transform.localPosition = JsonUtil.ArrayToVector3(bin3.Position);

        U0 = bin3.U0;
        U1 = bin3.U1;
        U2 = bin3.U2;
        U3 = bin3.U3;

        V0 = JsonUtil.ArrayToVector4(bin3.V0);
        V1 = JsonUtil.ArrayToVector3(bin3.V1);
        V2 = JsonUtil.ArrayToVector3(bin3.V2);

        TrackID = bin3.TrackID;
        RID = bin3.RID;
        U4 = bin3.U4;

        U5 = bin3.U5;
        U6 = bin3.U6;
        U7 = bin3.U7;

        U8 = bin3.U8;
        U9 = bin3.U9;
        U10 = bin3.U10;
        U11 = bin3.U11;
        U12 = bin3.U12;
    }

    //public void PostLoad(TrickyInstanceObject[] TempListInstance, EffectSlotObject[] TempListEffectSlot, PhysicsObject[] TempListPhysics, TrickyPrefabObject[] TempListPrefabObject)
    //{
    //    if (TempListInstance.Length - 1 >= PrevInstance && PrevInstance != -1)
    //    {
    //        PrevInstanceObject = TempListInstance[PrevInstance];
    //    }

    //    if (TempListInstance.Length - 1 >= NextInstance && NextInstance != -1)
    //    {
    //        NextInstanceObject = TempListInstance[NextInstance];
    //    }

    //    if (TempListEffectSlot.Length - 1 >= EffectSlotIndex && EffectSlotIndex != -1)
    //    {
    //        EffectSlotObject = TempListEffectSlot[EffectSlotIndex];
    //    }

    //    if (TempListPhysics.Length - 1 >= PhysicsIndex && PhysicsIndex != -1)
    //    {
    //        PhysicsObject = TempListPhysics[PhysicsIndex];
    //    }

    //    if (TempListPrefabObject.Length - 1 >= ModelID && ModelID != -1)
    //    {
    //        PrefabObject = TempListPrefabObject[ModelID];
    //    }


    //    LoadPrefabs();
    //    LoadCollisionModels();
    //}

    //[ContextMenu("Refresh Models")]
    //public void LoadPrefabs()
    //{
    //    if (Prefab != null)
    //    {
    //        DestroyImmediate(Prefab);
    //    }

    //    if (PrefabObject != null)
    //    {
    //        Prefab = PrefabObject.GeneratePrefab();
    //        //SceneVisibilityManager.instance.DisablePicking(Prefab, true);
    //        //SceneVisibilityManager.instance.EnablePicking(gameObject,false);
    //        Prefab.gameObject.name = "Prefab";
    //        Prefab.transform.parent = transform;
    //        Prefab.transform.localRotation = new Quaternion(0, 0, 0, 0);
    //        Prefab.transform.localPosition = new Vector3(0, 0, 0);
    //        Prefab.transform.localScale = new Vector3(1, 1, 1);
    //        Prefab.AddComponent<SelectParent>();

    //        SetLightingColour();
    //    }
    //    Prefab.SetActive(TrickyLevelManager.Instance.ShowInstanceModels);
    //}

    //public void SetLightingColour()
    //{
    //    if (PrefabObject != null)
    //    {
    //        var TempPrefablist = Prefab.transform.childCount;
    //        for (int i = 0; i < TempPrefablist; i++)
    //        {
    //            var TempChildPrefab = Prefab.transform.GetChild(i);

    //            TempChildPrefab.AddComponent<SelectParent>();

    //            for (int a = 0; a < TempChildPrefab.childCount; a++)
    //            {
    //                TempChildPrefab.GetChild(a).AddComponent<SelectParent>();
    //                var MeshRender = TempChildPrefab.GetChild(a).GetComponent<MeshRenderer>();
    //                var TempLight = (AmbentLightColour) / 255f;
    //                MeshRender.sharedMaterial.SetColor("_AmbientColour", new Color(TempLight.x, TempLight.y, TempLight.z, TempLight.w));
    //                TempLight = (LightColour1) / 255f;
    //                MeshRender.sharedMaterial.SetColor("_VectorColour1", new Color(TempLight.x, TempLight.y, TempLight.z, TempLight.w));
    //                MeshRender.sharedMaterial.SetVector("_VectorDir1", transform.TransformVector(LightVector1) * 100);
    //                TempLight = (LightColour2) / 255f;
    //                MeshRender.sharedMaterial.SetColor("_VectorColour2", new Color(TempLight.x, TempLight.y, TempLight.z, TempLight.w));
    //                MeshRender.sharedMaterial.SetVector("_VectorDir2", transform.TransformVector(LightVector2) * 100);
    //                TempLight = (LightColour3) / 255f;
    //                MeshRender.sharedMaterial.SetColor("_VectorColour3", new Color(TempLight.x, TempLight.y, TempLight.z, TempLight.w));
    //                MeshRender.sharedMaterial.SetVector("_VectorDir3", transform.TransformVector(LightVector3) * 100);
    //            }
    //        }
    //        ToggleLightingMode(TrickyLevelManager.Instance.LightmapMode);
    //    }
    //}

    //[ContextMenu("Refresh Collision Model")]
    //public void LoadCollisionModels()
    //{
    //    //Generate Collisions

    //    if (Collision != null)
    //    {
    //        DestroyImmediate(Collision);
    //    }

    //    Collision = new GameObject("CollisionModel");
    //    //SceneVisibilityManager.instance.DisablePicking(Collision, true);
    //    //SceneVisibilityManager.instance.EnablePicking(gameObject, false);
    //    Collision.AddComponent<SelectParent>();
    //    Collision.transform.parent = transform;
    //    Collision.transform.localRotation = new Quaternion(0, 0, 0, 0);
    //    Collision.transform.localPosition = new Vector3(0, 0, 0);
    //    Collision.transform.localScale = new Vector3(1, 1, 1);

    //    Collision.transform.hideFlags = HideFlags.HideInHierarchy;

    //    if (CollsionMode == 1)
    //    {
    //        if (CollsionModelPaths != null)
    //        {
    //            if (CollsionModelPaths.Length != 0)
    //            {
    //                //AddSubObjects
    //                for (int i = 0; i < CollsionModelPaths.Length; i++)
    //                {
    //                    var TempObject = new GameObject(i.ToString());
    //                    TempObject.AddComponent<SelectParent>();
    //                    TempObject.transform.parent = Collision.transform;
    //                    TempObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
    //                    TempObject.transform.localPosition = new Vector3(0, 0, 0);
    //                    TempObject.transform.localScale = new Vector3(1, 1, 1);

    //                    TempObject.AddComponent<MeshFilter>().sharedMesh = TrickyLevelManager.Instance.GetColMesh(CollsionModelPaths[i]);

    //                    var TempMaterial = new Material(Shader.Find("Standard"));
    //                    TempMaterial.color = Color.red;

    //                    TempObject.AddComponent<MeshRenderer>().sharedMaterial = TempMaterial;
    //                    TempObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    //                    TempObject.GetComponent<MeshRenderer>().receiveShadows = false;
    //                }
    //            }
    //        }
    //    }
    //    //else if (CollsionMode==2)
    //    //{
    //    //    //Calculate BBox
    //    //    Vector3 HighestBBox = new Vector3();
    //    //    Vector3 LowestBBox = new Vector3();
    //    //    bool Hotfix = false;

    //    //    var TempMeshList = Prefab.GetComponentsInChildren<MeshFilter>();

    //    //    for (int i = 0; i < TempMeshList.Length; i++)
    //    //    {
    //    //        var VertexList = TempMeshList[i].sharedMesh.vertices;

    //    //        for (int a = 0; a < VertexList.Length; a++)
    //    //        {
    //    //            var TempVector = TempMeshList[i].transform.TransformPoint(VertexList[a]);
    //    //            TempVector = transform.InverseTransformPoint(TempVector);

    //    //            if(!Hotfix)
    //    //            {
    //    //                HighestBBox = TempVector;
    //    //                LowestBBox = TempVector;
    //    //                Hotfix = true;
    //    //            }

    //    //            HighestBBox = JsonUtil.Highest(HighestBBox, TempVector);
    //    //            LowestBBox = JsonUtil.Lowest(LowestBBox, TempVector);
    //    //        }
    //    //    }

    //    //    //GenerateMesh
    //    //    var TempObject = new GameObject("0");
    //    //    TempObject.transform.parent = Collision.transform;
    //    //    TempObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
    //    //    TempObject.transform.localPosition = new Vector3(0, 0, 0);
    //    //    TempObject.transform.localScale = new Vector3(1, 1, 1);

    //    //    TempObject.AddComponent<MeshFilter>().sharedMesh = GenerateBBoxMesh(HighestBBox, LowestBBox);

    //    //    var TempMaterial = new Material(Shader.Find("Standard"));
    //    //    TempMaterial.color = Color.red;

    //    //    TempObject.AddComponent<MeshRenderer>().sharedMaterial = TempMaterial;
    //    //    TempObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    //    //    TempObject.GetComponent<MeshRenderer>().receiveShadows = false;
    //    //}

    //    Collision.SetActive(TrickyLevelManager.Instance.ShowCollisionModels);
    //}

    //public void RefreshHiddenModels()
    //{
    //    if (Prefab != null)
    //    {
    //        Prefab.SetActive(TrickyLevelManager.Instance.ShowInstanceModels);
    //    }
    //    if (Collision != null)
    //    {
    //        Collision.SetActive(TrickyLevelManager.Instance.ShowCollisionModels);
    //    }
    //}

    //public InstanceJsonHandler.InstanceJson GenerateInstance()
    //{
    //    InstanceJsonHandler.InstanceJson TempInstance = new InstanceJsonHandler.InstanceJson();
    //    TempInstance.InstanceName = transform.name;

    //    TempInstance.Location = JsonUtil.Vector3ToArray(transform.localPosition);
    //    TempInstance.Scale = JsonUtil.Vector3ToArray(transform.localScale);
    //    TempInstance.Rotation = JsonUtil.QuaternionToArray(Quaternion.Euler(transform.localEulerAngles));

    //    TempInstance.LightVector1 = JsonUtil.Vector4ToArray(JsonUtil.Vector3ToVector4(LightVector1, 0));
    //    TempInstance.LightVector2 = JsonUtil.Vector4ToArray(JsonUtil.Vector3ToVector4(LightVector2, 0));
    //    TempInstance.LightVector3 = JsonUtil.Vector4ToArray(JsonUtil.Vector3ToVector4(LightVector3, 0));
    //    TempInstance.AmbentLightVector = JsonUtil.Vector4ToArray(JsonUtil.Vector3ToVector4(AmbentLightVector, 0));

    //    TempInstance.LightColour1 = JsonUtil.Vector4ToArray(JsonUtil.Vector3ToVector4(LightColour1, 0));
    //    TempInstance.LightColour2 = JsonUtil.Vector4ToArray(JsonUtil.Vector3ToVector4(LightColour2, 0));
    //    TempInstance.LightColour3 = JsonUtil.Vector4ToArray(JsonUtil.Vector3ToVector4(LightColour3, 0));
    //    TempInstance.AmbentLightColour = JsonUtil.Vector4ToArray(AmbentLightColour);

    //    if (PrefabObject != null)
    //    {
    //        TempInstance.ModelID = TrickyLevelManager.Instance.dataManager.GetPrefabID(PrefabObject);
    //        //TempInstance.ModelID2 = TempInstance.ModelID;
    //    }
    //    else
    //    {
    //        TempInstance.ModelID = -1;
    //        //TempInstance.ModelID2 = -1;
    //    }

    //    if (PrevInstanceObject != null)
    //    {
    //        TempInstance.PrevInstance = TrickyLevelManager.Instance.dataManager.GetInstanceID(PrevInstanceObject);
    //    }
    //    else
    //    {
    //        TempInstance.PrevInstance = -1;
    //    }

    //    if (NextInstanceObject != null)
    //    {
    //        TempInstance.NextInstance = TrickyLevelManager.Instance.dataManager.GetInstanceID(NextInstanceObject);
    //    }
    //    else
    //    {
    //        TempInstance.NextInstance = -1;
    //    }

    //    TempInstance.UnknownInt26 = UnknownInt26;
    //    TempInstance.UnknownInt27 = UnknownInt27;
    //    TempInstance.UnknownInt28 = UnknownInt28;
    //    TempInstance.UnknownInt30 = UnknownInt30;
    //    TempInstance.UnknownInt31 = UnknownInt31;
    //    TempInstance.UnknownInt32 = UnknownInt32;

    //    TempInstance.LTGState = LTGState;

    //    TempInstance.Hash = Hash;
    //    TempInstance.IncludeSound = IncludeSound;

    //    if (IncludeSound)
    //    {
    //        InstanceJsonHandler.SoundData TempSound = new InstanceJsonHandler.SoundData();
    //        TempSound.CollisonSound = Sounds.CollisonSound;

    //        TempSound.ExternalSounds = new List<InstanceJsonHandler.ExternalSound>();

    //        for (int i = 0; i < Sounds.ExternalSounds.Count; i++)
    //        {
    //            var TempCollisionSound = new InstanceJsonHandler.ExternalSound();
    //            TempCollisionSound.U0 = Sounds.ExternalSounds[i].U0;
    //            TempCollisionSound.SoundIndex = Sounds.ExternalSounds[i].SoundIndex;
    //            TempCollisionSound.U2 = Sounds.ExternalSounds[i].U3;
    //            TempCollisionSound.U3 = Sounds.ExternalSounds[i].U3;
    //            TempCollisionSound.U4 = Sounds.ExternalSounds[i].U4;
    //            TempCollisionSound.U5 = Sounds.ExternalSounds[i].U5;
    //            TempCollisionSound.U6 = Sounds.ExternalSounds[i].U6;

    //            TempSound.ExternalSounds.Add(TempCollisionSound);
    //        }


    //        TempInstance.Sounds = TempSound;
    //    }

    //    TempInstance.U0 = U0;
    //    TempInstance.PlayerBounceAmmount = PlayerBounceAmmount;
    //    TempInstance.U2 = U2;
    //    TempInstance.U22 = U22;
    //    TempInstance.Visable = Visable;
    //    TempInstance.PlayerCollision = PlayerCollision;
    //    TempInstance.PlayerBounce = PlayerBounce;
    //    TempInstance.Unknown241 = Unknown241;
    //    TempInstance.UVScroll = UVScroll;

    //    TempInstance.SurfaceType = ((int)SurfaceType) - 1;
    //    TempInstance.CollsionMode = CollsionMode;
    //    TempInstance.CollsionModelPaths = CollsionModelPaths;
    //    TempInstance.U8 = U8;

    //    if (EffectSlotObject != null)
    //    {
    //        TempInstance.EffectSlotIndex = TrickyLevelManager.Instance.dataManager.GetEffectSlotID(EffectSlotObject);
    //    }
    //    else
    //    {
    //        TempInstance.EffectSlotIndex = -1;
    //    }

    //    if (PhysicsObject != null)
    //    {
    //        TempInstance.PhysicsIndex = TrickyLevelManager.Instance.dataManager.GetPhysicstID(PhysicsObject);
    //    }
    //    else
    //    {
    //        TempInstance.PhysicsIndex = -1;
    //    }


    //    return TempInstance;
    //}

    //Mesh GenerateBBoxMesh(Vector3 HighestBBox, Vector3 LowestBBox)
    //{
    //    List<Vector3> vector3s = new List<Vector3>();
    //    List<int> ints = new List<int>();

    //    vector3s.Add(new Vector3(HighestBBox.x, HighestBBox.y, HighestBBox.z));
    //    vector3s.Add(new Vector3(LowestBBox.x, HighestBBox.y, HighestBBox.z));
    //    vector3s.Add(new Vector3(HighestBBox.x, HighestBBox.y, LowestBBox.z));
    //    vector3s.Add(new Vector3(LowestBBox.x, HighestBBox.y, LowestBBox.z));
    //    vector3s.Add(new Vector3(HighestBBox.x, LowestBBox.y, HighestBBox.z));
    //    vector3s.Add(new Vector3(LowestBBox.x, LowestBBox.y, HighestBBox.z));
    //    vector3s.Add(new Vector3(HighestBBox.x, LowestBBox.y, LowestBBox.z));
    //    vector3s.Add(new Vector3(LowestBBox.x, LowestBBox.y, LowestBBox.z));

    //    //+Y
    //    ints.Add(2);
    //    ints.Add(1);
    //    ints.Add(0);

    //    ints.Add(1);
    //    ints.Add(2);
    //    ints.Add(3);

    //    //-Y
    //    ints.Add(4);
    //    ints.Add(5);
    //    ints.Add(6);

    //    ints.Add(7);
    //    ints.Add(6);
    //    ints.Add(5);

    //    //+Z
    //    ints.Add(5);
    //    ints.Add(4);
    //    ints.Add(1);

    //    ints.Add(0);
    //    ints.Add(1);
    //    ints.Add(4);

    //    //-Z
    //    ints.Add(3);
    //    ints.Add(2);
    //    ints.Add(6);

    //    ints.Add(7);
    //    ints.Add(3);
    //    ints.Add(6);

    //    //+X
    //    ints.Add(2);
    //    ints.Add(0);
    //    ints.Add(6);

    //    ints.Add(4);
    //    ints.Add(6);
    //    ints.Add(0);

    //    //-X
    //    ints.Add(1);
    //    ints.Add(3);
    //    ints.Add(5);

    //    ints.Add(7);
    //    ints.Add(5);
    //    ints.Add(3);

    //    var mesh = new Mesh();
    //    mesh.vertices = vector3s.ToArray();
    //    mesh.triangles = ints.ToArray();
    //    mesh.RecalculateNormals();
    //    return mesh;
    //}

    //[MenuItem("GameObject/Ice Saw/Instance", false, 13)]
    //public static void CreateInstance(MenuCommand menuCommand)
    //{
    //    GameObject TempObject = new GameObject("Instance");
    //    TempObject.AddComponent<TrickyInstanceObject>();
    //    if (menuCommand.context != null)
    //    {
    //        var AddToObject = (GameObject)menuCommand.context;

    //        TempObject.transform.parent = AddToObject.transform;
    //    }

    //}

    //Vector3 ConvertWorldPoint(Vector3 point, Transform objectTransform)
    //{
    //    if (TrickyLevelManager.Instance != null)
    //    {
    //        return TrickyLevelManager.Instance.transform.InverseTransformPoint(objectTransform.TransformPoint(point));
    //    }

    //    return objectTransform.TransformPoint(point);
    //}

    //public List<ObjExporter.MassModelData> GenerateModel()
    //{
    //    string[] TempTextures = PrefabObject.GetTextureNames();
    //    MeshFilter[] ObjectList = Prefab.GetComponentsInChildren<MeshFilter>();
    //    List<ObjExporter.MassModelData> MainList = new List<ObjExporter.MassModelData>();
    //    for (int a = 0; a < ObjectList.Length; a++)
    //    {
    //        ObjExporter.MassModelData TempModel = new ObjExporter.MassModelData();
    //        TempModel.Name = gameObject.name + a;

    //        //Go through and update points so they are correct for rotation and then regenerate normals
    //        var OldMesh = ObjectList[a].sharedMesh;
    //        var Verts = OldMesh.vertices;
    //        for (int i = 0; i < Verts.Length; i++)
    //        {
    //            Verts[i] = ObjectList[a].transform.TransformPoint(Verts[i]);
    //        }
    //        var TempMesh = new Mesh();
    //        TempMesh.vertices = Verts;
    //        TempMesh.uv = OldMesh.uv;
    //        TempMesh.normals = OldMesh.normals;
    //        TempMesh.triangles = OldMesh.triangles;

    //        TempMesh.Optimize();
    //        TempMesh.RecalculateNormals();

    //        TempModel.Model = TempMesh;
    //        TempModel.TextureName = TempTextures[a];
    //        MainList.Add(TempModel);
    //    }
    //    return MainList;
    //}

    //public void ToggleLightingMode(bool Lightmap)
    //{
    //    var TempPrefabs = Prefab.GetComponentsInChildren<Renderer>();
    //    for (int i = 0; i < TempPrefabs.Length; i++)
    //    {
    //        if (Lightmap)
    //        {
    //            TempPrefabs[i].sharedMaterial.SetFloat("_LightMode", 1f);
    //        }
    //        else
    //        {
    //            TempPrefabs[i].sharedMaterial.SetFloat("_LightMode", 0f);
    //        }
    //    }
    //}
}