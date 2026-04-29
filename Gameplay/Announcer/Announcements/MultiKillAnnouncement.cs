using Fusion;
using MultiplayCore;
using UnityEngine;

namespace MultiplayCore
{
    [CreateAssetMenu(menuName = "Announcements/Multi Kill")]
    public class MultiKillAnnouncement : Announcement
    {
        // Private members

        [SerializeField]
        private int _kills = 2;
        [SerializeField]
        private bool _inRowOnly = true;

        private PlayerRef _lastPlayer;
        private int _lastKills;

        // Announcement interface

        protected override bool CheckCondition(AnnouncerContext context)
        {
            // Player could change (e.g. when spectating)
            if (_lastPlayer != context.PlayerStatistics.PlayerRef)
            {
                _lastPlayer = context.PlayerStatistics.PlayerRef;
                _lastKills = context.PlayerStatistics.Kills;
                return false;
            }

            int lastKills = _lastKills;
            _lastKills = context.PlayerStatistics.Kills;

            if (context.PlayerStatistics.Kills > lastKills)
            {
                int currentKills = _inRowOnly == true ? context.PlayerStatistics.KillsInRow : context.PlayerStatistics.KillsWithoutDeath;

                if (currentKills == _kills)
                    return true;

                if (_inRowOnly == false && currentKills > _kills && currentKills % _kills == 0)
                    return true;
            }

            return false;
        }
    }
}