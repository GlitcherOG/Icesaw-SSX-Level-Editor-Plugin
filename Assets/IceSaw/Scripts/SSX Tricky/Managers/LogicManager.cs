using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LogicManager : MonoBehaviour
{
    public static LogicManager Instance;
    public SSFJsonHandler ssfJsonHandler;

    //GameObject Effects;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void GenerateEmptyObjects()
    {

    }

    public void LoadData(string path)
    {
        ssfJsonHandler = new SSFJsonHandler();
        ssfJsonHandler = SSFJsonHandler.Load(path + "\\SSFLogic.json");
    }

}
