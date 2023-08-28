using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ObjExporter
{
    void ObjSave(string path, Mesh mesh)
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
                NewVertIndex.Add(NewVerts.IndexOf(Verts[ID]));
            }
            else
            {
                NewVerts.Add(Verts[ID]);
                NewVertIndex.Add(NewVertIndex.Count - 1);
            }

            if (UV.Length != 0)
            {
                if (NewUV.Contains(UV[ID]))
                {
                    NewUVIndex.Add(NewUV.IndexOf(UV[ID]));
                }
                else
                {
                    NewUV.Add(UV[ID]);
                    NewUVIndex.Add(NewUVIndex.Count - 1);
                }
            }

            if (NewNormal.Contains(Normal[ID]))
            {
                NewNormalIndex.Add(NewNormal.IndexOf(Normal[ID]));
            }
            else
            {
                NewNormal.Add(Normal[ID]);
                NewNormalIndex.Add(NewNormalIndex.Count - 1);
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

    public void GenerateMTL(string path, string texturePath)
    {
        string ObjData = "#Exported From Unity Level Editor Plugin\n";
        ObjData += "newmtl Texture\n";
        ObjData += "map_Ka " + texturePath + "\n";
        ObjData += "map_Kd " + texturePath + "\n";
        File.WriteAllText(path, ObjData);
    }

    public void SaveModelList(string SavePath, List<MassModelData> MMD, List<TextureData> textures)
    {
        //String Path down to just folder location

        //Create Texture Folder
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

            var file = File.Create(SavePath + "\\Textures\\" + FileName);
            stream.Position = 0;
            stream.CopyTo(file);
            file.Close();
            stream.Dispose();
        }

        //Create Materials for every Texture
        for (int i = 0; i < textures.Count; i++)
        {


        }    

        //Save Models in Chunks of 500?
    }

    public struct MassModelData
    {
        public string Name;
        public Mesh Model;
        public string TextureName;
    }
}
