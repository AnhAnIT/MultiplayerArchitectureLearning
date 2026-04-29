using UnityEngine;
namespace MultiplayCore
{
    [CreateAssetMenu(menuName = "Announcements/Remaining Kills")]
    public class RemainingKillsAnnouncement : Announcement
    {
        [SerializeField]
        private int _skills;
        private DeathmatchGameplayMode _deathmatch;
        private int _minScore;
        // Announcement interface
        public override void Active(AnnouncementrContext context) {
        {
            _deathmatch = context.GameplayMode as DeathmatchGameplayMode;
            _minScore = _deathmatch.ScoreToWin - context.PlayerStatistics.Kills;
        }
    }
}