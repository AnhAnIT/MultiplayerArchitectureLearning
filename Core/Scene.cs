using UnityEngine;

namespace MultiplayCore {

    [System.Serializable]
    public class SceneContext
    { 
    }



        [System.Serializable]
    public class Scene : CoreBehavior
    {
        //Player
        [HideInInspector]
        public string PeerUserID;
        [HideInInspector]
        public PlayerData PlayerData;
        
    } 
}
