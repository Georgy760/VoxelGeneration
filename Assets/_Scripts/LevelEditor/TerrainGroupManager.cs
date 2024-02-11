using System.Collections.Generic;
using System.Numerics;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace HexDiff
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


    }
}