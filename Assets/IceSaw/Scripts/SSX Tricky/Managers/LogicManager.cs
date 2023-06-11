using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour
{
    public static LogicManager Instance;
    [SerializeField]
    public SSFJsonHandler ssfJsonHandler;

    GameObject Effects;

    public void Awake()
    {
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
