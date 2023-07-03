using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParticlePrefabObject : MonoBehaviour
{
     public List<ParticleObject> ParticleObjects;

    public void LoadParticle(ParticleModelJsonHandler.ParticleModelJson particleModel)
    {
        transform.name = particleModel.ParticleModelName;
        ParticleObjects = new List<ParticleObject>();
        for (int i = 0; i < particleModel.ParticleObjectHeaders.Count; i++)
        {
            var NewHeader = new ParticleObject();

            NewHeader.LowestXYZ = JsonUtil.ArrayToVector3(particleModel.ParticleObjectHeaders[i].ParticleObject.LowestXYZ);
            NewHeader.HighestXYZ = JsonUtil.ArrayToVector3(particleModel.ParticleObjectHeaders[i].ParticleObject.HighestXYZ);
            NewHeader.U1 = particleModel.ParticleObjectHeaders[i].ParticleObject.U1;

            NewHeader.AnimationFrames = new List<AnimationFrames>();

            for (int a = 0; a < particleModel.ParticleObjectHeaders[i].ParticleObject.AnimationFrames.Count; a++)
            {
                var NewAnimation = new AnimationFrames();

                NewAnimation.Position = JsonUtil.ArrayToVector3(particleModel.ParticleObjectHeaders[i].ParticleObject.AnimationFrames[a].Position);
                NewAnimation.Rotation = JsonUtil.ArrayToVector3(particleModel.ParticleObjectHeaders[i].ParticleObject.AnimationFrames[a].Rotation);
                NewAnimation.Unknown = particleModel.ParticleObjectHeaders[i].ParticleObject.AnimationFrames[a].Unknown;
                NewHeader.AnimationFrames.Add(NewAnimation);
            }


            ParticleObjects.Add(NewHeader);
        }
    
    }

    public ParticleModelJsonHandler.ParticleModelJson GenerateParticle()
    {
        ParticleModelJsonHandler.ParticleModelJson jsonHandler = new ParticleModelJsonHandler.ParticleModelJson();

        jsonHandler.ParticleModelName = transform.name;

        jsonHandler.ParticleObjectHeaders = new List<ParticleModelJsonHandler.ParticleObjectHeader>();

        for (int i = 0; i < ParticleObjects.Count; i++)
        {
            var TempParticle = new ParticleModelJsonHandler.ParticleObjectHeader();

            TempParticle.ParticleObject = new ParticleModelJsonHandler.ParticleObject();

            TempParticle.ParticleObject.LowestXYZ = JsonUtil.Vector3ToArray(ParticleObjects[i].LowestXYZ);
            TempParticle.ParticleObject.HighestXYZ = JsonUtil.Vector3ToArray(ParticleObjects[i].HighestXYZ);
            TempParticle.ParticleObject.U1 = ParticleObjects[i].U1;

            TempParticle.ParticleObject.AnimationFrames = new List<ParticleModelJsonHandler.AnimationFrames>();

            for (int a = 0; a < ParticleObjects[i].AnimationFrames.Count; a++)
            {
                var NewAnimationFrame = new ParticleModelJsonHandler.AnimationFrames();

                NewAnimationFrame.Rotation = JsonUtil.Vector3ToArray(ParticleObjects[i].AnimationFrames[a].Rotation);
                NewAnimationFrame.Position = JsonUtil.Vector3ToArray(ParticleObjects[i].AnimationFrames[a].Position);
                NewAnimationFrame.Unknown = ParticleObjects[i].AnimationFrames[a].Unknown;

                TempParticle.ParticleObject.AnimationFrames.Add(NewAnimationFrame);
            }
            jsonHandler.ParticleObjectHeaders.Add(TempParticle);
        }

        return jsonHandler;
    }

    [System.Serializable]
    public struct ParticleObject
    {
        public Vector3 LowestXYZ;
        public Vector3 HighestXYZ;
        public int U1;

        public List<AnimationFrames> AnimationFrames;
    }

    [System.Serializable]
    public struct AnimationFrames
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public float Unknown;
    }
}
