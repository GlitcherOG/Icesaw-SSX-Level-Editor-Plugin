using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameCheckerJson
{
    public int Game = 0;
    public int Version = 0;

    public static GameCheckerJson Load(string path)
    {
        string paths = path;
        if (File.Exists(paths))
        {
            var stream = File.ReadAllText(paths);
            var container = JsonConvert.DeserializeObject<GameCheckerJson>(stream);
            return container;
        }
        else
        {
            return new GameCheckerJson();
        }
    }
}
