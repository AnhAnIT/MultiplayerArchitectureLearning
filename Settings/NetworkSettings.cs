using UnityEngine;
using System;
using Fusion;

namespace MultiplayCore
{
    [Serializable]
    public class RegionInfo
    {
        public string DisplayName;
        public string Region;
        public Sprite Icon;
    }
    [Serializable]
    [CreateAssetMenu(fileName ="WorkingSettings", menuName = "MultiplayCore/Network Settings")]

    public class NetworkSettings : ScriptableObject
    {
        public RegionInfo[] Regions;
        public string QueueName;

        public RegionInfo GetRegionInfo(string region)
        {
            return Regions.Find(t => t.Region == region);
        }

        public string GetCustomOrDefaultQueueName()
        {
            if (ApplicationSettings.HasQueueName == true)
                return ApplicationSettings.QueueName;

            return QueueName;
        }
    }
}