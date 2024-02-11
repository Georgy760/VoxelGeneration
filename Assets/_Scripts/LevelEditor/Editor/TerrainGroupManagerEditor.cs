using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace HexDiff.LevelEditor
{
    [CustomEditor(typeof(TerrainGroupManager))]
    public class TerrainGroupManagerEditor : Editor
    {
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
            myTarget.SelectedResolutionIndex = EditorGUILayout.Popup("Resolution", myTarget.SelectedResolutionIndex, resolutionOptions);
            
            string selectedResolutionKey = resolutionOptions[myTarget.SelectedResolutionIndex];

            EditorGUILayout.LabelField("Current resolution", myTarget.CurrentResolution);

            if (GUILayout.Button("Group Terrains"))
            {
                myTarget.FindTerrainsByGroupID(myTarget.GroupID);
            }

            if (GUILayout.Button("Change Resolution"))
            {
                // Change the resolution of terrains
                myTarget.ChangeTerrainsResolution(selectedResolutionKey);
            }

            if(GUILayout.Button("Generate heightmap")){
                myTarget.GenerateGroupHeightmap();
            }
        }
    }

}
