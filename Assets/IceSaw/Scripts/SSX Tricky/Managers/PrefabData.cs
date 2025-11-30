using Newtonsoft.Json;
using SSXMultiTool.JsonFiles.Tricky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class PrefabData : MonoBehaviour
{
    public SSXTrickyConfig config = new SSXTrickyConfig();
    public CameraJSONHandler cameraJSONHandler;
    public InstanceJsonHandler instanceJSONHandler;
    public LightJsonHandler lightJSONHandler;
    public MaterialJsonHandler materialJSONHandler;
    public ParticleInstanceJsonHandler particleJSONHandler;
    public ParticleModelJsonHandler particleModelJSONHandler;
    public PatchesJsonHandler patchesJSONHandler;
    public ModelJsonHandler prefabJSONHandler;
    public SplineJsonHandler splineJSONHandler;
    public SSFJsonHandler sSFJSONHandler;

    public void CreateJson(string path, bool Inline = false)
    {
        var TempFormating = Formatting.None;
        if (Inline)
        {
            TempFormating = Formatting.Indented;
        }

        var serializer = JsonConvert.SerializeObject(this, TempFormating, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });
        File.WriteAllText(path, serializer);
    }

    public static PrefabData Load(string path)
    {
        string paths = path;
        if (File.Exists(paths))
        {
            var stream = File.ReadAllText(paths);
            var container = JsonConvert.DeserializeObject<PrefabData>(stream);
            return container;
        }
        else
        {
            return new PrefabData();
        }
    }
}
