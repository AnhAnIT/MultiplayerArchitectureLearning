using UnityEngine;
using System;
using Fusion;

namespace MultiplayCore
{
    [Serializable]
    [CreateAssetMenu(fileName ="AgentSettings", menuName = "MultiplayCore/Agent Settings")]
    public class AgentSettings : ScriptableObject
    {
        //Public members
        public AgentSetup[] Agents => _agents;
        //Private members
        [SerializeField]
        private AgentSetup[] _agents;
        //Public methods

        public AgentSetup GetAgentSetup(string agentID)
        {
            if (agentID.HasValue() == false)
                return null;

            return _agents.Find(t => t.ID == agentID);
        }

        public AgentSetup GetAgentSetup(NetworkPrefabRef prefabId)
        {
            if (prefabId.IsValid == false)
                return null;

            return _agents.Find(t => t.AgentPrefab == prefabId);
        }
        public AgentSetup GetRandomAgentSetup()
        {
            return _agents[UnityEngine.Random.Range(0, _agents.Length)];
        }
    }


    [Serializable]

    public class AgentSetup
    {
        //Public members
        public string ID => _id;
        public string DisplayName => _displayName;
        public string Description => _description;
        public Sprite Icon => _icon;
        public NetworkPrefabRef AgentPrefab => _agentPrefab;
        public GameObject MenuAgentPrefab => _menuAgentPrefab;

        //Private members
        [SerializeField] private string _id;
        [SerializeField] private string _displayName;
        [SerializeField,TextArea(3,6)] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private NetworkPrefabRef _agentPrefab;
        [SerializeField] private GameObject _menuAgentPrefab;


    }
}