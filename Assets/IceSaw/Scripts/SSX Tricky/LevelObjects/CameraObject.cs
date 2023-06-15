using SSXMultiTool.JsonFiles.Tricky;
using SSXMultiTool.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SSXMultiTool.JsonFiles.Tricky.CameraJSONHandler;

public class CameraObject : MonoBehaviour
{
    public int Type;
    public float FocalLength;
    public float AspectRatio;
    public float[] Aperture;
    public float[] ClipPlane;
    public float[] IntrestPoint;
    public float[] UpVector;
    public float AnimTime;

    public float[] InitialPosition;
    public float[] InitalRotation;
    public float U0; //Big ?
    public List<CameraAnimationHeader> AnimationHeaders;

    public int Hash;


    public void LoadCamera(CameraJSONHandler.CameraInstance cameraInstance)
    {
        transform.name = cameraInstance.CameraName;
        transform.localPosition = JsonUtil.ArrayToVector3(cameraInstance.Translation);
        transform.localEulerAngles = JsonUtil.ArrayToVector3(cameraInstance.Rotation);

        Type = cameraInstance.Type;
        FocalLength = cameraInstance.FocalLength;
        AspectRatio = cameraInstance.AspectRatio;
        Aperture = cameraInstance.Aperture;
        ClipPlane = cameraInstance.ClipPlane;
        IntrestPoint = cameraInstance.IntrestPoint;
        UpVector = cameraInstance.UpVector;
        AnimTime = cameraInstance.AnimTime;

        InitialPosition = cameraInstance.InitialPosition;
        InitalRotation = cameraInstance.InitalRotation;
        U0 = cameraInstance.U0;

        AnimationHeaders = new List<CameraAnimationHeader>();

        for (int i = 0; i < cameraInstance.AnimationHeaders.Count; i++)
        {
            var NewHeader = new CameraAnimationHeader();

            NewHeader.Action = cameraInstance.AnimationHeaders[i].Action;
            NewHeader.AnimationDatas = new List<CameraAnimationData>();

            for (int a = 0; a < cameraInstance.AnimationHeaders[i].AnimationDatas.Count; a++)
            {
                var NewAnimationData = new CameraAnimationData();

                NewAnimationData.Translation = cameraInstance.AnimationHeaders[i].AnimationDatas[a].Translation;

                NewAnimationData.Rotation = cameraInstance.AnimationHeaders[i].AnimationDatas[a].Rotation;

                NewHeader.AnimationDatas.Add(NewAnimationData);
            }

            AnimationHeaders.Add(NewHeader);
        }


        Hash = cameraInstance.Hash;

    }








    public struct CameraAnimationHeader
    {
        public int Action; //Could also be offset

        public List<CameraAnimationData> AnimationDatas;
    }

    public struct CameraAnimationData
    {
        //Probably Wrong I'll figure it out
        public float[] Translation;
        public float[] Rotation;
    }

}
