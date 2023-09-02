using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResetShow
{
    [MenuItem("Reset Plugin/Export Track")]
    public static void AddResetTextures()
    {
        if (LevelManager.Instance != null)
        {
            var Texture2DData = new LevelManager.TextureData();

            Texture2DData.Name = "Reset.png";
            Texture2DData.Texture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets\\IceSaw\\Textures\\Reset.png", typeof(Texture2D));

            LevelManager.Instance.texture2Ds.Add(Texture2DData);

            Texture2DData = new LevelManager.TextureData();

            Texture2DData.Name = "Bounce.png";
            Texture2DData.Texture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets\\IceSaw\\Textures\\Bounce.png", typeof(Texture2D));

            LevelManager.Instance.texture2Ds.Add(Texture2DData);



            var TempPatchList = WorldManager.Instance.GetPatchList();

            for (int i = 0; i < TempPatchList.Length; i++)
            {
                if (TempPatchList[i].PatchStyle == 0)
                {
                    TempPatchList[i].TextureAssigment = "Reset.png";
                    TempPatchList[i].UpdateTexture();
                }

                if (TempPatchList[i].PatchStyle == 10)
                {
                    TempPatchList[i].TextureAssigment = "Bounce.png";
                    TempPatchList[i].UpdateTexture();
                }
            }

            GameObject gameObject = new GameObject("U");

            gameObject.transform.parent = PrefabManager.Instance.MaterialHolder.transform;

            var TempMaterial = gameObject.AddComponent<MaterialObject>();

            TempMaterial.TexturePath = "Reset.png";
            TempMaterial.UnknownInt2 = -1;
            TempMaterial.UnknownInt3 = -1;

            TempMaterial.UnknownFloat4 = 1f;

            TempMaterial.UnknownInt8 = 6;
            TempMaterial.UnknownFloat4 = 115628.9f;

            TempMaterial.UnknownInt3 = 86024;

            TempMaterial.UnknownInt20 = -1;

            int MaterialID = gameObject.transform.GetSiblingIndex();


            var InstanceList = WorldManager.Instance.GetInstanceList();
            var EffectSlots = LogicManager.Instance.GetEffectSlotsList();
            var EffectList = LogicManager.Instance.GetEffectHeadersList();
            var PrefabList = PrefabManager.Instance.GetPrefabList();

            for (int i = 0; i < InstanceList.Length; i++)
            {
                if (InstanceList[i].EffectSlotIndex != -1)
                {
                    if (EffectSlots[InstanceList[i].EffectSlotIndex].CollisionEffectSlot != -1 && EffectSlots[InstanceList[i].EffectSlotIndex].CollisionEffectSlot<EffectList.Count)
                    {
                        var Effect = EffectList[EffectSlots[InstanceList[i].EffectSlotIndex].CollisionEffectSlot];

                        var PersistantEffect = EffectList[EffectSlots[InstanceList[i].EffectSlotIndex].PersistantEffectSlot];
                        bool Crowd = false;

                        for (int a = 0; a < PersistantEffect.Effects.Count; a++)
                        {
                            if (PersistantEffect.Effects[a].MainType==0)
                            {
                                if(PersistantEffect.Effects[a].type0.Value.SubType==17)
                                {
                                    Crowd = true;
                                    break;
                                }
                            }
                        }


                        if (!Crowd)
                        {
                            for (int a = 0; a < Effect.Effects.Count; a++)
                            {
                                if (Effect.Effects[a].MainType == 13)
                                {
                                    InstanceList[i].Visable = true;
                                    var SubPrefab = PrefabList[InstanceList[i].ModelID].GetPrefabSubObject();

                                    for (int c = 0; c < SubPrefab.Length; c++)
                                    {
                                        var MeshPrefab = SubPrefab[c].GetPrefabMesh();

                                        for (int d = 0; d < MeshPrefab.Length; d++)
                                        {
                                            MeshPrefab[d].MaterialID = MaterialID;
                                            InstanceList[i].LoadPrefabs();
                                            MeshPrefab[d].GenerateModel();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

}
