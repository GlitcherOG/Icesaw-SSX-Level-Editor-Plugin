using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static TrickyLevelManager;

[ExecuteInEditMode]
public class GenerateFence : MonoBehaviour
{
    public List<Vector3> Points;
    public float ZSize;
    public bool FlipTextureY;
    public int TextureID;

    [ContextMenu("DrawMesh")]
    public void GenerateMesh()
    {
        for (int i = 0; i < Points.Count-1; i++)
        {
            Mesh mesh = GenerateFenceSection(i);

            GameObject gameObject = new GameObject(i.ToString());
            gameObject.transform.parent = transform;
            gameObject.transform.localRotation = new Quaternion(0, 0, 0, 1);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            gameObject.transform.localPosition = new Vector3(0,0,0);

            var TempMaterial = new Material(Shader.Find("ModelShader"));
            Material mat = new Material(TempMaterial);

            Texture2D texture = null;
            if (TextureID<TrickyLevelManager.Instance.texture2Ds.Count)
            {
                texture = TrickyLevelManager.Instance.texture2Ds[TextureID].Texture;
            }

            TempMaterial.SetTexture("_MainTexture", texture);
            TempMaterial.SetFloat("_OutlineWidth", 0);
            TempMaterial.SetFloat("_OpacityMaskOutline", 0f);
            TempMaterial.SetColor("_OutlineColor", new Color32(255, 255, 255, 0));
            TempMaterial.SetFloat("_NoLightMode", 1);


            gameObject.AddComponent<MeshFilter>().sharedMesh = mesh;
            gameObject.AddComponent<MeshRenderer>().sharedMaterial = TempMaterial;


        }
    }

    public Mesh GenerateFenceSection(int ID)
    {
        Mesh mesh = new Mesh();
        mesh.name = "Mesh1";
        List<Vector3> MeshPoints = new List<Vector3>();

        Vector3 Point1 = Points[ID + 0];
        Vector3 Point2 = Points[ID + 1];

        Vector3 Point3 = Point1 + new Vector3(0,0,ZSize);
        Vector3 Point4 = Point2 + new Vector3(0, 0, ZSize);

        MeshPoints.Add(Point1);
        MeshPoints.Add(Point2);
        MeshPoints.Add(Point3);
        MeshPoints.Add(Point4);

        List<int> Indices = new List<int>();

        Indices.Add(0);
        Indices.Add(1);
        Indices.Add(2);

        Indices.Add(3);
        Indices.Add(2);
        Indices.Add(1);


        List<Vector2> vector2s = new List<Vector2>();

        vector2s.Add(new Vector2(0, 0));
        vector2s.Add(new Vector2(1, 0));
        vector2s.Add(new Vector2(0, 1));
        vector2s.Add(new Vector2(1, 1));
        //Points

        mesh.vertices = MeshPoints.ToArray();
        mesh.triangles = Indices.ToArray();
        mesh.uv = vector2s.ToArray();
        mesh.Optimize();

        return mesh;
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (Points.Count != 0)
        {
            for (int i = 0; i < Points.Count - 1; i++)
            {
                Handles.DrawLine(TrickyLevelManager.Instance.transform.TransformPoint(Points[i]), TrickyLevelManager.Instance.transform.TransformPoint(Points[i + 1]), 6f);

                Vector3 Point1 = Points[i];
                Vector3 Point2 = Points[i + 1];

                Point1.z += ZSize;
                Point2.z += ZSize;

                Handles.DrawLine(TrickyLevelManager.Instance.transform.TransformPoint(Point1), TrickyLevelManager.Instance.transform.TransformPoint(Point2), 6f);
            }
        }
    }
}
