using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager
{
    #region Prefab Manager
    public List<TrickyPrefabObject> trickyPrefabObjects;
    public List<TrickyPrefabSubObject> trickyPrefabSubObjects;
    public List<PrefabMeshObject> prefabMeshObjects;
 
    public List<TrickyMaterialObject> trickyMaterialObjects;
    public List<ParticlePrefabObject> particlePrefabObjects;
    #endregion

    #region Word Objects
    public List<TrickyPatchObject> trickyPatchObjects;
    public List <TrickyInstanceObject> trickyInstances;
    public List<TrickySplineObject> trickySplineObjects;
    public List<PaticleInstanceObject> paticleInstanceObjects;
    public List<LightObject> lightObjects;
    #endregion

    #region Skybox
    public List<TrickySkyboxMaterialObject> trickySkyboxMaterialObjects;
    public List<TrickySkyboxPrefabObject> trickySkyboxPrefabObjects;
    public List<TrickyPrefabSkyboxSubObject> trickyPrefabSkyboxSubObjects;
    public List<PrefabSkyboxMeshObject> prefabSkyboxMeshObjects;
    #endregion

    #region Logic
    public List<TrickyEffectHeader> trickyEffectHeaders;
    public List<PhysicsObject> trickyPhysicsObjects;
    public List<TrickyFunctionHeader> trickyFunctionHeaders;
    #endregion

    #region Paths
    public PathManager trickyGeneralPaths;
    public PathManager trickyShowoffPaths;
    #endregion

    List<TrickyBaseObject> trickyBaseObjects = new List<TrickyBaseObject>();

    public void RefreshObjectList()
    {
        trickyPrefabObjects = new List<TrickyPrefabObject>();
        trickyPrefabSubObjects= new List<TrickyPrefabSubObject>();
        prefabMeshObjects = new List<PrefabMeshObject>();

        trickyMaterialObjects = new List<TrickyMaterialObject>();
        particlePrefabObjects = new List<ParticlePrefabObject>();

        trickyPatchObjects = new List<TrickyPatchObject>();
        trickyInstances = new List<TrickyInstanceObject>();
        trickySplineObjects = new List<TrickySplineObject>();
        paticleInstanceObjects = new List<PaticleInstanceObject>();
        lightObjects = new List<LightObject>();

        trickySkyboxMaterialObjects = new List<TrickySkyboxMaterialObject>();
        trickySkyboxPrefabObjects = new List<TrickySkyboxPrefabObject>();

        trickyEffectHeaders = new List<TrickyEffectHeader>();
        trickyPhysicsObjects = new List<PhysicsObject>();
        trickyFunctionHeaders = new List<TrickyFunctionHeader>();

        trickyGeneralPaths = new PathManager();
        trickyShowoffPaths = new PathManager();

        trickyBaseObjects = new List<TrickyBaseObject>();

        var Scene = SceneManager.GetActiveScene();
        var ObjectList = Scene.GetRootGameObjects();
        //CAN PROBABLY SWAP OUT WITH GET COMPONENT IN CHILDREN
        for (int i = 0; i < ObjectList.Length; i++)
        {
            if (ObjectList[i].GetComponent<TrickyBaseObject>() != null)
            {
                trickyBaseObjects.Add(ObjectList[i].GetComponent<TrickyBaseObject>());
            }
            if(ObjectList[i].transform.childCount != 0)
            {
                GetChildTrickyBaseChild(ObjectList[i]);
            }
        }

        for (int i = 0; i < trickyBaseObjects.Count; i++)
        {
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Prefab)
            {
                trickyPrefabObjects.Add((TrickyPrefabObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.PrefabSub)
            {
                trickyPrefabSubObjects.Add((TrickyPrefabSubObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.PrefabMesh)
            {
                prefabMeshObjects.Add((PrefabMeshObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Material)
            {
                trickyMaterialObjects.Add((TrickyMaterialObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.ParticlePrefab)
            {
                particlePrefabObjects.Add((ParticlePrefabObject)trickyBaseObjects[i]);
            }


            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Patch)
            {
                trickyPatchObjects.Add((TrickyPatchObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Instance)
            {
                trickyInstances.Add((TrickyInstanceObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Spline)
            {
                trickySplineObjects.Add((TrickySplineObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Particle)
            {
                paticleInstanceObjects.Add((PaticleInstanceObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Light)
            {
                lightObjects.Add((LightObject)trickyBaseObjects[i]);
            }


            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.SkyboxMaterial)
            {
                trickySkyboxMaterialObjects.Add((TrickySkyboxMaterialObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.SkyboxPrefab)
            {
                trickySkyboxPrefabObjects.Add((TrickySkyboxPrefabObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.SkyboxPrefabSub)
            {
                trickyPrefabSkyboxSubObjects.Add((TrickyPrefabSkyboxSubObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.SkyboxPrefabMesh)
            {
                prefabSkyboxMeshObjects.Add((PrefabSkyboxMeshObject)trickyBaseObjects[i]);
            }

            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Effect)
            {
                trickyEffectHeaders.Add((TrickyEffectHeader)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Physics)
            {
                trickyPhysicsObjects.Add((PhysicsObject)trickyBaseObjects[i]);
            }
            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.Function)
            {
                trickyFunctionHeaders.Add((TrickyFunctionHeader)trickyBaseObjects[i]);
            }

            if (trickyBaseObjects[i].Type == TrickyBaseObject.ObjectType.PathManager)
            {
                if(((PathManager)trickyBaseObjects[i]).pathManagerType == PathManager.PathManagerType.General)
                {
                    trickyGeneralPaths = (PathManager)trickyBaseObjects[i];
                }

                if (((PathManager)trickyBaseObjects[i]).pathManagerType == PathManager.PathManagerType.Showoff)
                {
                    trickyShowoffPaths = (PathManager)trickyBaseObjects[i];
                }
            }
        }
    }

    public void GetChildTrickyBaseChild(GameObject gameObject)
    {
        //CAN PROBABLY SWAP OUT WITH GET COMPONENT IN CHILDREN
        var ChildCount = gameObject.transform.childCount;

        for (int i = 0; i < ChildCount; i++)
        {
            var ChildObject = gameObject.transform.GetChild(i);

            if (ChildObject.GetComponent<TrickyBaseObject>() != null)
            {
                trickyBaseObjects.Add(ChildObject.GetComponent<TrickyBaseObject>());
            }
            if (ChildObject.transform.childCount != 0)
            {
                GetChildTrickyBaseChild(ChildObject.gameObject);
            }
        }
    }


}
