using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HexDiff.LevelEditor
{
    [CustomEditor(typeof(TerrainGroupManager))]
    public class TerrainGroupManagerEditor : Editor
    {
        private int selectedResolutionIndex = 0;
        private string[] resolutionOptions;

        void OnEnable()
        {
            // Get the resolution keys from the TerrainManager's Resolution dictionary
            TerrainGroupManager terrainManager = (TerrainGroupManager)target;
            List<string> resolutionKeys = new List<string>(terrainManager.Resolution.Keys);
            resolutionOptions = resolutionKeys.ToArray();
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            TerrainGroupManager myTarget = (TerrainGroupManager)target;
            selectedResolutionIndex = EditorGUILayout.Popup("Resolution", selectedResolutionIndex, resolutionOptions);
            string selectedResolutionKey = resolutionOptions[selectedResolutionIndex];
            Vector2Int selectedResolution = myTarget.Resolution[selectedResolutionKey];

            // Display the selected resolution
            EditorGUILayout.LabelField("Selected Resolution", selectedResolution.x + "x" + selectedResolution.y);

            if (GUILayout.Button("Test"))
            {
                myTarget.FindTerrainsByGroupID(myTarget.GroupID);
            }


        }
    }
}