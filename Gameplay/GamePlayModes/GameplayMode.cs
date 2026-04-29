using Fusion;
using System;

namespace MultiplayCore
{
    using Fusion.LagCompensation;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

    public struct KillData : INetworkStruct
    {
        public PlayerRef KillerRef;
        public PlayerRef VictimRef;
        public EHitType HitType;
        public bool Headshot { get { return _flags.IsBitSet(0); } set { _flags.SetBit(0, value); } }

        private byte _flags;
    }
    public struct RespawnRequest : INetworkStruct
    {
        public PlayerRef PlayerRef;
        public TickTimer Timer;
    }
    public enum EGameplayType
    {
        None,
        Deathmatch,
        Elimination,
        BattleRoyale,
    }
    
    public abstract class GameplayMode : ContextBehaviour
    {
        public enum EState
        {
            None,
            Active,
            Finished,
        }
        public string GameplayName;
        public int MaxPlayers;
        public short ScorePerKill;
        public short ScorePerDeath;
        public short ScorePerSuicide;
        public float RespawnTime;
        public float TimeLimit;
        public float BackfillTimeLimit;

        public Announcement[] Announcements;

        //Public members 
        public EGameplayType Type => _type;
        public float Time => (Runner.Tick - _startTick) * Runner.DeltaTime;
        public float RemainingTime => _endTimer.IsRunning == true ? _endTimer.RemainingTime(Runner).Value : 0f;

        [Networked, HideInInspector]
        public EState State { get; private set; }

        public List<SpawnPoint> SpawnPoints => _allSpawnPoints;
        public ShrinkingArea ShrinkingArea => _shrinkingArea;

        public Action<PlayerRef> OnPlayerJoinedGame;
        public Action<string> OnPlayerLeftGame;
        public Action<KillData> OnAgentDeath;
        public Action<PlayerRef> OnPlayerEliminated;


        //Protected members
        [Networked, HideInInspector]
        protected int _startTick { get; set; }

        //Private members
        [SerializeField]
        private EGameplayType _type;

        [Networked, HideInInspector]
        protected TickTimer _endTimer { get; private set; }
        protected ShrinkingArea _shrinkingArea { get; set; }

        [SerializeField]
        private bool _useShrinkingArea;

        [SerializeField]
        private float _maxKillInRowDelay = 3.5f;

        private Queue<RespawnRequest> _respawnRequests = new Queue<RespawnRequest>(16);

        private List<SpawnPoint> _allSpawnPoints = new List<SpawnPoint>();
        private List<SpawnPoint> _availableSpawnPoints = new List<SpawnPoint>();
        private DefaultPlayerComparer _playerComparer = new DefaultPlayerComparer();
        private float _backfillTimerS;

        //Public methods 
        public void Activate()
        {
            if (Runner.IsServer == false)
                return;
            if (State != EState.None)
                return;
            _startTick = Runner.Tick;

            if (TimeLimit > 0f)
            {
                _endTimer = TickTimer.CreateFromSeconds(Runner, TimeLimit);
            }
            if (_useShrinkingArea == true && HasStateAuthority == true)
            {
                _shrinkingArea = Runner.SimulationUnityScene.GetComponent<ShrinkingArea>();
                if (_shrinkingArea != null && HasStateAuthority == true)
                {
                    _shrinkingArea.Activate();
                    _shrinkingArea.ShrinkingAnnounced += OnShrinkingAreaAnnounced;
                }
            }
        }


    }
}