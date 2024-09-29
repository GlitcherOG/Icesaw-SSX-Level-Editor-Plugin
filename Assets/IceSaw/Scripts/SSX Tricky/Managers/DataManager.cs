using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public TrickyLevelManager TrickyLevelManager;

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
    public List<TrickyMaterialObject> trickySkyboxMaterialObjects;
    public List<TrickyPrefabObject> trickySkyboxPrefabObjects;
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

}
