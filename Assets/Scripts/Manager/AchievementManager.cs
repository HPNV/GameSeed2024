using System;
using System.Collections.Generic;
using System.Linq;
using Achievement;
using Script;
using UnityEngine;

namespace Manager
{
    public class AchievementManager
    {
        private Dictionary<EAchievement, AchievementData> _achievements = new();
        public List<AchievementData> UnlockedAchievements { get; private set; } = new();
        public List<EAchievement> UnlockedEAchievements { get; private set; } = new();

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
            //TEMP 
            return;
            if (UnlockedEAchievements.Contains(achievement))
            {
                return;
            }
            
            UnlockedEAchievements.Add(achievement);
            UnlockedAchievements.Add(_achievements[achievement]);
            Debug.Log("Achievement Unlocked: " + _achievements[achievement].name);
        }

        public IEnumerable<EAchievement> GetRandomEAchievements(int amt)
        {
            var temp = UnlockedEAchievements.OrderBy(x => Guid.NewGuid());
            return temp.Take(amt);
        }
    }
}