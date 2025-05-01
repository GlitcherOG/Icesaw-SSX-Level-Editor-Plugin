using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using static SSXMultiTool.JsonFiles.Tricky.AIPSOPJsonHandler;

public class ScaleScript : EditorWindow
{
    public static float scalef = 0.3f;

    [MenuItem("Ice Saw WIP/Scale")]
    public static void Init()
    {
        if(TrickyLevelManager.Instance!=null)
        {
            TrickyLevelManager.Instance.dataManager.RefreshObjectList();
            var DataManager = TrickyLevelManager.Instance.dataManager;
            //Patches
            for (global::System.Int32 i = 0; i < DataManager.trickyPatchObjects.Count; i++)
            {
                var Patch = DataManager.trickyPatchObjects[i];

                Patch.transform.position = Patch.transform.position*scalef;
                Patch.transform.localScale = Patch.transform.localScale* scalef;

            }

            //Instances
            for (global::System.Int32 i = 0; i < DataManager.trickyInstances.Count; i++)
            {
                var Instance = DataManager.trickyInstances[i];

                Instance.transform.position = Instance.transform.position * scalef;
                //Instance.transform.localScale = Instance.transform.localScale * scalef;

            }

            //Lights
            for (global::System.Int32 i = 0; i < DataManager.lightObjects.Count; i++)
            {
                var Light = DataManager.lightObjects[i];

                Light.transform.position = Light.transform.position * scalef;
                Light.transform.localScale = Light.transform.localScale * scalef;

            }

            //Splines
            for (global::System.Int32 i = 0; i < DataManager.trickySplineObjects.Count; i++)
            {
                var spline = DataManager.trickySplineObjects[i];

                spline.transform.position = spline.transform.position * scalef;
                spline.transform.localScale = spline.transform.localScale * scalef;

            }

            //Particles
            for (global::System.Int32 i = 0; i < DataManager.particlePrefabObjects.Count; i++)
            {
                var particle = DataManager.particlePrefabObjects[i];

                particle.transform.position = particle.transform.position * scalef;
                particle.transform.localScale = particle.transform.localScale * scalef;

            }

            //Path General AI

            var GeneralPathAPoints = DataManager.trickyGeneralPaths.GetPathAPoints();

            for (global::System.Int32 i = 0; i < GeneralPathAPoints.Count; i++)
            {
                var pathA = GeneralPathAPoints[i];

                pathA.transform.position = pathA.transform.position * scalef;
                pathA.transform.localScale = pathA.transform.localScale * scalef;

                for (global::System.Int32 j = 0; j < pathA.PathEvents.Count; j++)
                {
                    var Events = pathA.PathEvents[j];

                    Events.EventStart *= scalef;
                    Events.EventEnd *= scalef;

                    pathA.PathEvents[j] = Events;
                }
            }

            //Path General Raceline
            var GeneralPathBPoints = DataManager.trickyGeneralPaths.GetPathBPoints();

            for (global::System.Int32 i = 0; i < GeneralPathBPoints.Count; i++)
            {
                var pathB = GeneralPathBPoints[i];

                pathB.transform.position = pathB.transform.position * scalef;
                pathB.transform.localScale = pathB.transform.localScale * scalef;

                for (global::System.Int32 j = 0; j < pathB.PathEvents.Count; j++)
                {
                    var Events = pathB.PathEvents[j];

                    Events.EventStart *= scalef;
                    Events.EventEnd *= scalef;

                    pathB.PathEvents[j] = Events;
                }

            }

            //Path Showoff AI

            var ShowoffPathAPoints = DataManager.trickyShowoffPaths.GetPathAPoints();

            for (global::System.Int32 i = 0; i < ShowoffPathAPoints.Count; i++)
            {
                var pathA = ShowoffPathAPoints[i];

                pathA.transform.position = pathA.transform.position * scalef;
                pathA.transform.localScale = pathA.transform.localScale * scalef;

                for (global::System.Int32 j = 0; j < pathA.PathEvents.Count; j++)
                {
                    var Events = pathA.PathEvents[j];

                    Events.EventStart *= scalef;
                    Events.EventEnd *= scalef;

                    pathA.PathEvents[j] = Events;
                }
            }

            //Path Showoff Raceline
            var ShowoffPathBPoints = DataManager.trickyShowoffPaths.GetPathBPoints();

            for (global::System.Int32 i = 0; i < ShowoffPathBPoints.Count; i++)
            {
                var pathB = ShowoffPathBPoints[i];

                pathB.transform.position = pathB.transform.position * scalef;
                pathB.transform.localScale = pathB.transform.localScale * scalef;

                for (global::System.Int32 j = 0; j < pathB.PathEvents.Count; j++)
                {
                    var Events = pathB.PathEvents[j];

                    Events.EventStart *= scalef;
                    Events.EventEnd *= scalef;

                    pathB.PathEvents[j] = Events;
                }
            }
        }
    }
}
