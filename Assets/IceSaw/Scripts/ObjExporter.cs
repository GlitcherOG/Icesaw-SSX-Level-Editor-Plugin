using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        for (int i = 0; i < Verts.Length; i++)
        {
            ObjData += "v " + Verts[i].x + " " + Verts[i].y + " " + Verts[i].z + "\n";
        }

        for (int i = 0; i < UV.Length; i++)
        {
            ObjData += "vt " + UV[i].x + " " + UV[i].y + "\n";
        }

        for (int i = 0; i < Normal.Length; i++)
        {
            ObjData += "vn " + Normal[i].x + " " + Normal[i].y + " " + Normal[i].z + "\n";
        }

        for (int i = 0; i < Index.Length/3; i++)
        {
            ObjData += "f " + Index[i * 3] + "/" + Index[i * 3] + "/" + Index[i * 3] + " " 
                + Index[i * 3 +1] + "/" + Index[i * 3+1] + "/" + Index[i * 3+1] + " " 
                + Index[i * 3+2] + "/" + Index[i * 3+2] + "/" + Index[i * 3+2] + "\n";
        }

        File.WriteAllText(path, ObjData);
    }
}
