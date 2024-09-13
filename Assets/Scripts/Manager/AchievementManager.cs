using System.Collections.Generic;
using Achievement;
using Script;
using UnityEngine;

namespace Manager
{
    public class AchievementManager
    {
        private Dictionary<EAchievement, AchievementData> _achievements = new();
        public List<AchievementData> UnlockedAchievements { get; private set; } = new();

        public void Initialize()
        {
            UnlockedAchievements = new List<AchievementData>();
            
            _achievements = new Dictionary<EAchievement, AchievementData>
            {
                { EAchievement.NewGardener, Resources.Load<AchievementData>("Achievement/New Gardener") }
            };
        }
        
        public void UnlockAchievement(EAchievement achievement)
        {
            if (UnlockedAchievements.Contains(_achievements[achievement]))
            {
                return;
            }
            
            UnlockedAchievements.Add(_achievements[achievement]);
            Debug.Log("Achievement Unlocked: " + _achievements[achievement].name);
        }
        
    }
}