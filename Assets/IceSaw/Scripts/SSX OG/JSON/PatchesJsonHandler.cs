﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSXMultiTool.JsonFiles.SSXOG
{
    [Serializable]
    public class PatchesJsonHandler
    {
        public List<PatchJson> Patches = new List<PatchJson>();

        public void CreateJson(string path, bool Inline = false)
        {
            var TempFormating = Formatting.None;
            if (Inline)
            {
                TempFormating = Formatting.Indented;
            }

            var serializer = JsonConvert.SerializeObject(this, TempFormating);
            File.WriteAllText(path, serializer);
        }

        public static PatchesJsonHandler Load(string path)
        {
            string paths = path;
            if (File.Exists(paths))
            {
                var stream = File.ReadAllText(paths);
                var container = JsonConvert.DeserializeObject<PatchesJsonHandler>(stream);
                return container;
            }
            else
            {
                return new PatchesJsonHandler();
            }
        }


        [Serializable]
        public struct PatchJson
        {
            public string PatchName;

            public float[] LightMapPoint;
            public float[,] UVPoints;
            public float[,] Points;

            public int PatchStyle;
            public string TexturePath;
            public int LightmapID;
        }
    }
}
