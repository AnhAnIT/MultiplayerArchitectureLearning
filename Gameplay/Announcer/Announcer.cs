using Fusion;
using System;
using UnityEngine;
namespace MultiplayCore
{
    public class AnnouncerContext
    {
        public NetworkRunner Runner;
        public GameplayMode GameplayMode;
        public int ActivePlayers;
        public int BestScore;
        public PlayerStatistics PlayerStatistics;
    }
    public class Announcer : SceneService
    {
        //Public members
        public Action<AnnouncementData> Announce;



    }

    }