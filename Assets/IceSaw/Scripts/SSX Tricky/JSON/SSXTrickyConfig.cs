﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SSXMultiTool.JsonFiles.Tricky
{
    public class SSXTrickyConfig
    {
        public int Version = 1;
        public string LevelName = "";
        public string Author = "";
        public string LevelVersion = "";
        public string Difficulty = "";
        public string Location = "";
        public string Vertical = "";
        public string Length = "";
        public string Description = "";
        public string BuildPath = "";

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

        public static SSXTrickyConfig Load(string path)
        {
            string paths = path;
            if (File.Exists(paths))
            {
                var stream = File.ReadAllText(paths);
                var container = JsonConvert.DeserializeObject<SSXTrickyConfig>(stream);
                return container;
            }
            else
            {
                return new SSXTrickyConfig();
            }
        }

    }
}
