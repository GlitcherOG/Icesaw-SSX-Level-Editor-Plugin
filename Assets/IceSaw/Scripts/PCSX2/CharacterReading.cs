using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class CharacterReading : MonoBehaviour
{
    public string StringAddress;
    public long CharacterAddress { get { return Convert.ToInt32(StringAddress, 16); } }

    public List<GameObject> CharacterBoneList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PCSX2Manager.Instance != null)
        {
            if (PCSX2Manager.Instance.active && CharacterBoneList.Count!=0)
            {
                float[] Location = PCSX2Manager.Instance.processMemory.ReadFloats(CharacterAddress, 16* CharacterBoneList.Count);

                for (int i = 0; i < CharacterBoneList.Count; i++)
                {
                    Matrix4x4 matrix4X4 = new Matrix4x4();
                    matrix4X4.m00 = Location[0+i*16];
                    matrix4X4.m01 = Location[1 + i * 16];
                    matrix4X4.m02 = Location[2 + i * 16];
                    matrix4X4.m03 = Location[3 + i * 16];
                    matrix4X4.m10 = Location[4 + i * 16];
                    matrix4X4.m11 = Location[5 + i * 16];
                    matrix4X4.m12 = Location[6 + i * 16];
                    matrix4X4.m13 = Location[7 + i * 16];
                    matrix4X4.m20 = Location[8 + i * 16];
                    matrix4X4.m21 = Location[9 + i * 16];
                    matrix4X4.m22 = Location[10 + i * 16];
                    matrix4X4.m23 = Location[11 + i * 16];
                    matrix4X4.m30 = Location[12 + i * 16];
                    matrix4X4.m31 = Location[13 + i * 16];
                    matrix4X4.m32 = Location[14 + i * 16];
                    matrix4X4.m33 = Location[15 + i * 16];
                    
                    if(matrix4X4.ValidTRS())
                    {
                        Vector3 pos = new Vector3(matrix4X4.m03, matrix4X4.m13, matrix4X4.m23);
                        Quaternion rotation = matrix4X4.rotation;
                        Vector3 scale = matrix4X4.lossyScale;

                        transform.localPosition = pos;
                        transform.localRotation = rotation;
                        transform.localScale = scale;
                    }

                }
            }
        }
    }
}
