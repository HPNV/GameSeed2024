using System.Collections.Generic;
using Achievement;
using UnityEngine;

namespace Manager
{
    public class AchievementManager
    {
        private Dictionary<string, AchievementData> _achievementData = new();
        private List<AchievementData> _unlockedAchievements = new();


        public void Initialize()
        {
            _unlockedAchievements = new List<AchievementData>();
            
            _achievementData = new Dictionary<string, AchievementData>
            {
                { "test", Resources.Load<AchievementData>("Achievement/New Gardener") }
            };
        }
        
        public void UnlockAchievement(string achievementId)
        {
            if (_unlockedAchievements.Contains(_achievementData[achievementId]))
            {
                return;
            }
            
            _unlockedAchievements.Add(_achievementData[achievementId]);
            Debug.Log("Achievement Unlocked: " + _achievementData[achievementId].name);
        }
        
    }
}