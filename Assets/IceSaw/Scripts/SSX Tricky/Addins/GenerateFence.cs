using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class GenerateFence : MonoBehaviour
{
    public List<Vector3> Points;
    public float ZSize;
    public bool FlipTextureY;
    public int TextureID;

    public Mesh GenerateFenceSection(int ID)
    {
        Mesh mesh = new Mesh();
        List<Vector3> Points = new List<Vector3>();

        Points.Add(Points[ID + 0]);
        Points.Add(Points[ID + 1]);
        //Points.Add();

        Points.Add(Points[ID + 1]);
        //Points.Add();
        //Points.Add();

        Points

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
        for (int i = 0; i < Points.Count-1; i++)
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
