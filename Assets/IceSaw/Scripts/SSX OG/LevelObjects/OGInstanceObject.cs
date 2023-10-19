using SSXMultiTool.JsonFiles.SSXOG;
using SSXMultiTool.Utilities;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OGInstanceObject : MonoBehaviour
{
    public int U2;
    public int U3;
    [OnChangedCall("LoadPrefabs")]
    public int PrefabID;

    public int U5; //16
    public int U6; //16

    public float U7;

    public int U8; //16
    public int U9; //16
    public int U10; //16
    public int U11; //16

    public float U12;
    public int U13;

    public int U14;
    public int U15;
    public int U16;
    public int U17;

    public GameObject Prefab;

    public void LoadInstance(InstanceJsonHandler.InstanceJson instance)
    {
        transform.localPosition = JsonUtil.ArrayToVector3(instance.Location);
        transform.localRotation = JsonUtil.ArrayToQuaternion(instance.Rotation);
        transform.localScale = JsonUtil.ArrayToVector3(instance.Scale);

        U2 = instance.U2;
        U3 = instance.U3;
        PrefabID = instance.PrefabID;

        U5 = instance.U5;
        U6 = instance.U6;

        U7 = instance.U7;

        U8 = instance.U8;
        U9 = instance.U9;
        U10 = instance.U10;
        U11 = instance.U11;

        U12 = instance.U12;
        U13 = instance.U13;

        U14 = instance.U14;
        U15 = instance.U15;
        U16 = instance.U16;
        U17 = instance.U17;
        LoadPrefabs();
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
            Prefab.AddComponent<SelectParent>();

            var TempPrefablist = Prefab.transform.childCount;
            for (int i = 1; i < TempPrefablist; i++)
            {
                var TempChildPrefab = Prefab.transform.GetChild(i);

                TempChildPrefab.AddComponent<SelectParent>();

            }
        }
        Prefab.SetActive(OGWorldManager.Instance.ShowInstanceModels);
    }
}
