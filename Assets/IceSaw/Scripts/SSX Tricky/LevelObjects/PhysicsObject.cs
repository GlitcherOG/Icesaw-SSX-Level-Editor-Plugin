using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public List<PhysicsData> PhysicsDatas;
    public void LoadPhysics(SSFJsonHandler.PhysicsHeader physicsHeader)
    {
        transform.name = physicsHeader.PhysicsName;
        PhysicsDatas = new List<PhysicsData>();

        for (int i = 0; i < physicsHeader.PhysicsDatas.Count; i++)
        {
            var NewPhysicsData = new PhysicsData();

            NewPhysicsData.U2 = physicsHeader.PhysicsDatas[i].U2;

            NewPhysicsData.UFloat0 = physicsHeader.PhysicsDatas[i].UFloat0;
            NewPhysicsData.UFloat1 = physicsHeader.PhysicsDatas[i].UFloat1;
            NewPhysicsData.UFloat2 = physicsHeader.PhysicsDatas[i].UFloat2;
            NewPhysicsData.UFloat3 = physicsHeader.PhysicsDatas[i].UFloat3;
            NewPhysicsData.UFloat4 = physicsHeader.PhysicsDatas[i].UFloat4;
            NewPhysicsData.UFloat5 = physicsHeader.PhysicsDatas[i].UFloat5;
            NewPhysicsData.UFloat6 = physicsHeader.PhysicsDatas[i].UFloat6;
            NewPhysicsData.UFloat7 = physicsHeader.PhysicsDatas[i].UFloat7;
            NewPhysicsData.UFloat8 = physicsHeader.PhysicsDatas[i].UFloat8;
            NewPhysicsData.UFloat9 = physicsHeader.PhysicsDatas[i].UFloat9;
            NewPhysicsData.UFloat10 = physicsHeader.PhysicsDatas[i].UFloat10;
            NewPhysicsData.UFloat11 = physicsHeader.PhysicsDatas[i].UFloat11;
            NewPhysicsData.UFloat12 = physicsHeader.PhysicsDatas[i].UFloat12;
            NewPhysicsData.UFloat13 = physicsHeader.PhysicsDatas[i].UFloat13;
            NewPhysicsData.UFloat14 = physicsHeader.PhysicsDatas[i].UFloat14;
            NewPhysicsData.UFloat15 = physicsHeader.PhysicsDatas[i].UFloat15;
            NewPhysicsData.UFloat16 = physicsHeader.PhysicsDatas[i].UFloat16;
            NewPhysicsData.UFloat17 = physicsHeader.PhysicsDatas[i].UFloat17;
            NewPhysicsData.UFloat18 = physicsHeader.PhysicsDatas[i].UFloat18;
            NewPhysicsData.UFloat19 = physicsHeader.PhysicsDatas[i].UFloat19;
            NewPhysicsData.UFloat20 = physicsHeader.PhysicsDatas[i].UFloat20;
            NewPhysicsData.UFloat21 = physicsHeader.PhysicsDatas[i].UFloat21;
            NewPhysicsData.UFloat22 = physicsHeader.PhysicsDatas[i].UFloat22;
            NewPhysicsData.UFloat23 = physicsHeader.PhysicsDatas[i].UFloat23;

            NewPhysicsData.uPhysicsStruct0 = new List<UPhysicsStruct>();
            for (int a = 0; a < physicsHeader.PhysicsDatas[i].uPhysicsStruct0.Count; a++)
            {
                var PhysicsStructData = new UPhysicsStruct();

                PhysicsStructData.U0 = physicsHeader.PhysicsDatas[i].uPhysicsStruct0[a].U0;
                PhysicsStructData.U1 = physicsHeader.PhysicsDatas[i].uPhysicsStruct0[a].U1;
                PhysicsStructData.U2 = physicsHeader.PhysicsDatas[i].uPhysicsStruct0[a].U2;

                NewPhysicsData.uPhysicsStruct0.Add(PhysicsStructData);
            }

            NewPhysicsData.UByteData = physicsHeader.PhysicsDatas[i].UByteData;

            PhysicsDatas.Add(NewPhysicsData);
        }
    }
    [System.Serializable]
    public struct PhysicsData
    {
        public int U2;

        public float UFloat0;
        public float UFloat1;
        public float UFloat2;
        public float UFloat3;
        public float UFloat4;
        public float UFloat5;
        public float UFloat6;
        public float UFloat7;
        public float UFloat8;
        public float UFloat9;
        public float UFloat10;
        public float UFloat11;
        public float UFloat12;
        public float UFloat13;
        public float UFloat14;
        public float UFloat15;
        public float UFloat16;
        public float UFloat17;
        public float UFloat18;
        public float UFloat19;
        public float UFloat20;
        public float UFloat21;
        public float UFloat22;
        public float UFloat23;

        public List<UPhysicsStruct> uPhysicsStruct0;

        public byte[] UByteData;
    }
    [System.Serializable]
    public struct UPhysicsStruct
    {
        public float U0;
        public float U1;
        public int U2;
    }
}
