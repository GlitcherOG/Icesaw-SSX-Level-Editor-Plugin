using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[SelectionBase]
public class TrickyInstanceObject : MonoBehaviour
{
    public Vector3 LightingVector;
    public Quaternion LightingRotation;

    public Vector4 Unknown9; //Some Lighting Thing
    public Vector4 Unknown10;
    public Vector4 Unknown11;
    public Vector4 RGBA;

    [OnChangedCall("LoadPrefabs")]
    public int ModelID;
    public int PrevInstance; //Next Connected Model 
    public int NextInstance; //Prev Connected Model

    public int UnknownInt26;
    public int UnknownInt27;
    public int UnknownInt28;
    public int ModelID2;
    public int UnknownInt30;
    public int UnknownInt31;
    public int UnknownInt32;

    public int LTGState;

    public int Hash;
    public bool IncludeSound;
    [SerializeField]
    public SoundData Sounds;

    //Object Properties

    public float U0;
    public float PlayerBounceAmmount;
    public int U2;
    public int U22;
    public bool Visable;
    public bool PlayerCollision;
    public bool PlayerBounce;
    public bool Unknown241;
    public bool UVScroll;

    public int U4;
    [OnChangedCall("LoadCollisionModels")]
    public int CollsionMode;
    [OnChangedCall("LoadCollisionModels")]
    public string[] CollsionModelPaths;
    public int EffectSlotIndex;
    public int PhysicsIndex;
    public int U8;

    GameObject Prefab;
    GameObject Collision;

    public void LoadInstance(InstanceJsonHandler.InstanceJson instance)
    {
        transform.name = instance.InstanceName;

        transform.localEulerAngles = JsonUtil.ArrayToQuaternion(instance.Rotation).eulerAngles;
        transform.localScale = JsonUtil.ArrayToVector3(instance.Scale);
        transform.localPosition = JsonUtil.ArrayToVector3(instance.Location);

        LightingVector = JsonUtil.ArrayToVector3(instance.LightingVector);
        LightingRotation = JsonUtil.ArrayToQuaternion(instance.LightingRotation);

        GameObject gameObject = new GameObject("Lighting");
        gameObject.transform.parent = transform;
        gameObject.transform.localRotation = new Quaternion(0, 0, 0, 1);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        gameObject.transform.position = (LightingVector * 100f) + transform.position;

        Unknown9 = JsonUtil.ArrayToVector4(instance.Unknown9);
        Unknown10 = JsonUtil.ArrayToVector4(instance.Unknown10);
        Unknown11 = JsonUtil.ArrayToVector4(instance.Unknown11);
        Unknown11 = JsonUtil.ArrayToVector4(instance.Unknown11);
        RGBA = JsonUtil.ArrayToVector4(instance.RGBA);


        ModelID = instance.ModelID;
        PrevInstance = instance.PrevInstance;
        NextInstance = instance.NextInstance;

        UnknownInt26 = instance.UnknownInt26;
        UnknownInt27 = instance.UnknownInt27;
        UnknownInt28 = instance.UnknownInt28;
        ModelID2 = instance.ModelID2;
        UnknownInt30 = instance.UnknownInt30;
        UnknownInt31 = instance.UnknownInt31;
        UnknownInt32 = instance.UnknownInt32;

        LTGState = instance.LTGState;

        Hash = instance.Hash;
        IncludeSound = instance.IncludeSound;

        if(IncludeSound)
        {
            SoundData soundData = new SoundData();
            var TempSound = instance.Sounds.Value;
            soundData.CollisonSound = TempSound.CollisonSound;

            soundData.ExternalSounds = new List<ExternalSound>();
            for (int i = 0; i < TempSound.ExternalSounds.Count; i++)
            {
                var NewTempSound = new ExternalSound();
                NewTempSound.U0 = TempSound.ExternalSounds[i].U0;
                NewTempSound.SoundIndex = TempSound.ExternalSounds[i].SoundIndex;
                NewTempSound.U2 = TempSound.ExternalSounds[i].U2;
                NewTempSound.U3 = TempSound.ExternalSounds[i].U3;
                NewTempSound.U4 = TempSound.ExternalSounds[i].U4;
                NewTempSound.U5 = TempSound.ExternalSounds[i].U5;
                NewTempSound.U6 = TempSound.ExternalSounds[i].U6;
                soundData.ExternalSounds.Add(NewTempSound);
            }

            Sounds = soundData;
        }

        U0 = instance.U0;
        PlayerBounceAmmount = instance.PlayerBounceAmmount;
        U2 = instance.U2;
        U22 = instance.U22;
        Visable = instance.Visable;
        PlayerCollision = instance.PlayerCollision;
        PlayerBounce = instance.PlayerBounce;
        Unknown241 = instance.Unknown241;
        UVScroll = instance.UVScroll;

        U4 = instance.U4;
        CollsionMode = instance.CollsionMode;
        CollsionModelPaths = instance.CollsionModelPaths;
        EffectSlotIndex = instance.EffectSlotIndex;
        PhysicsIndex = instance.PhysicsIndex;
        U8 = instance.U8;

        LoadPrefabs();
        LoadCollisionModels();
    }
    [ContextMenu("Refresh Models")]
    public void LoadPrefabs()
    {
        if(Prefab!=null)
        {
            DestroyImmediate(Prefab);
        }

        if (ModelID != -1)
        {
            Prefab = TrickyPrefabManager.Instance.GetPrefabObject(ModelID).GeneratePrefab();
            Prefab.gameObject.name = "Prefab";
            Prefab.transform.parent = transform;
            Prefab.transform.localRotation = new Quaternion(0, 0, 0, 0);
            Prefab.transform.localPosition = new Vector3(0, 0, 0);
            Prefab.transform.localScale = new Vector3(1, 1, 1);
            Prefab.AddComponent<SelectParent>();

            var TempPrefablist = Prefab.transform.childCount;
            for (int i = 0; i < TempPrefablist; i++)
            {
                var TempChildPrefab = Prefab.transform.GetChild(i);

                TempChildPrefab.AddComponent<SelectParent>();

                for (int a = 0; a < TempChildPrefab.childCount; a++)
                {
                    TempChildPrefab.GetChild(a).AddComponent<SelectParent>();
                }
            }
        }
        Prefab.SetActive(TrickyWorldManager.Instance.ShowInstanceModels);
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
        Collision.AddComponent<SelectParent>();
        Collision.transform.parent = transform;
        Collision.transform.localRotation = new Quaternion(0, 0, 0, 0);
        Collision.transform.localPosition = new Vector3(0, 0, 0);
        Collision.transform.localScale = new Vector3(1, 1, 1);

        Collision.transform.hideFlags = HideFlags.HideInHierarchy;

        if (CollsionMode == 1)
        {
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

                        TempObject.AddComponent<MeshFilter>().sharedMesh = TrickyPrefabManager.Instance.GetColMesh(CollsionModelPaths[i]);

                        var TempMaterial = new Material(Shader.Find("Standard"));
                        TempMaterial.color = Color.red;

                        TempObject.AddComponent<MeshRenderer>().sharedMaterial = TempMaterial;
                        TempObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                        TempObject.GetComponent<MeshRenderer>().receiveShadows = false;
                    }
                }
            }
        }
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

        Collision.SetActive(TrickyWorldManager.Instance.ShowCollisionModels);
    }

    public void RefreshHiddenModels()
    {
        if (Prefab != null)
        {
            Prefab.SetActive(TrickyWorldManager.Instance.ShowInstanceModels);
        }
        if (Collision != null)
        {
            Collision.SetActive(TrickyWorldManager.Instance.ShowCollisionModels);
        }
    }

    public InstanceJsonHandler.InstanceJson GenerateInstance()
    {
        InstanceJsonHandler.InstanceJson TempInstance = new InstanceJsonHandler.InstanceJson();
        TempInstance.InstanceName = transform.name;

        TempInstance.Location = JsonUtil.Vector3ToArray(transform.localPosition);
        TempInstance.Scale = JsonUtil.Vector3ToArray(transform.localScale);
        TempInstance.Rotation = JsonUtil.QuaternionToArray(Quaternion.Euler(transform.localEulerAngles));

        TempInstance.LightingVector = JsonUtil.Vector3ToArray(LightingVector);
        TempInstance.LightingRotation = JsonUtil.QuaternionToArray(LightingRotation);

        TempInstance.Unknown9 = JsonUtil.Vector4ToArray(Unknown9);
        TempInstance.Unknown10 = JsonUtil.Vector4ToArray(Unknown10);
        TempInstance.Unknown11 = JsonUtil.Vector4ToArray(Unknown11);
        TempInstance.RGBA = JsonUtil.Vector4ToArray(RGBA);

        TempInstance.ModelID = ModelID;
        TempInstance.PrevInstance = PrevInstance;
        TempInstance.NextInstance = NextInstance;

        TempInstance.UnknownInt26 = UnknownInt26;
        TempInstance.UnknownInt27 = UnknownInt27;
        TempInstance.UnknownInt28 = UnknownInt28;
        TempInstance.ModelID2 = ModelID2;
        TempInstance.UnknownInt30 = UnknownInt30;
        TempInstance.UnknownInt31 = UnknownInt31;
        TempInstance.UnknownInt32 = UnknownInt32;

        TempInstance.LTGState = LTGState;

        TempInstance.Hash = Hash;
        TempInstance.IncludeSound = IncludeSound;

        if(IncludeSound)
        {
            InstanceJsonHandler.SoundData TempSound = new InstanceJsonHandler.SoundData();
            TempSound.CollisonSound = Sounds.CollisonSound;

            TempSound.ExternalSounds = new List<InstanceJsonHandler.ExternalSound>();

            for (int i = 0; i < Sounds.ExternalSounds.Count; i++)
            {
                var TempCollisionSound = new InstanceJsonHandler.ExternalSound();
                TempCollisionSound.U0 = Sounds.ExternalSounds[i].U0;
                TempCollisionSound.SoundIndex = Sounds.ExternalSounds[i].SoundIndex;
                TempCollisionSound.U2 = Sounds.ExternalSounds[i].U3; 
                TempCollisionSound.U3 = Sounds.ExternalSounds[i].U3;
                TempCollisionSound.U4 = Sounds.ExternalSounds[i].U4;
                TempCollisionSound.U5 = Sounds.ExternalSounds[i].U5;
                TempCollisionSound.U6 = Sounds.ExternalSounds[i].U6;

                TempSound.ExternalSounds.Add(TempCollisionSound);
            }


            TempInstance.Sounds = TempSound;
        }

        TempInstance.U0 = U0;
        TempInstance.PlayerBounceAmmount = PlayerBounceAmmount;
        TempInstance.U2 = U2;
        TempInstance.U22 = U22;
        TempInstance.Visable = Visable;
        TempInstance.PlayerCollision = PlayerCollision;
        TempInstance.PlayerBounce = PlayerBounce;
        TempInstance.Unknown241 = Unknown241;
        TempInstance.UVScroll = UVScroll;

        TempInstance.U4 = U4;
        TempInstance.CollsionMode = CollsionMode;
        TempInstance.CollsionModelPaths = CollsionModelPaths;
        TempInstance.EffectSlotIndex = EffectSlotIndex;
        TempInstance.PhysicsIndex = PhysicsIndex;
        TempInstance.U8 = U8;


        return TempInstance;
    }

    [ContextMenu("Goto Effect Slot")]
    public void GotoEffectSlot()
    {
        var TempList = LogicManager.Instance.GetEffectSlotsList();

        if (TempList.Length - 1 >= EffectSlotIndex)
        {
            Selection.activeObject = TempList[EffectSlotIndex];
        }
    }

    [ContextMenu("Goto Physics")]
    public void GotoPhysicsEffect()
    {
        var TempList = LogicManager.Instance.GetPhysicsObjects();

        if (TempList.Length - 1 >= PhysicsIndex)
        {
            Selection.activeObject = TempList[PhysicsIndex];
        }
    }

    [ContextMenu("Goto Model")]
    public void GotoModel()
    {
        var TempList = TrickyPrefabManager.Instance.GetPrefabList();

        if (TempList.Length - 1 >= ModelID)
        {
            Selection.activeObject = TempList[ModelID];
        }
    }

    Mesh GenerateBBoxMesh(Vector3 HighestBBox, Vector3 LowestBBox)
    {
        List<Vector3> vector3s = new List<Vector3>();
        List<int> ints = new List<int>();

        vector3s.Add(new Vector3(HighestBBox.x, HighestBBox.y, HighestBBox.z));
        vector3s.Add(new Vector3(LowestBBox.x, HighestBBox.y, HighestBBox.z));
        vector3s.Add(new Vector3(HighestBBox.x, HighestBBox.y, LowestBBox.z));
        vector3s.Add(new Vector3(LowestBBox.x, HighestBBox.y, LowestBBox.z));
        vector3s.Add(new Vector3(HighestBBox.x, LowestBBox.y, HighestBBox.z));
        vector3s.Add(new Vector3(LowestBBox.x, LowestBBox.y, HighestBBox.z));
        vector3s.Add(new Vector3(HighestBBox.x, LowestBBox.y, LowestBBox.z));
        vector3s.Add(new Vector3(LowestBBox.x, LowestBBox.y, LowestBBox.z));

        //+Y
        ints.Add(2);
        ints.Add(1);
        ints.Add(0);

        ints.Add(1);
        ints.Add(2);
        ints.Add(3);

        //-Y
        ints.Add(4);
        ints.Add(5);
        ints.Add(6);

        ints.Add(7);
        ints.Add(6);
        ints.Add(5);

        //+Z
        ints.Add(5);
        ints.Add(4);
        ints.Add(1);

        ints.Add(0);
        ints.Add(1);
        ints.Add(4);

        //-Z
        ints.Add(3);
        ints.Add(2);
        ints.Add(6);

        ints.Add(7);
        ints.Add(3);
        ints.Add(6);

        //+X
        ints.Add(2);
        ints.Add(0);
        ints.Add(6);

        ints.Add(4);
        ints.Add(6);
        ints.Add(0);

        //-X
        ints.Add(1);
        ints.Add(3);
        ints.Add(5);

        ints.Add(7);
        ints.Add(5);
        ints.Add(3);

        var mesh = new Mesh();
        mesh.vertices = vector3s.ToArray();
        mesh.triangles = ints.ToArray();
        mesh.RecalculateNormals();
        return mesh;
    }

    [MenuItem("GameObject/Ice Saw/Instance", false, 13)]
    public static void CreateInstance(MenuCommand menuCommand)
    {
        GameObject TempObject = new GameObject("Instance");
        TempObject.AddComponent<TrickyInstanceObject>();
        if (menuCommand.context != null)
        {
            var AddToObject = (GameObject)menuCommand.context;

            TempObject.transform.parent = AddToObject.transform;
        }

    }

    Vector3 ConvertWorldPoint(Vector3 point, Transform objectTransform)
    {
        if (TrickyLevelManager.Instance != null)
        {
            return TrickyLevelManager.Instance.transform.InverseTransformPoint(objectTransform.TransformPoint(point));
        }

        return objectTransform.TransformPoint(point);
    }

    public List<ObjExporter.MassModelData> GenerateModel()
    {
        string[] TempTextures = TrickyPrefabManager.Instance.GetPrefabObject(ModelID).GetTextureNames();
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
                Verts[i] = ObjectList[a].transform.TransformPoint(Verts[i]);
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

    [System.Serializable]
    public struct SoundData
    {
        public int CollisonSound;
        public List<ExternalSound> ExternalSounds;
    }
    [System.Serializable]
    public struct ExternalSound
    {
        public int U0;
        public int SoundIndex;
        public float U2;
        public float U3;
        public float U4;
        public float U5; //Radius?
        public float U6;
    }
}

[CustomEditor(typeof(TrickyInstanceObject))]
public class TrickyInstanceObjectEditor : Editor
{
    //public override void OnInspectorGUI()
    //{
    //    DrawDefaultInspector();

    //    //Component.hideFlags = HideFlags.HideInInspector;

    //    if(EditorGUILayout.LinkButton("Refresh Textures"))
    //    {
    //        var Temp = (typeof(LevelManager))serializedObject.targetObject;
    //    }
    //}

    public VisualTreeAsset m_InspectorXML;

    public override VisualElement CreateInspectorGUI()
    {
        m_InspectorXML = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets\\IceSaw\\Scripts\\SSX Tricky\\LevelObjects\\Inspectors\\InstanceObjects.uxml");

        // Create a new VisualElement to be the root of our inspector UI
        VisualElement myInspector = new VisualElement();
        m_InspectorXML.CloneTree(myInspector);

        VisualElement inspectorGroup = myInspector.Q("Default_Inspector");

        MonoBehaviour monoBev = (MonoBehaviour)target;
        TrickyInstanceObject PatchObject = monoBev.GetComponent<TrickyInstanceObject>();

        TextElement Details = new TextElement();
        Details.style.fontSize = 16;
        Details.text = "Instance ID " + PatchObject.transform.GetSiblingIndex();

        inspectorGroup.Add(Details);

        InspectorElement.FillDefaultInspector(inspectorGroup, serializedObject, this);

        // Return the finished inspector UI
        return myInspector;
    }

    void OnSceneGUI()
    {
        
    }
}
