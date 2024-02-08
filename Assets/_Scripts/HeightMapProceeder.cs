using UnityEngine;
using UnityEngine.UI;

namespace HexDiff{
    public class HeightMapProceeder : MonoBehaviour{
        public static GameObject Instance;
        
        [SerializeField] private Texture2D _map;

        private void Awake(){
            if(Instance == null){
                Instance = this.gameObject;
            } else Destroy(this);
        }

        public static void ProccedChunk(ChunkData data, int x, int z){


        }

    }
}