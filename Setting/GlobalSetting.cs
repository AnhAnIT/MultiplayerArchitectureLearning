using UnityEngine;
using System;
using Fusion;

namespace MultiplayCore
{
    [SerializeField]
    [CreateAssetMenu(fileName ="GlobalSetting",menuName = "MultiplayCore/Global Setting")]
    public class GlobalSetting : ScriptableObject
    {
        public NetworkRunner RunnerPrefab;
        public string LoadingScene = "LoadingScene";
        public string MenuScene = "Menu";
        public bool SimulateMobileInput;

        public AgentSettings Agent;
        public MapSettings Map;
        public NetworkSettings Network;
        public OptionsData DefaultOptions;
    }
}