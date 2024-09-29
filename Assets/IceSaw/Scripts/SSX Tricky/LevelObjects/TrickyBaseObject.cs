using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickyBaseObject : MonoBehaviour
{
    [HideInInspector]
    public virtual ObjectType Type
    {
        get { return ObjectType.None; }
    }
    public enum ObjectType
    {
        None,
        Patch,
        Instance,
        Light,
        Particle,
        Physics,
        Camera,
        EffectSlot,
        Material,
        Spline,
        Prefab,
        ParticlePrefab,
        PathA,
        PathB,
        Effect
    }
}
