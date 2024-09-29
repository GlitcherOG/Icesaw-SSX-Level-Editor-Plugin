using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager
{
    #region Prefab Manager
    public List<TrickyPrefabObject> trickyPrefabObjects;
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
    #endregion

    #region Logic
    public List<TrickyEffectHeader> trickyEffectHeaders;
    public List<PhysicsObject> trickyPhysicsObjects;
    public List<TrickyFunctionHeader> trickyFunctionHeaders;
    #endregion

    #region Paths
    public PathManager trickyGeneralPaths;
    public PathManager trickyShowoffPath;
    #endregion

    List<TrickyBaseObject> trickyBaseObjects = new List<TrickyBaseObject>();

    public void RefreshObjectList()
    {
        trickyPrefabObjects = new List<TrickyPrefabObject>();
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
        trickyShowoffPath = new PathManager();

        trickyBaseObjects = new List<TrickyBaseObject>();

        var Scene = SceneManager.GetActiveScene();
        var ObjectList = Scene.GetRootGameObjects();
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
        }
    }

    public void GetChildTrickyBaseChild(GameObject gameObject)
    {
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
