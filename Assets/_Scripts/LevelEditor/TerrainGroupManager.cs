using System;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace HexDiff.LevelEditor
{
    public class TerrainGroupManager : MonoBehaviour
    {

        public int GroupID = 0;

        public Dictionary<string, Vector2Int> Resolution = new Dictionary<string, Vector2Int>(){
            { "1024x1024", new Vector2Int(1024, 1024) },
            { "512x512", new Vector2Int(512, 512) },
            { "256x256", new Vector2Int(256, 256) },
            { "128x128", new Vector2Int(128, 128) }
        };

        public int SelectedResolutionIndex = 0;
        public string CurrentResolution = "1024";
        public List<Terrain> terrainList = new();

        public void FindTerrainsByGroupID(int groupID)
        {

            Terrain[] allTerrains = Terrain.activeTerrains; // Get all active terrains in the scene
            if (allTerrains.Length != 0) terrainList.Clear();
            foreach (Terrain terrain in allTerrains)
            {
                // Check if the terrain has the specified group ID
                if (terrain.groupingID == groupID)
                {
                    // Add the terrain to the list
                    terrainList.Add(terrain);
                    if (terrain.transform.parent != this.transform) terrain.transform.SetParent(this.transform);
                }
            }
        }

        // Method to change the resolution of all terrains in the terrainList

        public void ChangeTerrainsResolution(string resolutionKey)
        {
            // Check if the resolution key exists in the Resolution dictionary
            if (Resolution.ContainsKey(resolutionKey))
            {
                // Get the resolution from the dictionary
                Vector2Int resolution = Resolution[resolutionKey];

                // Calculate the total width and depth of all terrains
                float totalWidth = resolution.x * Mathf.Sqrt(terrainList.Count);
                float totalDepth = resolution.y * Mathf.Sqrt(terrainList.Count);

                // Calculate the position increment for each terrain
                float xOffset = totalWidth / Mathf.Sqrt(terrainList.Count);
                float zOffset = totalDepth / Mathf.Sqrt(terrainList.Count);

                // Find the central terrain (terrain with (0, 0, 0) coordinates)
                Terrain centralTerrain = null;
                foreach (Terrain terrain in terrainList)
                {
                    if (terrain.transform.position == Vector3.zero)
                    {
                        centralTerrain = terrain;
                        break;
                    }
                }

                if (centralTerrain == null)
                {
                    Debug.LogError("No central terrain found with coordinates (0, 0, 0). Unable to adjust positions.");
                    return;
                }

                // Apply the resolution to each terrain in the terrainList
                for (int i = 0; i < terrainList.Count; i++)
                {

                    Terrain terrain = terrainList[i];
                    // Set the resolution
                    terrain.terrainData.heightmapResolution = resolution.x;
                    terrain.terrainData.alphamapResolution = resolution.x;
                    terrain.terrainData.baseMapResolution = resolution.x;

                    // Clamp the resolutionPerPatch value to the range [8, 1000]
                    int resolutionPerPatch = Mathf.Clamp(resolution.y, 8, 1000);
                    terrain.terrainData.SetDetailResolution(resolution.x, resolutionPerPatch);

                    // Calculate the new position for the terrain relative to the central terrain
                    Vector3 initialPosition = terrain.transform.position;

                    float newXPos = terrain.transform.position.x == 0 ? 0 : Mathf.Sign(terrain.transform.position.x) * terrain.terrainData.size.x;
                    float newZPos = terrain.transform.position.z == 0 ? 0 : Mathf.Sign(terrain.transform.position.z) * terrain.terrainData.size.z;
                    //Debug.Log($"Name: {terrain.name}; X: {newXPos}; Z: {newZPos}");
                    // Set the terrain's position
                    terrain.transform.position = new Vector3(newXPos, initialPosition.y, newZPos);
                    terrain.name = $"Terrain({terrain.transform.position.x}, {terrain.transform.position.y}, {terrain.transform.position.z})";
                }
                CurrentResolution = $"{resolution.x}x{resolution.y}";
            }
            else
            {
                Debug.LogError("Resolution key '" + resolutionKey + "' not found in the Resolution dictionary.");
            }
        }

        public void GenerateGroupHeightmap()
        {
            
        }

        
    }
}