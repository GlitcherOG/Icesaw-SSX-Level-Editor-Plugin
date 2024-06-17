using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using static TrickyLevelManager;

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
                NewVertIndex.Add(NewVerts.Count);
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
                    NewUVIndex.Add(NewUV.Count);
                }
            }

            if (NewNormal.Contains(Normal[ID]))
            {
                NewNormalIndex.Add(NewNormal.IndexOf(Normal[ID])+1);
            }
            else
            {
                NewNormal.Add(Normal[ID]);
                NewNormalIndex.Add(NewNormal.Count);
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

        File.AppendAllText(path, ObjData);
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
        int ReadSize = 500;

        while (ModelPos < MMD.Count)
        {
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
        StringBuilder sb = new StringBuilder("#Exported From Unity Level Editor Plugin\n");
        StringBuilder sb1 = new StringBuilder();

        sb.Append("mtllib " + MLTPath + "\n");
        //ObjData += "mtllib " + MLTPath + "\n";

        //Collapse Models
        var NewVerts = new List<Vector3>();
        var NewUV = new List<Vector2>();
        var NewNormal = new List<Vector3>();

        for (int a = 0; a < MMD.Count; a++)
        {
            //string Data = "";
            //StringBuilder sb1 = new StringBuilder();
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
                    NewVertIndex.Add(NewVerts.Count);
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
                        NewUVIndex.Add(NewUV.Count);
                    }
                }

                if (NewNormal.Contains(Normal[ID]))
                {
                    NewNormalIndex.Add(NewNormal.IndexOf(Normal[ID]) + 1);
                }
                else
                {
                    NewNormal.Add(Normal[ID]);
                    NewNormalIndex.Add(NewNormal.Count);
                }
            }

            sb1.Append("o " + MMD[a].Name + "\n");
            sb1.Append("usemtl " + MMD[a].TextureName.ToLower().Replace(".png", "") + "\n");

            for (int i = 0; i < NewVertIndex.Count / 3; i++)
            {
                if (NewUV.Count != 0)
                {
                    sb1.Append("f " + NewVertIndex[i * 3] + "/" + NewUVIndex[i * 3] + "/" + NewNormalIndex[i * 3] + " "
                        + NewVertIndex[i * 3 + 1] + "/" + NewUVIndex[i * 3 + 1] + "/" + NewNormalIndex[i * 3 + 1] + " "
                        + NewVertIndex[i * 3 + 2] + "/" + NewUVIndex[i * 3 + 2] + "/" + NewNormalIndex[i * 3 + 2] + "\n");
                }
                else
                {
                    sb1.Append("f " + NewVertIndex[i * 3] + "//" + NewNormalIndex[i * 3] + " "
                        + NewVertIndex[i * 3 + 1] + "//" + NewNormalIndex[i * 3 + 1] + " "
                        + NewVertIndex[i * 3 + 2] + "//" + NewNormalIndex[i * 3 + 2] + "\n");
                }
            }
            //ModelFaces.Add(sb1.ToString());
        }
        

        for (int i = 0; i < NewVerts.Count; i++)
        {
            sb.Append("v " + NewVerts[i].x + " " + NewVerts[i].y + " " + NewVerts[i].z + "\n");
        }

        for (int i = 0; i < NewUV.Count; i++)
        {
            sb.Append("vt " + NewUV[i].x + " " + NewUV[i].y + "\n");
        }

        for (int i = 0; i < NewNormal.Count; i++)
        {
            sb.Append("vn " + NewNormal[i].x + " " + NewNormal[i].y + " " + NewNormal[i].z + "\n");
        }

        sb.Append(sb1);
        File.AppendAllText(path, sb.ToString());
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
