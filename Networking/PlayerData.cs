using UnityEngine;
using Fusion;
using System.Diagnostics;
using System;
namespace MultiplayCore
{
    public interface IPlayer
    {
        string UserID { get; }
        string NickName { get; }
        NetworkPrefabRef AgentPrefabs { get; }

        string UnityID {  get; }
    }

    public class PlayerData : IPlayer
    {
        // Private members
        [SerializeField]
        private string _userID;
        [SerializeField]
        private string _unityID;
        [SerializeField]
        private string _nickname;
        [SerializeField]
        private string _agentID;
        [SerializeField]
        private bool _isLooked;
        [SerializeField]
        private int _lastProcessID;

        // Public members
        public string UserID => _userID;
        public string UnityID => _unityID;
        public NetworkPrefabRef AgentPrefabs => GetAgentPrefab();
        public bool IsDirty { get; private set; }
        public string NickName { get { return _nickname; }  set { _nickname = value; IsDirty = true; } }

        public string AgentID { get { return _agentID; } set { _agentID = value; IsDirty = true; } }

        //Constructor
        public PlayerData(string userID)
        {
            _userID = userID;
        }
        
        //Public methods
        public void ClearDirty()
        {
            IsDirty = false;
        }
        public bool IsLooked(bool checkProcess = true)
        {
            if (_isLooked== false)
                return false;

            if(checkProcess == true)
            {
                try
                {
                    var process = Process.GetProcessById(_lastProcessID);
                }
                catch (Exception)
                {
                    return false;
                }
            }
           return true;
        }

        public void Lock()
        {
            _isLooked = true;
            _lastProcessID = Process.GetCurrentProcess().Id;
        }

        //Private methods
        private NetworkPrefabRef GetAgentPrefab()
        {
            if (_agentID.HasValue() == false)
                return default;
            var setup = Global
        }