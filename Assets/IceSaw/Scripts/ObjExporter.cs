using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using static LevelManager;

public class ObjExporter
{
    public static void ObjSave(string path, Mesh mesh)
    {
        string ObjData = "#Exported From Unity Level Editor Plugin\n";

        var Verts = mesh.vertices;
        var UV = mesh.uv;
        var Normal = mesh.normals;
        var Index = mesh.triangles;

        //Collapse Models
        var NewVertIndex = new List<int>();
        var NewUVIndex = new List<int>();
        var NewNormalIndex = new List<int>();
        var NewVerts = new List<Vector3>();
        var NewUV = new List<Vector2>();
        var NewNormal = new List<Vector3>();

        //Go Through All Things and do stuff

        for (int i = 0; i < Index.Length; i++)
        {
            int ID = Index[i];

            if (NewVerts.Contains(Verts[ID]))
            {
                NewVertIndex.Add(NewVerts.IndexOf(Verts[ID])+1);
            }
            else
            {
                NewVerts.Add(Verts[ID]);
                NewVertIndex.Add(NewVertIndex.Count);
            }

            if (UV.Length != 0)
            {
                if (NewUV.Contains(UV[ID]))
                {
                    NewUVIndex.Add(NewUV.IndexOf(UV[ID])+1);
                }
                else
                {
                    NewUV.Add(UV[ID]);
                    NewUVIndex.Add(NewUVIndex.Count);
                }
            }

            if (NewNormal.Contains(Normal[ID]))
            {
                NewNormalIndex.Add(NewNormal.IndexOf(Normal[ID])+1);
            }
            else
            {
                NewNormal.Add(Normal[ID]);
                NewNormalIndex.Add(NewNormalIndex.Count);
            }
        }

        for (int i = 0; i < NewVerts.Count; i++)
        {
            ObjData += "v " + NewVerts[i].x + " " + NewVerts[i].y + " " + NewVerts[i].z + "\n";
        }

        for (int i = 0; i < NewUV.Count; i++)
        {
            ObjData += "vt " + NewUV[i].x + " " + NewUV[i].y + "\n";
        }

        for (int i = 0; i < NewNormal.Count; i++)
        {
            ObjData += "vn " + NewNormal[i].x + " " + NewNormal[i].y + " " + NewNormal[i].z + "\n";
        }
        ObjData += "o Mesh0\n";
        for (int i = 0; i < NewVertIndex.Count/3; i++)
        {
            if (NewUV.Count != 0)
            {
                ObjData += "f " + NewVertIndex[i * 3] + "/" + NewUVIndex[i * 3] + "/" + NewNormalIndex[i * 3] + " "
                    + NewVertIndex[i * 3 + 1] + "/" + NewUVIndex[i * 3 + 1] + "/" + NewNormalIndex[i * 3 + 1] + " "
                    + NewVertIndex[i * 3 + 2] + "/" + NewUVIndex[i * 3 + 2] + "/" + NewNormalIndex[i * 3 + 2] + "\n";
            }
            else
            {
                ObjData += "f " + NewVertIndex[i * 3] + "//" + NewNormalIndex[i * 3] + " "
                    + NewVertIndex[i * 3 + 1] + "//" + NewNormalIndex[i * 3 + 1] + " "
                    + NewVertIndex[i * 3 + 2] + "//" + NewNormalIndex[i * 3 + 2] + "\n";
            }
        }

        File.WriteAllText(path, ObjData);
    }

    public static void GenerateMTL(string path, List<LinkerData> textureData)
    {
        string ObjData = "#Exported From Unity Level Editor Plugin\n";
        for (int i = 0; i < textureData.Count; i++)
        {
            ObjData += "newmtl " + textureData[i].ItemName +"\n";
            ObjData += "map_Ka " + textureData[i].ItemLocation + "\n";
            ObjData += "map_Kd " + textureData[i].ItemLocation + "\n";   
        }
        File.WriteAllText(path, ObjData);
    }

    public static void SaveModelList(string SavePath, List<MassModelData> MMD, List<TextureData> textures)
    {
        string FileNameMain = Path.GetFileNameWithoutExtension(SavePath);
        //String Path down to just folder location
        SavePath = Path.GetDirectoryName(SavePath);

        Directory.CreateDirectory(SavePath + "\\" + FileNameMain + " Textures\\");
        
        //Create Texture Folder and Create Materials for every Texture
        List<LinkerData> LinkerDataList = new List<LinkerData>();
        for (int i = 0; i < textures.Count; i++)
        {
            var TempTexture = textures[i];
            MemoryStream stream = new MemoryStream();
            stream.Write(TempTexture.Texture.EncodeToPNG());

            string FileName = TempTexture.Name;

            if(!FileName.ToLower().EndsWith(".png"))
            {
                FileName = FileName + ".png";
            }

            if(File.Exists(FileName))
            {
                File.Delete(FileName);
            }

            var file = File.Create(SavePath + "\\"+FileNameMain+" Textures\\" + FileName);
            stream.Position = 0;
            stream.CopyTo(file);
            file.Close();
            stream.Dispose();

            LinkerData TempData = new LinkerData();
            TempData.ItemName = textures[i].Name.ToLower().Replace(".png", "");
            TempData.ItemLocation = "\\"+FileNameMain+" Textures\\" + FileName;
            LinkerDataList.Add(TempData);
        }
        GenerateMTL(SavePath+"\\" + FileNameMain + ".mtl", LinkerDataList);

        //Save Models in Chunks of 500?
        int ModelPos = 0;
        int FileID = 0;
        while (ModelPos < MMD.Count)
        {
            int ReadSize = 500;
            int CalMaths = ModelPos + ReadSize;

            if (CalMaths > MMD.Count)
            {
                CalMaths = MMD.Count;
            }
            List<MassModelData> massModelDatas = new List<MassModelData>();
            for (int i = ModelPos; i < CalMaths; i++)
            {
                var ModelData = MMD[i];
                massModelDatas.Add(ModelData);
                ModelPos++;
            }

            MassObjSave(SavePath + "\\" + FileNameMain + FileID + ".obj", massModelDatas, FileNameMain + ".mtl");
            FileID++;
        }
    }

    static void MassObjSave(string path, List<MassModelData> MMD, string MLTPath)
    {
        string ObjData = "#Exported From Unity Level Editor Plugin\n";
        ObjData += "mtllib " + MLTPath + "\n";

        //Collapse Models
        var NewVerts = new List<Vector3>();
        var NewUV = new List<Vector2>();
        var NewNormal = new List<Vector3>();
        var ModelFaces = new List<string>();

        for (int a = 0; a < MMD.Count; a++)
        {
            string Data = "";
            var NewVertIndex = new List<int>();
            var NewUVIndex = new List<int>();
            var NewNormalIndex = new List<int>();
            var Verts = MMD[a].Model.vertices;
            var UV = MMD[a].Model.uv;
            var Normal = MMD[a].Model.normals;
            var Index = MMD[a].Model.triangles;

            //Go Through All Things and do stuff
            //Alter this so it adds all mesh stuff leaving New Main Lists alone
            for (int i = 0; i < Index.Length; i++)
            {
                int ID = Index[i];

                if (NewVerts.Contains(Verts[ID]))
                {
                    NewVertIndex.Add(NewVerts.IndexOf(Verts[ID]) + 1);
                }
                else
                {
                    NewVerts.Add(Verts[ID]);
                    NewVertIndex.Add(NewVerts.Count - 1);
                }

                if (UV.Length != 0)
                {
                    if (NewUV.Contains(UV[ID]))
                    {
                        NewUVIndex.Add(NewUV.IndexOf(UV[ID]) + 1);
                    }
                    else
                    {
                        NewUV.Add(UV[ID]);
                        NewUVIndex.Add(NewUV.Count - 1);
                    }
                }

                if (NewNormal.Contains(Normal[ID]))
                {
                    NewNormalIndex.Add(NewNormal.IndexOf(Normal[ID]) + 1);
                }
                else
                {
                    NewNormal.Add(Normal[ID]);
                    NewNormalIndex.Add(NewNormal.Count - 1);
                }
            }

            Data += "o " + MMD[a].Name + "\n";
            Data += "usemtl " + MMD[a].TextureName.ToLower().Replace(".png", "") + "\n";

            for (int i = 0; i < NewVertIndex.Count / 3; i++)
            {
                if (NewUV.Count != 0)
                {
                    Data += "f " + NewVertIndex[i * 3] + "/" + NewUVIndex[i * 3] + "/" + NewNormalIndex[i * 3] + " "
                        + NewVertIndex[i * 3 + 1] + "/" + NewUVIndex[i * 3 + 1] + "/" + NewNormalIndex[i * 3 + 1] + " "
                        + NewVertIndex[i * 3 + 2] + "/" + NewUVIndex[i * 3 + 2] + "/" + NewNormalIndex[i * 3 + 2] + "\n";
                }
                else
                {
                    Data += "f " + NewVertIndex[i * 3] + "//" + NewNormalIndex[i * 3] + " "
                        + NewVertIndex[i * 3 + 1] + "//" + NewNormalIndex[i * 3 + 1] + " "
                        + NewVertIndex[i * 3 + 2] + "//" + NewNormalIndex[i * 3 + 2] + "\n";
                }
            }
            ModelFaces.Add(Data);
        }
        

        for (int i = 0; i < NewVerts.Count; i++)
        {
            ObjData += "v " + NewVerts[i].x + " " + NewVerts[i].y + " " + NewVerts[i].z + "\n";
        }

        for (int i = 0; i < NewUV.Count; i++)
        {
            ObjData += "vt " + NewUV[i].x + " " + NewUV[i].y + "\n";
        }

        for (int i = 0; i < NewNormal.Count; i++)
        {
            ObjData += "vn " + NewNormal[i].x + " " + NewNormal[i].y + " " + NewNormal[i].z + "\n";
        }

        for (int i = 0; i < ModelFaces.Count; i++)
        {
            ObjData += ModelFaces[i];
        }

        File.WriteAllText(path, ObjData);
    }

    public struct MassModelData
    {
        public string Name;
        public Mesh Model;
        public string TextureName;
    }

    public struct LinkerData
    {
        public string ItemName;
        public string ItemLocation;
        //Might need mtl name but idk
    }
}
